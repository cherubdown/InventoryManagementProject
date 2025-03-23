using InventoryManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public class ProductCache
    {
        
        private List<Product> ProductsCache { get; set; } = [];

        // implement a singleton product cache so that a second instance may never be made
        private static ProductCache? instance = null;
        private ProductCache()
        {
            ProductsCache = DataWriter.LoadDatabase();
        }
        public static ProductCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductCache();
                }
                return instance;
            }
        }

        public void Save()
        {
            DataWriter.SaveDatabase(ProductsCache);
        }

        public void ViewProducts()
        {
            foreach (Product product in ProductsCache)
            {
                WriteProductDetailsRowToConsole(product);
            }
        }

        public void AddProduct()
        {
            // basic name validation
            Console.WriteLine("Add Product: What is the name of the product?:");
            string? name = Console.ReadLine();
            if (!ValidateName(name))
            {
                return;
            }

            // basic price validation
            Console.WriteLine("Add Product: What is the price of the product?:");
            string? priceStr = Console.ReadLine();
            double price;
            if (!double.TryParse(priceStr, out price))
            {
                Console.WriteLine("Invalid price. Item not added.");
                return;
            }

            // basic stock validation
            Console.WriteLine("Add Product: How many do you have in stock?:");
            int stock;
            if (!CommonUtilities.IsPositiveIntegerFromUserInput(out stock))
            {
                return;
            }

            ProductsCache.Add(new Product(name, price, stock));
        }

        public void SellProduct()
        {
            Console.WriteLine("What is the name of the item you'd like to sell?");
            string? name = Console.ReadLine();
            if (!ValidateName(name) || !ValidateProductExists(name))
            {
                return;
            }

            Console.WriteLine("How many do you want to sell?:");
            int quantity;
            if (!CommonUtilities.IsPositiveIntegerFromUserInput(out quantity))
            {
                return;
            }

            Product? product = FindProductByName(name);
            // cannot have a negative quantity of stock
            if (quantity >= product.Stock)
            {
                Console.WriteLine($"Product '{product.Name}' has only {product.Stock} in stock. You cannot sell more than what is in stock.");
                return;
            }

            product.Stock -= quantity;
            Console.WriteLine($"Product sold. There {(product.Stock == 1 ? "is" : "are")} {product.Stock} now in stock.");
        }

        public void AddStock()
        {
            Console.WriteLine($"What is the name of the item you'd like to add stock to?");
            string? name = Console.ReadLine();
            if (!ValidateName(name) || !ValidateProductExists(name))
            {
                return;
            }

            Console.WriteLine("How much stock needs to be added?");
            int quantity;
            if (!CommonUtilities.IsPositiveIntegerFromUserInput(out quantity))
            {
                return;
            }

            Product? product = FindProductByName(name);
            product.Stock += quantity;
            Console.WriteLine($"Product stocked. There {(product.Stock == 1 ? "is" : "are")} {product.Stock} now in stock.");
        }

        private void WriteProductDetailsRowToConsole(Product product)
        {
            Console.WriteLine($"Product name: {product.Name}, price: {product.Price}, stock: {product.Stock}");
        }

        private Product? FindProductByName(string name)
        {
            return ProductsCache.Find(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        private bool ValidateName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("ERROR: Name cannot be empty.");
                return false;
            }
            return true;
        }

        private bool ValidateProductExists(string? name)
        {
            // find the product and return if it doesn't exist
            Product? product = FindProductByName(name);
            if (product == null)
            {
                Console.WriteLine($"Product '{name}' does not exist.");
                return false;
            }
            return true;
        }

        public void RemoveProduct()
        {
            Console.WriteLine("What is the name of the item you'd like to remove from the system?");
            string? name = Console.ReadLine();
            if (!ValidateName(name) || !ValidateProductExists(name))
            {
                return;
            }

            Product? product = FindProductByName(name);
            ProductsCache.Remove(product);
        }
    }
}
