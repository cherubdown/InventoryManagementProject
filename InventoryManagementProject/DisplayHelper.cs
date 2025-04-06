using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public static class DisplayHelper
    {
        private static readonly string TABLE_OUTPUT_FORMAT = "|{0,36}|{1,50}|{2,8}|{3,5}|";

        public static void DisplayProducts(IEnumerable<Product> ProductsCache)
        {
            if (!ProductsCache.Any())
            {
                Console.WriteLine("There are no Items in the system.");
                return;
            }
            DisplayTableHeader();
            foreach (Product product in ProductsCache)
            {
                WriteProductDetailsRowToConsole(product);
            }
            Console.WriteLine($"Products: {ProductsCache.Count()}");
        }

        public static void DisplaySingleProduct()
        {
            Console.WriteLine("Which product do you want to view?");
            Product? product = InputValidation.QueryByProductName();
            if (product != null)
            {
                DisplayTableHeader();
                WriteProductDetailsRowToConsole(product);
            }
        }

        private static void DisplayTableHeader()
        {
            Console.WriteLine(TABLE_OUTPUT_FORMAT, "ID", "Name", "Price", "Stock");
        }
        
        private static void WriteProductDetailsRowToConsole(Product product)
        {

            Console.WriteLine(TABLE_OUTPUT_FORMAT, product.ID, product.Name, product.Price, product.Stock);
            //Console.WriteLine($"Product name: {product.Name}, price: {product.Price.ToString("C")}, stock: {product.Stock}");
        }
    }
}
