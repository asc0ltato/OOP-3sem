namespace examen
{
    public class Card
    {
        public int number { get; }
        public int month;
        public int year { get; }
        public int balance
        { get; set; }


        public Card(int number, int month, int year, int balance)
        {
            this.balance = balance;
            this.number = number;
            this.month = month;
            this.year = year;
        }

        public static Card operator +(Card card, int x)
        {
            card.balance += x;
            return card;
        }
        public static Card operator -(Card card, int x)
        {
            card.balance -= x;
            return card;
        }
    }

    public class Computer : IComparable<Computer>
    {
        public string processor;
        public string ram;
        public int price;

        public Computer(string proc, string r, int price)
        {
            processor = proc;
            ram = r;
            this.price = price;
        }
        public int CompareTo(Computer other)
        {
            if (this.price < other.price) return -1;
            if (this.price > other.price) { return 1; }
            return 0;
        }
    }

    public static class StaticOperation
    {
        public static int WordCount(this string str)
        {
            int x = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0)
                {
                    if (!char.IsLower(str[i]))
                        x++;
                }
                else if (!char.IsLower(str[i]) && str[i - 1] == ' ')
                    x++;
            }
            return x;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Card card = new Card(11111, 11, 2020, 5000);
            card = card + 15;
            card = card - 10;
            Console.WriteLine(card.balance);

            Computer comp1 = new Computer("Intel", "8-gb", 200);
            Computer comp2 = new Computer("Intel", "16-gb", 200);

            Console.WriteLine(comp1.CompareTo(comp2));

            string str = "Hekw Hkajd Ljad0";
            Console.WriteLine(str.WordCount());


            List<Computer> list = new List<Computer> { new Computer("Amd", "8-gb", 200), new Computer("Amd", "16-gb", 300), new Computer("Intel", "8-gb", 220), new Computer("Intel", "8-gb", 100), new Computer("Intel", "16-gb", 170), new Computer("Intel", "8-gb", 270) };

            var res1 = list.Where(i => i.price < 200);
            var res2 = list.Where(i => i.processor == "Amd");
            var res3 = list.OrderBy(i => i.price);

            string str1 = "";

            foreach (var i in res1)
            {
                Console.WriteLine(i.processor + " " + i.price + " " + i.ram);
                str1 += i.processor + " " + i.price + " " + i.ram + "\n";
            }
            Console.WriteLine();
            str1 += "\n";
            foreach (var i in res2)
            {
                Console.WriteLine(i.processor + " " + i.price + " " + i.ram);
                str1 += i.processor + " " + i.price + " " + i.ram + "\n";
            }
            Console.WriteLine();
            str1 += "\n";
            foreach (var i in res3)
            {
                Console.WriteLine(i.processor + " " + i.price + " " + i.ram);
                str1 += i.processor + " " + i.price + " " + i.ram + "\n";
            }
            Console.WriteLine();
            str1 += "\n";

            File.WriteAllText("A.txt", str1);
        }
    }
}
