using System;
using System.Collections.Generic;
using System.Text;
using TheLibrary_DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace TheLibrary_DataAccessLayer.Data
{
    public class TheLibraryDbContext: DbContext
    {
        public TheLibraryDbContext(DbContextOptions<TheLibraryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<LibraryBranch> LibraryBranches { get; set; }
        public DbSet<Role> Roles { get; set; }

        //configurar modelos usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //toco las entidad que quiero, no es nesario que sean todas
            base.OnModelCreating(modelBuilder);

            //Library
            //se puede migrar a una carpeta configuration y crear clases con cada entidad
            modelBuilder.Entity<Library>(library =>
            {
                library.ToTable("Libraries").HasKey(l =>l.Id);
                //tiene más peso la fluent API pero genera confusión al interpretar
                library.Property(l => l.Name).IsRequired().HasMaxLength(100);
                library.Property(l => l.Address).IsRequired().HasMaxLength(250);

                //se puede crear nombre para FK
                library.HasMany(l => l.LibraryBranches)
                .WithOne(lb => lb.Library)
                .HasForeignKey(lb => lb.LibraryId);

                // Una biblioteca tiene muchos usuarios y un usuario puede ser socio de varias bibliotecas
                library.HasMany(l => l.Users)
                .WithMany(u => u.Libraries);

                //establecer las relaciones - agregar configuración

            });

            // LibraryBranch
            modelBuilder.Entity<LibraryBranch>(libraryBranch =>
            {
                libraryBranch.ToTable("LibraryBranches").HasKey(lb => lb.Id); //establece el ID.
                libraryBranch.Property(lb => lb.Name);
                libraryBranch.Property(lb => lb.Address);

                // establcer relación entre libros y sucursales
                // Una sucursal tiene muchos libros.
                // Cada libro pertenece a una sucursal.
                libraryBranch.HasMany(lb => lb.Books)
                    .WithOne(book => book.LibraryBranch)
                    .HasForeignKey(book => book.LibraryBranchId);

                // Una sucursal pertenece a una biblioteca.
                // Una biblioteca tiene muchas sucursales.
                libraryBranch.HasOne(lb => lb.Library)
                    .WithMany(library => library.LibraryBranches)
                    .HasForeignKey(lb => lb.LibraryId);
            });

            // Nos queda agregar configuraciones para el resto de entidades:
            // Book
            modelBuilder.Entity<Book>(book =>
            {
                book.ToTable("Books").HasKey(b => b.Id);
                book.Property(b => b.Title);
                book.Property(b => b.Year);
                book.Property(b => b.IsAvailable);

                //un libro tiene una biblioteca
                // y una biblioteca tiene muchos libros
            
                book.HasOne(b => b.LibraryBranch).WithMany(lb => lb.Books).HasForeignKey(b => b.LibraryBranchId);
            });

            // User
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users").HasKey(u => u.Id);
                user.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                user.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                user.Property(u => u.Email).IsRequired().HasMaxLength(150); //agregar validación

                user.HasIndex(u => u.Email).IsUnique(); //email unico e irrepetible

                user.Property(u => u.DateOfBirth).IsRequired(false); //agregar validación

                //un usuario es socio de muchas bibliotecas
                //y 1 biblioteca tiene muchos usuarios - no necesita un FK por que EF establece la tabla intermedia
                user.HasMany(u => u.Libraries).WithMany(library => library.Users);
            });

            // Role
        }

    }
}
