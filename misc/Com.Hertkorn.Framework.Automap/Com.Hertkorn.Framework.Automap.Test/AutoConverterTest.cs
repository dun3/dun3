using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.Automap
{
    [TestFixture]
    public class AutoConverterTest
    {
        [Test]
        public void AutoConvertTest()
        {
            Guid guid = Guid.NewGuid();

            AutoSource source = new AutoSource(1, "as", null, guid, 2);

            Target target = new Target();
            Assert.That(target.Prop1, Is.EqualTo(default(int)));
            Assert.That(target.Prop2, Is.EqualTo(default(string)));
            Assert.That(target.Prop3, Is.EqualTo(default(object)));
            Assert.That(target.Prop4, Is.EqualTo(default(Guid)));
            Assert.That(target.Prop5, Is.EqualTo(default(int)));

            target = source.AutoConvert<AutoSource, Target>();
            Assert.That(target.Prop1, Is.EqualTo(source.Prop1));
            Assert.That(target.Prop2, Is.EqualTo(source.Prop2));
            Assert.That(target.Prop3, Is.EqualTo(source.Prop3));
            Assert.That(target.Prop4, Is.EqualTo(source.Prop4));
            Assert.That(target.Prop5, Is.EqualTo(source.Prop5));
        }

        [Test]
        [ExpectedException(typeof(System.Reflection.TargetException))]
        public void AutoConvertUnableToConvertTest()
        {
            Guid guid = Guid.NewGuid();

            AutoSourceTooSmall source = new AutoSourceTooSmall(1, "as", null, guid);

            Target target = source.AutoConvert<AutoSourceTooSmall, Target>();
        }

        [Test]
        public void AutoConvertLargetSourceTest()
        {
            Guid guid = Guid.NewGuid();

            AutoSourceLarge source = new AutoSourceLarge(1, "as", null, guid, 2, 3);

            Target target = new Target();
            Assert.That(target.Prop1, Is.EqualTo(default(int)));
            Assert.That(target.Prop2, Is.EqualTo(default(string)));
            Assert.That(target.Prop3, Is.EqualTo(default(object)));
            Assert.That(target.Prop4, Is.EqualTo(default(Guid)));
            Assert.That(target.Prop5, Is.EqualTo(default(int)));

            target = source.AutoConvert<AutoSourceLarge, Target>();
            Assert.That(target.Prop1, Is.EqualTo(source.Prop1));
            Assert.That(target.Prop2, Is.EqualTo(source.Prop2));
            Assert.That(target.Prop3, Is.EqualTo(source.Prop3));
            Assert.That(target.Prop4, Is.EqualTo(source.Prop4));
            Assert.That(target.Prop5, Is.EqualTo(source.Prop5));
        }

        [Test]
        public void AutoConvertInterfaceTest()
        {
            Guid guid = Guid.NewGuid();

            AutoSource source = new AutoSource(1, "as", null, guid, 2);

            Target target = new Target();
            Assert.That(target.Prop1, Is.EqualTo(default(int)));
            Assert.That(target.Prop2, Is.EqualTo(default(string)));
            Assert.That(target.Prop3, Is.EqualTo(default(object)));
            Assert.That(target.Prop4, Is.EqualTo(default(Guid)));
            Assert.That(target.Prop5, Is.EqualTo(default(int)));

            target = source.AutoConvert<IAutoSource, Target>();
            Assert.That(target.Prop1, Is.EqualTo(source.Prop1));
            Assert.That(target.Prop2, Is.EqualTo(source.Prop2));
            Assert.That(target.Prop3, Is.EqualTo(source.Prop3));
            Assert.That(target.Prop4, Is.EqualTo(source.Prop4));
            Assert.That(target.Prop5, Is.EqualTo(source.Prop5));
        }

        [Test]
        public void AutoConvertDeepInterfaceTest()
        {
            Guid guid = Guid.NewGuid();

            AutoSourceDeepInterface source = new AutoSourceDeepInterface(1, "as", null, guid, 2);

            Target target = new Target();
            Assert.That(target.Prop1, Is.EqualTo(default(int)));
            Assert.That(target.Prop2, Is.EqualTo(default(string)));
            Assert.That(target.Prop3, Is.EqualTo(default(object)));
            Assert.That(target.Prop4, Is.EqualTo(default(Guid)));
            Assert.That(target.Prop5, Is.EqualTo(default(int)));

            target = source.AutoConvert<IAutoSourceDeepInterface, Target>();
            Assert.That(target.Prop1, Is.EqualTo(source.Prop1));
            Assert.That(target.Prop2, Is.EqualTo(source.Prop2));
            Assert.That(target.Prop3, Is.EqualTo(source.Prop3));
            Assert.That(target.Prop4, Is.EqualTo(source.Prop4));
            Assert.That(target.Prop5, Is.EqualTo(source.Prop5));
        }

        [Test]
        public void AutoConvertListTest()
        {
            List<AutoSource> sourceList = new List<AutoSource>();
            sourceList.Add(new AutoSource(0, "0", null, Guid.NewGuid(), 1));
            sourceList.Add(new AutoSource(2, "0", null, Guid.NewGuid(), 3));
            sourceList.Add(new AutoSource(4, "0", null, Guid.NewGuid(), 5));

            List<Target> targetList = sourceList.AutoConvertList<AutoSource, Target>();

            Assert.That(targetList.Count, Is.EqualTo(sourceList.Count));

            for (int i = 0; i < sourceList.Count; i++)
            {
                Assert.That(targetList[i].Prop1, Is.EqualTo(sourceList[i].Prop1));
                Assert.That(targetList[i].Prop2, Is.EqualTo(sourceList[i].Prop2));
                Assert.That(targetList[i].Prop3, Is.EqualTo(sourceList[i].Prop3));
                Assert.That(targetList[i].Prop4, Is.EqualTo(sourceList[i].Prop4));
                Assert.That(targetList[i].Prop5, Is.EqualTo(sourceList[i].Prop5));
            }
        }

        [Test]
        public void AutoConvertListInterfaceTest()
        {
            List<IAutoSource> sourceList = new List<IAutoSource>();
            sourceList.Add(new AutoSource(0, "0", null, Guid.NewGuid(), 1));
            sourceList.Add(new AutoSourceLarge(2, "0", null, Guid.NewGuid(), 3, 13));
            sourceList.Add(new AutoSource(4, "0", null, Guid.NewGuid(), 5));

            List<Target> targetList = sourceList.AutoConvertList<IAutoSource, Target>();

            Assert.That(targetList.Count, Is.EqualTo(sourceList.Count));

            for (int i = 0; i < sourceList.Count; i++)
            {
                Assert.That(targetList[i].Prop1, Is.EqualTo(sourceList[i].Prop1));
                Assert.That(targetList[i].Prop2, Is.EqualTo(sourceList[i].Prop2));
                Assert.That(targetList[i].Prop3, Is.EqualTo(sourceList[i].Prop3));
                Assert.That(targetList[i].Prop4, Is.EqualTo(sourceList[i].Prop4));
                Assert.That(targetList[i].Prop5, Is.EqualTo(sourceList[i].Prop5));
            }
        }

        public class AutoSourceTooSmall
        {
            /// <summary>
            /// Initializes a new instance of the AutoSourceTooSmall class.
            /// </summary>
            public AutoSourceTooSmall(int prop1, string prop2, object prop3, Guid prop4)
            {
                Prop1 = prop1;
                Prop2 = prop2;
                Prop3 = prop3;
                Prop4 = prop4;
            }
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public object Prop3 { get; set; }
            public Guid Prop4 { get; set; }
        }

        public class AutoSource : IAutoSource
        {
            /// <summary>
            /// Initializes a new instance of the AutoSource class.
            /// </summary>
            public AutoSource(int prop1, string prop2, object prop3, Guid prop4, int prop5)
            {
                Prop1 = prop1;
                Prop2 = prop2;
                Prop3 = prop3;
                Prop4 = prop4;
                Prop5 = prop5;
            }
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public object Prop3 { get; set; }
            public Guid Prop4 { get; set; }
            public int Prop5 { get; set; }
        }

        public interface IAutoSource
        {
            int Prop1 { get; }
            string Prop2 { get; }
            object Prop3 { get; }
            Guid Prop4 { get; }
            int Prop5 { get; }
        }

        public class AutoSourceLarge : IAutoSource
        {
            /// <summary>
            /// Initializes a new instance of the AutoSourceLarge class.
            /// </summary>
            public AutoSourceLarge(int prop1, string prop2, object prop3, Guid prop4, int prop5, int prop6)
            {
                Prop1 = prop1;
                Prop2 = prop2;
                Prop3 = prop3;
                Prop4 = prop4;
                Prop5 = prop5;
                Prop6 = prop6;
            }
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public object Prop3 { get; set; }
            public Guid Prop4 { get; set; }
            public int Prop5 { get; set; }
            public int Prop6 { get; set; }
        }

        public class AutoSourceDeepInterface : IAutoSourceDeepInterface
        {
            /// <summary>
            /// Initializes a new instance of the AutoSource class.
            /// </summary>
            public AutoSourceDeepInterface(int prop1, string prop2, object prop3, Guid prop4, int prop5)
            {
                Prop1 = prop1;
                Prop2 = prop2;
                Prop3 = prop3;
                Prop4 = prop4;
                Prop5 = prop5;
            }
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public object Prop3 { get; set; }
            public Guid Prop4 { get; set; }
            public int Prop5 { get; set; }
        }

        public interface IAutoSourceDeepInterface : IAutoSourceDeepInterface12, IAutoSourceDeepInterface34, IAutoSourceDeepInterface45
        {
            new int Prop1 { get; }
        }

        public interface IAutoSourceDeepInterface12 : IAutoSourceDeepInterface1, IAutoSourceDeepInterface2
        {

        }

        public interface IAutoSourceDeepInterface1
        {
            int Prop1 { get; }
        }

        public interface IAutoSourceDeepInterface2
        {
            string Prop2 { get; }
        }

        public interface IAutoSourceDeepInterface34 : IAutoSourceDeepInterface4
        {
            object Prop3 { get; }
        }

        public interface IAutoSourceDeepInterface45 : IAutoSourceDeepInterface4
        {
            int Prop5 { get; }
        }

        public interface IAutoSourceDeepInterface4
        {
            Guid Prop4 { get; }
        }





        public class Target
        {
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public object Prop3 { get; set; }
            public Guid Prop4 { get; set; }
            public int Prop5 { get; set; }
        }
    }
}
