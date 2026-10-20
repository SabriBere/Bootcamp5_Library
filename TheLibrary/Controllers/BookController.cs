using Microsoft.AspNetCore.Mvc;
using TheLibrary_BusinessLayer.DTOs;
using TheLibrary_BusinessLayer.Services;

namespace TheLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BookController : ControllerBase
    {
        //private static List<Book> _books = new List<Book>()
        //    {
        //        new Book { Id = 1, Title = "El Quijote", Year = "1605", IsAvailable = true },
        //        new Book { Id = 2, Title = "Cien Años de Soledad", Year = "1967", IsAvailable = true },
        //        new Book { Id = 3, Title = "La Sombra del Viento", Year = "2001", IsAvailable = true }

        //    };

        // Inyección de dependencias del servicio de libros
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Read
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _bookService.GetAllBooks();
            //var books = _books;
            return Ok();
        }

        // Read by Id
        [HttpGet("{id:int}")]
        public IActionResult GetBookById(int id)
        {
            //var book = _books.FirstOrDefault(b => b.Id == id);
            //if (book == null)
            //    return NotFound(new { Message = $"Book with ID {id} not found" });
            BookDTO book;
            try
            {
                book = _bookService.GetBookById(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(book);
        }

        // Create
        [HttpPost]
        public IActionResult Create([FromBody] BookCreateDTO bookCreateDTO)
        {
            //// Mapear el DTO a la entidad Book
            //var newBook = new Book
            //{
            //    Title = bookCreateDTO.Title,
            //    Year = bookCreateDTO.Year,
            //    IsAvailable = true // Por defecto, el libro está disponible
            //};

            // Lógica para guardar con un 201 Created

            try
            {
                _bookService.AddBook(bookCreateDTO);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Asignar un nuevo Id al libro
            //newBook.Id = _books.Max(b => b.Id) + 1;
            //_books.Add(newBook);
            return Ok(); // Retornar el libro creado con un 201 Created)
        }

        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook(int id)
        {
            //var book = _bookService.GetBookById(id);
            //var book = _books.FirstOrDefault(b => b.Id == id);
            //if (book == null)
            //{
            //    return NotFound(new { Message = $"Book with ID {id} not found" });
            //}
            //_books.Remove(book);
            try
            {
                _bookService.DeleteBook(id);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
            return NoContent();
        }

        // Update 
    }
}
