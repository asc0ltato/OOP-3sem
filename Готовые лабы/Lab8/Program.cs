using System;

delegate void BossUpgrade(string str);
delegate void BossAtack(int bossPower); 

class Boss
{
    public event BossUpgrade Upgraded;
    public event BossAtack Atacked;

    int _power = 1; 
    public void Upgrade() 
    {
        _power++;
        Upgraded?.Invoke($"Теперь босс имеет напряжение {_power}");
    }

    public void TurnOn() => Atacked?.Invoke(_power);
}

class Worker
{
    int _voltage;
    string _name;
    bool _isLive = true;
    public Worker(string name, int voltage)
    {
        this._voltage = voltage;
        this._name = name;
    }

    public void TakeVoltage(int _bossPower)
    {
        if (_isLive && _voltage < _bossPower)
        {
            Console.WriteLine($"{_name} Смерть");
            _isLive = false;
        }
        else if (_isLive)
        {
            Console.WriteLine($"{_name} Норм");
        }
    }
}

class Task
{
    public static void Main()
    {
        Boss boss = new Boss();
        Worker worker = new Worker("Sveta", 1);
        Worker worker2 = new Worker("Anya", 2);

        boss.Atacked += worker.TakeVoltage;
        boss.Atacked += worker2.TakeVoltage;
        boss.Upgraded += Console.WriteLine;

        boss.TurnOn();

        boss.Upgrade();
        boss.TurnOn();

        boss.Upgrade();
        boss.TurnOn();
        Console.WriteLine("-----------------------------------------------");

        string inputString = "Hello, World!";

        Action<string> removePunctuation = (s) =>
        {
            string cleanedString = new string(s.ToCharArray().Where(c => !char.IsPunctuation(c)).ToArray());
            Console.WriteLine($"Удаление знаков препинания: {cleanedString}");
        };

        Func<string, string, string> insertText = (s, text) =>
        {
            int indexToInsert = s.Length / 2;
            string result = s.Insert(indexToInsert, text);
            Console.WriteLine($"Вставка текста в середину строки: {result}");
            return result;
        };

        Func<string, string> toUpperCase = (s) =>
        {
            string upperCaseString = s.ToUpper();
            Console.WriteLine($"Преобразование строки в верхний регистр: {upperCaseString}");
            return upperCaseString;
        };

        Func<string, string> replaceChars = (s) =>
        {
            string replacedString = s.Replace('O', 'X');
            Console.WriteLine($"Замена 'O' на 'X': {replacedString}");
            return replacedString;
        };

        Predicate<string> startsWithH = (s) =>
        {
            bool startsWithHResult = s.StartsWith("H");
            Console.WriteLine($"Строка начинается с 'H': {startsWithHResult}");
            return startsWithHResult;
        };

        removePunctuation(inputString);
        string result1 = insertText(inputString, "=)");
        string result2 = toUpperCase(result1);
        replaceChars(result2);
        startsWithH(result2);
    }
}