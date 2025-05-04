using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    
    public class Book
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsCheckedOut { get; set; } = false;
        
        public Book(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }
    }
}
