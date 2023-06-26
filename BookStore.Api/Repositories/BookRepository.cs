using BookStore.Api.DbContext;
using BookStore.Api.Entities;
using BookStore.Api.Models;
using BookStore.Api.Settings;

using MongoDB.Driver;

namespace BookStore.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _bookCollection;

    public BookRepository(MongoContext dbContext)
    {
        _bookCollection = dbContext.Database.GetCollection<Book>(MongoCollectionNames.Books);
    }

    public async Task<Book> GetBook(string id)
    {
        var book = (await _bookCollection.FindAsync(b => b.Id == id)).FirstOrDefault();
        return book;
    }

    public async Task<IEnumerable<Book>> GetBooks(int? publishedYear)
    {
        var filter = Builders<Book>.Filter.Empty;

        if (publishedYear.HasValue)
        {
            filter = Builders<Book>.Filter.Eq(b => b.PublishedYear, publishedYear.Value);
        }

        var books = (await _bookCollection.FindAsync(filter)).ToList();

        return books;
    }

    public async Task<Book> AddBook(Book book)
    {
        await _bookCollection.InsertOneAsync(book);
        return book;
    }
}
