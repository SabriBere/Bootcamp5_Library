using TheLibrary_BusinessLayer.Services.Auth;
using TheLibrary_BusinessLayer.Services;
using TheLibrary_DataAccessLayer.Repositories;
using TheLibrary_DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using TheLibrary_DataAccessLayer.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//var service = new BookService();
//var controller = new BookController(service);
//controller.AddService(service);
//controller.bookService = service;


//BookController -> BookService -> BookRepository
//PL -> BL -> DAL
//PL -> DAL
//Program

//Add DbContext

// -----------------------------------------------------------------------------
// 1. Base de Datos y Microsoft Identity
// -----------------------------------------------------------------------------
builder.Services.AddDbContext<TheLibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<TheLibraryDbContext>().AddDefaultTokenProviders();

// -----------------------------------------------------------------------------
// 2. Configuración del Middleware de Autenticación JWT Bearer
// -----------------------------------------------------------------------------

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var jwtKey = jwtSettings["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidCastException("No se configuró JwtSettings:Key. Usá dotnet user-secrets.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; //solo en desarrollo
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

        ClockSkew = TimeSpan.Zero

    };
})
;

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add services to the container.
//AddSingleton, AddScoped, AddTransient
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger configuration
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresá el token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TheLibraryDbContext>();
    var seedResult = await DevelopmentDataSeeder.SeedAsync(context);

    app.Logger.LogInformation(
        "Development seed listo. LibraryId: {LibraryId}; LibraryBranchId: {LibraryBranchId}",
        seedResult.LibraryId,
        seedResult.LibraryBranchId);
}

// Configure the HTTP request pipeline.
// -----------------------------------------------------------------------------
// 4. Pipeline HTTP: Orden Estricto de Ejecución
// -----------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TheLibrary API");
    });
}

// ¡REGLA DE ORO DE ASP.NET CORE!:
// UseAuthentication debe ejecutarse obligatoriamente ANTES de UseAuthorization
app.UseAuthentication(); // 1. ¿Quién sos? Lee el header y valida el token
app.UseAuthorization();  // 2. ¿Tenés permiso? Evalúa [Authorize] y roles

app.MapControllers();

app.Run();
