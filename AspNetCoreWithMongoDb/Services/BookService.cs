using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreWithMongoDb.Data;
using AspNetCoreWithMongoDb.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AspNetCoreWithMongoDb.Services
{
    public class BookService
    {
        private readonly IMongoContext _context;

        public BookService(IMongoContext context)
        {
            _context = context;
        }

        public List<Book> Get()
        {
            return (List<Book>)  _context.Books.Find(i => true).ToList();
        }

        public Book Get(string id)
        {
            return _context.Books.Find<Book>(book => book.Id == id).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _context.Books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn)
        {
            _context.Books.ReplaceOne(book => book.Id == id, bookIn);
        }

        public void Remove(Book bookIn)
        {
            _context.Books.DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Remove(string id)
        {
            _context.Books.DeleteOne(book => book.Id == id);
        }
    }
}