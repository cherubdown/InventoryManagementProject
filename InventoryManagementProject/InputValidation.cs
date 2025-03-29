using InventoryManagementProject.Models;
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

        private static bool IsValidProductName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("ERROR: Name cannot be empty.");
                return false;
            }
            return true;
        }

        private static Product? FindProductByName(List<Product> products)
        {
            string? name = Console.ReadLine();
            if (!IsValidProductName(name))
            {
                return null;
            }
            Product result = products.Find(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
            if (result == null)
            {
                Console.WriteLine("Product not found in the inventory system.");
            }
            return result;
        }


        public static bool ValidateAddProduct(out Product product, List<Product> products)
        {
            product = null;
            Console.WriteLine("What is the name of the product you wish to add?");
            string? name = Console.ReadLine();
            if (!IsValidProductName(name))
            {
                return false;
            }
            product = products.Find(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
            if (product != null)
            {
                Console.WriteLine("Product is already in the system. You cannot add it again.");
                return false;
            }

            // basic price validation
            Console.WriteLine("Add Product: What is the price of the product?:");
            string? priceStr = Console.ReadLine();
            double price;
            if (!double.TryParse(priceStr, out price))
            {
                Console.WriteLine("Invalid price. Item not added.");
                return false;
            }

            // basic stock validation
            Console.WriteLine("Add Product: How many do you have in stock?:");
            int stock;
            if (!IsPositiveIntegerFromUserInput(out stock))
            {
                return false;
            }
            product = new Product(name, price, stock);
            return true;
        }

        public static bool ValidateSellProduct(out Product product, out int quantity, List<Product> products)
        {
            quantity = 0;
            Console.WriteLine("What is the name of the item you'd like to sell?");
            product = FindProductByName(products);
            if (product == null)
            {
                return false;
            }

            Console.WriteLine("How many do you want to sell?:");
            if (!IsPositiveIntegerFromUserInput(out quantity))
            {
                return false;
            }

            // cannot have a negative quantity of stock
            if (quantity >= product.Stock)
            {
                Console.WriteLine($"Product '{product.Name}' has only {product.Stock} in stock. You cannot sell more than what is in stock.");
                return false;
            }

            return true;
        }

        public static bool ValidateAddStock(out Product product, out int quantity, List<Product> products)
        {
            quantity = 0;
            Console.WriteLine($"What is the name of the item you'd like to add stock to?");
            product = FindProductByName(products);
            if (product == null)
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

        public static bool ValidateRemoveProduct(out Product product, List<Product> products)
        {
            Console.WriteLine("What is the name of the item you'd like to remove from the system?");
            product = FindProductByName(products);
            if (product == null)
            {
                return false;
            }

            return true;
        }

    }
}
