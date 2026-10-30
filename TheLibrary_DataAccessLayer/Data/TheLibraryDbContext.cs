using System;
using System.Collections.Generic;
using System.Text;
using TheLibrary_DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace TheLibrary_DataAccessLayer.Data
{
    public class TheLibraryDbContext: IdentityDbContext<User>
    {
        public TheLibraryDbContext(DbContextOptions<TheLibraryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<LibraryBranch> LibraryBranches { get; set; }
        public DbSet<Book> Books { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Loan> Loans { get; set; }

        //Configurar los modelos usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Library
            modelBuilder.Entity<Library>(library =>
            {
                //Propiedas o atributos
                library.HasKey(l => l.Id);
                library.Property(l => l.Name).IsRequired().HasMaxLength(100);
                library.Property(l => l.Address).IsRequired().HasMaxLength(250);

                //Relaciones
                library.HasMany(l => l.LibraryBranches)
                    .WithOne(lb => lb.Library)
                    .HasForeignKey(lb => lb.LibraryId);

                library.HasMany(l => l.Members)
                    .WithOne(m => m.Library)
                    .HasForeignKey(m => m.LibraryId);
            });

            //LibraryBranch
            modelBuilder.Entity<LibraryBranch>(libraryBranch => 
            { 
                libraryBranch.HasKey(lb => lb.Id);
                libraryBranch.Property(lb => lb.Name).IsRequired().HasMaxLength(100);
                libraryBranch.Property(lb => lb.Address).IsRequired().HasMaxLength(250);

                libraryBranch.HasMany(lb => lb.Books)
                    .WithOne(b => b.LibraryBranch)
                    .HasForeignKey(b => b.LibraryBranchId);
            });

            //Book
            modelBuilder.Entity<Book>(book =>
            {
                book.HasKey(b => b.Id);
                book.Property(b => b.Title).IsRequired().HasMaxLength(200);
                book.Property(b => b.Author).IsRequired().HasMaxLength(100);
                book.Property(b => b.ISBN).IsRequired().HasMaxLength(20);
                book.Property(b => b.Year).IsRequired().HasMaxLength(4);
                book.Property(b => b.IsAvailable).IsRequired();
                
                book.HasOne(b => b.LibraryBranch)
                    .WithMany(lb => lb.Books)
                    .HasForeignKey(b => b.LibraryBranchId);

                book.HasMany(b => b.Loans)
                    .WithOne(l => l.Book)
                    .HasForeignKey(l => l.BookId);
            });

            //User
            //modelBuilder.Entity<User>(user =>
            //{
            //    user.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            //    user.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            //    user.Property(u => u.Email).IsRequired().HasMaxLength(100);
            //    user.Property(u => u.DateOfBirth).IsRequired(false);
                
            //    user.HasMany(u => u.Subscriptions)
            //        .WithOne(s => s.Member)
            //        .HasForeignKey(s => s.MemberId);

            //    user.HasMany(u => u.Loans)
            //        .WithOne(l => l.User)
            //        .HasForeignKey(l => l.UserId);
            //});

            ////Role
            //modelBuilder.Entity<Role>(role =>
            //{
            //    role.HasKey(r => r.Id);
            //    role.Property(r => r.Name).IsRequired().HasMaxLength(50);

            //    role.HasMany(r => r.Users)
            //        .WithMany(u => u.Roles);
            //});

            //Membership
            modelBuilder.Entity<Membership>(membership =>
            {
                membership.HasKey(m => m.Id);
                membership.Property(m => m.StartDate).IsRequired();
                membership.Property(m => m.EndDate).IsRequired(false);
                membership.Property(m => m.IsActive).IsRequired();
                
                membership.HasOne(m => m.Member)
                    .WithMany(u => u.Subscriptions)
                    .HasForeignKey(m => m.MemberId);
                
                membership.HasOne(m => m.Library)
                    .WithMany(l => l.Members)
                    .HasForeignKey(m => m.LibraryId);
            });

            //Loan
            modelBuilder.Entity<Loan>(loan =>
            {
                loan.HasKey(l => l.Id);
                loan.Property(l => l.LoanDate).IsRequired();
                loan.Property(l => l.DueDate).IsRequired(false);
                loan.Property(l => l.ReturnDate).IsRequired(false);
                
                loan.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BookId);
                
                loan.HasOne(l => l.User)
                    .WithMany(u => u.Loans)
                    .HasForeignKey(l => l.UserId);
            });
        }
    }
}
