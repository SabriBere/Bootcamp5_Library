using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Repositories
{
    public class BookRepository: IBookRepository
    {
        // Colocar la lista 
        private static List<Book> _books = new List<Book>()
            {
                new Book { Id = 1, Title = "El Quijote", Year = "1605", IsAvailable = true },
                new Book { Id = 2, Title = "Cien Años de Soledad", Year = "1967", IsAvailable = true },
                new Book { Id = 3, Title = "La Sombra del Viento", Year = "2001", IsAvailable = true }
            };

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            // Asignar un nuevo Id al libro
            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var existingBook = GetBookById(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Year = book.Year;
                existingBook.IsAvailable = book.IsAvailable;
            }
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}