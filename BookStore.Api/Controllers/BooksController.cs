using BookStore.Api.Models;
using BookStore.Api.Services;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;

    public BooksController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetBooks(int? publishedYear)
    {
        var bookResponses = await _bookService.GetBooks(publishedYear);

        return Ok(bookResponses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponse>> GetBook(string id)
    {
        var bookResponse = await _bookService.GetBook(id);

        if (bookResponse == null)
        {
            return NotFound();
        }

        return Ok(bookResponse);
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse>> AddBook(BookRequest bookRequest)
    {
        if (string.IsNullOrEmpty(bookRequest.Title) || bookRequest.PublishedYear <= 0)
        {
            return BadRequest("Invalid book data");
        }

        var authorResponse = await _authorService.GetAuthor(bookRequest.AuthorId);

        if (authorResponse == null)
        {
            return BadRequest("Author not found");
        }

        var bookResponse = await _bookService.AddBook(bookRequest);

        return CreatedAtAction(nameof(GetBook), new { id = bookResponse.Id }, bookResponse);
    }
}
