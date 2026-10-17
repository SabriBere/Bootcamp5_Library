using Microsoft.AspNetCore.Mvc;
using TheLibrary.DTOs;
using TheLibrary.Models;
using TheLibrary.Services;

namespace TheLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BookController : ControllerBase
    {
        // Inyección de dependencias del servicio de libros
        private readonly IBookService _bookService;
        
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Create
        [HttpPost]
        public IActionResult Create([FromBody] BookCreateDTO bookCreateDTO)
        {
            // Mapear el DTO a la entidad Book
            var newBook = new Book
            {
                Title = bookCreateDTO.Title,
                Year = bookCreateDTO.Year,
                IsAvailable = true // Por defecto, el libro está disponible
            };

            // Lógica para guardar con un 201 Created
            _bookService.AddBook(newBook);

            return Ok(newBook); // Retornar el libro creado con un 201 Created)
        }

        // Read
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        // Read by Id
        [HttpGet("{id:int}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found" });
            }
            return Ok(book);
        }

        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found" });
            }

            _bookService.DeleteBook(id);
            return NoContent();
        }

        // Update 

    }
}
