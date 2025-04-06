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

        // implement a singleton product cache so that a second instance may never be created
        private static ProductCache? instance = null;
        private ProductCache()
        {
            ProductsCache = DataStore.LoadDatabase();
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
            DataStore.SaveDatabase(ProductsCache);
        }

        public void DisplayProducts()
        {
            DisplayHelper.DisplayProducts(ProductsCache);
        }

        public void DisplaySingleProduct()
        {
            DisplayHelper.DisplaySingleProduct();
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
