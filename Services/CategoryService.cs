using BookStoreServerNet.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServerNet.Services;

public class CategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string?>> GetAllCategoriesAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .GroupBy(c => c.Categories)
            .Where(grp => grp.Count() > 15)
            .Select(c => c.Key)
            .ToListAsync();
    }
}