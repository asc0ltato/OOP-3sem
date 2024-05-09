using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project1.Train;

namespace Project1
{
    public abstract class Transsred
    {
        public abstract string Name { get; set; }
        public abstract double Speed { get; set; }

        public string Benz;
        public int Year;
        public abstract void Vichle();
    }

    public class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Time() { }
        public Time(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
            {
                throw new ArgumentOutOfRangeException("Error");
            }

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }


        public static bool operator >(Time t1, Time t2)
        {
            if (t1.Hours > t2.Hours)
            {
                return true;
            }
            else if (t1.Hours == t2.Hours)
            {
                if (t1.Minutes > t2.Minutes)
                {
                    return true;
                }
                else if (t1.Minutes == t2.Minutes)
                {
                    return t1.Seconds > t2.Seconds;
                }
            }
            return false;
        }
        public static bool operator <(Time t1, Time t2)
        {
            return !(t1 > t2) && !(t1.Equals(t2));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Time other = (Time)obj;
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void Print()
        {
            Console.WriteLine($"{Hours:D2}:{Minutes:D2}:{Seconds:D2}");
        }
    }

    public class Train : Transsred
    {
        public double Number;
        public int Vagon;
        public float Passanger;

        public Train(double number, int vagon, float passanger)
        {
            Number = number;
            Vagon = vagon;
            Passanger = passanger;
        }

        public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override double Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Vichle()
        {
            Console.WriteLine("train is go");
        }

        public class Car : Transsred
        {

            public string Brand { get; set; }
            public string Model { get; set; }
            public override double Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override void Vichle()
            {
                Console.WriteLine("Car is go");
            }
            public Car(string brand, string model, int year)
            {
                Brand = brand;
                Model = model;
                Year = year;
            }
        }

    }

    class Program
    {
        public static void Main(string[] args)
        {
            Time time = new Time(16, 18, 03);
            time.Print();


            Car car1 = new Car("Toyota", "Corolla", 2019);
            car1.Vichle();
            Train train = new Train(2003_9, 6, 200);
            train.Vichle();


            List<Car> cars = new List<Car>
            {
                new Car("Toyota", "Coroll", 2002),
                new Car("Ford", "Mustung", 2021),
                new Car("BMW", "X5", 2004),
                new Car("Audi", "A4", 2005)
            };

            foreach (var car in cars)
            {
                Console.WriteLine($"Brand: {car.Brand}, Model: {car.Model}, Year: {car.Year}");
            }

            List<Car> sortedCars = cars.OrderBy(car => car.Brand).ToList();


            Console.WriteLine("\nSorts:");
            foreach (var car in sortedCars)
            {
                Console.WriteLine($"Brand: {car.Brand}, Model: {car.Model}");
            }


        }
    }
}