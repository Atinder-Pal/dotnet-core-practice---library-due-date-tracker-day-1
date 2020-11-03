using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDueDateDay1.Models
{
    public class Book
    {
        private DateTime _dueDate;
        private DateTime? _returnedDate;
        private int _id;
        public int ID => _id;

        private string _title;
        public string Title => _title;

        private string _author;
        public string Author => _author;

        private DateTime _publicationDate;
        public DateTime PublicationDate => _publicationDate;

        private DateTime _checkedOutDate;
        public DateTime CheckedOutDate => _checkedOutDate;

        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
            }
        }
        public DateTime? ReturnedDate
        {
            get
            {
                return _returnedDate;
            }
            set
            {
                _returnedDate = value;
            }
        }

        public Book(int id, string title, string author, DateTime publicationDate, DateTime checkedOutDate)
        {
            _id = id;
            _title = title;
            _author = author;
            _publicationDate = publicationDate;
            _checkedOutDate = checkedOutDate;
            DueDate = CheckedOutDate.AddDays(14);
            ReturnedDate = null;
        }       
    }
}
