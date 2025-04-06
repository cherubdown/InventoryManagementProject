using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    
    public class Product
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
    }
}
