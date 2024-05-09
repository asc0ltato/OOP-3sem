using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    partial class House
    {
        private House(int id)
        {
            ID = id;
            apartmentNumber = 1;
            area = 1;
            floor = 1;
            roomCount = 1;
            street = "Unknown";
            BuildingType = buildingOfType;
            YearsOfExploitation = 0;
            numberOfObjects++;
        }

        public static House CreateHouseWithID(int id)
        {
            return new House(id);
        }

        static House()
        {
            numberOfObjects = 0;
            Console.WriteLine($"Количество объектов: {numberOfObjects}");
        }

        public House(int apartmentNumber = 1, double area = 10.2, int floor = 4,
        int roomCount = 2, string street = "Partizanskaya", string buildingType =
         buildingOfType, int yearsOfExploitation = 40)
        {
            ID = idCounter++;
            ApartmentNumber = apartmentNumber;
            Area = area;
            Floor = floor;
            RoomCount = roomCount;
            Street = street;
            BuildingType = buildingType;
            YearsOfExploitation = yearsOfExploitation;
            numberOfObjects++;
        }

        public House()
        {
            ID = idCounter++;
            ApartmentNumber = 10;
            Area = 16.8;
            Floor = 3;
            RoomCount = 5;
            Street = "Lololoshka";
            BuildingType = buildingOfType;
            YearsOfExploitation = 20;
            numberOfObjects++;
        }

        public House(int id, int apartmentNumber, double area, int floor, int roomCount, string street, string buildingType, int yearsOfExploitation)
        {
            if (apartmentNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("Номер квартиры должен быть положительным числом.");
            }
            if (area <= 0)
            {
                throw new ArgumentOutOfRangeException("Площадь должна быть положительной.");
            }
            if (floor <= 0)
            {
                throw new ArgumentOutOfRangeException("Этаж должен быть положительным числом.");
            }
            if (roomCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Количество комнат должно быть положительным числом.");
            }
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentException("Улица не может быть пустой или содержать только пробелы.");
            }
            if (yearsOfExploitation < 0)
            {
                throw new ArgumentOutOfRangeException("Срок эксплуатации не может быть отрицательным.");
            }
            this.ID = id;
            this.ApartmentNumber = apartmentNumber;
            this.Area = area;
            this.Floor = floor;
            this.RoomCount = roomCount;
            this.Street = street;
            this.BuildingType = buildingType;
            this.YearsOfExploitation = yearsOfExploitation;
            numberOfObjects++;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Номер квартиры: {ApartmentNumber}, Площадь: {Area} кв. м, Этаж: {Floor}, Количество комнат: {RoomCount}, Улица: {Street}, Тип здания: {BuildingType}, Срок эксплуатации: {YearsOfExploitation} лет";
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public string CalculateAgeAndRenovationNeed()
        {
            int buildingAge = DateTime.Now.Year - YearsOfExploitation;
            if (buildingAge >= 2015)
            {
                return $"Здание {buildingAge} года постройки(ему {yearsOfExploitation} лет) и не требует капитального ремонта";
            }
            else
            {
                return $"Здание {buildingAge} года постройки(ему {yearsOfExploitation} лет) и требует капитального ремонта";
            }
        }
        public static void ShowStatic()
        {
            Console.WriteLine("Информация о классе House:");
            Console.WriteLine($"Тип здания по умолчанию: {buildingOfType}");
            Console.WriteLine($"Количество объектов: {numberOfObjects}");
        }

        public static House[] fourRoomApartments(ref House[] arr, int RoomCount, out int count)
        {
            count = arr.Count(house => house.RoomCount == RoomCount);
            return arr.Where(house => house.RoomCount == RoomCount).ToArray();
        }

        public static House[] twoRoomApartmentsOn2to5Floors(ref House[] arr, int RoomCount, int Floor1, int Floor2, out int count)
        {
            count = arr.Count(house => house.RoomCount == RoomCount && house.Floor >= Floor1 && house.Floor <= Floor2);
            return arr.Where(house => house.RoomCount == RoomCount && house.Floor >= Floor1 && house.Floor <= Floor2).ToArray();
        }
    }
}