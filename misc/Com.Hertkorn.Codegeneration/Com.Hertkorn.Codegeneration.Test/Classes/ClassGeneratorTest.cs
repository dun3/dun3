using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Com.Hertkorn.Codegeneration.Classes;
using NUnit.Framework.SyntaxHelpers;
using System.IO;

namespace Com.Hertkorn.Codegeneration.Test.Classes
{
    [TestFixture]
    public class ClassGeneratorTest
    {
        [Test]
        public void ClassGeneratorConstructorTest()
        {
            string namespaceName = "TestNamespace.Test";

            ClassGenerator generator = new ClassGenerator(namespaceName);

            Assert.That(generator.Namespace, Is.EqualTo(namespaceName));
        }

        [Test]
        public void GenerateClassStubTest()
        {
            string namespaceName = "TestNamespace.Test";
            string className = "TestClass";

            ClassGenerator generator = new ClassGenerator(namespaceName);

            ClassDefinition classDefinition = new ClassDefinition(className);

            StringWriter writer = new StringWriter();

            List<GeneratedPart> generatedPartz = generator.Generate(classDefinition);

            Assert.That(generatedPartz, Has.Count(1));
            Assert.That(generatedPartz[0].Name, Is.EqualTo(className + ".cs"));
            Assert.That(generatedPartz[0].GeneratedCode.Length, Is.GreaterThan(0));
            Assert.That(generatedPartz[0].GeneratedCode, Text.Matches(@".*namespace TestNamespace.Test\s*\{\s*using System;\s*public class TestClass\s*\{\s*public TestClass\(\)\s*\{\s*\}\s*\}\s*}\s*"));
        }

        [Test]
        public void GeneratePartialClassStubTest()
        {
            string namespaceName = "TestNamespace.Test";
            string className = "TestClass";

            ClassGenerator generator = new ClassGenerator(namespaceName);

            ClassDefinition classDefinition = new ClassDefinition(className, true);

            StringWriter writer = new StringWriter();

            List<GeneratedPart> generatedPartz = generator.Generate(classDefinition);

            Assert.That(generatedPartz, Has.Count(2));
            Assert.That(generatedPartz[0].Name, Is.EqualTo(className + ".cs"));
            Assert.That(generatedPartz[1].Name, Is.EqualTo(className + ".generated.cs"));
            Assert.That(generatedPartz[0].GeneratedCode.Length, Is.GreaterThan(0));
            Assert.That(generatedPartz[0].GeneratedCode, Text.Matches(@".*namespace TestNamespace.Test\s*\{\s*using System;\s*public partial class TestClass\s*\{\s*\}\s*}\s*"));
            Assert.That(generatedPartz[1].GeneratedCode.Length, Is.GreaterThan(0));
            Assert.That(generatedPartz[1].GeneratedCode, Text.Matches(@".*namespace TestNamespace.Test\s*\{\s*using System;\s*public partial class TestClass\s*\{\s*public TestClass\(\)\s*\{\s*\}\s*\}\s*}\s*"));
        }
    }
}
