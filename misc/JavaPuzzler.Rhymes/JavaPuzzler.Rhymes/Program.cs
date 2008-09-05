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
            switch (rnd.Next(1))
            {
                case 2: word = new StringBuilder('D'); break; // Does not compile without the break
                default: word = new StringBuilder('M'); break;
            }
            word.Append('i');
            word.Append('c');
            word.Append('h');
            Console.WriteLine(word);

        }
    }

    //class Program
    //{
    //    private static readonly int UPPER_BOUND = 1;
    //    private static readonly int EXCLUSIVE_UPPER_BOUND = UPPER_BOUND + 1;

    //    private static Random rnd = new Random();
    //    static void Main(string[] args)
    //    {
    //        string initialization = string.Empty;
    //        switch (rnd.Next(EXCLUSIVE_UPPER_BOUND))
    //        {
    //            case 1: initialization = "D"; break;
    //            default: initialization = "M"; break;
    //        }
    //        StringBuilder word = new StringBuilder(initialization);
    //        word.Append('i');
    //        word.Append('c');
    //        word.Append('h');
    //        Console.WriteLine(word);
    //    }
    //}
}
