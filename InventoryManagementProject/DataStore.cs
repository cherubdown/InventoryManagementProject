using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public class DataStore
    {
        /// <summary>
        /// todo: use config file
        /// </summary>
        private static readonly string DATA_FILE_NAME = "library.json";

        public static List<Book> LoadDatabase()
        {
            try
            {
                if (File.Exists(DATA_FILE_NAME))
                {
                    string jsonString = File.ReadAllText(DATA_FILE_NAME);
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        var deserializedResult = JsonSerializer.Deserialize<List<Book>>(jsonString);
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
            return new List<Book>();
        }

        public static void SaveDatabase(List<Book> book)
        {
            File.WriteAllText(DATA_FILE_NAME, JsonSerializer.Serialize(book));
        }
    }
}
