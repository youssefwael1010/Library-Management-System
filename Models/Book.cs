using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public  class Book :  LibraryItem , ISearchable
    {

        public string Author{get;set;} 
        public string Genre{ get; set; }
        public bool IsAvailable { get; set; }
        public int Year { get; set; }

        public Book(int id, string title, string author, string genre, int year) : base(id, title)
        {
            Author = author;
            Genre = genre;
            Year = year;
            IsAvailable = true;
        }
        public override  string GetInfo()
        {
            return $"ID: {Id}, Title: {Title}, Author: {Author}, Genre: {Genre}, Year: {Year}, {(IsAvailable ? "Available" : "Borrowed")}";

        }

        public  bool MatchesQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new Exception("Query is required");

            query = query.ToLower();

            return Title.ToLower() == query ;
        }
    }
}
