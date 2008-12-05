using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JavaPuzzler.RandomNr
{
    class Program
    {
        static void Main(string[] args)
        {
            var fs = new StreamWriter((new FileInfo("c:\\test.txt")).OpenWrite());
            long n = 0;

            for (int i = (int)Math.Pow(2, 24) - 100; i < (int)Math.Pow(2, 24) + 100; i++)
            {
                float f = (float)i;
                int r = (int)Math.Round(f);
                fs.WriteLine("{0} {1} {2} {3}", i, f, r, r == i);
            }

            fs.Flush();
            fs.Close();
        }
    }
}
