namespace _8_2
{
    internal class Program
    {
        abstract class Function
        {
            public virtual string Func() { return "\0"; }
            public int X { get; set; }

            public override int GetHashCode()
            {
                return 100 * new Random().Next(100);
            }
        }
        class Liner : Function
        {
            public int A { get; set; }
            public int B { get; set; }

            public Liner(int a, int b)
            {
                A = a;
                B = b;
            }

            public override string Func()
            {
                return $"{A}x + {B}";
            }

            public override string ToString()
            {
                return "Liner";
            }
        }
        class Sqr : Function
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }

            public Sqr(int a, int b, int c)
            {
                A = a;
                B = b;
                C = c;
            }

            public override string Func()
            {
                return $"{A}x2 + {B}x + {C}";
            }

            public override string ToString()
            {
                return "Sqr";
            }

        }

        class ArrayFunct<T> where T : Function
        {
            public List<T> list;

            public ArrayFunct()
            {
                list = new List<T>(new T[100]);
            }

            public T this[int index]
            {
                get { return list[index]; }
                set { list[index] = value; }
            }
        }

        static void Main(string[] args)
        {
            using (StreamWriter sw = new StreamWriter("txt.txt"))
            {

                Liner liner = new Liner(2, 3);
                Liner liner2 = new Liner(3, 3);
                Sqr sqr = new Sqr(2, 3, 4);
                Sqr sqr2 = new Sqr(0, 3, 4);

                ArrayFunct<Function> arrayFunct = new ArrayFunct<Function>();

                arrayFunct[0] = sqr;
                arrayFunct[1] = liner;
                arrayFunct[2] = liner2;
                arrayFunct[3] = sqr2;

                var a = arrayFunct.list[0];
                var b = arrayFunct.list[1];

                ((Sqr)a).GetHashCode();
                ((Liner)b).GetHashCode();

                Console.WriteLine(a.Func());
                Console.WriteLine(b.Func());
                sw.WriteLine(a.Func());
                sw.WriteLine(b.Func());

                Console.WriteLine(a);
                Console.WriteLine(b);
                sw.WriteLine(a);
                sw.WriteLine(b);

                var minValueFunc = arrayFunct.list
                .Where(x => x != null)
                .OrderBy(x => x.X)
                .ThenBy(x => (x is Liner linerObj) ? linerObj.A : ((x is Sqr sqrObj) ? sqrObj.A : 100))
                .FirstOrDefault();

                Console.WriteLine(minValueFunc?.Func());
                sw.WriteLine(minValueFunc?.Func());
            }
        }
    }
}
