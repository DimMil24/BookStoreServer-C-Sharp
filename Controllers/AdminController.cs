using BookStoreServerNet.Data;
using BookStoreServerNet.Models;
using BookStoreServerNet.Models.Requests;
using BookStoreServerNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServerNet.Controllers
{
	[Route("api/admin")]
	[Authorize]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly BookService _bookService;

		public AdminController(ApplicationDbContext context, BookService bookService)
		{
			_context = context;
			_bookService = bookService;
		}

		[HttpGet("books/page/{page}")]
        public async Task<IActionResult> GetBooks([FromQuery] FiltersRequest filters, int page = 1)
        {
            var books = await _bookService.GetBooksByPageAsync(page, filters);
            return Ok(books);
        }

		[HttpPost("books")]
		public async Task<IActionResult> CreateNewBook(NewBookRequest newBookRequest)
		{
			if (await _bookService.BookExists(newBookRequest.Isbn13) || await _bookService.BookExistsByTitle(newBookRequest.Title))
			{
				return BadRequest();
			}
			await _bookService.AddBook(newBookRequest);
			return Ok();
		}

		[HttpPut("books/{id}")]
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

		[HttpDelete("books/{id}")]
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
