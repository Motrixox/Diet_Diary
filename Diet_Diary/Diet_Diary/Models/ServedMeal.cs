using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Diet_Diary.Models
{
	public class ServedMeal
	{
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public string MealId { get; set; }
		public double Coeff { get; set; }
        public DateTime Date { get; set; }

    }
}
