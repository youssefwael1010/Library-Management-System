using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public class Member : ISearchable
    {
        public int Id {get;  }
        public string Name { get; set; }
        public string Email {get; set; }
        public DateTime JoinDate { get; }
        public Book[] BorrowedBooks { get; }

        public virtual int MaxBorrowLimit { get; }
        public virtual int LoanDays { get; }

        public Member(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            JoinDate = DateTime.Now;
            BorrowedBooks = new Book[20];
            MaxBorrowLimit = 5; // any number less than premium limit
            LoanDays = 14;
        }


        public virtual string GetInfo()
        {
            return $" Member: {Name}, ID : {Id}, Email: {Email}, Join Date: {JoinDate.ToShortDateString()}";
        }

        public bool MatchesQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new Exception("Query is required");
            query = query.ToLower();

            return Name.ToLower() == query;
        }
    }
}
