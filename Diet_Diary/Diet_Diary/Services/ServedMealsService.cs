using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Diet_Diary.Models;

namespace Diet_Diary.Services
{
    public class ServedMealsService
    {
        private readonly IMongoCollection<ServedMeal> _servedMealsCollection;

        public ServedMealsService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _servedMealsCollection = mongoDatabase.GetCollection<ServedMeal>("ServedMeals");
        }

        public List<ServedMeal> Get()
        {
            return _servedMealsCollection.Find(_ => true).ToList();
        }

        public List<ServedMeal> GetByMealID(string MealId)
        {
            return _servedMealsCollection.Find(x => x.MealId == MealId).ToList();
        }

        public void Create(ServedMeal newProduct)
        {
            _servedMealsCollection.InsertOne(newProduct);
        }

        public void Update(string id, ServedMeal updatedProduct)
        {
            _servedMealsCollection.ReplaceOne(x => x.MealId == id, updatedProduct);
        }

        public void Remove(string id)
        {
            _servedMealsCollection.DeleteOneAsync(x => x.MealId == id);
        }
    }
}
