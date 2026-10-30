using TheLibrary.Common.DTOs;

using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_BusinessLayer.Services
{
    public interface IBookService
    {
        PagedResult<BookDTO> GetAllBooks(int pageIndex, int pageSize, string? search    );
        BookDTO GetBookById(int id);
        void AddBook(BookCreateDTO book);
        void DeleteBook(int id);
    }
}