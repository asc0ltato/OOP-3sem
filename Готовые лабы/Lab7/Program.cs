using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

interface I<T> 
{
    void Add(T element);
    void Remove(T element);
    HashSet<T> FindGreater(T x);
    HashSet<T> FindLess(T x);
    HashSet<T> ViewAll();
}


public class Set<T> : I<T> where T : IComparable<T>
{
    public HashSet<T> elements;

    public Set(IEnumerable<T> initialElements)
    {
        elements = new HashSet<T>(initialElements);
    }

    public void Add(T element)
    {
        elements.Add(element);
    }
    public void Remove(T element)
    {
        elements.Remove(element);
    }

    public HashSet<T>? FindGreater(T x)
    {
        var answers = new HashSet<T>();
        foreach (var element in elements)
        {
            if (element.CompareTo(x) > 0)
            {
                answers.Add(element);
            }
        }
        return answers;
    }

    public HashSet<T>? FindLess(T x)
    {
        var answers = new HashSet<T>();
        foreach (var element in elements)
        {
            if (element.CompareTo(x) < 0)
            {
                answers.Add(element);
            }
        }
        return answers;
    }

    public HashSet<T> ViewAll()
    {
        return new HashSet<T>(elements);
    }

    public static Set<T> operator ++(Set<T> set)
    {

        Random rand = new Random();
        int randomElement = rand.Next(1, 100);
        set.Add((T)Convert.ChangeType(randomElement, typeof(T)));
        return set;
    }
    public static Set<T> operator --(Set<T> set)
    {
        if(set.elements.Count > 0)
        {
            var last = set.elements.Last();
            set.elements.Remove(last);
        }
        return set;
    }

    public static Set<T> operator +(Set<T> set1, Set<T> set2)
    {
        Set<T> resultSet = new Set<T>(set1.elements);
        foreach (T element in set2.elements)
        {
            resultSet.Add(element);
        }
        return resultSet;
    }

    public static bool operator >=(Set<T> set, Set<T> set2)
    {
        if (set.elements.Count >= set2.elements.Count)
        {
            return true;
        }
        return false;
    }

    public static bool operator <=(Set<T> set, Set<T> set2)
    {
        if (set.elements.Count <= set2.elements.Count)
        {
            return true;
        }
        return false;
    }

    public static implicit operator int(Set<T> set)
    {
        return set.elements.Count;
    }
    public static T operator %(Set<T> set, int index)
    {
        return set[index];
    }

    public T this[int index]
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

    public void Write(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                writer.WriteLine($"Тип Set: {this.ToString()}, количество элементов в Set: {this.elements.Count}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Завершение записи в файл.");
        }
    }

    public void Read(string filename)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Завершение чтения файла.");
        }
    }
}

public class QuestionsList<T> where T : Question
{
    public List<T> list = new List<T>();
    public QuestionsList()
    {
        list = new List<T>();
    }
    public void Add(T item)
    {
        list.Add(item);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Set<int> set = new Set<int>(new HashSet<int> { 1, 2, 3 });
        set.Add(5);
        set.Remove(3);
        Console.WriteLine("Set:");
        HashSet<int> allElementsInSet = set.ViewAll();
        foreach (int element in allElementsInSet)
        {
            Console.WriteLine(element);
        }

        Console.WriteLine("Числа в set больше 1");
        foreach (int i in set.FindGreater(1))
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("Числа в set меньше 4");
        foreach (int i in set.FindLess(5))
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("---------------------------------");
        Set<char> set_str = new Set<char>(new HashSet<char> { 'a', 'b', 'c' });
        set_str.Add('d');
        set_str.Remove('b');
        Console.WriteLine("Set_str:");
        HashSet<char> allElementsInSet_str = set_str.ViewAll();
        foreach (char element in allElementsInSet_str)
        {
            Console.WriteLine(element);
        }

        Console.WriteLine("Буквы в set_str больше a");
        foreach (char i in set_str.FindGreater('a'))
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("Буквы в set_str меньше c");
        foreach (char i in set_str.FindLess('c'))
        {
            Console.WriteLine(i);
        }

        QuestionsList<Question> questionList = new QuestionsList<Question>();

        Question question1 = new Question("Сколько будет 2 + 2?", "4");
        Question question2 = new Question("Какая столица Франции?", "Париж");

        questionList.Add(question1);
        questionList.Add(question2);

        foreach (var question in questionList.list)
        {
            Console.WriteLine(question.question);
            string userAnswer = Console.ReadLine(); 
            question.guess = userAnswer;
            Console.WriteLine(question.tryToPass() ? "Пройден" : "Не пройден");
        }

        set.Write("txt.txt");
        set.Read("txt.txt");
    }
}