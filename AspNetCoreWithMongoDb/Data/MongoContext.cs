using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCoreWithMongoDb.Models;
using AspNetCoreWithMongoDb.Models.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AspNetCoreWithMongoDb.Data
{
    public class MongoContext: IMongoContext
    {
        private readonly IMongoDatabase _db;
        public MongoContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.DatabaseName);
        }
        public IMongoCollection<Book> Books => _db.GetCollection<Book>("Books");
        public IMongoCollection<Category> Categories => _db.GetCollection<Category>("Categories");
        public IMongoCollection<Author> Authors => _db.GetCollection<Author>("Authors");
    }
}