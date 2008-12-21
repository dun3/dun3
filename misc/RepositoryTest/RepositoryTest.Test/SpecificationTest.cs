using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using RepositoryTest.Specification;

namespace RepositoryTest
{
    [TestFixture]
    public class SpecificationTest
    {
        [Test]
        public void PositivTest()
        {
            var t = new TestClass { Name = "BlaTest" };
            var s = new NameHasTestSpecification();
            Assert.That(s.IsSatisfiedBy(t), Is.True);
        }

        [Test]
        public void NegativTest()
        {
            var t = new TestClass { Name = "Bla" };
            var s = new NameHasTestSpecification();
            Assert.That(s.IsSatisfiedBy(t), Is.False);
        }

        [Test]
        public void AndTest()
        {
            var blaThe = new TestClass { Name = "BlaThe" };
            var blaTest = new TestClass { Name = "BlaTest" };
            var blaTheTest = new TestClass { Name = "BlaTheTest" };
            var testSpec = new NameHasTestSpecification();
            var theSpec = new NameHasTheSpecification();
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
            var testSpec = new NameHasTestSpecification();
            var theSpec = new NameHasTheSpecification();
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
            var testSpec = new NameHasTestSpecification();
            var notSpec = testSpec.Not();

            Assert.That(testSpec.IsSatisfiedBy(blaThe), Is.False);
            Assert.That(notSpec.IsSatisfiedBy(blaThe), Is.True);
            Assert.That(testSpec.IsSatisfiedBy(blaTest), Is.True);
            Assert.That(notSpec.IsSatisfiedBy(blaTest), Is.False);
        }

        public class NameHasTestSpecification : SpecificationBase<TestClass>
        {
            public override bool IsSatisfiedBy(TestClass entity)
            {
                return entity.Name.Contains("Test");
            }
        }

        public class NameHasTheSpecification : SpecificationBase<TestClass>
        {
            public override bool IsSatisfiedBy(TestClass entity)
            {
                return entity.Name.Contains("The");
            }
        }
    }
}
