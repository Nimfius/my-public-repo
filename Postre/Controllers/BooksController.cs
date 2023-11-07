using Microsoft.AspNetCore.Mvc;
using Postre.Models;

namespace Postre.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{title}")]
    public Task<ActionResult<BookDto>> Get([FromRoute] string title)
    {
        return _bookService.GetBookAsync(title);
    }

    [HttpPut("{title}")]
    public Task<ActionResult> Get([FromRoute] string title, [FromBody] UpdateBook updatedBook)
    {
        return _bookService.UpdateBookAsync(title, updatedBook);
    }

    [HttpPost("store")]
    public Task<ActionResult<BookDto>> Add([FromBody] StoreBook storeBook)
    {
        return _bookService.AddBookAsync(storeBook);
    }

    [HttpDelete("{title}")]
    public Task<ActionResult> Delete([FromRoute] string title)
    {
        return _bookService.RemoveBookAsync(title);
    }
}