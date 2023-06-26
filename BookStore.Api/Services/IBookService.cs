using BookStore.Api.Models;

namespace BookStore.Api.Services;

public interface IBookService
{
    Task<BookResponse> GetBook(string id);
    Task<IEnumerable<BookResponse>> GetBooks(int? publishedYear);
    Task<BookResponse> AddBook(BookRequest bookRequest);
}
