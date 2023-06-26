namespace BookStore.Api.Models;

public class BookRequest
{
    public string Title { get; set; }
    public string AuthorId { get; set; }
    public int PublishedYear { get; set; }
}
