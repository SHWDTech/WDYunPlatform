﻿using System;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;

namespace TestConsole
{
    class Program
    {
        static void Main()
        {
            var i = 0;
            var rd = new Random();
            while (i < 200)
            {
                Console.WriteLine($"rd1: {rd.Next(0, 1000)}, rd2{new Random().Next(0, 1000)}");
                i++;
            }
            Console.ReadKey();
        }
    }
}
