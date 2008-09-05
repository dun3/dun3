using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaPuzzler.RandomNr
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int n = 0;
            for (int i = 0; i < 100000; i++)
            {
                int j = rnd.Next();
                float f = j;
                if (Math.Round(f) != j)
                {
                    n++;
                }
            }

            Console.WriteLine(n);
        }
    }
}
