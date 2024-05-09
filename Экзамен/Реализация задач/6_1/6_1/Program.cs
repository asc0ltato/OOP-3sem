namespace _6_1
{
    class PinErrorException : Exception
    {
        public PinErrorException(string message) : base(message)
        {
        }
    }

    class CanNotException : Exception
    {
        public CanNotException (string message) : base(message)
        {
        }
    }

    interface ICreditCard
    {
        void Add(int obj);
        void Get(int obj);
    }

    public class CreditCard : ICreditCard
    {
        public int balance;
        public int number;
        private readonly int pin;
        private readonly int pin2;

        public CreditCard(int balance, int number, int pin, int pin2)
        {
            this.balance = balance;
            this.number = number;
            this.pin = pin;
            this.pin2 = pin2;
        }

        public override string ToString()
        {
            return $"Balance - {balance}, Number - {number}";
        }

        public void CheckBalance()
        {
            int pinInput = 0;
            while (true)
            {
                if (pinInput < 3)
                {
                    try
                    {
                        Console.WriteLine("Введите pin: ");
                        int pin_1 = Convert.ToInt32(Console.ReadLine());
                        if (pin_1 == pin)
                        {
                            Console.WriteLine("Баланс: " + balance);
                            break;
                        }
                        else
                        {
                            pinInput++;
                            throw new PinErrorException("Пароль введён неверно");
                        }
                    }
                    catch (PinErrorException ex)
                    {
                        LogException(ex, "CheckBalance", nameof(CreditCard));
                    }
                }
                else
                {

                    Console.WriteLine("Введите pin2: ");
                    int pin_2 = Convert.ToInt32(Console.ReadLine());
                    if (pin_2 == pin2)
                    {
                        Console.WriteLine("Баланс: " + balance);
                        break;
                    }
                    else
                    {
                        throw new PinErrorException("Пароль введён неверно");

                    }
                }
            }
        }

        public void Add(int obj)
        {
            balance = this.balance + obj;
            Console.WriteLine("Баланс пополнен на " + obj + " рублей и равен " + balance);
        }

        public void Get(int obj)
        {
            try
            {
                if (balance - obj >= 0)
                {
                    balance = this.balance - obj;
                    Console.WriteLine("Снято " + obj + " рублей. Баланс равен " + balance);
                }
                else
                {
                    throw new CanNotException("Недостаточно средств(снимаете больше, чем на карте)");
                }
            }
            catch (CanNotException ex)
            {
                LogException(ex, "Get", nameof(CreditCard));
            }
        }

        private void LogException(Exception ex, string methodName, string className)
        {
            using (StreamWriter writer = new StreamWriter("txt.txt"))
            {
                writer.WriteLine($"Тип исключения: {ex.GetType().Name}");
                writer.WriteLine($"Сообщение: {ex.Message}");
                writer.WriteLine($"Время: {DateTime.Now}");
                writer.WriteLine($"Метод: {methodName}");
                writer.WriteLine($"Класс: {className}");
                writer.WriteLine("-------------------------------");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CreditCard creditCard = new CreditCard(250, 19, 123, 321);
            CreditCard creditCard2 = new CreditCard(150, 29, 1441, 1001);
            CreditCard creditCard3 = new CreditCard(130, 39, 5555, 2004);
            creditCard.CheckBalance();
            creditCard.Add(45);
            creditCard.Get(100);

            List<CreditCard> creditCards = new List<CreditCard>();
            creditCards.Add(creditCard);
            creditCards.Add(creditCard2);
            creditCards.Add(creditCard3);


            var selectByMoney = creditCards
                 .Where(x => x.balance > 100 && x.balance < 200 && x.number.ToString().Contains("9"))
                 .Sum(x => x.balance);

            Console.WriteLine("Сумма балансов: " + selectByMoney);
        }
    }
}