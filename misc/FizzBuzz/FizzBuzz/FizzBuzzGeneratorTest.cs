using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzBuzz
{
    [TestFixture]
    public class FizzBuzzGeneratorTest
    {
        private IEnumerator<string> enumerator;

        [SetUp]
        public void SetUp()
        {
            FizzBuzzGenerator fizzBuzzGenerator = new FizzBuzzGenerator();
            enumerator = fizzBuzzGenerator.GetEnumerator();
        }

        [TearDown]
        public void TearDown()
        {
            enumerator = null;
        }

        [Test]
        public void FirstTest()
        {
            enumerator.MoveNext();
            Assert.That(enumerator.Current, Is.EqualTo("1"));
        }

        [Test]
        public void LastTest()
        {
            string current = null;
            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
            }
            Assert.That(current, Is.EqualTo("Buzz"));
        }

        [Test]
        public void Mod3Test()
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 3) == 0)
                {
                    Assert.That(enumerator.Current.Contains("Fizz"), Is.True);
                }
                else
                {
                    Assert.That(enumerator.Current.Contains("Fizz"), Is.False);
                }
            }
        }

        [Test]
        public void Mod5Test()
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 5) == 0)
                {
                    Assert.That(enumerator.Current.Contains("Buzz"), Is.True);
                }
                else
                {
                    Assert.That(enumerator.Current.Contains("Buzz"), Is.False);
                }
            }
        }

        [Test]
        public void Mod15Test()
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 15) == 0)
                {
                    Assert.That(enumerator.Current, Is.EqualTo("FizzBuzz"));
                }
                else
                {
                    Assert.That(enumerator.Current, Is.Not.EqualTo("FizzBuzz"));
                }
            }
        }

        [Test]
        public void NumbersTest()
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if (((i % 3) != 0) && ((i % 5) != 0))
                {
                    Assert.That(enumerator.Current, Is.EqualTo(i.ToString()));
                }
            }
        }

        [Test]
        public void CountTest()
        {
            int i = 0;
            while (enumerator.MoveNext())
            {
                i++;
            }
            Assert.That(i, Is.EqualTo(100));
        }
    }
}
