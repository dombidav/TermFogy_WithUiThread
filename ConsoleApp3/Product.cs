using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ConsoleApp3
{
    internal class Product
    {
        public int duration;
        public int remaining;
        public string name;
        private static readonly Random rnd = new Random();
        public bool IsCompleted => remaining < 1;

        public Product()
        {
            duration = rnd.Next(1, 11);
            name = RandomString();
            remaining = duration;
        }

        public static string RandomString( int length = 6 )
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[rnd.Next(s.Length)])
                                        .ToArray()
                            );
        }

        public override string ToString()
        {
            return remaining > 0 ? $"{name} - {remaining}" : $"{name}({duration})";
        }
    }
}