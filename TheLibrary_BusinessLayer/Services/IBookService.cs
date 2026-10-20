using TheLibrary_BusinessLayer.DTOs;
using TheLibrary_BusinessLayer.Models;

namespace TheLibrary_BusinessLayer.Services
{
    public interface IBookService
    {
        List<BookDTO> GetAllBooks();
        BookDTO GetBookById(int id);
        void AddBook(BookCreateDTO book);
        void DeleteBook(int id);
    }
}