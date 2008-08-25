using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;
using LogViewerTest.Framework.DTO;
using NUnit.Framework.SyntaxHelpers;

namespace LogViewerTest.Framework.Test.DTO
{
    [TestFixture]
    public class DtoMapTest
    {
        [Test]
        public void TestManualMap()
        {
            DtoMap<Source, Target> map = DtoMap<Source, Target>.New
                .AddMap(a => a.Source1, b => b.Target1)
                .AddMap(a => a.Source2, b => b.Target2)
                .AddMap(a => a.Source3, b => b.Target3)
                .AddMap(a => a.Source4, b => b.Target4)
                .AddMap(a => a.Source5, b => b.Target5);

            Guid guid = Guid.NewGuid();

            Source source = new Source(1, "as", null, guid, 2);

            Target target = new Target();
            Assert.That(target.Target1, Is.EqualTo(default(int)));
            Assert.That(target.Target2, Is.EqualTo(default(string)));
            Assert.That(target.Target3, Is.EqualTo(default(object)));
            Assert.That(target.Target4, Is.EqualTo(default(Guid)));
            Assert.That(target.Target5, Is.EqualTo(default(int)));

            target = source.Map(map);
            Assert.That(target.Target1, Is.EqualTo(source.Source1));
            Assert.That(target.Target2, Is.EqualTo(source.Source2));
            Assert.That(target.Target3, Is.EqualTo(source.Source3));
            Assert.That(target.Target4, Is.EqualTo(source.Source4));
            Assert.That(target.Target5, Is.EqualTo(source.Source5));
        }

        [Test]
        public void TestAutoConvert()
        {
            Guid guid = Guid.NewGuid();

            AutoSource source = new AutoSource(1, "as", null, guid, 2);

            Target target = new Target();
            Assert.That(target.Target1, Is.EqualTo(default(int)));
            Assert.That(target.Target2, Is.EqualTo(default(string)));
            Assert.That(target.Target3, Is.EqualTo(default(object)));
            Assert.That(target.Target4, Is.EqualTo(default(Guid)));
            Assert.That(target.Target5, Is.EqualTo(default(int)));

            target = source.AutoConvert<AutoSource, Target>();
            Assert.That(target.Target1, Is.EqualTo(source.Target1));
            Assert.That(target.Target2, Is.EqualTo(source.Target2));
            Assert.That(target.Target3, Is.EqualTo(source.Target3));
            Assert.That(target.Target4, Is.EqualTo(source.Target4));
            Assert.That(target.Target5, Is.EqualTo(source.Target5));
        }

        [Test]
        [ExpectedException(typeof(System.Reflection.TargetException))]
        public void TestAutoConvertUnableToConvert()
        {
            Guid guid = Guid.NewGuid();

            AutoSourceTooSmall source = new AutoSourceTooSmall(1, "as", null, guid);

            Target target = source.AutoConvert<AutoSourceTooSmall, Target>();
        }

        [Test]
        public void TestAutoConvert3()
        {
            Guid guid = Guid.NewGuid();

            AutoSourceLarge source = new AutoSourceLarge(1, "as", null, guid, 2, 3);

            Target target = new Target();
            Assert.That(target.Target1, Is.EqualTo(default(int)));
            Assert.That(target.Target2, Is.EqualTo(default(string)));
            Assert.That(target.Target3, Is.EqualTo(default(object)));
            Assert.That(target.Target4, Is.EqualTo(default(Guid)));
            Assert.That(target.Target5, Is.EqualTo(default(int)));

            target = source.AutoConvert<AutoSourceLarge, Target>();
            Assert.That(target.Target1, Is.EqualTo(source.Target1));
            Assert.That(target.Target2, Is.EqualTo(source.Target2));
            Assert.That(target.Target3, Is.EqualTo(source.Target3));
            Assert.That(target.Target4, Is.EqualTo(source.Target4));
            Assert.That(target.Target5, Is.EqualTo(source.Target5));
        }

        [Test]
        public void TestAutoConvertInterface()
        {
            Guid guid = Guid.NewGuid();

            AutoSource source = new AutoSource(1, "as", null, guid, 2);

            Target target = new Target();
            Assert.That(target.Target1, Is.EqualTo(default(int)));
            Assert.That(target.Target2, Is.EqualTo(default(string)));
            Assert.That(target.Target3, Is.EqualTo(default(object)));
            Assert.That(target.Target4, Is.EqualTo(default(Guid)));
            Assert.That(target.Target5, Is.EqualTo(default(int)));

            target = source.AutoConvert<IAutoSource, Target>();
            Assert.That(target.Target1, Is.EqualTo(source.Target1));
            Assert.That(target.Target2, Is.EqualTo(source.Target2));
            Assert.That(target.Target3, Is.EqualTo(source.Target3));
            Assert.That(target.Target4, Is.EqualTo(source.Target4));
            Assert.That(target.Target5, Is.EqualTo(source.Target5));
        }

