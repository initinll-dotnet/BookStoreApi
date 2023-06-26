using BookStore.Api.Controllers;
using BookStore.Api.Models;
using BookStore.Api.Services;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using Xunit;

namespace BookStoreApi.Web.Tests.Unit;
public class BooksControllerTests
{
    private readonly BooksController _sut;
    private readonly IBookService _bookService = Substitute.For<IBookService>();
    private readonly IAuthorService _authorService = Substitute.For<IAuthorService>();

    public BooksControllerTests()
    {
        _sut = new BooksController(_bookService, _authorService);
    }

    [Fact]
    public async Task AddBook_ShouldCreateBook_WhenAddBookRequestIsValid()
    {
        // Arrange
        var authorResponse = new AuthorResponse
        {
            Id = "64942600ec3302a008506f82",
            Name = "TestAuthor",
            Birthdate = DateTime.Now
        };

        var bookRequest = new BookRequest
        {
            AuthorId = authorResponse.Id,
            PublishedYear = 2014,
            Title = "TestBook"
        };

        var bookResponse = new BookResponse
        {
            Id = Guid.NewGuid().ToString(),
            AuthorName = authorResponse.Name,
            Title = bookRequest.Title,
            PublishedYear = bookRequest.PublishedYear
        };

        _authorService.GetAuthor(bookRequest.AuthorId).Returns(authorResponse);
        _bookService.AddBook(bookRequest).Returns(bookResponse);

        // Act
        var result = await _sut.AddBook(bookRequest);

        // Asert
        result.Result.As<CreatedAtActionResult>().StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task AddBook_ShouldReturnBadRequest_WhenAuthorDoesNotExists()
    {
        // Arrange
        var bookRequest = new BookRequest
        {
            AuthorId = "testid",
            PublishedYear = 2014,
            Title = "TestBook"
        };

        _authorService.GetAuthor(bookRequest.AuthorId).ReturnsNull();

        // Act
        var result = await _sut.AddBook(bookRequest);

        // Asert
        result.Result.As<BadRequestObjectResult>().StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task AddBook_ShouldReturnBadRequest_WhenBookRequestIsInvalid()
    {
        // Arrange


        var bookRequest = new BookRequest
        {
            AuthorId = "",
            PublishedYear = 0,
            Title = ""
        };

        // Act
        var result = await _sut.AddBook(bookRequest);

        // Asert
        result.Result.As<BadRequestObjectResult>().StatusCode.Should().Be(400);
    }
}
