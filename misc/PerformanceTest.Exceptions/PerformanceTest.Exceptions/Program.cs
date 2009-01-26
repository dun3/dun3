using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.Timing;
using System.Threading;

namespace PerformanceTest.Exceptions
{
    public class Program
    {
        static Random m_random = new Random();

        static readonly int LENGTH = 1000000;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            int opcount = LENGTH;
            Thread.Sleep(1000);
            Stopwatch swException = Stopwatch.Time("Exception", () => RunMe(true, opcount));
            swException.OperationCount = opcount;

            opcount = opcount * 1000;

            Thread.Sleep(1000);
            Stopwatch swReturnvalueWith = Stopwatch.Time("ReturnvalueWithTryCatch", () => RunMe(false, opcount));
            swReturnvalueWith.OperationCount = opcount;
            Thread.Sleep(1000);
            Stopwatch swReturnvalueWithout = Stopwatch.Time("ReturnvalueWithoutTryCatch", () => RunMeWithoutTryCatch(opcount));
            swReturnvalueWithout.OperationCount = opcount;

            Console.WriteLine(swException.ToString());
            Console.WriteLine(swReturnvalueWith.ToString());
            Console.WriteLine(swReturnvalueWithout.ToString());
            Console.Read();
        }

        static void RunMe(bool throwException, int length)
        {
            int count = 0;

            for (int i = 0; i < length; i++)
            {
                bool returnValue;
                try
                {
                    returnValue = Eval(throwException);
                }
                catch (Exception)
                {
                    returnValue = false;
                }
                if (returnValue)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        static void RunMeWithoutTryCatch(int length)
        {
            int count = 0;

            for (int i = 0; i < length; i++)
            {
                bool returnValue = false;

                returnValue = Eval(false);

                if (returnValue)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        static bool Eval(bool throwException)
        {
            int next = m_random.Next();
            if ((next % 2) == 0)
            {
                return true;
            }
            else
            {
                if (throwException)
                {
                    throw new Exception(next.ToString());
                }
                return false;
            }
        }
    }
}
