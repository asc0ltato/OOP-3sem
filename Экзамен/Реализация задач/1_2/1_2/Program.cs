namespace _1_2
{
    abstract class Transport
    {
        public string Name { get; set; }
        public Transport (string name) 
        {
            this.Name = name;
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

    public enum Status_enum
    {
        Fly,
        Ready,
        Stop
    }

    class Air : Transport, IAirable, IAir
    {
        public int CountOfPassengers { get; set; }
        public int Speed { get; set; }
        public Status_enum Status { get; set; }

        public Air(string name) : base(name) { }

       public void Check()
        {
            if (CountOfPassengers == 0 && Speed == 0)
            {
                Status = Status_enum.Stop;
            } 
            else if (CountOfPassengers > 0 && Speed == 0)
            {
                Status = Status_enum.Ready;
            } 
            else if (Status == Status_enum.Ready && CountOfPassengers > 0 && Speed > 0)
            {
                Status = Status_enum.Fly;
            }
        }

        void IAir.Check()
        {
            if (CountOfPassengers > 20 && CountOfPassengers < 100)
            {
                Console.WriteLine("Ready");
            }
        }

        public void Fly()
        {
            if (Status == Status_enum.Fly)
            {
                Console.WriteLine("Flying");
            }
            else
            {
                throw new Exception("Cannot fly. Check the status.");
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter sw = new StreamWriter("text.txt"))
            {
                Air air = new Air("Airplane");
                air.CountOfPassengers = 10;
                air.Speed = 0;
                air.Check();
                ((IAir)air).Check();
                sw.WriteLine("Status: " + air.Status);
                Console.WriteLine("Status: " + air.Status);

                air.Speed = 100;
                air.Check();
                sw.WriteLine("Status: " + air.Status);
                Console.WriteLine("Status: " + air.Status);

                air.Fly();

                List<Air> airList = new List<Air>();
                airList?.Add(new Air("Airplane1"));
                airList?.Add(new Air("Airplane2"));
                airList?.Add(new Air("Airplane3"));
                airList?.Add(new Air("Airplane4"));
                airList?.Add(new Air("Airplane5"));

                airList[0].CountOfPassengers = 10;
                airList[0].Speed = 200;
                airList[0].Check();

                airList[1].CountOfPassengers = 10;
                airList[1].Speed = 0;
                airList[1].Check();

                airList[2].CountOfPassengers = 0;
                airList[2].Speed = 0;
                airList[2].Check();

                airList[3].CountOfPassengers = 30;
                airList[3].Speed = 300;
                airList[3].Check();

                airList[4].CountOfPassengers = 25;
                airList[4].Speed = 250;
                airList[4].Check();

                var flyAir = airList.Where(x => x.Status == Status_enum.Fly);
                sw.WriteLine("Fly = " + flyAir.Count());
                Console.WriteLine("Fly = " + flyAir.Count());

                var averageSpeed = airList.Where(x => x.Status == Status_enum.Fly).Average(x => x.Speed);
                sw.WriteLine("Average speed = " + averageSpeed);
                Console.WriteLine("Average speed = " + averageSpeed);
            }
             
        }
    }
}
