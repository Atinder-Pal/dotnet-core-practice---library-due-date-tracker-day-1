using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDueDateDay1.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDueDateDay1.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult Create(int id, string title, string author, DateTime publicationDate, DateTime checkedOutDate)
        {
            try
            {
                CreateBook(id, title, author, publicationDate, checkedOutDate);
            }
            catch(Exception e)
            {
                ViewBag.addMessage = $"Unable to check out book:{e.Message}";
            }
            ViewBag.addMessage = $"You have successfully checked out {title} until {checkedOutDate.AddDays(14)}.";
            
            return View();
        }

        public IActionResult List()
        {
            ViewBag.list = Books;
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public static List<Book> Books = new List<Book>();

        public void CreateBook(int id, string title, string author, DateTime publicationDate, DateTime checkedOutDate)
        {           
            if (Books.Where(book => book.ID == id).ToList().Count == 0)
            {
                var newBook = new Book(id, title, author, publicationDate, checkedOutDate);
                Books.Add(newBook);
            }
            else
            {
                throw new Exception("ID already exists. Try adding it witha new ID");
            }
            
        }
    }
}
