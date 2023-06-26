namespace BookStore.Api.Models;

public class BookResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public int PublishedYear { get; set; }
}
