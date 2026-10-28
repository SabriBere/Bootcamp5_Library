using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

using TheLibrary_DataAccessLayer.Data;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Repositories
{
    public class BookEFRepository : IBookRepository
    {
        //Campo Privado con el tipo de dato igual a DbContext
        private readonly TheLibraryDbContext _context;
        
        //Constructor
        public BookEFRepository(TheLibraryDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books
                .Include(b => b.LibraryBranch) // Incluir la propiedad de navegación LibraryBranch
                    .ThenInclude(lb => lb.Library) // Incluir la propiedad de navegación Library dentro de LibraryBranch
                .Include(b => b.Loans) // Incluir la propiedad de navegación Loans
                    .ThenInclude(l => l.User) // Incluir la propiedad de navegación User dentro de Loans    
                .ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }        

        public void UpdateBook(Book book)
        {
            var existingBook = GetBookById(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                _context.SaveChanges();
            }
        }
        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
