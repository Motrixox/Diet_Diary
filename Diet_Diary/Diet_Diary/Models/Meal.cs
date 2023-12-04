using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Diet_Diary.Models
{
    public class Meal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; }
        public Dictionary<string, int> Products { get; set; }
        public double Calories { get; set; }

        public DateTime Date { get; set; }
    }
}
