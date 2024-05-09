using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace _3_41
{

    interface INumber
    {
        int Number { get; set; }
    }

    class Bill : INumber
    {
        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                if (value == 5 || value == 10 || value == 20 || value == 50 || value == 100)
                {
                    number = value;
                }
                else
                {
                    throw new Exception("Неверное значение");
                }
            }
        }
    }


    class Wallet<T> where T : INumber
    {
        public List<T> bills = new List<T>();
        public void AddBill(T bill)
        {
            if (bills.Sum(x => x.Number) + bill.Number > 200)
            {
                throw new Exception("Сумма купюр больше 200");
            }
            else
            {
                bills.Add(bill);
            }
        }
        public void RemoveBill()
        {
            if (bills.Count == 0)
            {
                throw new Exception("Купюр нет");
            }
            else
            {
                bills.Remove(bills.OrderBy(x => x.Number).First());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Wallet<Bill> wallet = new Wallet<Bill>();
            wallet.AddBill(new Bill { Number = 5 });
            wallet.AddBill(new Bill { Number = 10 });
            wallet.AddBill(new Bill { Number = 20 });
            wallet.AddBill(new Bill { Number = 50 });
            wallet.AddBill(new Bill { Number = 50 });
            wallet.RemoveBill();

            // На основе LINQ посчитайте кол-во купюр каждого достоинства в кошельке.
            var query = from bill in wallet.bills
                        group bill by bill.Number into g
                        select new { Number = g.Key, Count = g.Count() };
            foreach (var item in query)
            {
                Console.WriteLine("Номинал {0} - {1} шт.", item.Number, item.Count);
            }

            // Сохраните данные в файл в формате TXT
            using (StreamWriter sw = new StreamWriter("wallet.txt"))
            {
                foreach (var item in query)
                {
                    sw.WriteLine("Номинал {0} - {1} шт.", item.Number, item.Count);
                }
            }
        }
    }
}