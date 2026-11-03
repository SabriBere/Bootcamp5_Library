using TheLibrary.Common.DTOs;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_BusinessLayer.Services
{
    public interface IBookService
    {
        Task<PagedResult<BookDTO>> GetAllBooksAsync(int pageIndex, int pageSize, string? search);
        Task<BookDTO> GetBookByIdAsync(int id);
        Task<BookDTO> AddBookAsync(BookCreateDTO book);
        Task DeleteBookAsync(int id);
    }
}
