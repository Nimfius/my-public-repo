using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postre.Models;

namespace Postre;

public class BookService : IBookService
{
    private readonly BooksDbContext _dbContext;

    public BookService(BooksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ActionResult<BookDto>> GetBookAsync(string title)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(book => book.Title == title);
        if (book == null)
        {
            Console.WriteLine($"Book {title} is not found.");
            return new BadRequestResult();
        }

        return ConvertBook(book);
    }

    public async Task<ActionResult> UpdateBookAsync(string title, UpdateBook updatedBook)
    {
        var affectedRows = await _dbContext.Books.Where(book => book.Title == title)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(book => book.Title, book => updatedBook.Title)
                    .SetProperty(book => book.Summary, book => updatedBook.Summary)
                    .SetProperty(book => book.Price, book => updatedBook.Price));
        if (affectedRows == 0)
        {
            Console.WriteLine($"Book {title} is not found.");
            return new BadRequestResult();
        }

        return new NoContentResult();
    }

    public async Task<ActionResult<BookDto>> AddBookAsync(StoreBook storeBook)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(book => book.Title == storeBook.Title);
        if (book != null)
        {
            Console.WriteLine($"Book {storeBook.Title} is already stored.");
            return new BadRequestResult();
        }

        var bookEntity = new Book { Title = storeBook.Title, Price = storeBook.Price, Summary = storeBook.Summary };
        var storedEntity = await _dbContext.Books.AddAsync(bookEntity);
        await _dbContext.SaveChangesAsync();
        return ConvertBook(storedEntity.Entity);
    }

    public async Task<ActionResult> RemoveBookAsync(string title)
    {
        var affectedRows = await _dbContext.Books.Where(book => book.Title == title).ExecuteDeleteAsync();
        if (affectedRows == 0)
        {
            Console.WriteLine($"Book {title} is not found.");
            return new BadRequestResult();
        }

        return new NoContentResult();
    }

    private static BookDto ConvertBook(Book book)
    {
        return new BookDto { Price = book.Price, Summary = book.Summary, Title = book.Title };
    }
}