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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Rol> Roles { get; set; }
    }
}
