using System;
using System.Collections.Generic;
using System.Text;

using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Repositories
{
    public interface IBookRepository
    {
        // List<Book> GetAllBooks();
        Task<(List<Book> Books, int TotalCount)> GetAllBooksAsync(int pageIndex, int pageSize, string? search);
        // Book? GetBookById(int id);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
