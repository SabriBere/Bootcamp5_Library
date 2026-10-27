using System;
using System.Collections.Generic;
using System.Text;
using TheLibrary_DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;

namespace TheLibrary_DataAccessLayer.Data
{
    public class TheLibraryDbContext: DbContext
    {
        public TheLibraryDbContext(DbContextOptions<TheLibraryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<LibrayBranch> LibrayBranches { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        //Configurar los modelos usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>(library =>
            {
                library.HasKey(l => l.Id);
                library.Property(l => l.Name).IsRequired().HasMaxLength(100);
                library.Property(l => l.Address).IsRequired().HasMaxLength(250);
                library.HasMany(l => l.LibrayBranches)
                    .WithOne(lb => lb.Library)
                    .HasForeignKey(lb => lb.LibraryId);
            });

            //Nos queda agregar configuraciones para el resto de entidades
            //LibraryBranch
            //Book
            //User
            //Role
        }
    }
}
