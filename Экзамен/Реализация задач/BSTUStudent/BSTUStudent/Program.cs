using System.Collections;

namespace _3_21
{
    class Program
    {
        interface ICleanable
        {
            void Clean();
        }

        public enum Spec
        {
            poit,
            isit,
            mobile
        }

        class Stud
        {
            public string? Name { get; set; }
            public int Group { get; set; }
            public int Course { get; set; }
            public Spec specialization;
            public int Exam1, Exam2, Exam3;

            public Stud(string name, int group, int course, Spec spec, int mark1, int mark2, int mark3)
            {
                Name = name;
                Group = group;
                Course = course;
                specialization = spec;
                Exam1 = mark1;
                Exam2 = mark2;
                Exam3 = mark3;
            }

            public (int, int, double) Marks()
            {
                int[] arr = new int[] { Exam1, Exam2, Exam3 };
                int max = arr.Max();
                int min = arr.Min();
                double avg = Math.Round(arr.Average(), 2);
                return (max, min, avg);
            }
        }

        class Group : ICleanable
        {
            List<Stud> groups = new List<Stud>();

            public List<Stud> GetStudents()
            {
                return groups;
            }

            public void Add(Stud student)
            {
                groups.Add(student);
            }
            public void Print()
            {
                foreach (Stud student in groups)
                {
                    Console.WriteLine($"{student.Name} {student.Course} {student.Group}");
                }
            }

            public void Clean()
            {
                groups.Clear();
            }
        }

        static void Main(string[] args)
        {
            Stud student1 = new Stud("Sveta", 2, 2, Spec.isit, 9, 9, 5);
            Stud student2 = new Stud("Lera", 2, 2, Spec.isit, 9, 7, 10);
            Stud student3 = new Stud("Sneg", 2, 2, Spec.isit, 9, 7, 10);
            Stud student4 = new Stud("Liza", 2, 2, Spec.mobile, 5, 10, 10);
            Stud student5 = new Stud("Anya", 2, 2, Spec.mobile, 5, 10, 8);
            Console.WriteLine(student1.specialization);
            Console.WriteLine(student1.Marks());

            Group group = new Group();
            group.Add(student1);
            group.Add(student2);
            group.Add(student3);
            group.Add(student4);
            group.Add(student5);
            group.Print();


            var maxAvgBySpecialization = group.GetStudents().GroupBy(s => s.specialization).Select(g => g.OrderByDescending(s => s.Marks().Item3).First());
            Console.WriteLine("\nСтуденты, набравшие максимальный средний балл по каждой специализации:");
            foreach (var stud in maxAvgBySpecialization)
            {
                Console.WriteLine($"{stud.Name} - {stud.specialization} - {stud.Marks().Item3}");
            }

            try
            {
                group.Clean();
                group.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
