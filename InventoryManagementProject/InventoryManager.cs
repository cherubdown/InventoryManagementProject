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

            while(true)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Welcome to the Inventory Management System. What do you want to do?");
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
                WriteProductToConsole(product);
            }
        }

        private void WriteProductToConsole(ConcreteProduct product)
        {
            Console.WriteLine($"Product name: {product.Name}, price: {product.Price}, stock: {product.Stock}");
        }

        

        private void AddProductWithInputValidation()
        {
            Console.WriteLine("What is the name of the product?:");
            string? name = Console.ReadLine();
            if (name == null)
            {
                Console.WriteLine("Name cannot be empty. Item not added.");
                return;
            }

            Console.WriteLine("What is the price of the product?:");
            string? priceStr = Console.ReadLine();
            double price;
            if (!double.TryParse(priceStr, out price))
            {
                Console.WriteLine("Invalid price. Item not added.");
                return;
            }

            Console.WriteLine("How many do you have in stock?:");
            string? stockStr = Console.ReadLine();
            int stock;
            if (!int.TryParse(stockStr, out stock) && stock >= 0)
            {
                Console.WriteLine("Invalid stock price. It should represent a positive integer or zero. Item not added.");
                return;
            }
            AddProduct(new ConcreteProduct(name, price, stock));
        }

        public void AddProduct(ConcreteProduct product)
        {
            if (FindProductByName(product.Name) == null)
            {
                Products.Add(product);
                WriteProductToConsole(product);
            } 
            else
            {
                Console.WriteLine("That product already exists.");
            }
        }

        public ConcreteProduct? FindProductByName(string name)
        {
            return Products.Find(p => p.Name == name);
        }

        public void SellProductByName(string name, int quantity)
        {
            ConcreteProduct? product = FindProductByName(name);

        }
    }
}
