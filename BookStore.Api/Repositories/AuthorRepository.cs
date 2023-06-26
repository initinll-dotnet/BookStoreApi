using BookStore.Api.DbContext;
using BookStore.Api.Entities;
using BookStore.Api.Settings;

using MongoDB.Driver;

namespace BookStore.Api.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly IMongoCollection<Author> _authorCollection;
    public AuthorRepository(MongoContext dbContext)
    {
        _authorCollection = dbContext.Database.GetCollection<Author>(MongoCollectionNames.Authors);
    }

    public async Task<IEnumerable<Author>> GetAuthors()
    {
        var author = (await _authorCollection.FindAsync(Builders<Author>.Filter.Empty)).ToList();
        return author;
    }

    public async Task<Author> GetAuthor(string authorId)
    {
        var author = (await _authorCollection.FindAsync(a => a.Id == authorId)).FirstOrDefault();
        return author;
    }

    public async Task<Author> AddAuthor(Author author)
    {
        await _authorCollection.InsertOneAsync(author);
        return author;
    }
}
