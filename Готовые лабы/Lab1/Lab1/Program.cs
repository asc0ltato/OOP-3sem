using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Programm
    {
        public static void Main(string[] args)
        {
            /*1.Типы.
            а)Определите переменные всех возможных примитивных типов С# и проинициализируйте их. Осуществите ввод и вывод их значений используя консоль.*/

            bool boolValue = true;
            byte byteValue = 255; 
            sbyte sbyteValue = 127; 
            char charValue = 'A'; 
            decimal decimalValue = decimal.MaxValue; 
            double doubleValue = double.MaxValue; 
            float floatValue = float.MaxValue; 
            int intValue = 2147483647; 
            uint uintValue = 4294967295; 
            nint nintValue = nint.MaxValue; 
            nuint nuintValue = nuint.MaxValue;
            long longValue = 9223372036854775807;
            ulong ulongValue = 18446744073709551615; 
            short shortValue = 32767; 
            ushort ushortValue = 65535; 

            Console.WriteLine($"bool: {boolValue}");
            Console.WriteLine($"byte: {byteValue}");
            Console.WriteLine($"sbyte: {sbyteValue}");
            Console.WriteLine($"char: {charValue}");
            Console.WriteLine($"decimal: {decimalValue}");
            Console.WriteLine($"double: {doubleValue}");
            Console.WriteLine($"float: {floatValue}");
            Console.WriteLine($"int: {intValue}");
            Console.WriteLine($"uint: {uintValue}");
            Console.WriteLine($"nint: {nintValue}");
            Console.WriteLine($"nuint: {nuintValue}");
            Console.WriteLine($"long: {longValue}");
            Console.WriteLine($"ulong: {ulongValue}");
            Console.WriteLine($"short: {shortValue}");
            Console.WriteLine($"ushort: {ushortValue}");

            //b)Выполните 5 операций явного и 5 неявного приведения  типов данных.Изучите возможности класса Convert.

            Console.WriteLine("\n" + "Явное приведение");

            double firstDouble = 15.3;
            decimal firstDecimal = (decimal)(firstDouble);
            Console.WriteLine($"firstDecimal = {firstDecimal}");

            int firstIntValue = 16;
            byte secondIntValue = (byte)(firstIntValue);
            Console.WriteLine($"secondIntValue = {secondIntValue}");

            bool isTrue = true;
            int intFromBool = Convert.ToInt32(isTrue);
            Console.WriteLine($"intFromBool = {intFromBool}");

            float numberFloat = 2.345f;
            short numberShort = (short)(numberFloat);
            Console.WriteLine($"numberInt = {numberShort}");

            float firstFloatValue = 4.952f;
            decimal secondDecimalValue = (decimal)(firstFloatValue);
            Console.WriteLine($"secondDoubleValue = {secondDecimalValue}");

            Console.WriteLine("\n" + "Неявное приведение");

            byte b = 10;
            short s = b;
            int i = b;
            long l = b;
            float f = b;
            decimal d = b;
            Console.WriteLine($"{b}-{b.GetType()}\n{s}-{s.GetType()}\n{i}-{i.GetType()}\n{l}-{l.GetType()}\n{f}-{f.GetType()}\n{d}-{d.GetType()}");

            //с)Выполните упаковку и распаковку значимых типов.

            Console.WriteLine("\n" + "Упаковка и распаковка");
            int firstNumber = 5;
            object obj = firstNumber;
            Console.WriteLine(obj);
            int secondNumber = (int)obj;
            Console.WriteLine(secondNumber + " " + secondNumber.GetType());

            //d)Продемонстрируйте работу с неявно типизированной переменной.

            Console.WriteLine("\n" + "Неявно типизированные переменные");

            var firstVar = 10;
            Console.WriteLine($"firstvar = {firstVar} - {firstVar.GetType()}");
            var secondVar = 6.8f;
            Console.WriteLine($"secondvar = {secondVar} - {secondVar.GetType()}");
            var thirdVar = "Hello, mother";
            Console.WriteLine($"thirdvar = {thirdVar} - {thirdVar.GetType()}");
            var fourthVar = 'A';
            Console.WriteLine($"fourthvar = {fourthVar} - {fourthVar.GetType()}");
            var fifthVar = false;
            Console.WriteLine($"fifthvar = {fifthVar} - {fifthVar.GetType()}");

            //e)Продемонстрируйте пример работы с Nullable переменной 

            Console.WriteLine("\n" + "Работа с Nullable переменной");
            int? nullableInt = null;
            Console.WriteLine(nullableInt);
            int y = nullableInt ?? 2;
            Console.WriteLine(y);
            nullableInt = 10;
            Console.WriteLine(nullableInt);
            Console.WriteLine(nullableInt.HasValue);
            Console.WriteLine(nullableInt.Value);

            

            /*f)Определите переменную  типа  var и присвойте ей любое значение. Затем следующей инструкцией присвойте ей значение другого типа.
            Объясните причину ошибки.*/

            Console.WriteLine("\n" + "Объявление через var");
            /*var numberVar = true;
            numberVar = "String";*/

            /*2.Строки
            a)Объявите строковые литералы. Сравните их.*/

            Console.WriteLine("\n" + "Строковые литералы");
            string str1 = "Hello, mother!";
            string str2 = "Hello, mother!"; 
            string str3 = "Hello, mother";

            Console.WriteLine(string.Compare(str1, str2)); 
            Console.WriteLine(string.Compare(str1, str3)); 

            Console.WriteLine(string.CompareOrdinal(str1, str2)); 
            Console.WriteLine(string.CompareOrdinal(str1, str3));

            Console.WriteLine(str1.CompareTo(str2)); 
            Console.WriteLine(str1.CompareTo(str3) + "\n"); 

            /*b)Создайте три строки на основе String. Выполните: сцепление, копирование, выделение подстроки, разделение строки на слова,
            вставки подстроки в заданную позицию, удаление заданной подстроки.Продемонстрируйте интерполирование строк.*/

            string firstName = "Жук";
            string secondName = "Светлана";
            string thirdName = "Сергеевна";

            Console.WriteLine($"Сцепление: {string.Concat(firstName, " ", secondName, " ", thirdName)}");
            Console.WriteLine($"Копирование: {string.Copy(secondName)}");
            Console.WriteLine($"Выделение подстроки: {secondName.Substring(0, 4)}");

            string text = "Good morning, my mother!";
            string[] words = text.Split(' ');
            foreach (string word in words)
                Console.WriteLine($"Разделение строки на слова: {word}");

            Console.WriteLine($"Вставка подстроки в заданную позицию: {secondName.Insert(0, "Привет, ")}");
            Console.WriteLine($"Удаление заданной подстроки: {secondName.Remove(4)}");

            /*с)Создайте пустую и null строку.Продемонстрируйте использование метода string.IsNullOrEmpty.
            Продемонстрируйте что еще можно выполнить с такими строками*/

            Console.WriteLine("\n" + "Пустая и null строка");
            string strEmpty = "";
            string strNull = null;
            Console.WriteLine(string.IsNullOrEmpty(strEmpty));
            Console.WriteLine(string.IsNullOrEmpty(strNull));

            string combinedString = strEmpty + "Привет, мир!";
            Console.WriteLine(combinedString);

            string combinedNull = strNull + "Привет, мир!";
            Console.WriteLine(combinedNull);

            Console.WriteLine($"Длина пустой строки: {strEmpty.Length}");
            //Console.WriteLine($"Длина пустой строки: {strNull.Length}"); 

            /*d)Создайте строку на основе StringBuilder. Удалите определенные
            позиции и добавьте новые символы в начало и конец строки.*/

            Console.WriteLine("\n" + "Строка на основе StringBuilder");

            StringBuilder builder = new StringBuilder("Hello, mother");
            builder.Remove(0, 2);
            builder.Insert(0, "Hi");
            builder.Append("!");
            Console.WriteLine(builder);

            /*3.a)Создайте целый двумерный массив и выведите его на консоль в
            отформатированном виде (матрица)*/

            Console.WriteLine("\n" + "Двумерный массив");

            int[,] array = new int[3, 3] 
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };

            for (int cnt = 0; cnt < array.GetLength(0); cnt++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[cnt, j]}" + "  ");
                }
                Console.WriteLine();
            }

            /*b)Создайте одномерный массив строк. Выведите на консоль его содержимое, длину массива. Поменяйте произвольный элемент
            (пользователь определяет позицию и значение).*/

            Console.WriteLine("\n" + "Одномерный массив строк");

            string[] stringArr = new string[] { "нуль", "один", "два", "три" };

            Console.WriteLine("Массив:");

            for (int count = 0; count < stringArr.Length; count++)
            {
                Console.Write("{0} ", stringArr[count]);
            }

            Console.WriteLine();
            Console.WriteLine("Длина массива: {0}", stringArr.Length);
            Console.WriteLine("Введите номер элемента массива:");

            int index = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите новое значение элемента массива:");

            string value = Console.ReadLine();

            stringArr[index] = value;
            Console.WriteLine("Массив после изменения:");

            for (int count = 0; count < stringArr.Length; count++)
            {
                Console.Write("{0} ", stringArr[count]);
            }

            /*c)Создайте ступечатый(не выровненный) массив вещественных чисел с 3 - мя строками, в каждой из которых 2, 3 и 4 столбцов
            соответственно.Значения массива введите с консоли.*/

            Console.WriteLine("\n" + "\n" + "Ступенчатый массив с вещественными числами");

            double[][] myArr = new double[3][] { new double[2], new double[3], new double[4] };

            for (int count = 0; count < myArr.Length; count++)
            {
                for (int j = 0; j < myArr[count].Length; j++)
                {
                    Console.WriteLine("Введите значение элемента массива {0}:{1}", count, j);
                    myArr[count][j] = double.Parse(Console.ReadLine());
                }
            }
            Console.WriteLine("\n");
            for (int count = 0; count < myArr.Length; count++)
            {
                Console.WriteLine(string.Join(" ", myArr[count]));
            }

            /*d)Создайте неявно типизированные переменные для хранения массива и строки.*/

            Console.WriteLine("\n" + "Неявно типизированные переменные для хранения массива и строки");

            var arrayType = new[] { 1, 2, 3, 4, 5 };
            var strType = "Привет";
            Console.WriteLine("Массив:");
            for (int count = 0; count < arrayType.Length; count++)
            {
                Console.Write("{0} ", arrayType[count]);
            }
            Console.WriteLine($"\nТип переменной arrayType: {arrayType.GetType()}");

            Console.WriteLine("\nСтрока:");
            for (int count = 0; count < strType.Length; count++)
            {
                Console.Write(strType[count]);
            }
            Console.WriteLine($"\nТип переменной srtType: {strType.GetType()}");

            //4.a)Задайте кортеж из 5 элементов с типами int, string, char, string,ulong.

            (int, string, char, string, ulong) tuple = (42, "hello", 'A', "mother", 12ul);

            //b)Выведите кортеж на консоль целиком и выборочно (например 1,3, 4 элементы)

            Console.WriteLine("\n" + "Вывод кортежа");

            Console.WriteLine(tuple);
            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item3);
            Console.WriteLine(tuple.Item4);

            //c)Выполните распаковку кортежа в переменные.Продемонстрируйте различные способы распаковки кортежа. Продемонстрируйте использование переменной(_).

            Console.WriteLine("\n" + "Распаковка кортежа");

            var (a1, b1, c1, d1, e1) = tuple;
            Console.WriteLine(a1);
            Console.WriteLine(b1);
            Console.WriteLine(c1);
            Console.WriteLine(d1);
            Console.WriteLine(e1);

            Console.WriteLine("\n" + "Использование переменной(_)");

            var (f1, _, l1, _, _) = tuple;
            Console.WriteLine(f1);
            Console.WriteLine(l1);

            //d)Сравните два кортежа.

            Console.WriteLine("\n" + "Сравнение двух кортежей");

            var tupleTwo = (54, "goodbye", 'C', "daddy", 1ul);
            Console.WriteLine(tuple == tupleTwo);

            /*5.Создайте локальную функцию в main и вызовите ее. Формальные параметры функции – массив целых и строка. 
            Функция должна вернуть кортеж, содержащий: максимальный и минимальный элементы массива, сумму элементов
            массива и первую букву строки.*/

            Console.WriteLine("\n" + "Локальная функция");

            int[] numbers = { 1, 5, 3, 0, 1 };
            string inputString = "Hello, mother!";

            var result = LocalFunction(numbers, inputString);

            Console.WriteLine($"({result.Item1}, {result.Item2}, {result.Item3}, {result.Item4})");

            static (int, int, int, char) LocalFunction(int[] arrVar, string strVar)
            {
                int maxArrayElement = arrVar.Max();
                int minArrayElement = arrVar.Min();
                int arrayElementsSum = arrVar.Sum();
                char firstStringChar = strVar[0];
                return (maxArrayElement, minArrayElement, arrayElementsSum, firstStringChar);
            }

            /*6.Определите две локальные функции. Разместите в одной из них блок checked, в котором определите переменную типа int с 
            максимальным возможным значением этого типа.Во второй функции определите блок unchecked с таким же содержимым.Вызовите две функции. 
            Проанализируйте результат*/

            Console.WriteLine("\n" + "Checked и unchecked");

            void CheckedFunc()
            {
                try
                {
                    checked
                    {
                        int Max = int.MaxValue;
                        Console.WriteLine($"Максимальное значение (checked): {Max}");
                        Max++; 
                        Console.WriteLine($"Результат увеличения (checked): {Max}");
                    }
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine($"Исключение при переполнении (checked): {ex.Message}");
                }
            }
            void UncheckedFunc()
            {
                unchecked
                {
                    int Max = int.MaxValue;
                    Console.WriteLine($"Максимальное значение (unchecked): {Max}");
                    Max++;
                    Console.WriteLine($"Результат увеличения (unchecked): {Max}");
                }
            }
            CheckedFunc();
            UncheckedFunc();
        }
    }
}