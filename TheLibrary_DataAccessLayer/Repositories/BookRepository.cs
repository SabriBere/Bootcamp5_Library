using TheLibrary_DataAccessLayer.Data;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Repositories
{
    public class BookRepository: IBookRepository
    {
        // Consumo el contexto de la base de datos

        private readonly TheLibraryDbContext _context;

        public BookRepository(TheLibraryDbContext context)
        {
            _context = context;
        }

        // Colocar la lista 
        // private static List<Book> _books = new List<Book>()
        //     {
        //         new Book { Id = 1, Title = "El Quijote", Year = "1605", IsAvailable = true },
        //         new Book { Id = 2, Title = "Cien Años de Soledad", Year = "1967", IsAvailable = true },
        //         new Book { Id = 3, Title = "La Sombra del Viento", Year = "2001", IsAvailable = true }
        //     };

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
            // return _books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            // Asignar un nuevo Id al libro
            // book.Id = _books.Max(b => b.Id) + 1;
            // _books.Add(book);

            _context.Books.Add(book);
            _context.SaveChanges(); //guardo los cambios en la db
        }

        public void DeleteBook(int id)
        {
            var bookFound = GetBookById(id);
            _context.Books.Remove(bookFound);
            if (bookFound != null)
            {
                _context.Books.Remove(bookFound);
                _context.SaveChanges();
            }
        }
    }
}