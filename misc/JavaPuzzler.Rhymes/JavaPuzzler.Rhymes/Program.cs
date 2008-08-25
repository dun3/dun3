using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaPuzzler.Rhymes
{
    /// <summary>
    /// 'P' is char -> sets capacity!
    /// </summary>
    class Program
    {
        private static Random rnd = new Random();
        static void Main(string[] args)
        {
            StringBuilder word = null;
            switch (rnd.Next(2))
            {
                case 1: word = new StringBuilder('P'); break; // Does not compile without the break
                case 2: word = new StringBuilder('G'); break;
                default: word = new StringBuilder('M'); break;
            }
            word.Append('a');
            word.Append('i');
            word.Append('n');
            Console.WriteLine(word);

        }
    }
}
