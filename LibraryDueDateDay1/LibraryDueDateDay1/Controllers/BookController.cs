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
        public static List<Book> Books = new List<Book>();
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult Create(string id, string title, string author, string publicationDate, string checkedOutDate)
        {
            try
            {
                Book createdBook = CreateBook(int.Parse(id), title, author, DateTime.Parse(publicationDate), DateTime.Parse(checkedOutDate));
                ViewBag.addMessage = $"You have successfully checked out {createdBook.Title} until {createdBook.DueDate}.";
            }
            catch(Exception e)
            {
                ViewBag.addMessage = $"Unable to check out book: {e.Message}";
            }           
            return View();
        }

        public IActionResult List()
        {
            ViewBag.list = Books;
            return View();
        }

        public IActionResult Details(string id, string op)
        {
            if(string.IsNullOrEmpty(id.Trim())|| string.IsNullOrWhiteSpace(id.Trim()) || Books.Any(x => x.ID == int.Parse(id.Trim())) == false)
            {
                ViewBag.errorMessage = "No book selected.";
            }
            else
            {
                if(id != null && op == "extend")
                {
                    ExtendDueDateForBookByID(int.Parse(id));
                }
                if (id != null && op == "return")
                {
                    ReturnBookByID(int.Parse(id));
                }
                if (id != null && op == "delete")
                {
                    DeleteBookByID(int.Parse(id));
                }

                ViewBag.bookDetails = GetBookById(int.Parse(id.Trim()));
            }   
            return View();
        }
        
        public Book CreateBook(int id, string title, string author, DateTime publicationDate, DateTime checkedOutDate)
        {           
            if (! Books.Any(book => book.ID == id))
            {
                var newBook = new Book(id, title, author, publicationDate, checkedOutDate);
                Books.Add(newBook);
                return newBook;
            }
            else
            {
                throw new Exception("ID already exists. Try adding it with a new ID");
            }            
        }
        public Book GetBookById(int id)            
        {
            return Books.Where(x => x.ID == id).SingleOrDefault();
        }
        public void ExtendDueDateForBookByID(int id)
        {
            GetBookById(id).DueDate = DateTime.Now.AddDays(7);
        }

        public void ReturnBookByID(int id)
        {
            GetBookById(id).ReturnedDate = DateTime.Now;
        }

        public void DeleteBookByID(int id)
        {
            Books.Remove(GetBookById(id));
        }
    }
}
