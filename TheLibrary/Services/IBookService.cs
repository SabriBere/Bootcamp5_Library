using TheLibrary.Models;

namespace TheLibrary.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void DeleteBook(int id);
    }
}