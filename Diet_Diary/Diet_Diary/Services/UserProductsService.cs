using Diet_Diary.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Diet_Diary.Services
{
    public class UserProductsService
    {
        private readonly IMongoCollection<UserProduct> _productsCollection;

        public UserProductsService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<UserProduct>("UserProducts");
        }

        public List<UserProduct> Get()
        {
            return _productsCollection.Find(_ => true).ToList();
        }

        public UserProduct? GetByName(string name)
        {
            return _productsCollection.Find(x => x.Name == name).FirstOrDefault();
        }

        public void Create(UserProduct newProduct)
        {
            _productsCollection.InsertOne(newProduct);
        }

        public void Update(string id, UserProduct updatedProduct)
        {
            _productsCollection.ReplaceOne(x => x.Id == id, updatedProduct);
        }

        public void Remove(string id)
        {
            _productsCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
