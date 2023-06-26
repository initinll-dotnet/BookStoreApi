using BookStore.Api.Models;

namespace BookStore.Api.Services;

public interface IAuthorService
{
    public Task<IEnumerable<AuthorResponse>> GetAuthors();
    public Task<AuthorResponse> GetAuthor(string id);
    Task<AuthorResponse> AddAuthor(AuthorRequest authorRequest);
}
