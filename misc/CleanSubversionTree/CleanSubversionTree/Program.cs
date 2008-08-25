// <copyright file="Program.cs" company="test">
//   Tobias Hertkorn
// </copyright>
namespace CleanSubversionTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    /// <summary>
    /// bala aaa aaa aaa
    /// </summary>
    public class Program
    {
        /// <summary>
        /// aaaaaaaag gasd asdf as
        /// </summary>
        /// <param name="args">hallo ma lacha</param>
        public static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo("c:\\dev");
            DirectoryInfo[] dis = di.GetDirectories("*", SearchOption.AllDirectories);
            //// Array.Sort(dis, new ReverseComparer());
            foreach (var item in dis)
            {
                if (!item.FullName.Contains(".svn"))
                {
                    ////Console.WriteLine(item.FullName);
                    foreach (var file in item.GetFiles())
                    {
                        ////Console.WriteLine(file.FullName);
                        file.Delete();
                    }
                }
            }
            ////Console.Read();
        }
    }
}
