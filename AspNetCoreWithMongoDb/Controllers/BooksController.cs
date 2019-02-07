using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWithMongoDb.Models;
using AspNetCoreWithMongoDb.Models.Books;
using AspNetCoreWithMongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWithMongoDb.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var books = _bookService.Get();
            return View(books);
        }

        public IActionResult Add()
        {
            var model = new AddViewModel()
            {
                Book = new Book()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            _bookService.Create(model.Book);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            var model = new AddViewModel()
            {
                Book = book
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(string id, AddViewModel model)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Name = model.Book.Name;
            book.Author = model.Book.Author;
            book.Category = model.Book.Category;
            book.Price = model.Book.Price;

            _bookService.Update(id, book);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book);
            return RedirectToAction("Index");
        }
    }
}