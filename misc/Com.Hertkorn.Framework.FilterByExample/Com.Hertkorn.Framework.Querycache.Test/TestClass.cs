using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.Querycache
{
    public class TestClass
    {
        public TestClass(string testString, int testInt, long testLong, int readOnly)
        {
            TestString = testString;
            TestInt = testInt;
            TestLong = testLong;
            ReadOnly = readOnly;
        }

        private string m_TestString;
        public string TestString
        {
            get
            {
                return m_TestString;
            }
            set
            {
                m_TestString = value;
            }
        }
        private int m_TestInt;
        public int TestInt
        {
            get
            {
                return m_TestInt;
            }
            set
            {
                m_TestInt = value;
            }
        }
        private long m_TestLong;
        public long TestLong
        {
            get
            {
                return m_TestLong;
            }
            set
            {
                m_TestLong = value;
            }
        }

        public int ReadOnly { private get; set; }
    }
}
