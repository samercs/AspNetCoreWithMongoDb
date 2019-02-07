using AspNetCoreWithMongoDb.Models;
using AspNetCoreWithMongoDb.Models.Entity;
using MongoDB.Driver;

namespace AspNetCoreWithMongoDb.Data
{
    public interface IMongoContext
    {
        IMongoCollection<Book> Books { get; }
        IMongoCollection<Category> Categories { get; }
        IMongoCollection<Author> Authors { get;  } 
    }
}