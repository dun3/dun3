using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CleanSubversionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo("c:\\dev");
            DirectoryInfo[] dis = di.GetDirectories("*", SearchOption.AllDirectories);
            // Array.Sort(dis, new ReverseComparer());
            foreach (var item in dis)
            {
                if (!item.FullName.Contains(".svn"))
                {
                    //Console.WriteLine(item.FullName);
                    foreach (var file in item.GetFiles())
                    {
                        //Console.WriteLine(file.FullName);
                        file.Delete();
                    }
                }
            }
            //Console.Read();
        }

    }
    public class ReverseComparer : IComparer<DirectoryInfo>
    {
        public int Compare(DirectoryInfo x, DirectoryInfo y)
        {
            // Compare y and x in reverse order.
            return y.FullName.CompareTo(x.FullName);
        }
    }

}
