using System;

namespace ControlWork1
{
    public interface ICheck
    {
        void Check();
    }

    class Time
    {
        public void Check()
        {
            if (Hours >= 0 && Hours < 6)
                Console.WriteLine("Ночь");
            else if (Hours < 12)
                Console.WriteLine("Утро");
            else if (Hours < 18)
                Console.WriteLine("День");
            else
                Console.WriteLine("Вечер");
        }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Time(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public enum TimeOfDay
        {
            AM,
            PM
        }

        public override string ToString()
        {
            TimeOfDay time_format = TimeOfDay.AM;
            int hours = Hours;
            if (hours > 12)
            {
                hours -= 12;
                time_format = TimeOfDay.PM;
            }
            return $"{hours}:{Minutes}:{Seconds} {time_format}";
        }

        public int TotalSeconds => Hours * 3600 + Minutes * 60 + Seconds;

        public static bool operator <(Time time1, Time time2)
        {
            return time1.TotalSeconds < time2.TotalSeconds;
        }

        public static bool operator >(Time time1, Time time2)
        {
            return time1.TotalSeconds > time2.TotalSeconds;
        }

        public static bool operator ==(Time time1, Time time2)
        {
            return time1.TotalSeconds == time2.TotalSeconds;
        }

        public static bool operator !=(Time time1, Time time2)
        {
            return time1.TotalSeconds != time2.TotalSeconds;
        }
    }

    class FullTime : Time
    {
        public FullTime(int hours, int minutes, int seconds) : base(hours, minutes, seconds)
        {

        }

        public void Check()
        {
            TimeSpan timeUntilMidnight = new TimeSpan(24, 0, 0) - new TimeSpan(Hours, Minutes, Seconds);
            Console.WriteLine($"Осталось до полночи: {timeUntilMidnight}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            short maxShort = short.MaxValue;
            Console.WriteLine(maxShort);

            int[] numbers = { 1, 2, 3, 4, 5 };
            string numbers_string = string.Join(", ", numbers);
            Console.WriteLine(numbers_string);


            Time time = new Time(8, 30, 0);
            Time time1 = new Time(14, 45, 30);
            Console.WriteLine(time.ToString());
            Console.WriteLine(time1.ToString());

            Console.WriteLine("Сравнение времени");
            bool time2 = time < time1;
            Console.WriteLine(time2);
            bool time3 = time > time1;
            Console.WriteLine(time3);
            Console.WriteLine(time == time1);

            Console.WriteLine("Метод check в классе FullTime");
            FullTime fullTime = new FullTime(22, 30, 15);
            fullTime.Check();

            Console.WriteLine("Метод check в классе Time");
            Time time4 = new Time(13, 30, 45);
            time4.Check();
        }
    }
}