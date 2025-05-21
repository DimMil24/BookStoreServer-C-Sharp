using BookStoreServerNet.Data;
using BookStoreServerNet.Models;
using BookStoreServerNet.Models.Requests;
using BookStoreServerNet.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServerNet.Services;

public class BookService
{
    private readonly ApplicationDbContext _context;
    
    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> FindBookById(long bookId)
    {
        return await _context.Books.AsNoTracking().FirstOrDefaultAsync(book => book.Isbn13 == bookId);
    }

    public async Task<bool> BookExists(long bookId)
    {
        return await _context.Books.AnyAsync(book => book.Isbn13 == bookId);
    }
    
    public async Task<bool> BookExistsByTitle(string title)
    {
        return await _context.Books.AnyAsync(book => book.Title.ToLower() == title.ToLower());
    }

    public async Task AddBook(NewBookRequest newBook)
    {
        
        var book = new Book
        {
            Isbn13 = newBook.Isbn13,
            Isbn10 = newBook.Isbn10,
            Title = newBook.Title,
            Subtitle = newBook.Subtitle,
            Authors = newBook.Authors,
            Categories = newBook.Categories,
            Price = newBook.Price,
            Thumbnail = newBook.Thumbnail,
            Description = newBook.Description,
            Year = newBook.Year,
            AverageRating = newBook.AverageRating,
            NumberOfPages = newBook.NumberOfPages,
            RatingsCount = newBook.RatingsCount,
        };
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Book>> GetBooksByAuthorAsync(string author)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(book => book.Authors == author)
            .ToListAsync();
    }

    public async Task<BookResponse> GetBooksByPageAsync(int page, FiltersRequest filters)
    {
        int pageSize = filters.PageSize;
        var query = _context.Books.AsNoTracking();
        query = query.Where(b => b.Year <= filters.YearHigh && b.Year >= filters.YearLow);
        query = query.Where(b => b.AverageRating >= filters.RatingLow && b.AverageRating <= filters.RatingHigh);
        query = query.Where(b => b.Price >= filters.PriceLow && b.Price <= filters.PriceHigh);
        if (!string.IsNullOrEmpty(filters.Search))
        {
            query = query.Where(b => b.Title.ToUpper().Contains(filters.Search.ToUpper()));
        }

        if (!string.IsNullOrEmpty(filters.Category))
        {
            query = query.Where(b => b.Categories==filters.Category);
        }
        query = GetSortedData(query,filters.OrderByDescending,filters.OrderBy);
        
        var books = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        var result = new BookResponse
        {
            content = books,
            totalPages = (int)Math.Ceiling(query.Count() / (double)pageSize),
        };
        return result;
    }

    private IQueryable<Book> GetSortedData(IQueryable<Book> query, bool desc, string orderBy)
    {
        switch (orderBy.ToLowerInvariant())
        {
            case "title":
                return desc ? query.OrderByDescending(c => c.Title) : query.OrderBy(c => c.Title);
            case "subtitle":
                return desc ? query.OrderByDescending(c => c.Subtitle) : query.OrderBy(c => c.Subtitle);
            case "authors":
                return desc ? query.OrderByDescending(c => c.Authors) : query.OrderBy(c => c.Authors);
            case "published_year":
                return desc ? query.OrderByDescending(c => c.Year) : query.OrderBy(c => c.Year);
            case "average_rating":
                return desc ? query.OrderByDescending(c => c.AverageRating) : query.OrderBy(c => c.AverageRating);
            case "num_pages":
                return desc ? query.OrderByDescending(c => c.NumberOfPages) : query.OrderBy(c => c.NumberOfPages);
            case "ratings_count":
                return desc ? query.OrderByDescending(c => c.RatingsCount) : query.OrderBy(c => c.RatingsCount);
            case "price":
                return desc ? query.OrderByDescending(c => c.Price) : query.OrderBy(c => c.Price);
            default:
                return desc ? query.OrderByDescending(c => c.Isbn13) : query.OrderBy(c => c.Isbn13);
        }
    }
}