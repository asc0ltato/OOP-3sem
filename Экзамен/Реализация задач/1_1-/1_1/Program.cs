using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace _1_1
{
    interface Figure
    {
         void Print();
    }

    [DataContract]
    class Rectangle : Figure
    {
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }
        [DataMember]
        public int H { get; set; }
        [DataMember]
        public int L { get; set; }
        [DataMember]
        public string Color { get; set; }

        public Rectangle()
        {
            X = 0;
            Y = 0;
            H = 0;
            L = 0;
            Color = "Белый";
        }

        public Rectangle(int x, int y, string color) : this()
        {
            X = x;
            Y = y;
            Color = color;
        }
        public Rectangle(int x, int y, int l, int h, string color) : this(x, y, color) 
        {

            L = l;
            H = h;

        }
        public static Rectangle operator +(Rectangle A, int i)
        {
            A.H += i;
            A.L += i;
            return A;
        }

        public int Sqr()
        {
            return H * L;
        }

        public virtual void Print()
        {
            Console.WriteLine($"Координаты: {X}, {Y}, длина: {L}, высота: {H}, цвет: {Color}");
        }
        public override string ToString()
        {
            return X + " " + Y + " " + L + " " + H + " " + Color;
        }
    }

    [DataContract]
    class SerializableList
    {
        [DataMember]
        public List<Rectangle> Rectangles { get; set; }

        public SerializableList(List<Rectangle> rectangles)
        {
            Rectangles = rectangles;
        }
    }
    [DataContract]
    class Program
    {
        static void Main(string[] args)
        {
            List<Rectangle> listochek = new List<Rectangle>();
            Rectangle rec1 = new Rectangle(22, 24, 22, 25, "Синий");
            Rectangle rec2 = new Rectangle(12, 14, 12, 15, "Красный");
            Rectangle rec3 = new Rectangle(12, 14, 12, 15, "Черный");
            Rectangle rec4 = new Rectangle(12, 14, 1, 1, "Белый");
            Rectangle rec5 = new Rectangle(12, 14, 12, 15, "Оранжевый");
            Rectangle rec6 = new Rectangle(12, 14, 12, 15, "Розовый");
            listochek.Add(rec1);
            listochek.Add(rec2);
            listochek.Add(rec3);
            listochek.Add(rec4);
            listochek.Add(rec5);
            listochek.Add(rec6);

            rec2 += 20;
            rec1.Print();

            var sortedRectangles = listochek.OrderBy(r => r.X).ThenBy(r => r.Y).ThenBy(r => r.Sqr()).ToList();
            sortedRectangles.First().Print();
            sortedRectangles.Last().Print();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(SerializableList));
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                SerializableList serializableList = new SerializableList(listochek);
                jsonFormatter.WriteObject(fs, serializableList);
            }
        }
    }
}
