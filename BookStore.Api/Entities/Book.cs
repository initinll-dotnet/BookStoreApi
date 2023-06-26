using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Api.Entities;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; }

    [BsonElement("AuthorId")]
    public string AuthorId { get; set; }

    [BsonElement("PublishedYear")]
    public int PublishedYear { get; set; }
}
