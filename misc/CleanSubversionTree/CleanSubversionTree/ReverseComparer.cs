// <copyright file="ReverseComparer.cs" company="test">
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
    public class ReverseComparer : IComparer<DirectoryInfo>
    {
        /// <summary>
        /// adsfasdfasdf adf asdf aass adf sadf
        /// </summary>
        /// <param name="x">asdf adsf as</param>
        /// <param name="y">adsf adsf sd</param>
        /// <returns>asdf adsf ssdfa f</returns>
        public int Compare(DirectoryInfo x, DirectoryInfo y)
        {
            // Compare y and x in reverse order.
            return y.FullName.CompareTo(x.FullName);
        }
    }
}
