using Sprint1;

public class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        library.SeedData();

        bool exit = false;

        do
        {

            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("      Library Management System");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Register Member");
            Console.WriteLine("3. Borrow Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Search");
            Console.WriteLine("6. View Available Books");
            Console.WriteLine("7. Member Borrow History");
            Console.WriteLine("8. Late Return Report");
            Console.WriteLine("9. Show All Books");
            Console.WriteLine("10. Show All Members");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Choose: ");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case 1:

                        Console.Write("Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Author: ");
                        string author = Console.ReadLine();

                        Console.Write("Year: ");
                        int year = int.Parse(Console.ReadLine());

                        Console.Write("Genre: ");
                        string genre = Console.ReadLine();

                        library.AddBook(title, author, year, genre);

                        break;

                    case 2:

                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Premium Member? (y/n): ");

                        bool premium = (Console.ReadLine().ToLower() == "y");

                        library.RegisterMember(name, email,premium);

                        break;

                    case 3:

                        Console.Write("Book ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int borrowBookId))
                        {
                            Console.WriteLine("Invalid number.");
                            break;
                        }
                        

                        Console.Write("Member ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int memberId))
                        {
                            Console.WriteLine("Invalid number.");
                            break;
                        }

                        library.BorrowBook(memberId, borrowBookId);

                        break;

                    case 4:

                        Console.Write("Book ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int returnBookId))
                        {
                            Console.WriteLine("Invalid number.");
                            break;
                        }
                       

                        library.ReturnBook(returnBookId);

                        break;

                    case 5:

                        Console.Write("Search: ");

                        string query = Console.ReadLine();

                        library.SearchCatalog(query);

                        break;

                    case 6:

                        library.ViewAvailableBooks();

                        break;

                    case 7:

                        Console.Write("Member ID: ");

                        if (!int.TryParse(Console.ReadLine(), out int historyId))
                        {
                            Console.WriteLine("Invalid number.");
                            break;
                        }
                     

                        library.MemberBorrowingHistory(historyId);

                        break;

                    case 8:

                        library.LateReturnReport();

                        break;

                    case 9:

                        library.ShowAllBooks();

                        break;

                    case 10:

                        library.ShowAllMembers();

                        break;

                    case 0:

                        exit = true;

                        break;

                    default:

                        Console.WriteLine("Invalid choice.");

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }

        }
        while (!exit);

        Console.WriteLine("Bye Bye");
    }
}

