using System;

namespace TestConsole
{
    class Program
    {
        static void Main()
        {
            var i = 0;
            var rd = new Random();
            while (i < 2000)
            {
                Console.WriteLine($@"rd1：{rd.Next(0, 1000)}， rd2：{new Random().Next(0, 1000)}，DateTime：{DateTime.Now : mm:ss fff}");
                i++;
            }
            Console.ReadKey();
        }
    }
}
