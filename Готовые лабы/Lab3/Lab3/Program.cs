using System;
using System.Collections.Generic;
using System.Linq;

public class Set
{
    public HashSet<int> elements;

    public Set(IEnumerable<int> initialElements)
    {
        elements = new HashSet<int>(initialElements);
    }

    public void Add(int element)
    {
        elements.Add(element);
    }

    public static Set operator ++(Set set)
    {
        Random rand = new Random();
        int randomElement = rand.Next(1, 100);
        set.Add(randomElement);
        return set;
    }

    public static Set operator --(Set set)
    {
        int last = set.elements.Last();
        set.elements.Remove(last);
        return set;
    }

    public static Set operator +(Set set1, Set set2)
    {
        Set resultSet = new Set(set1.elements);
        foreach (int element in set2.elements)
        {
            resultSet.Add(element);
        }
        return resultSet;
    }

    public static bool operator >=(Set set, Set set2)
    {
        if (set.elements.Count >= set2.elements.Count)
        {
            return true;
        }
        return false;
    }
    public static bool operator <=(Set set, Set set2)
    {
        if (set.elements.Count <= set2.elements.Count)
        {
            return true;
        }
        return false;
    }

    public static implicit operator int(Set set)
    {
        return set.elements.Count;
    }

    public static int operator %(Set set, int index)
    {
        return set[index];
    }

    public int this[int index]
    {
        get
        {
            if (index >= 0 && index < elements.Count)
            {
                return elements.ElementAt(index);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

    public override string ToString()
    {
        return string.Join(", ", elements);
    }

    public class Production
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }

        public Production(int id, string orgName)
        {
            Id = id;
            OrganizationName = orgName;
        }
        public override string ToString()
        {
            return $"Production ID: {Id}, Organization: {OrganizationName}";
        }
    }

    public class Developer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }

        public Developer(int id, string fullName, string department)
        {
            Id = id;
            FullName = fullName;
            Department = department;
        }
        public override string ToString()
        {
            return $"Developer ID: {Id}, FullName: {FullName}, Department: {Department}";
        }
    }

    public static class StatisticOperation
    {
        public static int Sum(Set set)
        {
            return set.elements.Sum();
        }

        public static int MaxMinusMin(Set set)
        {
            if (set.elements.Count == 0)
                throw new InvalidOperationException("Set пусто");

            return set.elements.Max() - set.elements.Min();
        }

        public static int Count(Set set)
        {
            return set.elements.Count;
        }
    }
}

public static class StringExtensions
{
    public static string Encrypt(this string input, int shift)
    {
        char[] chars = input.ToCharArray(); 
        for (int i = 0; i < chars.Length; i++) 
        {
            if (char.IsLetter(chars[i])) 
            {
                char offset = char.IsUpper(chars[i]) ? 'A' : 'a'; 
                chars[i] = (char)((chars[i] - offset + shift) % 26 + offset);  
            }
        }
        return new string(chars); 
    }

    public static bool IsOrdered(this Set set)
    {
        var elementsList = set.elements.ToList(); 
        for (int i = 0; i < elementsList.Count - 1; i++)
        {
            if (elementsList[i] > elementsList[i + 1])
            {
                return false;
            }
        }
        return true;
    }
}

class Program
{
    static void Main()
    {
        Set set1 = new Set(new HashSet<int> { 1, 2, 3 });
        Set set2 = new Set(new HashSet<int> { 3, 2, 4 });
        set2++;
        set1--;

        Console.WriteLine("Set 1: " + string.Join(", ", set1));
        Console.WriteLine("Set 2: " + string.Join(", ", set2));

        Set set3 = set1 + set2;
        Console.WriteLine("Объединение set1 and set2: " + string.Join(", ", set3));

        bool isSubset = set1 <= set3;
        Console.WriteLine("Set1 - подмножество set3: " + isSubset);
        bool isSubset1 = set1 >= set3;
        Console.WriteLine("Set3 - подмножество set1: " + isSubset1);

        int setSize = set1;
        Console.WriteLine("Размер set1: " + setSize);

        int element = set1 % 0;
        Console.WriteLine("Элемент с индексом [0] в set1: " + element);

        Set.Production production = new Set.Production(1, "БГТУ");
        Set.Developer developer = new Set.Developer(101, "Жук Светлана Сергеевна", "Системотехник");
        Console.WriteLine(production);
        Console.WriteLine(developer);

        Console.WriteLine("Сумма set1: " + Set.StatisticOperation.Sum(set1));
        Console.WriteLine("Разница max и min set1: " + Set.StatisticOperation.MaxMinusMin(set1));
        Console.WriteLine("Количество элементов в set1: " + Set.StatisticOperation.Count(set1));

        string encryptedString = "Hello, World!".Encrypt(3);
        Console.WriteLine("Зашифрованная строка: " + encryptedString);

        bool isOrdered1 = set1.IsOrdered(); 
        Console.WriteLine("Упорядоченность set1: " + isOrdered1);

        bool isOrdered2 = set2.IsOrdered();
        Console.WriteLine("Упорядоченность set2: " + isOrdered2);
    }
}