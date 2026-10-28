using TheLibrary_DataAccessLayer.Repositories;
using TheLibrary_DataAccessLayer.Models;
using TheLibrary_BusinessLayer.DTOs;

namespace TheLibrary_BusinessLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        //Inyeccion de dependencias
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<BookDTO> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            var booksDto = books.Select(b => new BookDTO
            {
                Title = b.Title,
                Year = b.Year,
                IsAvailable = b.IsAvailable,
                LibraryBranchId = b.LibraryBranchId,
                LibraryBranchName = b.LibraryBranch.Name,
                LibraryId = b.LibraryBranch.LibraryId,
                LibraryName = b.LibraryBranch.Library.Name,
            }).ToList();
            return booksDto;
        }

        public BookDTO GetBookById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del libro debe ser mayor que cero.");
            }
            
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el ID {id}.");
            }

            var bookDto = new BookDTO
            {
                Title = book?.Title,
                Year = book?.Year,
                IsAvailable = book?.IsAvailable ?? false,
            };

            return bookDto;
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
            
            var book = new Book

            {
                Title = bookCreateDTO.Title,
                Year = bookCreateDTO.Year,
                IsAvailable = true, // Por defecto, el libro es disponible
                LibraryBranchId = bookCreateDTO.LibraryBranchId //indique en que sucursal se encuentra
            };

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
