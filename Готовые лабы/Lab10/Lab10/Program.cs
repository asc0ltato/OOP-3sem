using System;
using System.ComponentModel;

partial class House
{
    int apartmentNumberBase = 1;
    int floorBase = 1;
    int squareBase = 1;
    int numberOfRoomsBase = 1;
    static int counter = 0;
    static int currentYear;


    public readonly int id;
    public int apartmentNumber
    {
        set
        {
            if (apartmentNumber < 0)
                Console.WriteLine("Номер квартиры должен быть положительный");
            else
                apartmentNumberBase = value;
        }

        get
        {
            return apartmentNumberBase;
        }
    }
    public int square
    {
        set
        {
            if (value <= 0)
                Console.WriteLine("Площадь должен быть положительной");
            else
                squareBase = value;
        }

        get
        {
            return squareBase;
        }
    }
    public int floor
    {
        set
        {
            if (value < 1 || value >= 300)
                Console.WriteLine("Этаж должен быть в диапозоне от 1 до 300");
            else
                floorBase = value;
        }

        get
        {
            return floorBase;
        }
    }
    public int numberOfRooms
    {
        set
        {
            if (numberOfRoomsBase > 1)
                Console.WriteLine("Количество комнат должно быть положительным: ");
            else
                numberOfRoomsBase = value;
        }

        get
        {
            return numberOfRoomsBase;
        }
    }
    public string adress { get; set; }
    public string buildingType { get; set; }
    public int lifetime { get; set; }



    public House(int floor, int numberOfRooms, string adress, string buildingType, int apartmentNumber = 2, int square = 135, int lifetime = 2025)
    {
        counter++;
        this.apartmentNumber = apartmentNumber;
        this.square = square;
        this.floor = floor;
        this.numberOfRooms = numberOfRooms;
        this.adress = adress;
        this.buildingType = buildingType;
        this.lifetime = lifetime;
        id = (apartmentNumber * adress.Length) / numberOfRooms * square / floor;
    }
    public House()
    {
        counter++;
        apartmentNumber = 1;
        square = 135;
        floor = 3;
        numberOfRooms = 1;
        adress = "Peresedenskay";
        buildingType = "mansion";
        lifetime = 2024;
        id = GetHashCode();
    }
    public House(int apartmentNumber, int square, int floor, int numberOfRooms, string adress, string buildingType)
    {
        counter++;
        this.apartmentNumber = apartmentNumber;
        this.square = square;
        this.floor = floor;
        this.numberOfRooms = numberOfRooms;
        this.adress = adress;
        this.buildingType = buildingType;
        id = GetHashCode();
    }
    static House()
    {
        currentYear = DateTime.Now.Year;
    }


    public void Overhaul(ref int lifetime, out int whenOverhaul)
    {
        whenOverhaul = lifetime - currentYear;
    }
    static public void PrintInfoAboutClass()
    {
        Console.WriteLine($"Это класс House\n");
    }
    public void WriteStreet()
    {

        Console.WriteLine($"Id - {id} \n" +
                          $"Номер квартиры - {apartmentNumber} \n" +
                          $"Площадь - {square} \n" +
                          $"Этаж - {floor}  \n" +
                          $"Количество комнат - {numberOfRooms}  \n" +
                          $"Улица - {adress}  \n" +
                          $"Тип здания - {buildingType}  \n" +
                          $"Cрок эксплуатации до {lifetime}");
    }


    public override int GetHashCode()
    {
        return (apartmentNumber * adress.Length) / numberOfRooms * square / floor;
    }
    public override bool Equals(object obj)
    {
        return false;
    }
    public override string ToString()
    {
        return $"{adress}";
    }
}

public class Person
{
    public string name;
    public string adress;

