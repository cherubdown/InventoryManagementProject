using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject.Models
{
    
    public class Product : IProduct
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        
        public Product(string name, double price, int stock)
        {
            ID = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = stock;
        }

        public bool IsValidProductUpdate(int quantity)
        {
            return quantity <= Stock;
        }
    }
}
