using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Lab11
{
    public static class Reflector
    {
        public static string GetAssemblyName(string className)
        {
            Type? type = Type.GetType(className);
            return type?.Assembly.FullName;
        }

        public static bool HasPublicConstructors(string className)
        {
            Type? type = Type.GetType(className);
            return type?.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any() ?? false;
        }

        public static IEnumerable<string> GetPublicMethods<T>()
        {
            Type type = typeof(T);
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(method => method.Name);
        }

        public static IEnumerable<string> GetFieldsAndProperties<T>()
        {
            Type? type = typeof(T);
            var fields = type.GetFields().Select(field => field.Name);
            var properties = type.GetProperties().Select(property => property.Name);
            return fields.Concat(properties);
        }

        public static IEnumerable<string> GetImplementedInterfaces<T>()
        {
            Type? type = typeof(T);
            return type.GetInterfaces().Select(interf => interf.Name);
        }

        public static IEnumerable<string> GetMethodsWithParameterType<T>(Type parameterType)
        {
            Type? type = typeof(T);
            return type.GetMethods()
                .Where(method => method.GetParameters().Any(param => param.ParameterType == parameterType))
                .Select(method => method.Name);
        }

        public static void Invoke<T>(T obj, string methodName, object[] parameters)
        {
            Type? type = typeof(T);
            MethodInfo? methodInfo = type.GetMethod(methodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(obj, parameters);
            }
            else
            {
                Console.WriteLine($"Метод {methodName} не найден в классе {type.Name}.");
            }
        }
        public static T Create<T>() where T : new()
        {
            return new T();
        }

        public static void WriteClassInfoToFile<T>(string filePath, string className)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                Type type = typeof(T);
                writer.WriteLine($"Имя сборки, в которой определен класс '{className}': {GetAssemblyName(className)}");
                writer.WriteLine($"Есть ли публичные конструкторы класса '{className}': {HasPublicConstructors(className)}");

                writer.WriteLine($"Общедоступные публичные методы класса '{className}':");
                IEnumerable<string> publicMethods = GetPublicMethods<T>();
                foreach (var method in publicMethods)
                {
                    writer.WriteLine(method);
                }

                writer.WriteLine($"Поля и свойства класса '{className}':");
                IEnumerable<string> fieldsAndProperties = GetFieldsAndProperties<T>();
                foreach (var fieldOrProperty in fieldsAndProperties)
                {
                    writer.WriteLine(fieldOrProperty);
                }

                writer.WriteLine($"Интерфейсы, реализованные классом '{className}':");
                IEnumerable<string> interfaces = GetImplementedInterfaces<T>();
                foreach (var interf in interfaces)
                {
                    writer.WriteLine(interf);
                }

                writer.WriteLine($"Методы класса '{className}' с заданным типом параметра:");
                IEnumerable<string> methodsWithParameterType = GetMethodsWithParameterType<T>(typeof(string));
                foreach (var method in methodsWithParameterType)
                {
                    writer.WriteLine(method);
                }
            }
        }
    }

    public class Person
    {
        public string? Name { get; }
        public int? age;
        public Person(string name) => Name = name;
        public Person(int name) { }
        public Person()
        {
            Name = "Sveta";
            age = 19;
        }

        public void Walk()
        {
            Console.WriteLine("Я гуляю");
        }
        public void Eat(string food) { }
        public void Sleep(int hour) { }

    }

    public class House
    {
        public string Address { get;  }
        public int NumberOfRooms { get; set; }

        public House(string address, int numberOfRooms)
        {
            Address = address;
            NumberOfRooms = numberOfRooms;
        }

        public void OpenDoor()
        {
            Console.WriteLine("Дверь  открыта");
        }

        public void CloseDoor(string close) {}
    }

    class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "Person.txt";
            string filePath1 = "House.txt";
            string filePath2 = "StandardTypesInfo.txt";
           
            Reflector.WriteClassInfoToFile<Person>(filePath, "Lab11.Person");
            Console.WriteLine("Информация о классе записана в файл");

            Person person = Reflector.Create<Person>();
            Console.WriteLine(person);

            Reflector.Invoke(person, "Walk", null);

            using (StreamReader reader = new StreamReader(filePath))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }

            Reflector.WriteClassInfoToFile<House>(filePath1, "Lab11.House");
            Console.WriteLine("Информация о классе записана в файл");

            using (StreamReader reader = new StreamReader(filePath1))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }

            Reflector.WriteClassInfoToFile<string>(filePath2, "System.String");
            Console.WriteLine("Информация о классе записана в файл");

            using (StreamReader reader = new StreamReader(filePath2))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }
        }
    }
}