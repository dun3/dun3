using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaPuzzler.HelloGoodbye
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello world");
                Environment.Exit(1);
            }
            finally
            {
                Console.WriteLine("Goodbye world");
            }
        }       
    }
}
