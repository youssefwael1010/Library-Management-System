using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public abstract class LibraryItem
    {
        public  string Title { get; set; }
        public int Id { get;  }
        public  DateTime AddedDate { get;  }

        public LibraryItem(int id, string title)
        {
            Id = id;
            Title = title;
            AddedDate = DateTime.Now;
        }
        public abstract string GetInfo();
    }
}
