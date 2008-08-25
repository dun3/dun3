using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaPuzzler.DelightInEveryByte
{
    class Program
    {        
        static void Main(string[] args)
        {            
            for (sbyte b = SByte.MinValue; b < SByte.MaxValue; b++)
            {
                if (b == 0x90)
                {
                    Console.WriteLine("Joy!");
                }
            }
        }
    }
}
