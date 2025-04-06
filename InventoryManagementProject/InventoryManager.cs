using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryManagementProject
{
    public class InventoryManager
    {
        private ProductCache ProductsCache { get; set; } = ProductCache.Instance;

        public void Start()
        {
            Console.WriteLine("Welcome to the Inventory Management System. What do you want to do?");

            while (true)
            {

                Console.WriteLine(Environment.NewLine + "Options:" + Environment.NewLine);
                Console.WriteLine("1. View all products");
                Console.WriteLine("2. Sell a product");
                Console.WriteLine("3. Add stock to a product");
                Console.WriteLine("4. Add new product to the inventory system");
                Console.WriteLine("5. Remove product from the inventory system");
                Console.WriteLine("6. Exit");

                ConsoleKeyInfo response = Console.ReadKey();
                Console.WriteLine(Environment.NewLine);

                switch (response.KeyChar)
                {
                    case '1':
                        ProductsCache.DisplayProducts();
                        continue;
                    case '2':
                        ProductsCache.SellProduct();
                        break;
                    case '3':
                        ProductsCache.AddStock();
                        break;
                    case '4':
                        ProductsCache.AddProduct();
                        break;
                    case '5':
                        ProductsCache.RemoveProduct();
                        break;
                    case '6':
                        Environment.Exit(0);
                        continue;
                    default:
                        Console.WriteLine("Invalid Input.");
                        continue;
                }

                // except for View Products, every action needs to save the database for persistance
                ProductsCache.Save();
            }
        }



    }
}
