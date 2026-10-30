using System;
using System.Collections.Generic;
using System.Text;

using TheLibrary.Common.DTOs;

using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Repositories
{
    public interface IBookRepository
    {
        PagedResult<BookDTO> GetAllBooks(int pageIndex, int pageSize, string? search);
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
