using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public static class DisplayHelper
    {
        private static readonly string TABLE_OUTPUT_FORMAT = "|{0,30}|{1,12}|";

        public static void DisplayAll(IEnumerable<Book> Cache)
        {
            if (!Cache.Any())
            {
                Console.WriteLine("There are no Books in the system.");
                return;
            }
            DisplayTableHeader();
            foreach (Book book in Cache)
            {
                WriteDetailsRowToConsole(book);
            }
            Console.WriteLine($"Books: {Cache.Count()}");
        }

        public static void DisplaySingle()
        {
            Console.WriteLine("Which book do you want to view?");
            Book? book = InputValidation.QueryByName();
            if (book != null)
            {
                DisplayTableHeader();
                WriteDetailsRowToConsole(book);
            }
        }

        private static void DisplayTableHeader()
        {
            Console.WriteLine(TABLE_OUTPUT_FORMAT, "Book Name", "Availability");
        }
        
        private static void WriteDetailsRowToConsole(Book book)
        {

            Console.WriteLine(TABLE_OUTPUT_FORMAT, book.Name, book.IsCheckedOut ? "CHECKED OUT" : "AVAILABLE");
        }
    }
}
