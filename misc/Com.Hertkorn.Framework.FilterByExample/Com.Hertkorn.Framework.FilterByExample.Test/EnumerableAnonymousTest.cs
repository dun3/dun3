using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.FilterByExample
{
    [TestFixture]
    public class EnumerableAnonymousTest
    {
        [SetUp]
        public void SetUp()
        {
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

        [TearDown]
        public void TearDown()
        {
            m_exampleEnumerable = null;
        }

        private IEnumerable<TestClass> m_exampleEnumerable;

        [Test]
        public void OnePropertyTest()
        {
            var filtered = m_exampleEnumerable.FilterByExample(new { TestString = "test1" }).ToList();

            Assert.That(filtered.Count, Is.EqualTo(m_exampleEnumerable.FilterByExample(new TestClass("test1", 0, 0, 0), x => x.TestInt, x => x.TestLong).Count()));
        }
    }
}
