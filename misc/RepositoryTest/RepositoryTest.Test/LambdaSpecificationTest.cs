using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using RepositoryTest.Specification;
using System.Linq.Expressions;

namespace RepositoryTest
{
    [TestFixture]
    public class LambdaSpecificationTest
    {
        [Test]
        public void PositivTest()
        {
            var test = new TestClass { Name = "BlaTest" };
            var s = new LambdaSpecification<TestClass>((t) => t.Name.Contains("Test"));
            Assert.That(s.IsSatisfiedBy(test), Is.True);
        }

        [Test]
        public void NegativTest()
        {
            var test = new TestClass { Name = "Bla" };
            var s = new LambdaSpecification<TestClass>(t => t.Name.Contains("Test"));
            Assert.That(s.IsSatisfiedBy(test), Is.False);
        }

        [Test]
        public void AndTest()
        {
            var blaThe = new TestClass { Name = "BlaThe" };
            var blaTest = new TestClass { Name = "BlaTest" };
            var blaTheTest = new TestClass { Name = "BlaTheTest" };
            var testSpec = new LambdaSpecification<TestClass>(t => t.Name.Contains("Test"));
            var theSpec = new LambdaSpecification<TestClass>(t => t.Name.Contains("The"));
            var andSpec = testSpec.And(theSpec);

            Assert.That(testSpec.IsSatisfiedBy(blaThe), Is.False);
            Assert.That(theSpec.IsSatisfiedBy(blaThe), Is.True);
            Assert.That(andSpec.IsSatisfiedBy(blaThe), Is.False);

            Assert.That(testSpec.IsSatisfiedBy(blaTest), Is.True);
            Assert.That(theSpec.IsSatisfiedBy(blaTest), Is.False);
            Assert.That(andSpec.IsSatisfiedBy(blaTest), Is.False);

            Assert.That(testSpec.IsSatisfiedBy(blaTheTest), Is.True);
            Assert.That(theSpec.IsSatisfiedBy(blaTheTest), Is.True);
            Assert.That(andSpec.IsSatisfiedBy(blaTheTest), Is.True);
        }

        [Test]
        public void OrTest()
        {
            var blaThe = new TestClass { Name = "BlaThe" };
            var blaTest = new TestClass { Name = "BlaTest" };
            var blaTheTest = new TestClass { Name = "BlaTheTest" };
            var testSpec = new LambdaSpecification<TestClass>(t => t.Name.Contains("Test"));
            var theSpec = new LambdaSpecification<TestClass>(t => t.Name.Contains("The"));
            var orSpec = testSpec.Or(theSpec);

            Assert.That(testSpec.IsSatisfiedBy(blaThe), Is.False);
            Assert.That(theSpec.IsSatisfiedBy(blaThe), Is.True);
            Assert.That(orSpec.IsSatisfiedBy(blaThe), Is.True);

            Assert.That(testSpec.IsSatisfiedBy(blaTest), Is.True);
            Assert.That(theSpec.IsSatisfiedBy(blaTest), Is.False);
            Assert.That(orSpec.IsSatisfiedBy(blaTest), Is.True);

            Assert.That(testSpec.IsSatisfiedBy(blaTheTest), Is.True);
            Assert.That(theSpec.IsSatisfiedBy(blaTheTest), Is.True);
            Assert.That(orSpec.IsSatisfiedBy(blaTheTest), Is.True);
        }

        [Test]
        public void NotTest()
        {
            var blaThe = new TestClass { Name = "BlaThe" };
            var blaTest = new TestClass { Name = "BlaTest" };
            var testSpec = new LambdaSpecification<TestClass>(t => t.Name.Contains("Test"));
            var notSpec = testSpec.Not();

            Assert.That(testSpec.IsSatisfiedBy(blaThe), Is.False);
            Assert.That(notSpec.IsSatisfiedBy(blaThe), Is.True);
            Assert.That(testSpec.IsSatisfiedBy(blaTest), Is.True);
            Assert.That(notSpec.IsSatisfiedBy(blaTest), Is.False);
        }
    }
}
