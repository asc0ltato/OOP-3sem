using System.Diagnostics;

namespace Lab15
{
    static partial class TPL
    {
        public static void Task1(int N)
        {
            Console.WriteLine("-----------Задание 1-----------");
            Stopwatch stw = new();

            Task task = new(() => FindSimpleNums(N, stw));

            Console.WriteLine($"Идентификатор текущей задачи: {task.Id}");
            Console.WriteLine($"Статус до запуска: {task.Status}");
            stw.Start();
            task.Start();
            Console.WriteLine($"Статус после запуска: {task.Status}");
            Console.WriteLine($"Задача завершена: {task.IsCompleted}");
            Console.WriteLine($"Время выполнения задачи: {stw.Elapsed}");
        }

        public static void Task2(int N)
        {
            Console.WriteLine("-----------Задание 2-----------");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task task = new(() => FindSimpleNums(N, ref token), token);

            Console.WriteLine($"Идентификатор текущей задачи: {task.Id}");
            Console.WriteLine($"Статус до запуска: {task.Status}");

            task.Start();

            Console.WriteLine($"Статус после запуска: {task.Status}");

            cts.Cancel();
            Thread.Sleep(2000);

            Console.WriteLine($"Статус после отмены: {task.Status}");
        }

        private static List<int> FindSimpleNums(int N, Stopwatch stw = null)
        {
            List<int> simpleNums = new();

            for (int i = 2; i < N; i++)
            {
                bool isSimple = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isSimple = false;
                        break;
                    }
                }

                if (isSimple)
                {
                    simpleNums.Add(i);
                }
            }

            /*foreach (var num in simpleNums)
            {
                Console.Write($"{num}\n");
            }*/

            if (stw != null)
            {
                stw.Stop();
            }

            Console.WriteLine($"Время выполнения задачи: {stw.Elapsed}");

            return simpleNums;
        }

        private static List<int> FindSimpleNums(int N, ref CancellationToken token)
        {
            List<int> simpleNums = new();

            for (int i = 2; i < N; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Задача отменена");
                    return null;
                }

                bool isSimple = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isSimple = false;
                        break;
                    }
                }

                if (isSimple)
                {
                    simpleNums.Add(i);
                }
            }

            Console.WriteLine("Выполнено!");

            return simpleNums;
        }
    }
}
