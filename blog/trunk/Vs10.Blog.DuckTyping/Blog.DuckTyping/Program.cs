// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.DuckTyping
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            
            dynamic d = new Duck();
            d.Test = i;
            dynamic a = d.Test;

            Console.WriteLine(a);
            Console.WriteLine(a.GetType().FullName);

            // Duck!?
            IQuack t = d;

            Console.WriteLine(t.Test);

            t.Test = "2";
            // Would not compile anymore: 
            // t.Test = i;

            Console.WriteLine(t.Test);

            try
            {
                Console.WriteLine(t.NotSet);
                Console.WriteLine("You should never see this");
            }
            catch (MemberAccessException)
            {
                Console.WriteLine("Caught expected MemberAccessException");
            }

            t.NotSet = "3";

            Console.WriteLine("Should work now: " + t.NotSet);

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
