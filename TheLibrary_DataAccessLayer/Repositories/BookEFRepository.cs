using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

using TheLibrary.Common.DTOs;

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

        public PagedResult<BookDTO> GetAllBooks(int pageIndex, int pageSize, string? search)
        {
            //0-Obtener todos los libros de la base de datos
            //Armar la Query para obtener los 
            IQueryable<Book> query = _context.Books
                .Include(b => b.LibraryBranch) // Incluir la propiedad de navegación LibraryBranch
                    .ThenInclude(lb => lb.Library) // Incluir la propiedad de navegación Library dentro de LibraryBranch
                .Include(b => b.Loans) // Incluir la propiedad de navegación Loans
                    .ThenInclude(l => l.User); // Incluir la propiedad de navegación User dentro de Loans    
            
            //1-Filtro
            // Filtrar por título o autor si se proporciona un término de búsqueda
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search) || b.ISBN.Contains(search));
            }

            //2-Obtener el total de registros
            int totalRecords = query.Count();

            //3-Paginación
            var books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagedResult<BookDTO>
            {
                Items = books
                    .Select(b => new BookDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        ISBN = b.ISBN,
                        Year = b.Year,
                        IsAvailable = b.IsAvailable,
                        LibraryBranchName = b.LibraryBranch.Name,
                        LibraryName = b.LibraryBranch.Library.Name
                    })
                    .ToList(),
                TotalCount = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
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
