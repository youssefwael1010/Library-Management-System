using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public class BorrowRecord
    {

        public int Id { get; }
        public  Book  book{ get; }
        public  Member member{ get;  }
        public DateTime BorrowDate { get; set; } 
        public DateTime? ReturnDate{ get; set; }

        public BorrowRecord(int id, Book book, Member member)
        {
            Id = id;
            this.book = book;
            this.member = member;
            BorrowDate = DateTime.Now;
            ReturnDate = null;
        }
        public bool IsLate()
        {
            if (ReturnDate != null)
                return false;


            return (DateTime.Now - BorrowDate).TotalDays > 14;
            
            
        }
    }
}
