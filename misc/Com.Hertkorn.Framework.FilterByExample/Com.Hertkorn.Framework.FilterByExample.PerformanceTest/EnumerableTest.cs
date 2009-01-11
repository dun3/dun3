using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.FilterByExample.PerformanceTest
{
    public class EnumerableTest
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
        }

        public void TearDown()
        {
            m_example = null;
            m_exampleEnumerable = null;
        }

        private TestClass m_example;
        private IEnumerable<TestClass> m_exampleEnumerable;

        public int NoIgnoredPropertiesNoHitTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 100, 100, 100));

            return filtered.Count();
        }

        public int NoIgnoredProperties1HitTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(m_example);

            return filtered.Count();
        }

        public int NoIgnoredPropertiesMultipleHitsTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("test0", 9, 3, 9));

            return filtered.Count();
        }

        public int NoIgnoredPropertiesMultiTest()
        {
            var filtered0 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 100, 100, 100));
            var filtered1 = m_exampleEnumerable.FilterByExample<TestClass>(m_example);
            var filtered5 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("test0", 9, 3, 9));

            return filtered0.Count() + filtered1.Count() + filtered5.Count();
        }

        public int OneIgnoredPropertiesTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 9, 3, 9), x => x.TestString);

            return filtered.Count();
        }

        public int TwoIgnoredPropertiesTest()
        {
            var filtered0 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("asd", 50, 100, 100), x => x.TestLong, x => x.TestString);
            var filtered1 = m_exampleEnumerable.FilterByExample<TestClass>(new TestClass("test0", 50, 100, 100), x => x.TestLong, x => x.TestInt);

            return filtered0.Count() + filtered1.Count();
        }

    }
}
