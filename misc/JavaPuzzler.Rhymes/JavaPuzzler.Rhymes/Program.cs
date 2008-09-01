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
                case 1: word = new StringBuilder('S'); break; // Does not compile without the break
                case 2: word = new StringBuilder('D'); break;
                default: word = new StringBuilder('M'); break;
            }
            word.Append('i');
            word.Append('c');
            word.Append('h');
            Console.WriteLine(word);

        }
    }
}
