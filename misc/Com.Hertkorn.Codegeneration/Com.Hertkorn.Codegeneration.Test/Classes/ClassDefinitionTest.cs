using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Com.Hertkorn.Codegeneration.Classes;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Codegeneration.Test.Classes
{
    [TestFixture]
    public class ClassDefinitionTest
    {
        [Test]
        public void ClassDefinitionConstructorTest()
        {
            string className = "TestClass";

            ClassDefinition classDefinition = new ClassDefinition(className);

            Assert.That(classDefinition.Name, Is.EqualTo(className));
        }
    }
}
