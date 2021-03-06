﻿using DefaultMvcControllersAndAlternatives.DAL;
using DefaultMvcControllersAndAlternatives.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefaultMvcControllersAndAlternatives.Controllers
{
    public class AlternativeCategoryController : Controller
    {
        BookContext db = new BookContext();
        // GET: AlternativeCategory
        public ActionResult Index()
        {
            return View(db.Categories);
        }
        public ActionResult Show(int id)
        {
            string categoryName = (from c in db.Categories where c.Id == id select c.Name).FirstOrDefault();
            ViewBag.Title = categoryName + " Books";
            ViewBag.Id = id;
            var books = (from b in db.Books where b.CategoryId == id select b).ToList();
            return View(books.OrderBy(x => x.Name).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return Redirect("Index");
        }
        public ActionResult CreateBook(int id)
        {
            string categoryName = (from c in db.Categories where c.Id == id select c.Name).FirstOrDefault();
            ViewBag.CategoryName = categoryName;
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(Book book, int id)
        {
            book.CategoryId = id;
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Show", new { id = book.CategoryId });
        }
        public ActionResult EditBook(int id)
        {
            var categories = db.Categories.OrderBy(c => c.Name).ToList().Select(c => new SelectListItem
            {
                Selected = false,
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            ViewBag.Categories = categories;
            ViewBag.Id = categories.First().Value;
            Book book = db.Books.Where(b => b.Id == id).FirstOrDefault();
            return View(book);
        }
        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            Book editedBook = db.Books.Where(b => b.Id == book.Id).FirstOrDefault();
            editedBook.CategoryId = book.CategoryId;
            editedBook.Name = book.Name;
            editedBook.ISBN = book.ISBN;
            editedBook.Author = book.Author;
            editedBook.Publisher = book.Publisher;
            editedBook.PublicationDate = book.PublicationDate;
            editedBook.Price = book.Price;
            editedBook.ReducedPrice = book.ReducedPrice;
            db.SaveChanges();
            return RedirectToAction("Show", new { id = book.CategoryId });
        }
        public ActionResult DeleteBook(int id)
        {
            var categories = db.Categories.OrderBy(c => c.Name).ToList().Select(c => new SelectListItem
            {
                Selected = false,
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            ViewBag.Categories = categories;
            ViewBag.Id = categories.First().Value;
            Book book = db.Books.Where(b => b.Id == id).FirstOrDefault();
            return View(book);
        }
        [HttpPost]
        public ActionResult DeleteBook(Book book)
        {
            Book deletedBook = db.Books.Where(b => b.Id == book.Id).FirstOrDefault();
            db.Books.Remove(deletedBook);
            db.SaveChanges();
            return RedirectToAction("Show", new { id = deletedBook.CategoryId });
        }
        public ActionResult DetailsBook(int id)
        {
            var categories = db.Categories.OrderBy(c => c.Name).ToList().Select(c => new SelectListItem
            {
                Selected = false,
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            ViewBag.Categories = categories;
            ViewBag.Id = categories.First().Value;
            Book book = db.Books.Where(b => b.Id == id).FirstOrDefault();
            return View(book);
        }
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            Category editedCategory = db.Categories.Where(c => c.Id == category.Id).FirstOrDefault();
            editedCategory.Name = category.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            Category deletedCategory = db.Categories.Where(c => c.Id == category.Id).FirstOrDefault();
            db.Categories.Remove(deletedCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }
    }
}