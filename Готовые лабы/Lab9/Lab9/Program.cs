using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Lab9
{
    interface IList<T>
    {
        int Count { get; } 
        void Add(T item); 
        void Clear(); 
        bool Contains(T item); 
        void CopyTo(T[] array, int arrayIndex); 
        IEnumerator<T> GetEnumerator(); 
        int IndexOf(T item); 
        void Insert(int index, T item); 
        bool Remove(T item); 
        void RemoveAt(int index); 
    }
    public class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }

        public Software(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Software otherSoftware)
            {
                return this.Name == otherSoftware.Name && this.Version == otherSoftware.Version;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Name + Version).GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name} (Version {Version})";
        }
    }

    public class SoftwareCollection : IList<Software>
    {
        private List<Software> softwareList = new List<Software>();

        public Software this[int index]
        {
            get { return softwareList[index]; }
            set { softwareList[index] = value; }
        }

        public int Count => softwareList.Count;

        public void Add(Software item)
        {
            softwareList.Add(item);
        }

        public void Clear()
        {
            softwareList.Clear();
        }

        public bool Contains(Software item)
        {
            return softwareList.Contains(item);
        }

        public void CopyTo(Software[] array, int arrayIndex)
        {
            softwareList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Software> GetEnumerator()
        {
            return softwareList.GetEnumerator();
        }

        public int IndexOf(Software item)
        {
            return softwareList.IndexOf(item);
        }

        public void Insert(int index, Software item)
        {
            softwareList.Insert(index, item);
        }

        public bool Remove(Software item)
        {
            return softwareList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            softwareList.RemoveAt(index);
        }

        public void PrintSoftwareCollection()
        {
            foreach (var software in softwareList)
            {
                Console.WriteLine(software);
            }
        }
    }

    class Programm
    {
        public static void Main()
        {
            SoftwareCollection sc = new SoftwareCollection();

            sc.Add(new Software("Software1", "1"));
            sc.Add(new Software("Software2", "2"));
            sc.Add(new Software("Software3", "3"));

            Console.WriteLine("Список программного обеспечения после добавления:");
            sc.PrintSoftwareCollection();
            Console.WriteLine();

            var softwareToRemove = new Software("Software2", "2");
            sc.Remove(softwareToRemove);

            Console.WriteLine("Список ПО после удаления Software2 (2):");
            sc.PrintSoftwareCollection();
            Console.WriteLine();

            var softwareToFind = new Software("Software1", "1");
            var index = sc.IndexOf(softwareToFind);
            if (index != -1)
            {
                Console.WriteLine($"Найден элемент {softwareToFind} в позиции {index}");
            }
            else
            {
                Console.WriteLine($"Элемент {softwareToFind} не найден");
            }
            Console.WriteLine();

            SortedList<string, Software> sc2 = new SortedList<string, Software>();

            sc2.Add("Adobe Photoshop", new Software("Adobe Photoshop", "CC 2023"));
            sc2.Add("Microsoft Word", new Software("Microsoft Word", "Office 2023"));
            sc2.Add("Microsoft Visual Studio", new Software("Microsoft Visual Studio", "2023"));

            Console.WriteLine("Первая коллекция (SortedList):");
            foreach (var software in sc2)
            {
                Console.WriteLine($"Key: {software.Key}, Value: {software.Value}");
            }

            int n = 2;
            for (int i = 0; i < n; i++)
            {
                sc2.RemoveAt(0);
            }

            sc2.Add("Microsoft Visual Studio Code", new Software("Microsoft Visual Studio Code", "2023"));

            Dictionary<string, Software> sc3 = new Dictionary<string, Software>();
            foreach (var software in sc2)
            {
                sc3.Add(software.Key, software.Value);
            }

            Console.WriteLine("\nВторая коллекция (Dictionary):");
            foreach (var software in sc3)
            {
                Console.WriteLine($"Key: {software.Key}, Value: {software.Value}");
            }

            var searchValue = new Software("Microsoft Word", "Office 2023");
            bool found = sc3.ContainsValue(searchValue);
            Console.WriteLine($"\nНайдено: {found}");

            ObservableCollection<Software> observableSoftwareCollection = new ObservableCollection<Software>(sc3.Values);
            observableSoftwareCollection.CollectionChanged += OnCollectionChanged;

            observableSoftwareCollection.Add(new Software("Adobe Illustrator", "CC 2023"));
            var softwareToRemove_ = new Software("Visual Studio", "2023");
            observableSoftwareCollection.Remove(softwareToRemove_);
        }

        private static void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine($"Добавлен новый объект");
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine($"Удален объект");
            }
        }
    }
}