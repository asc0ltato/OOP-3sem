using System;
using System.Collections.Generic;

class Author : IAgeable
{
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public string Country { get; set; }

    public Author(string name, DateTime birthday, string country)
    {
        Name = name;
        Birthday = birthday;
        Country = country;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        Author other = (Author)obj;
        return Name == other.Name && Birthday == other.Birthday;
    }

    public int Age()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - Birthday.Year;
        if (Birthday.Date > today.AddYears(-age)) age--;
        return age;
    }
}

class Book : Author, IAgeable
{
    public string Genre { get; set; }

    public Book(string name, DateTime birthday, string country, string genre) : base(name, birthday, country)
    {
        Genre = genre;
    }

    public new int Age()
    {
        throw new NotImplementedException("Метод определения возраста еще не реализован для книг");
    }
}

interface IAgeable
{
    int Age();
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите строку:");
        string inputString = Console.ReadLine();

        string[] words = inputString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (words.Length >= 2)
        {
            string secondWord = words[1];
            Console.WriteLine($"Второе слово в строке: {secondWord}");
        }
        else
        {
            Console.WriteLine("В строке недостаточно слов для получения второго слова.");
        }

        int[,] array = new int[3, 3];
        int count = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                array[i, j] = count;
                count++;
            }
        }

        Console.WriteLine("Двумерный массив:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }

        int max = int.MinValue;
        foreach (int num in array)
        {
            if (num > max)
            {
                max = num;
            }
        }
        Console.WriteLine($"Максимальный элемент двумерного массива: {max}");

        Author author1 = new Author("Светлана Алексиевич", new DateTime(1969, 10, 10), "США");
        Author author2 = new Author("Jane Smith", new DateTime(1975, 8, 15), "Великобритания");
        Book book1 = new Book("Книга_1", new DateTime(1990, 12, 20), "Канада", "Приключение");
        Book book2 = new Book("Книга_2", new DateTime(1985, 6, 30), "Индия", "Фантазия");

        Console.WriteLine($"Сравнение авторов: {author1.Equals(author2)}");

        IAgeable[] ageableObjects = { author1, author2, book1, book2 };

        foreach (var obj in ageableObjects)
        {
            try
            {
                int age = obj.Age();
                Console.WriteLine($"Возраст объекта: {age}");
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine($"Исключение: {e.Message}");
            }
        };
    }
}