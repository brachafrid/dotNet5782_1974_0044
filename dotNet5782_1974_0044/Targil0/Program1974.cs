using System;

namespace Targil0
{
    class Program
    {
        static void Main(string[] args)
        {
            welcome1974();
            welcome0044();
            Console.ReadKey();
        }

        private static void welcome1974()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
        static partial void welcome0044();
    }
}
