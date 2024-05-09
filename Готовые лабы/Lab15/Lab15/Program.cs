using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab15
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TPL.Task1(2200);
                TPL.Task2(6000);
                TPL.Task3();
                TPL.Task4_1();
                TPL.Task4_2();
                TPL.Task5();
                TPL.Task6();
                TPL.Task7();
                TPL.Task8();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
