using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Diet_Diary.Models;

namespace Diet_Diary.Services
{
    public class MealsService
    {
        private readonly IMongoCollection<Meal> _mealsCollection;

        public MealsService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _mealsCollection = mongoDatabase.GetCollection<Meal>("Meals");
        }

        public List<Meal> Get()
        {
            return _mealsCollection.Find(_ => true).ToList();
        }
        public List<Meal> GetByUsername(string Username)
        {
            return _mealsCollection.Find(x => x.Username == Username).ToList();
        }
        public Meal? GetById(string id)
        {
            return _mealsCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Create(Meal newProduct)
        {
            _mealsCollection.InsertOne(newProduct);
        }

        public void Update(string id, Meal updatedProduct)
        {
            _mealsCollection.ReplaceOne(x => x.Id == id, updatedProduct);
        }

        public void Remove(string id)
        {
            _mealsCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
