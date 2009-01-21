using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.PropertyWiring
{
    [TestFixture]
    public class ReflectionHelperTest
    {
        [Test]
        public void ValueTypeTest()
        {
            string propertyName = ReflectionHelper.GetPropertyName<TestClass>(t => t.IntProperty);
            Assert.That(propertyName, Is.EqualTo("IntProperty"));
        }

        [Test]
        public void ReferenceTypeTest()
        {
            string propertyName = ReflectionHelper.GetPropertyName<TestClass>(t => t.ObjectProperty);
            Assert.That(propertyName, Is.EqualTo("ObjectProperty"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ValueMethodTest()
        {
            string propertyName = ReflectionHelper.GetPropertyName<TestClass>(t => t.IntMethod());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReferenceMethodTest()
        {
            string propertyName = ReflectionHelper.GetPropertyName<TestClass>(t => t.ObjectMethod());
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void ObsoleteValueTypeTest()
        {
            string propertyName = ReflectionHelper.GetPropertyNameObsolete<TestClass>(t => t.IntProperty);
            Assert.That(propertyName, Is.EqualTo("IntProperty"));
        }

        [Test]
        public void ObsoleteReferenceTypeTest()
        {
            string propertyName = ReflectionHelper.GetPropertyNameObsolete<TestClass>(t => t.ObjectProperty);
            Assert.That(propertyName, Is.EqualTo("ObjectProperty"));
        }
    }
}