        [Test]
        public void TestAutoConvertList()
        {
            List<AutoSource> sourceList = new List<AutoSource>();
            sourceList.Add(new AutoSource(0, "0", null, Guid.NewGuid(), 1));
            sourceList.Add(new AutoSource(2, "0", null, Guid.NewGuid(), 3));
            sourceList.Add(new AutoSource(4, "0", null, Guid.NewGuid(), 5));

            List<Target> targetList = sourceList.AutoConvertList<AutoSource, Target>();

            Assert.That(targetList.Count, Is.EqualTo(sourceList.Count));

            for (int i = 0; i < sourceList.Count; i++)
            {
                Assert.That(targetList[i].Target1, Is.EqualTo(sourceList[i].Target1));
                Assert.That(targetList[i].Target2, Is.EqualTo(sourceList[i].Target2));
                Assert.That(targetList[i].Target3, Is.EqualTo(sourceList[i].Target3));
                Assert.That(targetList[i].Target4, Is.EqualTo(sourceList[i].Target4));
                Assert.That(targetList[i].Target5, Is.EqualTo(sourceList[i].Target5));
            }
        }

        [Test]
        public void TestAutoConvertListInterface()
        {
            List<IAutoSource> sourceList = new List<IAutoSource>();
            sourceList.Add(new AutoSource(0, "0", null, Guid.NewGuid(), 1));
            sourceList.Add(new AutoSourceLarge(2, "0", null, Guid.NewGuid(), 3, 13));
            sourceList.Add(new AutoSource(4, "0", null, Guid.NewGuid(), 5));

            List<Target> targetList = sourceList.AutoConvertList<IAutoSource, Target>();

            Assert.That(targetList.Count, Is.EqualTo(sourceList.Count));

            for (int i = 0; i < sourceList.Count; i++)
            {
                Assert.That(targetList[i].Target1, Is.EqualTo(sourceList[i].Target1));
                Assert.That(targetList[i].Target2, Is.EqualTo(sourceList[i].Target2));
                Assert.That(targetList[i].Target3, Is.EqualTo(sourceList[i].Target3));
                Assert.That(targetList[i].Target4, Is.EqualTo(sourceList[i].Target4));
                Assert.That(targetList[i].Target5, Is.EqualTo(sourceList[i].Target5));
            }
        }
    }

    public class Source
    {
        /// <summary>
        /// Initializes a new instance of the Source class.
        /// </summary>
        public Source(int source1, string source2, object source3, Guid source4, int source5)
        {
            Source1 = source1;
            Source2 = source2;
            Source3 = source3;
            Source4 = source4;
            Source5 = source5;
        }
        public int Source1 { get; set; }
        public string Source2 { get; set; }
        public object Source3 { get; set; }
        public Guid Source4 { get; set; }
        public int Source5 { get; set; }
    }


    public class AutoSourceTooSmall
    {
        /// <summary>
        /// Initializes a new instance of the AutoSourceTooSmall class.
        /// </summary>
        public AutoSourceTooSmall(int target1, string target2, object target3, Guid target4)
        {
            Target1 = target1;
            Target2 = target2;
            Target3 = target3;
            Target4 = target4;
        }
        public int Target1 { get; set; }
        public string Target2 { get; set; }
        public object Target3 { get; set; }
        public Guid Target4 { get; set; }
    }

    public class AutoSource : IAutoSource
    {
        /// <summary>
        /// Initializes a new instance of the AutoSource class.
        /// </summary>
        public AutoSource(int target1, string target2, object target3, Guid target4, int target5)
        {
            Target1 = target1;
            Target2 = target2;
            Target3 = target3;
            Target4 = target4;
            Target5 = target5;
        }
        public int Target1 { get; set; }
        public string Target2 { get; set; }
        public object Target3 { get; set; }
        public Guid Target4 { get; set; }
        public int Target5 { get; set; }
    }

    public interface IAutoSource
    {
        int Target1 { get; }
        string Target2 { get; }
        object Target3 { get; }
        Guid Target4 { get; }
        int Target5 { get; }
    }


    public class AutoSourceLarge : IAutoSource
    {
        /// <summary>
        /// Initializes a new instance of the AutoSourceLarge class.
        /// </summary>
        public AutoSourceLarge(int target1, string target2, object target3, Guid target4, int target5, int target6)
        {
            Target1 = target1;
            Target2 = target2;
            Target3 = target3;
            Target4 = target4;
            Target5 = target5;
            Target6 = target6;
        }
        public int Target1 { get; set; }
        public string Target2 { get; set; }
        public object Target3 { get; set; }
        public Guid Target4 { get; set; }
        public int Target5 { get; set; }
        public int Target6 { get; set; }
    }

    public class Target
    {
        public int Target1 { get; set; }
        public string Target2 { get; set; }
        public object Target3 { get; set; }
        public Guid Target4 { get; set; }
        public int Target5 { get; set; }
    }
}
