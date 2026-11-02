using Microsoft.AspNetCore.Mvc;
using TheLibrary.Common.DTOs;
using TheLibrary_BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // Read by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            BookDTO book;
            try
            {
                book = await _bookService.GetBookByIdAsync(id);
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
        public async Task<IActionResult> Create([FromBody] BookCreateDTO bookCreateDTO)
        {
            // Lógica para guardar con un 201 Created
            try
            {
                var createdBook = await _bookService.AddBookAsync(bookCreateDTO);

                return CreatedAtAction(
                    nameof(GetBookById),
                    new { id = createdBook.Id },
                    createdBook
                );
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

            // return Ok(); // Retornar el libro creado con un 201 Created)
        }

        // Delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
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
