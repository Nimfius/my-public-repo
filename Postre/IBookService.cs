using Microsoft.AspNetCore.Mvc;
using Postre.Models;

namespace Postre;

public interface IBookService
{
    Task<ActionResult<BookDto>> GetBookAsync(string title);
    Task<ActionResult> UpdateBookAsync(string title, UpdateBook updatedBook);
    Task<ActionResult<BookDto>> AddBookAsync(StoreBook storeBook);
    Task<ActionResult> RemoveBookAsync(string title);
}