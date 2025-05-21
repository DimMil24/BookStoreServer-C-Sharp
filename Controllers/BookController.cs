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
        public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly BookService _bookService;

        public BookController(ApplicationDbContext context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        [HttpGet("books/author/{author}")]
        public async Task<List<Book>> GetBooksByAuthor([FromRoute] string author)
        {
            return await _bookService.GetBooksByAuthorAsync(author);
        }
            
        [HttpGet("books/{id}")]
        public async Task<Book?> GetBookById(long id)
        {
            return await _bookService.FindBookById(id);
        }

        [HttpGet("books/page/{page}")]
        public async Task<IActionResult> GetBooks([FromQuery] FiltersRequest filters, int page = 1)
        {
            var books = await _bookService.GetBooksByPageAsync(page, filters);
            return Ok(books);
        }
    }
}
