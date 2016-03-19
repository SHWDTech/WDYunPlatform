using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var datetime = typeof (DateTime).Name;
            var inttype = typeof (int).Name;
            var stringtype = typeof (string).Name;
            var longtype = typeof (long).Name;
            var bytetype = typeof (byte).Name;
            var shorttype = typeof (short).Name;
            var ushorttype = typeof (ushort).Name;
            var uinttype = typeof (uint).Name;
            var UInt16 = typeof (UInt16).Name;

            Console.WriteLine($"{datetime}-{inttype}-{stringtype}-{longtype}-{bytetype}-{shorttype}-{ushorttype}-{uinttype}-{UInt16}");
        }
    }
}
