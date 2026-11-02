using TheLibrary.Common.DTOs;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_BusinessLayer.Services
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> GetBookByIdAsync(int id);
        Task<BookDTO> AddBookAsync(BookCreateDTO book);
        Task DeleteBookAsync(int id);
    }
}
