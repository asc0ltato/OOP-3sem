namespace Exam
{
    interface IAction<T>
    {
        void Add(T obj);
        void Remove(T obj);
        void Clean();
        void Info();
    }

    class NullSizeCollection : Exception
    {
        public NullSizeCollection(string message) : base(message)
        {
            Console.WriteLine("Коллекция пуста");
        }
    }

    public class ExamCard<T> : IAction<T> where T : Student
    {
        List<T> list = new List<T>();
   
        public List<T> List
        {
            get { return list; }
            set { list = value; }
        }
        public void Add(T obj)
        {
            list.Add(obj);
        }
        public void Remove(T obj)
        {
            try
            {
                if (list.Count == 0)
                {
                    throw new NullSizeCollection("Коллекция пустая");
                }
                else list.Remove(obj);
            }
            catch (NullSizeCollection ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Clean()
        {
            try
            {
                if (list.Count == 0)
                {
                    throw new NullSizeCollection("Коллекция пустая");
                }
                else list.Clear();
            }
            catch (NullSizeCollection ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Info()
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

    }

    public class Student
    {
        public string Name;
        public string Subject;
        public int Mark;

        public Student(string name, string subject, int mark)
        {
            this.Name = name;
            this.Subject = subject;
            this.Mark = mark;
        }

        public override string ToString()
        {
            return $"Name: {Name} Subject: {Subject} Mark: {Mark}";
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student("Ivan", "Math", 5);
            Student student2 = new Student("Petr", "Physics", 3);
            Student student3 = new Student("Sidor", "OAP", 7);

            ExamCard<Student> examCard = new ExamCard<Student>();
            examCard.Add(student1);
            examCard.Add(student2);
            examCard.Add(student3);
            examCard.Info();

            var query_4 = examCard.List.Where(student => student.Mark >= 4);
            Console.WriteLine("Students with mark >= 4");
            Console.WriteLine(query_4.Count());
            foreach (var item in query_4)
            {
                Console.WriteLine(item);
            }

            double average = query_4.Average(student => student.Mark);
            Console.WriteLine("Average mark: " + average);

            Random random = new Random();
            foreach (var item in query_4)
            {
                item.Mark += random.Next(1, 3);
                Console.WriteLine(item);
            }
        }
    }
}