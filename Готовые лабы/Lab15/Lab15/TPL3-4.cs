
namespace Lab15
{
    static partial class TPL
    {
        public static void Task3()
        {
            Console.WriteLine("-----------Задание 3---------");

            int Sum(int a, int b) { return a + b; }
            int Subtraction(int a, int b) => a - b;
            int Division(int a, int b) => a / b;
            int Multiplication(int a, int b, int c) => a * b * c;

            Task<int> task1 = new Task<int>(() => Sum(4, 5));
            Task<int> task2 = new Task<int>(() => Subtraction(6, 3));
            Task<int> task3 = new Task<int>(() => Division(8, 4));
            Task<int> task4 = new Task<int>(() => Multiplication(task1.Result,
                                                                task2.Result,
                                                                task3.Result));

            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();

            Console.WriteLine(task4.Result);
        }

        public static void Task4_1()
        {
            Console.WriteLine("-----------Задание 4-1----------");
            int Sum(int a, int b) { return a + b; }

            Task<int> task1 = new Task<int>(() => Sum(4, 5));
            Task task2 = task1.ContinueWith(task => Console.WriteLine(task1.Result));

            task1.Start();

            task2.Wait();
        }

        public static void Task4_2()
        {
            Console.WriteLine("-----------Задание 4-2----------");
            int Sum(int a, int b) { return a + b; }

            Task<int> task1 = new Task<int>(() => Sum(4, 5));

            task1.Start();
            task1.Wait();
            Console.WriteLine(task1.GetAwaiter().GetResult());
        }
    }
}
