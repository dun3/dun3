using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSvn;

namespace SharpSvnSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SvnClient client = new SvnClient())
            {
                SvnUpdateResult result;
                // Checkout the code to the specified directory
                client.CheckOut(new Uri("https://svn01.bauerverlag.de/svn/dev_net/dev/Beispiele/"), @"C:\test\SharpSvnCheckoutTest", out result);

            }
        }
    }
}