using BookStore.Api.Entities;

namespace BookStore.Api.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthors();
    Task<Author> GetAuthor(string authorId);
    Task<Author> AddAuthor(Author author);
}
