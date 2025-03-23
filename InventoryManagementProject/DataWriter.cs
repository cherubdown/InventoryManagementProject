using InventoryManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public class DataWriter
    {
        /// <summary>
        /// todo: use config file
        /// </summary>
        private static readonly string DATA_FILE_NAME = "inventory.json";

        public static List<Product> LoadDatabase()
        {
            try
            {
                if (File.Exists(DATA_FILE_NAME))
                {
                    string jsonString = File.ReadAllText(DATA_FILE_NAME);
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        var deserializedResult = JsonSerializer.Deserialize<List<Product>>(jsonString);
                        if (deserializedResult != null)
                        {
                            return deserializedResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred loading database.");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Environment.Exit(0);
            }
            return new List<Product>();
        }

        public static void SaveDatabase(List<Product> products)
        {
            File.WriteAllText(DATA_FILE_NAME, JsonSerializer.Serialize(products));
        }
    }
}
