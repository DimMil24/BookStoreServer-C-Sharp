using BookStoreServerNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServerNet.Data;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    
}