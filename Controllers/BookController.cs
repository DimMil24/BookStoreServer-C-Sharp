using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreServerNet.Data;
using BookStoreServerNet.Models;
using BookStoreServerNet.Models.Requests;
using BookStoreServerNet.Services;
using Microsoft.AspNetCore.Cors;

namespace BookStoreServerNet.Controllers
{
    [Route("api/")]
    [ApiController]
    // [EnableCors("AllowAll")]
        public class BookController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly BookService _bookService;

        public BookController(BookContext context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var data = await _bookService.GetAllCategoriesAsync();
            return Ok(data);
        }

        [HttpGet("author/{author}")]
        public async Task<List<Book>> GetBooksByAuthor([FromRoute] string author)
        {
            return await _bookService.GetBooksByAuthorAsync(author);
        }
            
        [HttpGet("books/id/{isbn13}")]
        public async Task<Book?> GetBookById(long isbn13)
        {
            return await _bookService.FindBookById(isbn13);
        }

        [HttpGet("books/{page}")]
        public async Task<IActionResult> GetBooks([FromQuery] FiltersRequest filters, int page = 1)
        {
            var books = await _bookService.GetBooksByPageAsync(page, filters);
            return Ok(books);
        }
        
        [HttpPut("books/id/{id}")]
        public async Task<IActionResult> PutBook(long id, Book book)
        {
            if (id != book.Isbn13)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(long id)
        {
            return _context.Books.Any(e => e.Isbn13 == id);
        }
    }
}
