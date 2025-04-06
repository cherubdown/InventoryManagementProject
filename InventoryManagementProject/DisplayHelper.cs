using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public static class DisplayHelper
    {
        private static readonly string TABLE_OUTPUT_FORMAT = "|{0,36}|{1,30}|{2,8}|{3,5}|";

        public static void DisplayProducts(IEnumerable<Product> ProductsCache)
        {
            if (!ProductsCache.Any())
            {
                Console.WriteLine("There are no Items in the system.");
                return;
            }
            Console.WriteLine(TABLE_OUTPUT_FORMAT, "ID", "Name", "Price", "Stock");
            foreach (Product product in ProductsCache)
            {
                WriteProductDetailsRowToConsole(product);
            }
        }
        
        private static void WriteProductDetailsRowToConsole(Product product)
        {

            Console.WriteLine(TABLE_OUTPUT_FORMAT, product.ID, product.Name, product.Price, product.Stock);
            //Console.WriteLine($"Product name: {product.Name}, price: {product.Price.ToString("C")}, stock: {product.Stock}");
        }
    }
}
