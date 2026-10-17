using TheLibrary.Repositories;
using TheLibrary.Models;

namespace TheLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository _bookRepository;

        // Inyeccion de dependencias
        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del libro debe ser mayor que cero.");
            }

            return _bookRepository.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "El libro no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("El título del libro no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(book.Year))
            {
                throw new ArgumentException("El año del libro no puede estar vacío.");
            }
            _bookRepository.AddBook(book);
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el ID {id}.");
            }
            // Aquí podrías implementar la lógica para eliminar el libro de la lista
            // Por ejemplo, podrías agregar un método DeleteBook en el repositorio
            _bookRepository.DeleteBook(id);
        }
    }
}
