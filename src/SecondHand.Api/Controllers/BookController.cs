using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Models;
using SecondHand.Services;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{   
    private readonly BooksService _booksService;

    private readonly ILogger<BookController> _logger;

    public BookController(ILogger<BookController> logger, BooksService booksService)
    {
        _logger = logger;
        _booksService = booksService;
    }

    [HttpGet()]
    public async Task<List<Book>> Get() => await _booksService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book newBook)
    {
        await _booksService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _booksService.RemoveAsync(id);

        return NoContent();
    }
}
