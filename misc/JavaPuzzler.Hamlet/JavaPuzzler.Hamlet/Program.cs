using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaPuzzler.Hamlet
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            bool toBe = (rnd.Next() % 2) == 0;
            var result = (toBe || !toBe) ? 3 : 1.0f;
            Console.WriteLine(result + " " + result.GetType().Name); // GetType notwendig, da 3 rauskommt ;)            
        }
    }
}
