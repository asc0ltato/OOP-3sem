using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace _3_3
{
    class Program
    {
        class DeleteException : Exception
        {
            public DeleteException(string error) : base(error) { }
        }

        class D2Point
        {
            public int a;
            public int X { get; set; }
            public int Y { get; set; }

            public D2Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void ChangeZnak()
            {
                X = -X; Y = -Y;
            }

            public static bool operator ==(D2Point a, D2Point b)
            {
                if (Equals(a, b))
                    return true;
                if (a is null || b is null)
                    return false;
                return a.X == b.X && a.Y == b.Y;
            }

            public static bool operator !=(D2Point a, D2Point b)
            {
                return !(a == b);
            }


            public override string ToString()
            {
                return "X = " + X + "Y = " + Y;
            }
        }
        class D2Path 
        {
            public delegate void StateHandler();
            public event StateHandler? Change;

            public void ChangeEwe()
            {
                Change?.Invoke();
            }

            List<D2Point> points = new List<D2Point>();

            public void Add(D2Point item)
            {
                points.Add(item);
            }

            public void Delete(int item)
            {
                if (points.Count > 0) points.RemoveAt(item);
                else throw new DeleteException("Попытка удаления из пустой коллекции.");
            }

            public void Clear()
            {
                points.Clear();
            }

            public (int, int, int, int) CountPoints()
            {
                int fq = 0, sq = 0, tq = 0, foq = 0;
                foreach (var x in points)
                {
                    if (x.X > 0 && x.Y > 0) fq++;
                    if (x.X < 0 && x.Y > 0) sq++;
                    if (x.X < 0 && x.Y < 0) tq++;
                    if (x.X > 0 && x.Y < 0) foq++;
                }
                return (fq, sq, tq, foq);
            }

            public override string ToString()
            {
                string result = "";
                foreach (var point in points)
                {
                    result += $"Point: X = {point.X}, Y = {point.Y}\n";
                }
                return result;
            }
        }

        static void Main(string[] args)
        {
            D2Point p1 = new D2Point(3, 5);
            D2Point p2 = new D2Point(4, 7);
            D2Point p3 = new D2Point(4, 7);
           
            D2Path d2Path = new D2Path();
            d2Path.Add(p1);
            d2Path.Add(p2);
            d2Path.Add(p3);

            d2Path.Change += p1.ChangeZnak;
            d2Path.ChangeEwe();
            Console.WriteLine(d2Path.ToString());
            Console.WriteLine(p2 == p3);
            Console.WriteLine(p1 == p3);

            try
            {
                d2Path.Delete(0);
            }
            catch (DeleteException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(d2Path.CountPoints());
            d2Path.Clear();

            Type myType = typeof(D2Path);
            //Console.WriteLine($"Name: {myType.Name}");              // получаем краткое имя типа
            //Console.WriteLine($"Full Name: {myType.FullName}");     // получаем полное имя типа
            //Console.WriteLine($"Namespace: {myType.Namespace}");    // получаем пространство имен типа
            //Console.WriteLine($"Is struct: {myType.IsValueType}");  // является ли тип структурой
            //Console.WriteLine($"Is class: {myType.IsClass}");       // является ли тип классом
            //Console.WriteLine("Реализованные интерфейсы:");
            //foreach (Type i in myType.GetInterfaces())
            //{
            //    Console.WriteLine(i.Name);
            //}
            //foreach (MemberInfo member in myType.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            //{
            //    Console.WriteLine($"{member.DeclaringType} {member.MemberType} {member.Name}");
            //}
            //foreach (MethodInfo method in myType.GetMethods())
            //{
            //    string modificator = "";

            //    // если метод статический
            //    if (method.IsStatic) modificator += "static ";
            //    // если метод виртуальный
            //    if (method.IsVirtual) modificator += "virtual ";

            //    Console.WriteLine($"{modificator}{method.ReturnType.Name} {method.Name} ()");
            //}
            foreach (ConstructorInfo member in myType.GetConstructors())
            {
                Console.WriteLine($"{member.DeclaringType} {member.MemberType} {member.Name}");
            }
            foreach (FieldInfo member in myType.GetFields())
            {
                Console.WriteLine($"{member.DeclaringType} {member.MemberType} {member.Name}");
            }
        }
    }
}
