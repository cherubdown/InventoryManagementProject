using InventoryManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public class InventoryManager
    {
        
        private List<ConcreteProduct> Products { get; set; } = [];


        public void Begin()
        {
            Products = DataWriter.LoadDatabase();
            Console.WriteLine("Welcome to the Inventory Management System. What do you want to do?");

            while (true)
            {

                Console.WriteLine(Environment.NewLine + "Options:" + Environment.NewLine);
                Console.WriteLine("1. View products");
                Console.WriteLine("2. Add new product");
                Console.WriteLine("3. Sell a product");
                Console.WriteLine("4. Restock a product");
                Console.WriteLine("5. Remove product from the inventory system");
                Console.WriteLine("6. Exit");

                ConsoleKeyInfo response = Console.ReadKey();
                Console.WriteLine(Environment.NewLine);

                switch (response.KeyChar)
                {
                    case '1':
                        ViewProducts();
                        continue;
                    case '2':
                        AddProductWithInputValidation();
                        break;
                    case '6':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input.");
                        break;
                }

                // except for View Products, every action needs to save the database for persistance
                DataWriter.SaveDatabase(Products);
            }
        }


        private void ViewProducts()
        {
            foreach(ConcreteProduct product in Products)
            {
                WriteProductDetailsRowToConsole(product);
            }
        }

        private void WriteProductDetailsRowToConsole(ConcreteProduct product)
        {
            Console.WriteLine($"Product name: {product.Name}, price: {product.Price}, stock: {product.Stock}");
        }

        private void AddProductWithInputValidation()
        {
            Console.WriteLine("Add Product: What is the name of the product?:");
            string? name = Console.ReadLine();
            if (name == null)
            {
                Console.WriteLine("Name cannot be empty. Item not added.");
                return;
            }

            Console.WriteLine("Add Product: What is the price of the product?:");
            string? priceStr = Console.ReadLine();
            double price;
            if (!double.TryParse(priceStr, out price))
            {
                Console.WriteLine("Invalid price. Item not added.");
                return;
            }

            Console.WriteLine("Add Product: How many do you have in stock?:");
            string? stockStr = Console.ReadLine();
            int stock;
            if (!int.TryParse(stockStr, out stock) && stock >= 0)
            {
                Console.WriteLine("Invalid stock price. It should represent a positive integer or zero. Item not added.");
                return;
            }

            AddProduct(new ConcreteProduct(name, price, stock));
        }

        private void AddProduct(ConcreteProduct product)
        {
            if (FindProductByName(product.Name) == null)
            {
                Products.Add(product);
                WriteProductDetailsRowToConsole(product);
            } 
            else
            {
                Console.WriteLine("ERROR: That product already exists.");
            }
        }

        private ConcreteProduct? FindProductByName(string name)
        {
            return Products.Find(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        private void SellProductByName(string name, int quantity)
        {
            // find the product and return if it doesn't exist
            ConcreteProduct? product = FindProductByName(name);
            if (product == null)
            {
                Console.WriteLine($"Product '{name}' does not exist.");
                Console.WriteLine($"Would you like to add it to inventory? Y/N");
                if (CommonUtilities.ReadUserKeyAndDetermineYesOrNo())
                {

                }
                return;
            }

            // cannot have a negative quantity of stock
            if (product.Stock > quantity)
            {
                
                Console.WriteLine($"Product '{name}' has only {product.Stock} in stock. You cannot sell more than what is in stock.");
                Console.WriteLine($"Would you like to sell all {product.Stock} instead? Y/N");

                // if user would like to sell the entire stock, call the function again with the known stock amount
                // otherwise kick out of process
                if (CommonUtilities.ReadUserKeyAndDetermineYesOrNo())
                {
                    SellProductByName(name, product.Stock);
                }
                else
                {
                    return;
                }
            }

        }
    }
}
