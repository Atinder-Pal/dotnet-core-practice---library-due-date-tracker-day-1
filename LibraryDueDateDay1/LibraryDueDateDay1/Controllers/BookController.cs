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
            if(id !=null || title != null || author != null || publicationDate != null || checkedOutDate != null)
            {
                try
                {
                    Book createdBook = CreateBook(int.Parse(id), title, author, DateTime.Parse(publicationDate), DateTime.Parse(checkedOutDate));
                    ViewBag.addMessage = $"You have successfully checked out {createdBook.Title} until {createdBook.DueDate}.";
                }
                catch (Exception e)
                {
                    ViewBag.addMessage = $"Unable to check out book: {e.Message}";
                }
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

                ViewBag.bookDetails = GetBookByID(int.Parse(id.Trim()));
                if(ViewBag.bookDetails == null)
                {
                    ViewBag.errorMessage = "No book selected.";                    
                }
                    
            }   
            return View();
        }
        
        public Book CreateBook(string id, string title, string author, string publicationDate, string checkedOutDate)
        {
            int parsedID = int.Parse(id);
            if (!Books.Exists(x => x.ID == parsedID))
            {
                var newBook = new Book(parsedID, title.Trim(), author.Trim(), DateTime.Parse(publicationDate), DateTime.Parse(checkedOutDate));
                Books.Add(newBook);
                return newBook;
            }
            else
            {
                throw new Exception("ID already exists. Try adding it with a new ID");
            }            
        }
        public Book GetBookByID(string id)            
        {
            return Books.Where(x => x.ID == int.Parse(id)).Single();
        }
        public void ExtendDueDateForBookByID(string id)
        {
            GetBookByID(id).DueDate = GetBookByID(id).DueDate.AddDays(7);
        }

        public void ReturnBookByID(int id)
        {
            GetBookByID(id).ReturnedDate = DateTime.Now;
        }

        public void DeleteBookByID(int id)
        {
            Books.Remove(GetBookByID(id));
        }
    }
}
