using System;
using System.Collections.Generic;
using System.Linq;

public class PDate
{
    private int day;
    private int month;

    public int Day
    {
        get { return this.day; }
        set
        {
            if (IsValidDay(value))
            {
                this.day = value;
            }
            else
            {
                Console.WriteLine("Ошибка при вводе дня");
            }
        }
    }

    public int Month
    {
        get { return this.month; }
        set
        {
            if (IsValidMonth(value))
            {
                this.month = value;
            }
            else
            {
                Console.WriteLine("Ошибка при вводе месяца");
            }
        }
    }

    public int Year { get; set; }

    public PDate(int day, int month)
    {
        this.day = day;
        this.month = month;
    }

    private PDate() { }

    public virtual void NextDay()
    {
        day++;
        if (day > 30)
        {
            day = 1;
            month++;
            if (month > 12)
            {
                month = 1;
            }
        }
    }

    private bool IsValidDay(int day)
    {
        if (day >= 1 && day <= 31)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsValidMonth(int month)
    {
        if (month >= 1 && month <= 12)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class PMDate : PDate
{
    public int Year
    {
        get { return base.Year; }
        set { base.Year = value; }
    }

    public PMDate(int day, int month, int year) : base(day, month)
    {
        Year = year;
    }

    public override void NextDay()
    {
        Day++;
        if (Day == 1)
        {
            Month++;
            if (Month == 1)
            {
                Year++;
            }
        }
    }

    public static bool operator >(PMDate date1, PMDate date2)
    {
        if (date1.Year > date2.Year)
        {
            return true;
        }
        else if (date1.Year == date2.Year)
        {
            if (date1.Month > date2.Month)
            {
                return true;
            }
            else if (date1.Month == date2.Month)
            {
                if (date1.Day > date2.Day)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool operator <(PMDate date1, PMDate date2)
    {
        return !(date1 > date2);
    }
}

public class Circle
{
    private double _radius;

    public double Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Radius must be positive.");
            }

            _radius = value;
        }
    }

    public double Area => Math.PI * Radius * Radius;

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override string ToString()
    {
        return $"Circle(Radius={Radius}, Area={Area})";
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (!(obj is Circle circle))
        {
            return false;
        }

        return Radius.Equals(circle.Radius);
    }

    public override int GetHashCode()
    {
        return Radius.GetHashCode();
    }

    public int CompareTo(Circle other)
    {
        return Area.CompareTo(other.Area);
    }
}

static class Program
{
    static void Main(string[] args)
    {
        var c1 = new Circle(10);
        var c2 = new Circle(5);
        var c3 = new Circle(15);
        var c4 = new Circle(7);
        var c5 = new Circle(5);

        Console.WriteLine(c1);
        Console.WriteLine(c2);

        //Console.WriteLine(c1.Equals(c5));
        Console.WriteLine(c1.GetHashCode() == c2.GetHashCode());

        Console.WriteLine(c1.CompareTo(c2));

        Console.WriteLine(c1.Area);
        Console.WriteLine(c2.Area);

        List<Circle> circles = new List<Circle>() { c1, c2, c3, c4 };
        circles.Sort((a, b) => a.Area.CompareTo(b.Area));

        foreach (var circle in circles)
        {
            Console.WriteLine(circle);
        }

        Console.WriteLine("\n================================================================");

        PDate date1 = new PDate(15, 6);

        PDate date3 = new PDate(14, 7);

        Console.WriteLine($"Date1: {date1.Day}/{date1.Month}");
        date1.NextDay();
        Console.WriteLine($"Next day: {date1.Day}/{date1.Month}");

        PMDate date2 = new PMDate(28, 2, 2024);
        Console.WriteLine($"Date2: {date2.Day}/{date2.Month}/{date2.Year}");
        date2.NextDay();
        date2.NextDay();
        date2.NextDay();
        Console.WriteLine($"Next day: {date2.Day}/{date2.Month}/{date2.Year}");
    }
}