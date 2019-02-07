using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreWithMongoDb.Data;
using AspNetCoreWithMongoDb.Extentions;
using AspNetCoreWithMongoDb.Models;
using AspNetCoreWithMongoDb.Models.Entity;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
            var books = (List<Book>)  _context.Books.AsQueryable().ToList();
            return books.Include<Book, Category>(_context).Include<Book,Author>(_context);
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

        private List<Book> IncludeCategory(List<Book> books)
        {
            foreach (var book in books)
            {
                book.Category = _context.Categories.Find(i => i.Id.Equals(book.CategoryId)).FirstOrDefault();
            }
            return books;
        }
        private List<Book> IncludeAuthor(List<Book> books)
        {
            foreach (var book in books)
            {
                book.Author = _context.Authors.Find(i => i.Id.Equals(book.AuthorId)).FirstOrDefault();
            }
            return books;
        }
    }
}