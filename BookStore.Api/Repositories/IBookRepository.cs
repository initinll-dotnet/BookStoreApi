using BookStore.Api.Entities;

namespace BookStore.Api.Repositories;

public interface IBookRepository
{
    Task<Book> GetBook(string id);
    Task<IEnumerable<Book>> GetBooks(int? publishedYear);
    Task<Book> AddBook(Book book);
}
