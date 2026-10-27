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

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        // public DbSet<Employee> Employees { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<LibraryBranch> LibraryBranches { get; set; }
        public DbSet<Role> Roles { get; set; }

        //configurar modelos usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //toco las entidad que quiero, no es nesario que sean todas
            base.OnModelCreating(modelBuilder);

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

                //establecer las relaciones - agregar configuración

            });

            //agregar el resto de configuraciones para las otras entidades
        }

    }
}
