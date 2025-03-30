using InventoryManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        private void WriteProductDetailsRowToConsole(Product product)
        {
            Console.WriteLine($"Product name: {product.Name}, price: {product.Price}, stock: {product.Stock}");
        }

        public Product? FindProductByNameInCache(string name)
        {
            return ProductsCache.Find(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public void AddProduct()
        {
            Product product;
            if (!InputValidation.ValidateAddProduct(out product))
            {
                return;
            }
            ProductsCache.Add(product);
        }

        public void SellProduct()
        {
            Product product;
            int quantity;
            if (InputValidation.ValidateSellProduct(out product, out quantity))
            {
                product.Stock -= quantity;
                Console.WriteLine($"Product sold. There {(product.Stock == 1 ? "is" : "are")} {product.Stock} now in stock.");
            }

        }

        public void AddStock()
        {
            Product product;
            int quantity;
            if (InputValidation.ValidateAddStock(out product, out quantity))
            {
                product.Stock += quantity;
                Console.WriteLine($"Product stocked. There {(product.Stock == 1 ? "is" : "are")} {product.Stock} now in stock.");
            }
        }

        public void RemoveProduct()
        {
            Product product;
            if (InputValidation.ValidateRemoveProduct(out product))
            {
                ProductsCache.Remove(product);
            }
        }

    }
}
