using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    partial class House
    {
        public readonly int ID;
        public const string buildingOfType = "Многоквартирный дом";
        private static int idCounter = 1;
        private int apartmentNumber;
        private double area;
        private int floor;
        private int roomCount;
        private string street;
        private string buildingType;
        private int yearsOfExploitation;
        private static int numberOfObjects = 0;

        public int ApartmentNumber
        {
            get => apartmentNumber;
            private set
            {
                if (value > 0)
                {
                    apartmentNumber = value;
                }
                else
                {
                    throw new Exception("Номер квартиры должен быть положительным");
                }
            }
        }

        public double Area
        {
            get => area;
            set
            {
                if (value > 0)
                {
                    area = value;
                }
                else
                {
                    throw new Exception("Площадь должна быть положительной");
                }
            }
        }

        public int Floor
        {
            get => floor;
            set
            {
                if (value >= 1)
                {
                    floor = value;
                }
                else
                {
                    throw new Exception("Этаж должен быть не меньше 1");
                }
            }
        }

        public int RoomCount
        {
            get => roomCount;
            set
            {
                if (value >= 0)
                {
                    roomCount = value;
                }
                else
                {
                    throw new Exception("Количество комнат должно быть не меньше 0");
                }
            }
        }

        public string Street
        {
            get => street;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    street = value;
                }
                else
                {
                    throw new Exception("Улица не может быть пустой");
                }
            }
        }

        public string BuildingType
        {
            get => buildingType;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    buildingType = value;
                }
                else
                {
                    throw new Exception("Тип здания не может быть пустым");
                }
            }
        }

        public int YearsOfExploitation
        {
            get => yearsOfExploitation;
            set
            {
                if (value >= 0)
                {
                    yearsOfExploitation = value;
                }
                else
                {
                    throw new Exception("Срок эксплуатации должен быть не меньше 0");
                }
            }
        }

        public static int NumberOfObject
        {
            get => numberOfObjects;
        }

        public int HouseID
        {
            get { return ID; }
        }
    }
}
