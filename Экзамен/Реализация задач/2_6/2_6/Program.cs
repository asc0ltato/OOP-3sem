
using System.Text.Json;

namespace _2_6
{
    public enum Status
    {
        Signin,
        Signout
    }
    [Serializable]
    class User: IComparable
    {
        private string email;
        public string password;
        public Status status;

        public override string ToString()
        {
            return "Email: " + email + " Password: " + password + " Status: " + status;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            User other = (User)obj;
            return this.email == other.email;
        }

        public override int GetHashCode()
        {
            return email.GetHashCode();
        }

        public int CompareTo(object? obj)
        {
            if (obj == null)
                return 1;
            User other = (User)obj;
            if (other == null)
                return 1;
            else
                return email.CompareTo(other.email);
        }

        /* public int CompareTo(object? obj)
         {
             if (obj == null)
                 return 1;
             User other = (User)obj;
             if (other == null)
                 return 1;
             else
             {
                 int emailComp = email.CompareTo(other.email);
                 if (emailComp != 0)
                 {
                     return emailComp;
                 }
                 else 
                 {
                     return password.CompareTo(other.password);
                 }
             }
         }*/

        public User(string email, string password, Status status)
        {
            this.email = email;
            this.password = password;
            this.status = status;
        }
    }

    [Serializable]
    class WebNet
    {
        public LinkedList<User> users = new LinkedList<User>();

        public void AddUser(User user)
        {
            users.AddLast(user);
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
        }

        public void PrintUsers()
        {
            Console.WriteLine("Users:");
            foreach (User user in users)
            {
                Console.WriteLine(user);
            }
        }
    }
    


    class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User("dimatruba2004@yandex.ru", "123456", Status.Signin);
            User user2 = new User("desrvdgf@mail.ru", "123456", Status.Signout);
            User user3 = new User("dimatruba2004@yandex.ru", "123456345", Status.Signin);

            Console.WriteLine(user1);
            Console.WriteLine(user2);
            Console.WriteLine(user3);

            Console.WriteLine(user1.Equals(user2));
            Console.WriteLine(user1.Equals(user3));

            Console.WriteLine(user1.CompareTo(user3));
            Console.WriteLine(user1.CompareTo(user2));

            WebNet github = new WebNet();
            github.AddUser(user1);
            github.AddUser(user2);
            github.AddUser(user3);
            github.PrintUsers();

            var users = github.users.Where(user => user.status == Status.Signin);
            Console.WriteLine("Users with status Signin:");
            foreach (User user in users)
            {
                Console.WriteLine(user);
            }

           /* var usersS = github.users.Where(u => u.password.Length < 8 && u.password.All(c => c >= '0' && c <= '9'));
            Console.WriteLine("Users with password:");
            foreach (User user in usersS)
            {
                Console.WriteLine(user);
            }*/

            string json = JsonSerializer.Serialize(github.users);
            File.WriteAllText("json.json", json);
        }
    }
}