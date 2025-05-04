using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public static class InputValidation
    {
        /// <summary>
        /// Asks the user for a common Y/N and converts to boolean (Yes is true, No is false)
        /// If the user does not press Y or N (or y or n) this function will loop until they do
        /// Exiting out of this is not an option
        /// </summary>
        /// <returns>A boolean that represents Yes or No</returns>
        public static bool ReadUserKeyAndDetermineYesOrNo()
        {
            char keyPressed = Console.ReadKey().KeyChar;
            if (keyPressed == 'y')
            {
                return true;
            }
            else if (keyPressed == 'n')
            {
                return false;
            }
            else
            {
                Console.WriteLine("That is not a valid response. Please enter Y or N (or y or n) for Yes or No respectively.");
                return ReadUserKeyAndDetermineYesOrNo();
            }
        }

        public static bool IsPositiveIntegerFromUserInput(out int quantity)
        {
            string? quantityStr = Console.ReadLine();
            bool result = int.TryParse(quantityStr, out quantity) && quantity >= 0;
            if (!result)
            {
                Console.WriteLine("Quantity must be positive or zero.");
            }
            return result;
        }

        public static bool IsAValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("ERROR: Name cannot be empty.");
                return false;
            }
            return true;
        }

        public static Book? QueryByName()
        {
            string? name = Console.ReadLine();
            if (!IsAValidName(name))
            {
                return null;
            }
            Book result = Cache.Instance.FindByNameInCache(name);
            if (result == null)
            {
                Console.WriteLine("Book not found in the system.");
            }
            return result;
        }


        public static bool ValidateRemove(out Book book)
        {
            Console.WriteLine("What is the name of the book to remove?");
            book = QueryByName();
            if (book == null)
            {
                Console.WriteLine("Book does not exist.");
                return false;
            }

            return true;
        }

        public static bool ValidateAddStock(out Book book, out int quantity)
        {
            quantity = 0;
            Console.WriteLine($"What is the name of the book you'd like to add stock to?");
            book = QueryByName();
            if (book == null)
            {
                return false;
            }

            Console.WriteLine("How much stock needs to be added?");
            if (!IsPositiveIntegerFromUserInput(out quantity))
            {
                return false;
            }
            return true;
        }


    }
}
