using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Diet_Diary.Models
{
    public class UserProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int Weight { get; set; }
    }
}
