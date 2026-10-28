using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(book => book.LibraryBranch)
                .ThenInclude(branch => branch.Library)
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return  await _context.Books.FindAsync(id);
            // return _books.FirstOrDefault(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            // Asignar un nuevo Id al libro
            // book.Id = _books.Max(b => b.Id) + 1;
            // _books.Add(book);

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync(); //guardo los cambios en la db
        }

        public async Task DeleteBookAsync(int id)
        {
            var bookFound = await GetBookByIdAsync(id);

            if (bookFound is not null)
            {
                _context.Books.Remove(bookFound);
                await _context.SaveChangesAsync();
            }
        }
    }
}