    public Person( string name, string ad)
    {
        this.name = name;
        adress = ad;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-------------Задание1---------------");
        string[] months = { "June", "July", "May", "December", "January", "August", "September", "October", "March", "April", "November", "February" };
        Console.WriteLine("Введите число, которое означает длину строки:");
        int length = Convert.ToInt32(Console.ReadLine());

        var monthsWithLengthN = months.Where(month => month.Length == length);
        Console.WriteLine("Месяцы с длиной строки равной " + length + ":");
        foreach (var month in monthsWithLengthN)
        {
            Console.WriteLine(month);
        }
        Console.WriteLine("----------------------------");
        var winterAndSummer = months.Where(n => n == "June" || n == "July" || n == "August" || n == "December" || n == "January" || n == "February");
        Console.WriteLine("Летние и зимние месяцы:");
        foreach (string month in winterAndSummer)
        {
            Console.WriteLine(month);
        }
        Console.WriteLine("----------------------------");
        var orderedMonths = from p in months
                            orderby p
                            select p;
        Console.WriteLine("Месяцы в алфавитном порядке:");
        foreach (string month in orderedMonths)
        {
            Console.WriteLine(month);
        }
        Console.WriteLine("----------------------------");
        var containsU = from p in months
                        where p.Contains('u') && p.Length >= 4
                        select p;
        int count = containsU.Count();
        Console.WriteLine("Количество месяцев, содержащих 'u' и имеющих длину не менее 4-х: " + count);

        Console.WriteLine("\n-------------Задание2---------------");
        List<House> listOfHouses = new List<House>()
        {
            new House(5, 35, 4, 4, "Lenina", "flat"),
            new House(1, 55, 3, 1, "Sverdlova", "dom"),
            new House(25, 100, 3, 2, "Bobruyskay", "house"),
            new House(35, 25, 1, 1, "Bobruyskay", "flat"),
            new House(6, 45, 5, 2, "Sverdlova", "flat"),
            new House(5, 20, 1, 1, "Lenina", "flat"),
            new House(15, 25, 1, 3, "Ozernoe", "house"),
            new House(25, 35, 3, 4, "Ozernoe", "house"),
            new House(26, 65, 3, 4, "Bobruyskay", "flat"),
            new House(17, 37, 2, 3, "Ozernoe", "flat"),
            new House(5, 35, 4, 1, "Nemiga", "house"),
        };

        var cuurentRooms = from p in listOfHouses
                           where p.numberOfRooms == 4
                           select p;
        Console.WriteLine("Cписок квартир, имеющих заданное число комнат(4): ");
        foreach (object si in cuurentRooms)
        {
            House? obj = si as House;
            Console.WriteLine($"Дом {obj.apartmentNumber}, улица {obj.adress} - количество комнат {obj.numberOfRooms}");
        }
        Console.WriteLine("----------------------------");

        var streetAndBuilding = listOfHouses.Where(p => (p.adress == "Lenina" && p.buildingType == "flat")).Take(5);
        Console.WriteLine("Пять первых квартир на заданной улице(Lenina) заданного дома(flat): ");
        foreach (object si in streetAndBuilding)
        {
            House? obj = si as House;
            Console.WriteLine($"Дом {obj.apartmentNumber}, улица {obj.adress} - количество комнат {obj.numberOfRooms}");
        }
        Console.WriteLine("----------------------------");

        var street = listOfHouses.Where(p => p.adress == "Bobruyskay" && p.buildingType == "flat");
        int countStreet = street.Count();
        Console.WriteLine("Количество квартир на определенной улице: " + countStreet);

        Console.WriteLine("----------------------------");

        var listOfflats = listOfHouses.Where(p => (p.numberOfRooms == 2 && (p.floor < 5 && p.floor > 1)));
        Console.WriteLine("Список квартир, имеющих заданное число комнат(2) и расположенных на этаже, который находится в заданном промежутке(от 1 до 5): ");
        foreach (object si in listOfflats)
        {
            House? obj = si as House;
            Console.WriteLine($"Дом {obj.apartmentNumber}, улица {obj.adress} - количество комнат {obj.numberOfRooms}");
        }
        Console.WriteLine("\n-------------Задание3---------------");

        var selectedHouses = listOfHouses
        .Where(house => house.square >= 35)
        .OrderBy(house => house.numberOfRooms)
        .Select(house => new { house.apartmentNumber, house.numberOfRooms })
        .GroupBy(house => house.numberOfRooms)
        .Select(group => new {NumberOfRooms = group.Key, TotalApartments = group.Count()})
        .Sum(result => result.TotalApartments);

        Console.WriteLine($"Общее количество комнат в домах с площадью не менее 35: {selectedHouses}");
        Console.WriteLine("\n-------------Задание4---------------");

        var persons = new List<Person>()
        {
            new Person("Anna", "Nemiga"),
            new Person("Nastya", "Ozernoe"),
            new Person("John", "Bobruyskay"),
            new Person("David", "Ozernoe")
        };

        var targetStreet = "Ozernoe";

        var personsOnTargetStreet = (from person in persons
                                     join house in listOfHouses on person.adress equals house.adress
                                     where person.adress == targetStreet
                                     select new { person.name, person.adress })
                                    .Distinct();

        foreach (var person in personsOnTargetStreet)
        {
            Console.WriteLine($"Имя: {person.name}, Адрес: {person.adress}");
        }
    }
}