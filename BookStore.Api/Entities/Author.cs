using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Api.Entities;

public class Author
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Birthdate")]
    public DateTime Birthdate { get; set; }
}
