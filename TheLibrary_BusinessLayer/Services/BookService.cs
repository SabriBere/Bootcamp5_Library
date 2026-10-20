using TheLibrary_BusinessLayer.Repositories;
using TheLibrary_BusinessLayer.Models;
using TheLibrary_BusinessLayer.DTOs;

namespace TheLibrary_BusinessLayer.Services
{
    public class BookService : IBookService
    {
        private static List<Book> _books = new List<Book>()
            {
                new Book { Id = 1, Title = "El Quijote", Year = "1605", IsAvailable = true },
                new Book { Id = 2, Title = "Cien Años de Soledad", Year = "1967", IsAvailable = true },
                new Book { Id = 3, Title = "La Sombra del Viento", Year = "2001", IsAvailable = true }

            };
        //private readonly BookRepository _bookRepository;

        // Inyeccion de dependencias
        //public BookService(BookRepository bookRepository)
        //{
        //    _bookRepository = bookRepository;
        //}

        public BookService()
        {
            
        }

        public List<BookDTO> GetAllBooks()
        {
            //return _bookRepository.GetAllBooks();
            var booksDto = _books.Select(b => new BookDTO
            {
                Title = b.Title,
                Year = b.Year,
                IsAvailable = b.IsAvailable
            }).ToList();
            return booksDto;
        }

        public BookDTO GetBookById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del libro debe ser mayor que cero.");
            }
            var book = _books.FirstOrDefault(b => b.Id == id);
            
            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el ID {id}.");
            }
            var bookDto = new BookDTO
            {
                Title = book?.Title,
                Year = book?.Year,
                IsAvailable = book?.IsAvailable ?? false
            };
            return bookDto;
            //return _bookRepository.GetBookById(id);
        }

        public void AddBook(BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO == null)
            {
                throw new ArgumentNullException(nameof(bookCreateDTO), "El libro no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(bookCreateDTO.Title))
            {
                throw new ArgumentException("El título del libro no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(bookCreateDTO.Year))
            {
                throw new ArgumentException("El año del libro no puede estar vacío.");
            }
            //_bookRepository.AddBook(book);
            var book = new Book
            {
                Id = _books.Max(b => b.Id) + 1, // Genera un nuevo ID basado en el máximo existente
                Title = bookCreateDTO.Title,
                Year = bookCreateDTO.Year,
                IsAvailable = true // Por defecto, el libro es disponible
            };
            _books.Add(book);
        }

        public void DeleteBook(int id)
        {
            //var book = _bookRepository.GetBookById(id);
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el ID {id}.");
            }
            // Aquí podrías implementar la lógica para eliminar el libro de la lista
            // Por ejemplo, podrías agregar un método DeleteBook en el repositorio
            //_bookRepository.DeleteBook(id);
            _books.Remove(book);
        }
    }
}
