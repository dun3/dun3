using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.FilterByExample.PerformanceTest
{
    public class PrecompiledEnumerableTest
    {
        public void SetUp()
        {
            m_example = new TestClass("test0", 1, 2, 6);

            m_exampleEnumerable = new List<TestClass>()
            {
                new TestClass("test0", 1, 2, 6),
                new TestClass("test0", 2, 3, 7),
                new TestClass("test0", 3, 4, 8),
                new TestClass("test1", 4, 3, 9),
                new TestClass("test1", 5, 3, 9),
                new TestClass("test1", 6, 3, 9),
                new TestClass("test1", 7, 3, 9),
                new TestClass("test1", 8, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test1", 10, 3, 9),
                new TestClass("test0", 11, 3, 9),
                new TestClass("test1", 12, 3, 9),
                new TestClass("test1", 13, 3, 9),
                new TestClass("test0", 14, 3, 9),
                new TestClass("test1", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test0", 18, 3, 9),
                new TestClass("test1", 19, 3, 9),
                new TestClass("test0", 10, 3, 9),
                new TestClass("test1", 11, 3, 9),
                new TestClass("test1", 12, 3, 9),
                new TestClass("test0", 13, 3, 9),
                new TestClass("test1", 14, 3, 9),
                new TestClass("test1", 15, 3, 9),
                new TestClass("test0", 16, 3, 9),
                new TestClass("test1", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test0", 19, 3, 9),
                new TestClass("test1", 20, 3, 9),
                new TestClass("test0", 11, 3, 9),
                new TestClass("test0", 12, 3, 9),
                new TestClass("test1", 13, 3, 9),
                new TestClass("test1", 14, 3, 9),
                new TestClass("test0", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test1", 19, 3, 9),
                new TestClass("test0", 30, 3, 9),
                new TestClass("test0", 11, 3, 9),
                new TestClass("test1", 12, 3, 9),
                new TestClass("test0", 13, 3, 9),
                new TestClass("test1", 14, 3, 9),
                new TestClass("test1", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test0", 19, 3, 9),
                new TestClass("test1", 40, 3, 9),
                new TestClass("test1", 11, 3, 9),
                new TestClass("test0", 12, 3, 9),
                new TestClass("test1", 13, 3, 9),
                new TestClass("test0", 14, 3, 9),
                new TestClass("test0", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test0", 19, 3, 9),
                new TestClass("test1", 50, 3, 9)
            }.AsEnumerable();

            m_noIgnored = Enumerable.CreateFilter<TestClass>();
            m_ignoreTestString = Enumerable.CreateFilter<TestClass>(x => x.TestString);
            m_ignoreTestLongTestString = Enumerable.CreateFilter<TestClass>(x => x.TestLong, x => x.TestString);
            m_ignoreTestLongTestInt = Enumerable.CreateFilter<TestClass>(x => x.TestLong, x => x.TestInt);
        }

        public void TearDown()
        {
            m_example = null;
            m_exampleEnumerable = null;

            m_noIgnored = null;
            m_ignoreTestString = null;
            m_ignoreTestLongTestString = null;
            m_ignoreTestLongTestInt = null;
        }

        private TestClass m_example;
        private IEnumerable<TestClass> m_exampleEnumerable;

        private Func<TestClass, TestClass, bool> m_noIgnored;
        private Func<TestClass, TestClass, bool> m_ignoreTestString;
        private Func<TestClass, TestClass, bool> m_ignoreTestLongTestString;
        private Func<TestClass, TestClass, bool> m_ignoreTestLongTestInt;

        public int NoIgnoredPropertiesNoHitTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 100, 100, 100), m_noIgnored);

            return filtered.Count();
        }

        public int NoIgnoredProperties1HitTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(m_example, m_noIgnored);

            return filtered.Count();
        }

        public int NoIgnoredPropertiesMultipleHitsTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("test0", 9, 3, 9), m_noIgnored);

            return filtered.Count();
        }

        public int NoIgnoredPropertiesMultiTest()
        {
            var filtered0 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 100, 100, 100), m_noIgnored);
            var filtered1 = m_exampleEnumerable.FilterByExample<TestClass>(m_example, m_noIgnored);
            var filtered5 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("test0", 9, 3, 9), m_noIgnored);

            return filtered0.Count() + filtered1.Count() + filtered5.Count();
        }

        public int OneIgnoredPropertiesTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 9, 3, 9), m_ignoreTestString);

            return filtered.Count();
        }

        public int TwoIgnoredPropertiesTest()
        {
            var filtered0 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 50, 100, 100), m_ignoreTestLongTestString);
            var filtered1 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("test0", 50, 100, 100), m_ignoreTestLongTestInt);

            return filtered0.Count() + filtered1.Count();
        }

    }
}
