using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDueDateDay1.Models
{
    public class Book
    {
        public int ID { get; }
        public string Title { get; }
        public string Author { get; }
        public DateTime PublicationDate { get; }
        public DateTime CheckedOutDate { get; }

        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
       

        public Book(int id, string title, string author, DateTime publicationDate, DateTime checkedOutDate)
        {
            ID = id;
            Title = title;
            Author = author;
            PublicationDate = publicationDate;
            CheckedOutDate = checkedOutDate;
            DueDate = CheckedOutDate.AddDays(14);
            ReturnedDate = null;
        }
        public Book()
        {
            ID = -1;
            Title = "Defaulttitle";
            Author = "Default Author";
            PublicationDate = DateTime.Now;
            CheckedOutDate = DateTime.Now;
            DueDate = CheckedOutDate.AddDays(14);
            ReturnedDate = null;
        }
    }
}
