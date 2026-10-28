using System;
using System.Collections.Generic;
using System.Text;

using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Repositories
{
    public interface IBookRepository
    {
        // List<Book> GetAllBooks();
        Task<List<Book>> GetAllBooksAsync();
        // Book? GetBookById(int id);
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
