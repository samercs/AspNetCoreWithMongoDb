using AspNetCoreWithMongoDb.Models;
using MongoDB.Driver;

namespace AspNetCoreWithMongoDb.Data
{
    public interface IMongoContext
    {
        IMongoCollection<Book> Books { get; }
    }
}