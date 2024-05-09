using System.Collections;

namespace _8_1
{
    public interface IEnumerable
    {
        IEnumerator GetEnumerator();
    }
    public class Item
    {
        public string name { get; set; }
        public int ID { get; set; }
        public double price { get; set; }

        public Item(string name, int ID, int price)
        {
            this.name = name;
            this.ID = ID;
            this.price = price;
        }
        public override string ToString()
        {
            return $"Name: {name}, ID: {ID}, Price: {price}";
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void OnSale()
        {
            price *= 0.5;
            Console.WriteLine($"Распродажа сейчас");
        }
    }
    public class Manager
    {
        public event _Sale sale;

        public void Sale()
        {
            Console.WriteLine("Вызван метод Sale()");
            sale?.Invoke();
        }
    }
    public delegate void _Sale();

    public class Shop : IEnumerable
    {
        Queue<Item> queue = new Queue<Item>();

        public void Add(Item item)
        {
            queue.Enqueue(item);
        }

        public void Remove()
        {
            queue.Dequeue();
        }
        public void Delete()
        {
            queue.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return queue.GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Item item1 = new Item("shirt", 1236, 2000);
            Item item2 = new Item("dress", 3466, 1500);
            Item item3 = new Item("boots", 4578, 3000);
            Item item4 = new Item("shirt", 145, 3000);
            Item item5 = new Item("shirt", 126, 5000);

            Queue<Item> queue = new Queue<Item>();
            queue.Enqueue(item1);
            queue.Enqueue(item2);
            queue.Enqueue(item3);
            queue.Enqueue(item4);
            queue.Enqueue(item5);

            Console.WriteLine("Items in the queue:");
            foreach (Item item in queue)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(item1.ToString());
            Console.WriteLine(item2.GetHashCode());
            
            Manager manager = new Manager();
            manager.sale += item1.OnSale;
            manager.sale += item3.OnSale;
            manager.Sale();
            foreach (Item a in queue)
                Console.WriteLine(a);
            Console.WriteLine();

            var query = queue.Where(x => x.name == "shirt").Sum(x => x.price);
            Console.WriteLine("Sum of prices: " + query);
        }
    }
}
