
    public abstract class Transport
    {
        public abstract string Name { get; set; }
        public abstract double Speed { get; set; }
        public string Benz;
        public int Year;
        public abstract void PrintTransport();
    }

    public class STime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public STime() { }
        public STime(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
            {
                throw new Exception("Некорректный ввод врмени. Попробуйте еще раз");
            }

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }


        public static bool operator >(STime t1, STime t2)
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
        public static bool operator <(STime t1, STime t2)
        {
            return !(t1 < t2) && !(t1.Equals(t2));
        }


        public override bool Equals (object obj)
        {
            if(obj == null || GetType() == obj.GetType())
                return false;
            STime other = (STime)obj;
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

    public class Train : Transport
    {
        public int Number;
        public int Vagon;
        public int Passanger;

        public Train(int number, int vagon, int passanger)
        {
            this.Number = number;
            this.Vagon = vagon;
            this.Passanger = passanger;
        }
        public override double Speed { get => throw new Exception(); set => throw new Exception(); }
        public override string Name { get => throw new Exception(); set => throw new Exception(); }

        public override void PrintTransport()
        {
            Console.WriteLine("Поезд поехал");
        }
    }

    public class Car : Transport
    {

        public string Brand { get; set; }
        public string Model { get; set; }
        public override double Speed { get => throw new Exception(); set => throw new Exception(); }
        public override string Name { get => throw new Exception(); set => throw new Exception(); }

        public override void PrintTransport()
        {
            Console.WriteLine("Машина поехала.");
        }

        public Car(string brand, string model, int year)
        {
            Brand = brand;
            Model = model;
            Year = year;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
        using (StreamWriter sw = new StreamWriter("txt.txt"))
        {
            STime time = new STime(16, 18, 03);
            time.Print();

            Car car1 = new Car("Тайота", "Модель1", 2013);
            car1.PrintTransport();
            Train train = new Train(13, 12, 100);
            train.PrintTransport();

            List<Car> cars = new List<Car>
            {
                new Car("Тайота", "Модель1", 2013),
                new Car("Мерседес", "Модель2", 2014),
                new Car("Бмв", "Модель3", 2005),
                new Car("Пижо", "Модель4", 2016)
            };

            foreach (var car in cars)
            {
                Console.WriteLine($"Марка: {car.Brand}, Модель: {car.Model}, Год выпуска: {car.Year}");
            }

            List<Car> sortedCars = cars.Where(car => car.Brand == "Тайота").ToList();
            Console.WriteLine("\nСортировка по определенному бренду:");
            foreach (var car in sortedCars)
            {
                Console.WriteLine($"Brand: {car.Brand}, Model: {car.Model}");
            }
        }
        }
    }
