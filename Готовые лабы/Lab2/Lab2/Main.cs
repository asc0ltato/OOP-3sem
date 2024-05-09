using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            House house1 = new House(1);
            House house2 = new House();
            House house3 = House.CreateHouseWithID(3); 
            House house4 = new House(4, 404, 110.7, 1, 5, "Cedar Lane", "Частный дом", 3);
            House house5 = new House(5, 505, 95.3, 3, 4, "Mama Street", "Многоквартирный", 12);
            House house6 = new House(6, 202, 85.0, 2, 4, "Olala Avenue", "Частный дом", 10);

            House[] houses = { house1, house2, house3, house4, house5, house6 };

            Console.WriteLine("Информация о домах:");
            foreach (House house in houses)
            {
                Console.WriteLine(house);
            }

            Console.WriteLine("\nИнформация о классе House:");
            House.ShowStatic();

            Console.WriteLine("\nHouse: {0}\n" + "Сравнение: {1}\n" + "Свойства вывода / методы: house2.Street = {2}; house2.GetHashCode() = {3}\n", house1.GetType(), house4.Equals(house5), house2.Street, house2.GetHashCode());

            House[] fourRoomHouses = House.fourRoomApartments(ref houses, 4, out int fourRoomCount);
            Console.WriteLine($"\nКоличество квартир с 4 комнатами: {fourRoomCount}");

            House[] twoRoomHouses = House.twoRoomApartmentsOn2to5Floors(ref houses, 2, 2, 5, out int twoRoomCount);
            Console.WriteLine($"\nКоличество квартир с 2 комнатами на этаже с 2 по 5: {twoRoomCount}");

            Console.WriteLine("\nc) Расчет возраста домов и определение необходимости в капитальном ремонте:");
            foreach (House house in houses)
            {
                string renovationStatus = house.CalculateAgeAndRenovationNeed();
                Console.WriteLine($"Дом с ID {house.ID}: {renovationStatus}");
            }

            var anonymousHouse = new
            {
                ApartmentNumber = 101,
                Area = 120.5,
                Floor = 7,
                RoomCount = 3,
                Street = "Sunset Boulevard",
                BuildingType = "Многоквартирный",
                YearsOfExploitation = 5
            };

            Console.WriteLine("\nИнформация об анонимном доме:");
            Console.WriteLine($"Номер квартиры: {anonymousHouse.ApartmentNumber}");
            Console.WriteLine($"Площадь: {anonymousHouse.Area}");
            Console.WriteLine($"Этаж: {anonymousHouse.Floor}");
            Console.WriteLine($"Количество комнат: {anonymousHouse.RoomCount}");
            Console.WriteLine($"Улица: {anonymousHouse.Street}");
            Console.WriteLine($"Тип здания: {anonymousHouse.BuildingType}");
            Console.WriteLine($"Срок эксплуатации: {anonymousHouse.YearsOfExploitation} лет");
        }
    }
}