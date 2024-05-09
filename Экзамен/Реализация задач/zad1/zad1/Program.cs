using System.Xml;
using Newtonsoft.Json;

namespace Ekzamen
{
    public class Circle : IComparable
    {
        public int Radius { get; set; }
        public string Color { get; set; }

        public Circle(int radius, string color)
        {
            this.Radius = radius;
            this.Color = color;
        }

        public double Area(int radius)
        {
            double area = Math.Pow(radius, 2) * Math.PI;
            return area;
        }

        public override string ToString()
        {
            return $"Круг с радиусом {Radius} и цветом {Color}";
        }

        public bool Equals(Circle circle)
        {
            if (circle == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return 1;
            }

            Circle other = (Circle)obj;
            return Radius.CompareTo(other.Radius);
        }

        public int CompareTo(Circle circle)
        {
            return Radius.CompareTo(circle.Radius);
        }
    }

    public class NTDate : NDate
    {
        public int Year
        {
            get { return base.Year; }
            set { base.Year = value; }
        }

        public NTDate(int day, int month, int year) : base(day, month)
        {
            Year = year;
        }

        public override void NextDay()
        {
            Day++;
            if (Day == 1)
            {
                Month++;
                if (Month == 1)
                {
                    Year++;
                }
            }
        }

        public static bool operator >(NTDate date1, NTDate date2)
        {
            if (date1.Year > date2.Year)
            {
                return true;
            }
            else if (date1.Year == date2.Year)
            {
                if (date1.Month > date2.Month)
                {
                    return true;
                }
                else if (date1.Month == date2.Month)
                {
                    if (date1.Day > date2.Day)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool operator <(NTDate date1, NTDate date2)
        {
            return !(date1 > date2);
        }
    }

    public class NDate
    {
        private int day;
        private int month;

        public int Day
        {
            get { return this.day; }
            set
            {
                if (IsValidDay(value))
                {
                    this.day = value;
                }
                else
                {
                    Console.WriteLine("Не правильный день (1-31)");
                }
            }
        }

        private bool IsValidDay(int day)
        {
            if (day >= 1 && day <= 31)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Month
        {
            get { return this.month; }
            set
            {
                if (IsValidMonth(value))
                {
                    this.month = value;
                }
                else
                {
                    Console.WriteLine("Не правильный месяц (1-12)");
                }
            }
        }

        public int Year { get; set; }

        public NDate(int day, int month)
        {
            this.day = day;
            this.month = month;
        }

        private NDate()
        {
        }

        public virtual void NextDay()
        {
            day++;
            if (day > 31)
            {
                day = 1;
                month++;
                if (month > 12)
                {
                    month = 1;
                }
            }
        }

        private bool IsValidMonth(int month)
        {
            if (month >= 1 && month <= 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static class Program
    {
        static void Main()
        {
            List<Circle> circles = new List<Circle>()
            {
                new Circle(2, "red"),
                new Circle(3, "green"),
                new Circle(3, "yellow"),
                new Circle(4, "purple"),
                new Circle(5, "green"),
                new Circle(6, "red")
            };
            var sortedCircles = circles
                .Skip(3)
                .OrderBy(c => c.Color)
                .GroupBy(c => c.Color);

            foreach (var group in sortedCircles)
            {
                Console.WriteLine("Цвет: " + group.Key);
                foreach (var circle in group)
                {
                    Console.WriteLine("Радиус: " + circle.Radius);
                }
                Console.WriteLine();
            }

            /*XmlSerializer serializer = new XmlSerializer(typeof(List<Circle>));

            using (StreamWriter writer = new StreamWriter("circles.xml"))
            {
                serializer.Serialize(writer, sortedCircles);
            }*/

            string json = JsonConvert.SerializeObject(sortedCircles);
            Console.WriteLine(json);
            File.WriteAllText("rectangle.json", json);


            NDate date = new NDate(15, 10);
            Console.WriteLine("День: " + date.Day);
            Console.WriteLine("Месяц: " + date.Month);
            date.NextDay();
            Console.WriteLine("Следующий день: " + date.Day + "." + date.Month);

            NTDate date1 = new NTDate(15, 10, 2023);
            NTDate date2 = new NTDate(20, 10, 2023);

            Console.WriteLine("Дата 1: " + date1.Day + "." + date1.Month + "." + date1.Year);
            Console.WriteLine("Дата 2: " + date2.Day + "." + date2.Month + "." + date2.Year);

            if (date1 > date2)
            {
                Console.WriteLine("Дата 1 позже Дата 2");
            }
            else if (date1 < date2)
            {
                Console.WriteLine("Дата 1 раньше Дата 2");
            }
            else
            {
                Console.WriteLine("Дата 1 и Дата 2 одинаковы");
            }
        }
    }
}

