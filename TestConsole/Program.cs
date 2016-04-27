using System;
using SHWDTech.Platform.Utility;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = DateTime.Parse("2011-5-6 16:08:44");

            var y = Globals.GetDateBytes(x);

            Console.WriteLine(x);

            var z = Globals.GetDateFormLong(y);

            Console.WriteLine(z.ToString("yyyy-MM-dddd HH:mm:ss"));

            Console.ReadLine();
        }
    }
}
