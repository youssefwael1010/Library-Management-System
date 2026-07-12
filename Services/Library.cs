using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public class Library
    {

        private const int MAX_BOOKS = 100;
        private const int MAX_MEMBERS = 100;
        private const int MAX_RECORDS = 500;
        //counters
        private int bookCount = 0;
        private int memberCount = 0;
        private int recordCount = 0;
        // Ids
        private int BookId = 1;
        private int MemberId = 1;
        private int RecordId = 1;
        //containers
        private Book[] books = new Book[MAX_BOOKS];
        private BorrowRecord[] records = new BorrowRecord[MAX_RECORDS];
        private Member[] members = new Member[MAX_MEMBERS];

        public void AddBook(string title, string author, int year, string genre)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Title is required.");

            if (string.IsNullOrWhiteSpace(author))
                throw new Exception("Author is required.");

            if (string.IsNullOrWhiteSpace(genre))
                throw new Exception("Genre is required.");

            if (year <= 0)
                throw new Exception("Invalid year.");

            books[bookCount++] = new Book(BookId++, title, author, genre, year);

            Console.WriteLine("Book added successfully.");

        }
        public void RegisterMember(string name, string email, bool premium)
        {
            if (memberCount >= MAX_MEMBERS)
            {
                Console.WriteLine("Cannot register more members. Maximum limit reached.");
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name is required.");

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Eamil is required.");

            if (!email.Contains("@"))
                throw new Exception("Invalid Email.");

            if (premium)
                members[memberCount++] = new PremiumMember(MemberId++, name, email);
            else
                members[memberCount++] = new Member(MemberId++, name, email);


            Console.WriteLine("Member registered successfully.");
        }

        // helper functions

        private Member FindMember(int memberID)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].Id == memberID)
                    return members[i];
            }
            return null;
        }
        private Book FindBook(int bookId)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (books[i].Id == bookId)
                    return books[i];
            }
            return null;
        }
        private BorrowRecord FindBorrowRecord(int recordId)
        {
            for (int i = 0; i < recordCount; i++)
            {
                if (records[i].Id == recordId)
                    return records[i];
            }
            return null;
        }
        private void AddBookToMember(Member member, Book book)
        {
            int borrowed = 0;

            foreach (Book b in member.BorrowedBooks)
            {
                if (b != null)
                    borrowed++;
            }

            if (borrowed >= member.MaxBorrowLimit)
                throw new Exception("Borrow limit reached.");

            for (int i = 0; i < member.BorrowedBooks.Length; i++)
            {
                if (member.BorrowedBooks[i] == null)
                {
                    member.BorrowedBooks[i] = book;
                    return;
                }
            }
        }
        private void RemoveBookFromMember(Member member, Book book)
        {
            for (int i = 0; i < member.BorrowedBooks.Length; i++)
            {
                if (member.BorrowedBooks[i] == book)
                {
                    member.BorrowedBooks[i] = null;
                    return;
                }
            }
            throw new Exception("Book not found in member's borrowed books.");
        }
        public void ShowAllBooks()
        {
            if (bookCount == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            for (int i = 0; i < bookCount; i++)
            {
                Console.WriteLine(books[i].GetInfo());
            }
        }
        public void ShowAllMembers()
        {
            if (memberCount == 0)
            {
                Console.WriteLine("No members found.");
                return;
            }

            for (int i = 0; i < memberCount; i++)
            {
                Console.WriteLine(members[i].GetInfo());
            }
        }


        // 
              
        public void BorrowBook(int memberID, int BookID)
        {

            Book book = FindBook(BookID);

            if (book == null)
                throw new Exception("Book not found.");

            Member member = FindMember(memberID);

            if (member == null)
                throw new Exception("Member not found.");

            if (!book.IsAvailable)
                throw new Exception("Book is already borrowed.");

            book.IsAvailable = false;
            records[recordCount++] = new BorrowRecord(RecordId++, book, member);

            AddBookToMember(member, book);

            Console.WriteLine("Book borrowed successfully.");
        }

        public void ReturnBook(int BookID)
        {

            Book book = FindBook(BookID);

            if (book == null)
                throw new Exception("Book not found.");

            BorrowRecord record = FindBorrowRecord(BookID);
            if (record == null)
                throw new Exception("Book is available");

            book.IsAvailable = true;
            record.ReturnDate = DateTime.Now;
            RemoveBookFromMember(record.member, book);

            Console.WriteLine("Book returned successfully.");
        }

        public void SearchCatalog(string query)
        {
            bool found = false;
            Console.WriteLine("\n===== Books =====");
            foreach (var book in books)
            {
                if (book != null && book.MatchesQuery(query))
                {
                    Console.WriteLine(book.GetInfo());
                    found = true;
                }
            }

            Console.WriteLine("\n===== Members =====");

            foreach (var member in members)
            {
                if (member != null && member.MatchesQuery(query))
                {
                    Console.WriteLine(member.GetInfo());
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("No Results found.");
        }

        public void ViewAvailableBooks()
        {
            bool found = false;
            Console.WriteLine("\n===== Available Books =====");

            foreach (var book in books)
            {
                if (book != null && book.IsAvailable)
                {
                    Console.WriteLine(book.GetInfo());
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("No Available Books.");
        }

        public void MemberBorrowingHistory(int memberID)
        {
            Member member = FindMember(memberID);

            if (member == null)
                throw new Exception("Member not found.");

            bool found = false;
            Console.WriteLine($"\nBorrow History For {member.Name}");


            foreach (var record in records)
            {
                if (record == null)
                    continue;

                if (record.member.Id == memberID)
                {
                    found = true;
                    Console.WriteLine("--------------------------------");

                    Console.WriteLine($"Book : {record.book.Title}");

                    Console.WriteLine($"Borrow Date : {record.BorrowDate}");
                    Console.WriteLine($"{(record.ReturnDate == null ? "Status : Still Borrowed." : $"Returned : {record.ReturnDate}" )}");
                }
            }
            if (!found)
            {
                Console.WriteLine("No borrowing history.");
            }

        }

        public void LateReturnReport()
        {
            bool found = false;
            Console.WriteLine("\n===== Late Return Report =====");
            foreach (var record in records)
            {
                if (record != null && record.ReturnDate==null && record.IsLate())
                {
                    found = true;
                    int daysOverdue = (int)(DateTime.Now - record.BorrowDate).TotalDays - 14;

                    Console.WriteLine("--------------------------------");
                    Console.WriteLine($"Member : {record.member.Name}");
                    Console.WriteLine($"Book   : {record.book.Title}");
                    Console.WriteLine($"Borrow : {record.BorrowDate:d}");
                    Console.WriteLine($"Overdue: {daysOverdue} day(s)"); 
                }
            }
            if (!found)
            {
                Console.WriteLine("No late Books.");
            }
        }

        // for testing 
        public void SeedData()
        {
            books[bookCount++] = new Book(BookId++, "Berserk", "Kentaro Miura", "Manga", 1990);

            books[bookCount++] = new Book(BookId++, "Harry Potter", "J.K. Rowling", "Fantasy", 1997);

            books[bookCount++] = new Book(BookId++, "Rich dad Poor dad", "Robert Kiyosaki", "Self Help", 1997);


            members[memberCount++] = new Member(MemberId++, "Joe", "joe@gmail.com");

            members[memberCount++] = new PremiumMember(MemberId++, "Menna", "menna@gmail.com");

            // late record
            Book lateBook = books[0];

            lateBook.IsAvailable = false;

            AddBookToMember(members[0], lateBook);

            BorrowRecord record = new BorrowRecord(RecordId++,lateBook,members[0]);

            record.BorrowDate = DateTime.Now.AddDays(-20);// exceeds max loan days

            records[recordCount++] = record;
        }

    }
}
