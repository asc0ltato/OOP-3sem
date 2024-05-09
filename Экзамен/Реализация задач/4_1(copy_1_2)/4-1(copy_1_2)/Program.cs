using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_2
{
    abstract class Transport
    {
        public string name;
        public Transport(string name)
        {
            this.name = name;
        }
    }

    public interface IAirable
    {
        void Check();
        void Fly();
    }

    public interface IAir
    {
        void Check();
    }

    class Air : Transport, IAirable, IAir
    {
        private int countOfPassengers = 0;
        private int speed = 0;
        private string status = null;
        public Air(string name) : base(name)
        {
        }

        public int CountOfPassengers
        {
            get { return countOfPassengers; }
            set { countOfPassengers = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        enum Status_enum
        {
            Fly,
            Ready,
            Stop
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        void IAirable.Check()
        {
            if (countOfPassengers == 0 && speed == 0)
            {
                status = Status_enum.Stop.ToString();
            }

            if (countOfPassengers > 0 && speed == 0)
            {
                status = Status_enum.Ready.ToString();
            }

            if (countOfPassengers > 0 && speed > 0)
            {
                status = Status_enum.Fly.ToString();
            }
        }

        void IAir.Check()
        {
            if (countOfPassengers > 20 && countOfPassengers < 100)
            {
                Console.WriteLine("Ready");
            }
        }

        public void Fly()
        {
            if (status == Status_enum.Fly.ToString())
            {
                Console.WriteLine("Airplane is flying");
            }
            else
            {
                throw new Exception("Airplane is not flying");
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            string text = "";
            Air air = new Air("Airplane");
            air.CountOfPassengers = 10;
            air.Speed = 0;

            ((IAirable)air).Check();
            //((IAir)air).Check();
            text += air.Status + "\n";
            Console.WriteLine(air.Status);

            air.Speed = 100;
            ((IAirable)air).Check();
            text += air.Status + "\n";
            ((IAirable)air).Fly();
            File.WriteAllText(@"text.txt", text);

            // Создать коллекцию из Air и добавить 5 объектов.
            // С помощью linq запросов вывести количество самолетов, находящихся в Status = fly, а так же посчитать среднюю их скорость.

            List<Air> airList = new List<Air>();
            airList.Add(new Air("Airplane1"));
            airList.Add(new Air("Airplane2"));
            airList.Add(new Air("Airplane3"));

            airList[0].CountOfPassengers = 10;
            airList[0].Speed = 200;
            ((IAirable)airList[0]).Check();

            airList[1].CountOfPassengers = 10;
            airList[1].Speed = 400;
            ((IAirable)airList[1]).Check();

            airList[2].CountOfPassengers = 10;
            airList[2].Speed = 0;
            ((IAirable)airList[2]).Check();

            var flyAir = airList.Where(x => x.Status == "Fly");
            Console.WriteLine("Fly = " + flyAir.Count());

            var averageSpeed = airList.Where(x => x.Status == "Fly").Average(x => x.Speed);
            Console.WriteLine("Average speed = " + averageSpeed);
        }
    }
}
