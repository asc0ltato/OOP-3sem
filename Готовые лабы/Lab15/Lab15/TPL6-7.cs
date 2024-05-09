using System.Collections.Concurrent;

namespace Lab15
{
    static partial class TPL
    {
        public static void Task6()
        {
            Console.WriteLine("-----------Задание 6---------");

            Parallel.Invoke(
                () =>
                {
                    Thread.Sleep(4000);
                    Console.WriteLine("Первая функция выполнена!");
                },
                () =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Вторая функция выполнена!");
                },
                () =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Третья функция выполнена!");
                },
                () =>
                {
                    Thread.Sleep(2500);
                    Console.WriteLine("Четвертая функция выполнена!");
                }
                );
        }

        public static void Task7()
        {
            Console.WriteLine("-----------Задание 7---------");

            BlockingCollection<Product> products = new BlockingCollection<Product>();

            Task[] providers = new Task[]
            {
                Task.Run(() => Supplier(1, 2, 170, products)),
                Task.Run(() => Supplier(2, 2, 200, products)),
                Task.Run(() => Supplier(3, 2, 600, products)),
                Task.Run(() => Supplier(4, 2, 300, products)),
                Task.Run(() => Supplier(5, 2, 250, products))
            };

            Task[] clients = new Task[]
            {
                Task.Run(() => Customer(1, products)),
                Task.Run(() => Customer(2, products)),
                Task.Run(() => Customer(3, products)),
                Task.Run(() => Customer(4, products)),
                Task.Run(() => Customer(5, products)),
                Task.Run(() => Customer(6, products)),
                Task.Run(() => Customer(7, products)),
                Task.Run(() => Customer(8, products)),
                Task.Run(() => Customer(9, products)),
                Task.Run(() => Customer(10, products))
            };

            Task.WaitAll(providers);
            Console.WriteLine("Все поставщики завершили работу.");

            foreach (var client in clients)
            {
                client.Wait();
            }

             Console.WriteLine("Все покупатели завершили работу.");


            void Supplier(int supplierId, int count, int sleepTime, BlockingCollection<Product> products)
            {
                for (int i = 0; i < count; i++)
                {
                    Thread.Sleep(sleepTime);
                    Product product = new Product();
                    products.Add(product);
                    Console.WriteLine($"Поставщик {supplierId} добавил товар");
                }
            }

            void Customer(int customerId, BlockingCollection<Product> products)
            {
                while (!products.IsCompleted)
                {
                    Thread.Sleep(1000); 

                    Product product;
                    if (products.TryTake(out product))
                    {
                        Console.WriteLine($"Клиент {customerId} забрал продукт");
                    }
                    else
                    {
                        Console.WriteLine($"Товара нет на складе, клиент {customerId} уходит");
                        break; 
                    }
                }
            }
            }
    }

    class Product
    {
        public string Name { get; set; }
        public uint Price { get; set; }

        public Product(string name, uint price)
        {
            Name = name;
            Price = price;
        }

        public Product()
        {
            Name = String.Empty;
            Price = 0;
        }

        public override string ToString() => $"Имя: {Name}, Цена: {Price}";
    }
}
