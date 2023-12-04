using Diet_Diary.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using System.Linq;

namespace Diet_Diary.Services
{
    public class ProductsService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductsService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Product>("Products");
        }

        public List<Product> Get()
        {
            return _productsCollection.Find(_ => true).ToList();
        }

        public List<string> GetCategories()
        {
            var filter = new BsonDocument();
            List<string> categoriesList = _productsCollection.Distinct<string>("Category", filter).ToList();
            return categoriesList;
        }

        public List<Product> GetByCategory(string category)
        {
            return _productsCollection.Find(x => x.Category == category).ToList();
        }

        public List<Product> GetSuggestionsByMetaScore(string searchText)
        {
            var filter = Builders<Product>.Filter.Text(searchText);

            var projection = Builders<Product>.Projection.MetaSearchScore("Score")
                .Include(x => x.Name)
                .Include(x => x.Calories);

            var sort = Builders<Product>.Sort.MetaTextScore(searchText);

            var suggestions = _productsCollection.Find(filter)
                .Project<Product>(projection)
                .Sort(sort)
                .Limit(10)
                .ToList();

            suggestions = suggestions.DistinctBy(x => x.Name).ToList();

            return suggestions;
        }


        public List<Product> GetSuggestionsByRegex(string searchText)
        {
            var regex = new BsonRegularExpression(searchText, "i");
            var filter = Builders<Product>.Filter.Regex(x => x.Name, regex);
            var sort = Builders<Product>.Sort.Ascending("Name");

            var suggestions = _productsCollection.Find(filter).Limit(10).Sort(sort).ToList();

            return suggestions;
        }

        public Product? Get(string id)
        {
            return _productsCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Create(Product newProduct)
        {
            _productsCollection.InsertOne(newProduct);
        }

        public void Update(string id, Product updatedProduct)
        {
            _productsCollection.ReplaceOne(x => x.Id == id, updatedProduct);
        }

        public void Remove(string id)
        {
            _productsCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
