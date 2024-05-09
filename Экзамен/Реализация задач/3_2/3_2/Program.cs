using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_2
{
    public enum Status
    {
        free,
        busy
    }

    class Location
    {
        public int lat { get; set; }
        public int lon { get; set; }
        public int speed { get; set; }

        public Location(int lat, int lon, int speed)
        {
            this.lat = lat;
            this.lon = lon;
            this.speed = speed;
        }

    }
  
    class Taxi
    {
        public string Number { get; set; }
        public Location Location { get; set; }
        public Status Status;

        public Taxi(string number, Location location, Status status)
        {
            this.Number = number;
            this.Location = location;
            this.Status = status;
        }

        public override string ToString()
        {
            return "Taxi number: " + Number + " Location: " + Location.lat + " " + Location.lon + " Speed: " + Location.speed + " Status: " + Status;
        }

    }

    class Park<T>
    {
        public List<T> list = new List<T>();

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }

        public void ClearPark()
        {
            list.Clear();
        }

        public T Find(Func<T, bool> predicate)
        {
            return list.FirstOrDefault(predicate);
        }

        public override string ToString()
        {
            string str = "";
            foreach (T item in list)
            {
                str += item + "\n";
            }
            return str;

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Park<Taxi> uber = new Park<Taxi>();

            uber.Add(new Taxi("A123AA", new Location(10, 10, 10), Status.free));
            uber.Add(new Taxi("B123BB", new Location(20, 20, 20), Status.free));
            uber.Add(new Taxi("C123CC", new Location(30, 30, 30), Status.free));
            uber.Add(new Taxi("D123DD", new Location(40, 40, 40), Status.free));

            Console.WriteLine("Список такси:");
            Console.WriteLine(uber.ToString());

            Console.WriteLine("Введите вашу текущую координату (широта):");
            int lat = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите вашу текущую координату (долгота):");
            int lon = Convert.ToInt32(Console.ReadLine());

            uber.list.Sort((x, y) => CalculateDistance(x.Location.lat, x.Location.lon, lat, lon).CompareTo(CalculateDistance(y.Location.lat, y.Location.lon, lat, lon)));
            Console.WriteLine(uber.ToString());

            var taxi = uber.list.OrderBy(x => CalculateDistance(x.Location.lat, x.Location.lon, lat, lon)).First();
            Console.WriteLine("Ближайшее такси:");
            Console.WriteLine(taxi);

            using (StreamWriter fs = new StreamWriter("txt.txt"))
            {
                fs.WriteLine(taxi);
            };

            static double CalculateDistance(int lat1, int long1, int lat2, int long2)
            {
                return Math.Sqrt(Math.Pow(lat2 - lat1, 2) + Math.Pow(long2 - long1, 2));
            }
        }
    }
}