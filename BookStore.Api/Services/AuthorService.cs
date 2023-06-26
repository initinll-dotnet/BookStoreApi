using BookStore.Api.Entities;
using BookStore.Api.Models;
using BookStore.Api.Repositories;

namespace BookStore.Api.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorResponse>> GetAuthors()
    {
        var authors = await _authorRepository.GetAuthors();
        var authorResponses = authors.Select(a => MapToAuthorResponse(a)).ToList();
        return authorResponses;
    }

    public async Task<AuthorResponse> GetAuthor(string id)
    {
        var author = await _authorRepository.GetAuthor(id);
        var authorResponse = MapToAuthorResponse(author);
        return authorResponse;
    }

    public async Task<AuthorResponse> AddAuthor(AuthorRequest authorRequest)
    {
        var author = new Author
        {
            Name = authorRequest.Name,
            Birthdate = authorRequest.Birthdate
        };

        author = await _authorRepository.AddAuthor(author);
        var authorResponse = MapToAuthorResponse(author);
        return authorResponse;
    }

    private AuthorResponse MapToAuthorResponse(Author author)
    {
        var authorResponse = new AuthorResponse
        {
            Id = author.Id,
            Name = author.Name,
            Birthdate = author.Birthdate
        };
        return authorResponse;
    }
}
