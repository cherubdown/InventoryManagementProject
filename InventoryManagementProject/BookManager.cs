using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryManagementProject
{
    public class BookManager
    {
        private Cache BooksCache { get; set; } = Cache.Instance;

        public void Start()
        {
            Console.WriteLine("Welcome to the Library System. What do you want to do?");

            while (true)
            {
                DisplayOptions();

                ConsoleKeyInfo response = Console.ReadKey();
                Console.WriteLine(Environment.NewLine);

                switch (response.KeyChar)
                {
                    case '1':
                        BooksCache.CheckOut();
                        break;
                    case '2':
                        BooksCache.CheckIn();
                        break;
                    case '3':
                        BooksCache.Display();
                        continue;
                    case '4':
                        BooksCache.DisplaySingle();
                        continue;
                    case '5':
                        BooksCache.Add();
                        break;
                    case '6':
                        BooksCache.Remove();
                        break;
                    case '7':
                        Environment.Exit(0);
                        continue;
                    default:
                        Console.WriteLine("Invalid Input.");
                        continue;
                }

                // except for Displaying, every action needs to save the database for persistance
                BooksCache.Save();
            }
        }

        private void DisplayOptions()
        {
            Console.WriteLine(Environment.NewLine + "Options:" + Environment.NewLine);
            Console.WriteLine("1. Check Out book");
            Console.WriteLine("2. Return book");
            Console.WriteLine("3. View all books");
            Console.WriteLine("4. Search for a book");
            Console.WriteLine("5. Add book to the system");
            Console.WriteLine("6. Remove book from the system");
            Console.WriteLine("7. Exit");
        }


    }
}
