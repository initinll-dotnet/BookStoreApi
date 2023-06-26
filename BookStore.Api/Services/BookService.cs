using BookStore.Api.Entities;
using BookStore.Api.Models;
using BookStore.Api.Repositories;

namespace BookStore.Api.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<BookResponse> GetBook(string id)
    {
        var book = await _bookRepository.GetBook(id);
        var bookResponse = await MapToBookResponse(book);

        return bookResponse;
    }

    public async Task<IEnumerable<BookResponse>> GetBooks(int? publishedYear)
    {
        var books = await _bookRepository.GetBooks(publishedYear);

        if (books == null || books.Count() == 0)
        {
            return Enumerable.Empty<BookResponse>();
        }

        var bookResponses = new List<BookResponse>();

        foreach (var book in books)
        {
            var bookResponse = await MapToBookResponse(book);
            bookResponses.Add(bookResponse);
        }

        return bookResponses;
    }

    public async Task<BookResponse> AddBook(BookRequest bookRequest)
    {
        var book = new Book
        {
            Title = bookRequest.Title,
            AuthorId = bookRequest.AuthorId,
            PublishedYear = bookRequest.PublishedYear
        };

        book = await _bookRepository.AddBook(book);
        var bookResponse = await MapToBookResponse(book);
        return bookResponse;
    }

    private async Task<BookResponse> MapToBookResponse(Book book)
    {
        var author = await _authorRepository.GetAuthor(book.AuthorId);
        var bookResponse = new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            AuthorName = author.Name,
            PublishedYear = book.PublishedYear
        };
        return bookResponse;
    }
}
