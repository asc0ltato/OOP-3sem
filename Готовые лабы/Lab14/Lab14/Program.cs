using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Lab14
{
    class Program
    {
        private static bool isPaused = false;
        static object locker = new object();
        private static bool isEvenTurn = true;
        static Random random = new Random();

        static void Main()
        {

            Console.WriteLine("-------------------Задание 1-------------------");

            var Processs = Process.GetProcesses();
            foreach (var process in Processs)
            {
                try
                {
                    Console.WriteLine($"Текущий процесс:\n" +
                                      $"ID процесса: {process.Id}\n" +
                                      $"Имя процесса: {process.ProcessName}\n" +
                                      $"Приоритет процесса: {process.BasePriority}\n" +
                                      $"Время запуска процесса: {process.StartTime}\n" +
                                      $"Текущее состояние процесса: {process.Responding}\n" +
                                      $"Время работы процесса: {process.TotalProcessorTime}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Возникло исключение: {ex.Message}");
                }
            }

            Console.WriteLine("-------------------Задание 2-------------------");

            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine($"Имя домена: {domain.FriendlyName}");
            Console.WriteLine($"Базовый каталог для получения сборок: {domain.BaseDirectory}");
            Console.WriteLine($"Детали конфигурации: {domain.SetupInformation}");
            Console.WriteLine($"Все сборки, загруженные в домен:");
            foreach (Assembly asm in domain.GetAssemblies())
            {
                Console.WriteLine(asm.GetName().Name);
            }
            Console.WriteLine();

            Console.WriteLine("-------------------Задание 3-------------------");

            var number = new Thread(PrintSimpleNumbers);
            number.Start();
            number.Join();
            Console.WriteLine();
            static void Info(object thread)
            {
                var primeThread = thread as Thread;
                primeThread.Name = "Поток поиска простых чисел";

                Console.WriteLine($"Состояние потока: {primeThread.ThreadState}");
                Console.WriteLine($"Имя потока: {primeThread.Name}");
                Console.WriteLine($"Приоритет потока: {primeThread.Priority}");
                Console.WriteLine($"Числовой идентификатор потока: {primeThread.ManagedThreadId}");
                Console.WriteLine($"Поток фоновый: {primeThread.IsBackground}");
                Console.WriteLine($"Поток запущен: {primeThread.IsAlive}");
                Console.WriteLine($"Поток приостановлен: {primeThread.IsThreadPoolThread}");
            }

            static void PrintSimpleNumbers()
            {
                Thread numbers = new Thread(new ParameterizedThreadStart(Info));
                numbers.Start(Thread.CurrentThread);
                numbers.Join();

                Console.Write("\nВведите число n для поиска простых чисел от 1 до n: ");
                int n = Convert.ToInt32(Console.ReadLine());

                string filePath = "primes.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 2; i <= n; i++)
                    {
                        if (IsPrime(i))
                        {
                            writer.WriteLine(i);
                            Console.WriteLine(i);
                            Thread.Sleep(200);
                            while (isPaused)
                            {
                                Thread.Sleep(100);
                            }
                        }
                    }
                }
            }

            static bool IsPrime(int number)
            {
                if (number <= 1) return false;
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0) return false;
                }
                return true;
            }

            Console.WriteLine("-------------------Задание 4-------------------");

            int n = 10;

            Thread evenThread = new Thread(() =>
            {
                for (int i = 2; i <= n; i += 2)
                {
                    lock (locker)
                    {
                        while (!isEvenTurn)
                            Monitor.Wait(locker);

                        WriteToFile($"Четное: {i}");
                        Thread.Sleep(100);
                        isEvenTurn = false;
                        Monitor.Pulse(locker);
                    }
                }
            });


            Thread oddThread = new Thread(() =>
            {
                for (int i = 1; i <= n; i += 2)
                {
                    lock (locker)
                    {
                        while (isEvenTurn)
                            Monitor.Wait(locker);

                        WriteToFile($"Нечетное: {i}");
                        Thread.Sleep(1000);
                        isEvenTurn = true;
                        Monitor.Pulse(locker);
                    }
                }
            });

            evenThread.Start();
            oddThread.Start();

            evenThread.Priority = ThreadPriority.Highest;

            evenThread.Join();
            oddThread.Join();

            static void WriteToFile(string text)
            {
                Console.WriteLine(text);
                using (StreamWriter writer = new StreamWriter("numbers.txt", true))
                {
                    writer.WriteLine(text);
                }
            }

            Console.WriteLine("-------------------Задание 5-------------------");

            TimerCallback tm = new TimerCallback(GetPrice);
            Timer timer = new Timer(tm, 100, 0, 100);
            Console.ReadLine();

            static void GetPrice(object state)
            {
                int value = random.Next(0, 50);
                Console.WriteLine((int)state - value);
            }
        }
    }
}