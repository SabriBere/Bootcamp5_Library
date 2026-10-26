using TheLibrary.Controllers;
using TheLibrary_BusinessLayer.Services;
using TheLibrary_DataAccessLayer.Repositories;
using TheLibrary_DataAccessLayer.Data;

using Microsoft.EntityFrameworkCore;

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
builder.Services.AddDbContext<TheLibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
//AddSingleton, AddScoped, AddTransient
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger configuration
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
