using System;

class Day
{
    private string[] daysOfWeek = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

    public string this[int index]
    {
        get
        {
            if (index >= 0 && index < daysOfWeek.Length)
                return daysOfWeek[index];
            else
                throw new IndexOutOfRangeException();
        }
    }
}

interface IMove
{
    void Move();
}

abstract class Transport : IMove
{
    public abstract void Move();
}

class Car : Transport
{
    public override void Move()
    {
        Console.WriteLine("Автомобиль едет");
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("Введите первую строку:");
        string str1 = Console.ReadLine();

        Console.WriteLine("Введите вторую строку:");
        string str2 = Console.ReadLine();

        bool contains = str1.Contains(str2);
        Console.WriteLine($"Первая строка содержит в себе вторую: {contains}");
        Console.WriteLine("---------------------------------------------------");
        string[] stringsArray = { "один", "два", "три", "четыре", "пять" };

        Array.Reverse(stringsArray);

        Console.WriteLine("Строки в обратном порядке:");
        foreach (var str in stringsArray)
        {
            Console.WriteLine(str);
        }
        Console.WriteLine("---------------------------------------------------");
        Day days = new Day();

        try
        {
            Console.WriteLine("Введите индекс дня недели (от 0 до 6):");
            int index = Convert.ToInt32(Console.ReadLine());

            string day = days[index];
            Console.WriteLine($"День недели под индексом {index}: {day}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.WriteLine("---------------------------------------------------");
        Car car = new Car();
        car.Move();
    }
}