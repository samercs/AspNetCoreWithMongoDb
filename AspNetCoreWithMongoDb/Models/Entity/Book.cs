using AspNetCoreWithMongoDb.Attribute;
using AspNetCoreWithMongoDb.Models.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AspNetCoreWithMongoDb.Models
{
    public class Book
    {
        public Book()
        {
            Category = new Category();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string AuthorId { get; set; }
        public string CategoryId { get; set; }
        [MongoRef("Categories", "CategoryId", "Id")]
        public Category Category { get; set; }
        [MongoRef("Authors", "AuthorId", "Id")]
        public Author Author { get; set; }


    }
}