﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_4
{
    public interface IScore
    {
        int Amount { get; set; }
        int AddMoney();
        int RemMoney();
    }

    abstract class Human
    {
        DateTime Date { get; set; }
    }

    class Person : Human, IScore
    {
        public static int countobj;
        public string Name;
        public string SecName;
        public DateTime Date;
        public int amount;
        public int Amount { get; set; }

        static Person()
        {
            countobj = 0;
        }

        public Person(string name, string secName, DateTime date, int amount)
        {
            Name = name;
            SecName = secName;
            Date = date;
            Amount = amount;
            countobj++;
        }

        public int AddMoney()
        {
            int count = Convert.ToInt32(Console.ReadLine());
            Amount += count;
            return Amount;
        }

        public int RemMoney()
        {
            int count = Convert.ToInt32(Console.ReadLine());
            Amount -= count;
            return Amount;
        }

        public static void CountobjToString()
        {
            Console.WriteLine("Создано {0} объектов Person", countobj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Person other = (Person)obj;
            return this.Date == other.Date;
        }

        public override string ToString()
        {
            return ("Имя " + Name + " Фамилия " + SecName + " количество " + Amount + " дата " + Date);
        }
    }

    class Bank : List<Person>
    {
        public void show()
        {
            Bank central = new Bank();

            foreach (Person item in central)
            {

                Console.WriteLine(item.Name);
                Console.WriteLine(item.SecName);
                Console.WriteLine(item.Amount);

            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateTime time1 = new DateTime(2001, 5, 20);
            DateTime time2 = new DateTime(2002, 12, 21);
            DateTime time3 = new DateTime(2003, 6, 24);
            DateTime time4 = new DateTime(2004, 7, 22);

            Person person1 = new Person("Arsenii", "Mingazov", time1, 200);
            Person person2 = new Person("Dima", "Radovid", time2, 200);
            Person person3 = new Person("Jorj", "Geraklit", time3, 200);
            Person person4 = new Person("Salam", "Abdul", time4, 200);
            Console.WriteLine(person1.ToString());

            Console.WriteLine(person1.AddMoney());
            Console.WriteLine(person1.ToString());
            Console.WriteLine(person1.RemMoney());
            Console.WriteLine(person1.ToString());
            Person.CountobjToString();
            Console.WriteLine(person1.Equals(person2));
            Console.WriteLine(person1.Equals(person3));
            Console.WriteLine(person1.Equals(person4));
            Console.WriteLine(person1.Equals(person1));

            Bank belarus = new Bank();
            belarus.Add(person1);
            belarus.Add(person2);
            belarus.Add(person3);

            Bank alfa = new Bank();
            alfa.Add(person4);
            alfa.Add(person2);
            alfa.Add(person3);

            Bank central = new Bank();
            central.Add(person1);
            central.Add(person3);
            central.show();

            DateTime aaa = new DateTime(2003, 6, 24);
            Task<List<Person>> task1 = new Task<List<Person>>(() => belarus.Where(x => x.Date == aaa).ToList());
            task1.Start();
            Task<List<Person>> task2 = new Task<List<Person>>(() => alfa.Where(x => x.Date == aaa).ToList());
            task2.Start();
            Task<List<Person>> task3 = new Task<List<Person>>(() => central.Where(x => x.Date == aaa).ToList());
            task3.Start();
            foreach (var p in task1.Result)
            {
                Console.WriteLine(p.Name);
            }
            foreach (var p in task2.Result)
            {
                Console.WriteLine(p.Name);
            }
            foreach (var p in task3.Result)
            {
                Console.WriteLine(p.Name);
            }
        }
    }
}