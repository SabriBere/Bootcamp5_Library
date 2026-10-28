using Microsoft.EntityFrameworkCore;
using TheLibrary_DataAccessLayer.Data;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Repositories
{
    public class BookRepository: IBookRepository
    {

        private readonly TheLibraryDbContext _context;

        public BookRepository(TheLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(book => book.LibraryBranch)
                .ThenInclude(branch => branch.Library)
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return  await _context.Books.Include(book => book.LibraryBranch).ThenInclude(branch => branch.Library).FirstOrDefaultAsync(b => b.Id == id);
            // return _books.FirstOrDefault(b => b.Id == id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            // Asignar un nuevo Id al libro
            // book.Id = _books.Max(b => b.Id) + 1;
            // _books.Add(book);

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync(); //guardo los cambios en la db

            return book;
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
