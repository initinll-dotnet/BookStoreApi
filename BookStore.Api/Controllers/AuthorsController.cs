using BookStore.Api.Models;
using BookStore.Api.Services;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorResponse>>> GetAuthors()
    {
        var authors = await _authorService.GetAuthors();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorResponse>> GetAuthor(string id)
    {
        var authorResponse = await _authorService.GetAuthor(id);

        if (authorResponse == null)
        {
            return NotFound();
        }

        return Ok(authorResponse);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponse>> AddAuthor(AuthorRequest authorRequest)
    {
        if (string.IsNullOrEmpty(authorRequest.Name) || authorRequest.Birthdate == default)
        {
            return BadRequest("Invalid author data");
        }

        var authorResponse = await _authorService.AddAuthor(authorRequest);

        return CreatedAtAction(nameof(GetAuthor), new { id = authorResponse.Id }, authorResponse);
    }
}
