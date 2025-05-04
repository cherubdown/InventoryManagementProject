using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryManagementProject
{
    public class Cache
    {
        public const int MAXCHECKEDOUT = 3;
        public const int MAXBOOKS = 5;

        private List<Book> _cache { get; set; } = [];

        // implement a singleton cache so that a second instance may never be created
        private static Cache? instance = null;
        private Cache()
        {
            _cache = DataStore.LoadDatabase();
        }

        public static Cache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Cache();
                }
                return instance;
            }
        }

        public int Count()
        {
            return _cache.Count();
        }

        public int CheckedOutBooks()
        {
            return _cache.Where(b => b.IsCheckedOut).Count();
        }

        public void Save()
        {
            DataStore.SaveDatabase(_cache);
        }

        public void Display()
        {
            DisplayHelper.DisplayAll(_cache);
        }

        public void DisplaySingle()
        {
            DisplayHelper.DisplaySingle();
        }

        public Book? FindByNameInCache(string name)
        {
            return _cache.Find(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public void CheckOut()
        {
            Book? book = null;
            Console.WriteLine("What is the name of the book you wish check out?");
            string? name = Console.ReadLine();
            if (!InputValidation.IsAValidName(name ?? ""))
            {
                return;
            }

            book = Instance.FindByNameInCache(name ?? "");
            if (book == null)
            {
                Console.WriteLine($"Book {name} isn't in the library.");
                return;
            }

            if (book.IsCheckedOut)
            {
                Console.WriteLine("I'm sorry. That book is already checked out.");
                return;
            }

            if (Instance.CheckedOutBooks() >= MAXCHECKEDOUT)
            {
                Console.WriteLine($"I'm sorry. The maximum books allowed for checkout is {MAXCHECKEDOUT}.");
                return;
            }
            else
            {
                book.IsCheckedOut = true;
                Console.WriteLine($"You just checked out {book.Name}.");
                return;
            }
        }

        public void CheckIn()
        {
            Book? book = null;
            Console.WriteLine("What is the name of the book you wish check in?");
            string? name = Console.ReadLine();
            if (!InputValidation.IsAValidName(name ?? ""))
            {
                return;
            }

            book = Instance.FindByNameInCache(name ?? "");
            if (book == null)
            {
                Console.WriteLine($"Book {name} isn't in the library.");
                return;
            }

            if (!book.IsCheckedOut)
            {
                Console.WriteLine("That book is already checked in.");
                return;
            }
            book.IsCheckedOut = false;
        }

        public void Add()
        {
            Book book = null;
            Console.WriteLine("What is the name of the book you wish to add?");
            string? name = Console.ReadLine();
            if (!InputValidation.IsAValidName(name))
            {
                return;
            }
            
            book = Instance.FindByNameInCache(name);
            if (book != null)
            {
                Console.WriteLine("Book is already added.");
                return;
            }

            if (_cache.Count() >= MAXBOOKS)
            {
                Console.WriteLine($"You cannot add more than {MAXBOOKS} to this library.");
                return;
            }

            book = new Book(name);
            _cache.Add(book);
        }

        public void Remove()
        {
            Book book;
            if (InputValidation.ValidateRemove(out book))
            {
                _cache.Remove(book);
            }
        }

    }
}
