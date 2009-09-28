using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in new FizzBuzzGenerator())
            {
                Console.WriteLine(item);
            }
            Console.Read();
        }
    }
}
