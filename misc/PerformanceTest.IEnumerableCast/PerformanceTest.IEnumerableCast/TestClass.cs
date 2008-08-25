using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceTest.IEnumerableCast
{
    public class TestClass : ITestInterface
    {
        public TestClass(string a, int b)
        {
            SomeString = a;
            SomeInt = b;
        }

        #region ITestInterface Members

        public string SomeString { get; set; }

        public int SomeInt { get; set; }

        #endregion
    }
}
