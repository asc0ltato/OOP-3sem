using System.Collections;

namespace _3_3
{
    public class SomeString: IComparer
    {
        public string str;

        public SomeString(string str) 
        {
            this.str = str;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            SomeString other = (SomeString)obj;
            return (this.str.Length == other.str.Length && this.str.First() == other.str.First() && this.str.Last() == other.str.Last());
        }

        public int Compare(object s1, object s2)
        {
            if (s1.ToString().Length > s2.ToString().Length)
                return 1;
            else if (s1.ToString().Length < s2.ToString().Length)
                return -1;
            else return 0;
        }

        public static SomeString operator +(SomeString s1, char s2)
        {
            return new SomeString(s1.str + s2);
        }

        public static SomeString operator -(SomeString s1, char s2)
        {
            try
            {
                if (s1.str.Length == 0)
                    throw new Exception("Строка пустая");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new SomeString(s1.str = s1.str.Remove(0, 1));
        }
    }

    public static class SomeStringExtention
    {
        public static int CountSpaces(this SomeString s1)
        {
            int count = 0;
            foreach (var s in s1.str)
            {
                if (s == ' ')
                {
                   count++;
                }
            }
            return count;
        }

        public static string Remove(this SomeString s1)
        {
            var charsToRemove = new string[] { ".", ",", "!", ";", "-" };
            foreach (var s in charsToRemove)
            {
                s1.str = s1.str.Replace(s, "");
            }
            return s1.str;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter sw = new StreamWriter("text.txt"))
            {
                SomeString s1 = new SomeString("мама папа я,,!");
                SomeString s2 = new SomeString("мама папа я,,!");
                SomeString s3 = new SomeString("мама я!");

                sw.WriteLine(s1.Equals(s2));
                sw.WriteLine(s2.Equals(s3));

                sw.WriteLine(s1.Compare(s1, s2));
                s1 += 'a';
                s2 -= ' ';
                sw.WriteLine(s1.str);
                sw.WriteLine(s2.str);
                sw.WriteLine(s3.str);

                sw.WriteLine(SomeStringExtention.CountSpaces(s3));
                sw.WriteLine(SomeStringExtention.Remove(s1));
    
                sw.WriteLine(s2.str);
                sw.WriteLine(s3.str);

                SomeString[] someStrings = new SomeString[3];
                someStrings[0] = s1;
                someStrings[1] = s2;
                someStrings[2] = s3;

                var someString = someStrings.Sum(s => s.CountSpaces());
                sw.Write(someString);
            }
        }
    }

}