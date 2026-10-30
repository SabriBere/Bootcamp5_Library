using Microsoft.AspNetCore.Mvc;

using TheLibrary.Common.DTOs;

using TheLibrary_BusinessLayer.Services;

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

        // Read
        [HttpGet]
        public IActionResult GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var books = _bookService.GetAllBooks(pageIndex, pageSize, search);
            return Ok(books);
        }

        // Read by Id
        [HttpGet("{id:int}")]
        public IActionResult GetBookById(int id)
        {
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

            return Ok(); // Retornar el libro creado con un 201 Created)
        }

        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook(int id)
        {
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
