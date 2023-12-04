using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Diet_Diary.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public string Category { get; set; } = null!;

        public int Calories { get; set; }
        [BsonElement("Score")]
        public double Score { get; set; }
    }
}
