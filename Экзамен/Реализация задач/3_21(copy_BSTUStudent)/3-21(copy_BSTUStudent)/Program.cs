using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_21
{
    public interface IClearnable
    {
        void Clearn();
    }
    public enum Specialization
    {
        POIT,
        ISIT,
        POIBMS,
        DEIVI
    }
    class BSTUStudent
    {
        public string name;
        public int group;
        public int course;
        public Specialization specialization;
        public int mark1, mark2, mark3, mark4;

        public BSTUStudent(string name, int group, int course, Specialization specialization, int mark1, int mark2, int mark3, int mark4)
        {
            this.name = name;
            this.group = group;
            this.course = course;
            this.specialization = specialization;
            this.mark1 = mark1;
            this.mark2 = mark2;
            this.mark3 = mark3;
            this.mark4 = mark4;
        }

        public static (int min, int max, int avr) Getmarks(BSTUStudent obj)
        {
            var result = (min: 0, max: 0, avr: 0);
            int[] nums = new int[4];
            nums[0] = obj.mark1;
            nums[1] = obj.mark2;
            nums[2] = obj.mark3;
            nums[3] = obj.mark4;
            result.max = nums.Max();
            result.min = nums.Min();
            result.avr = (int)nums.Average();
            return result;
        }
        public override string ToString()
        {
            return ("Имя " + name + " Группа " + group + " Курс " + course + " Специальность " + specialization + " Оценки " + mark1 + " " + mark2 + " " + mark3 + " " + mark4);
        }
    }
    class SGroup : IClearnable
    {
        ArrayList list = new ArrayList();
        public ArrayList GetList
        {
            get
            {
                return list;
            }
        }
        public void Add(object obj)
        {
            list.Add(obj);
        }
        public void Remove(object obj)
        {
            list.Remove(obj);
        }

        public void Clearn()
        {
            list.Clear();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BSTUStudent student1 = new BSTUStudent("Dmitry", 2, 5, Specialization.POIT, 6, 7, 8, 5);
            BSTUStudent student2 = new BSTUStudent("Andrey", 1, 3, Specialization.ISIT, 4, 5, 8, 8);
            BSTUStudent student3 = new BSTUStudent("Dasha", 3, 8, Specialization.POIBMS, 4, 4, 4, 9);
            BSTUStudent student4 = new BSTUStudent("Shyra", 4, 10, Specialization.DEIVI, 7, 7, 7, 7);

            var tuple = BSTUStudent.Getmarks(student1);
            Console.WriteLine(tuple);

            // Поиск через LINQ двух студентов с наибольшим средним баллом
            BSTUStudent[] students = new BSTUStudent[4];
            students[0] = student1;
            students[1] = student2;
            students[2] = student3;
            students[3] = student4;

            var result = (from student in students
                          orderby student.mark1 + student.mark2 + student.mark3 + student.mark4 descending
                          select student).Take(2);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            SGroup listic = new SGroup();
            listic.Add(student1);
            listic.Add(student2);
            listic.Add(student3);
            listic.Add(student4);

            foreach (BSTUStudent stud in listic.GetList)
            {
                Console.WriteLine(stud.name);
                Console.WriteLine(stud.group);
                Console.WriteLine(stud.specialization);
            }

            listic.Clearn();
            Console.WriteLine("Список очищен");
            foreach (BSTUStudent stud in listic.GetList)
            {
                Console.WriteLine(stud.name);
                Console.WriteLine(stud.group);
                Console.WriteLine(stud.specialization);
            }
        }
    }
}