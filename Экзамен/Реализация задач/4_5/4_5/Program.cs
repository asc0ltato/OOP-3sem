using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace _4_5
{
    class MuchMoney : Exception
    {
        public MuchMoney(string message) : base(message)
        {
        }
    }

    class NoToDeleteFromWallet : Exception
    {
        public NoToDeleteFromWallet (string message) : base(message)
        {
        }
    }

    interface INumber
    {
        int Number { get; set; }
    }

    [DataContract]
    class Bill : INumber
    {
        [DataMember]
        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                if (value == 10 || value == 20 || value == 50 || value == 100)
                {
                    number = value;
                }
                else
                {
                    throw new Exception("Неверное значение");
                }
            }
        }
         
        public Bill(int number)
        {
            Number = number;
        }
    }

    [DataContract]
    class Wallet<T> where T : INumber
    {
        [DataMember]
        public List<Bill> bills = new List<Bill>();

        public void AddBill(Bill bill)
        {
            if (bills.Sum(x => x.Number) + bill.Number > 100)
            {
                throw new MuchMoney("Сумма купюр больше 100");
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
                throw new NoToDeleteFromWallet("Купюр нет");
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
            Bill bill1 = new Bill(10);
            Bill bill2 = new Bill(10);
            Bill bill3 = new Bill(10);
            Bill bill4 = new Bill(10);
            Bill bill5 = new Bill(50);

            Wallet<Bill> wallet = new Wallet<Bill>();
            wallet.AddBill(bill1);
            wallet.AddBill(bill2);
            wallet.AddBill(bill3);
            wallet.AddBill(bill4);
            wallet.AddBill(bill5);
            wallet.RemoveBill();    

            var query = wallet.bills
            .GroupBy(bill => bill.Number)
            .Select(g => new { Number = g.Key, Count = g.Count() });
            foreach (var item in query)
            {
                Console.WriteLine("Номинал {0} - {1} шт.", item.Number, item.Count);
            }

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Wallet<Bill>));
            using (FileStream sw = new FileStream("json.json", FileMode.OpenOrCreate)) 
            {
                jsonFormatter.WriteObject(sw, wallet);
            }   
        }
    }
}