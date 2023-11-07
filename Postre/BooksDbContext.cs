using Microsoft.EntityFrameworkCore;

namespace Postre;

public class BooksDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}