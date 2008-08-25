using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.Automap
{
    [TestFixture]
    public class SimpleConverterTest
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

            SimpleConverter simpleConverter = new SimpleConverter();
            simpleConverter.Map(source, target);

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

            Target target = new Target();

            SimpleConverter simpleConverter = new SimpleConverter();
            simpleConverter.Map(source, target);
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

            SimpleConverter simpleConverter = new SimpleConverter();
            simpleConverter.Map(source, target);

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

            SimpleConverter simpleConverter = new SimpleConverter();
            simpleConverter.Map(source, target);

            Assert.That(target.Prop1, Is.EqualTo(source.Prop1));
            Assert.That(target.Prop2, Is.EqualTo(source.Prop2));
            Assert.That(target.Prop3, Is.EqualTo(source.Prop3));
            Assert.That(target.Prop4, Is.EqualTo(source.Prop4));
            Assert.That(target.Prop5, Is.EqualTo(source.Prop5));
        }

        //[Test]
        //public void AutoConvertListTest()
        //{
        //    List<AutoSource> sourceList = new List<AutoSource>();
        //    sourceList.Add(new AutoSource(0, "0", null, Guid.NewGuid(), 1));
        //    sourceList.Add(new AutoSource(2, "0", null, Guid.NewGuid(), 3));
        //    sourceList.Add(new AutoSource(4, "0", null, Guid.NewGuid(), 5));

        //    List<Target> targetList = sourceList.AutoConvertList<AutoSource, Target>();

        //    Assert.That(targetList.Count, Is.EqualTo(sourceList.Count));

        //    for (int i = 0; i < sourceList.Count; i++)
        //    {
        //        Assert.That(targetList[i].Target1, Is.EqualTo(sourceList[i].Target1));
        //        Assert.That(targetList[i].Target2, Is.EqualTo(sourceList[i].Target2));
        //        Assert.That(targetList[i].Target3, Is.EqualTo(sourceList[i].Target3));
        //        Assert.That(targetList[i].Target4, Is.EqualTo(sourceList[i].Target4));
        //        Assert.That(targetList[i].Target5, Is.EqualTo(sourceList[i].Target5));
        //    }
        //}

        //[Test]
        //public void AutoConvertListInterfaceTest()
        //{
        //    List<IAutoSource> sourceList = new List<IAutoSource>();
        //    sourceList.Add(new AutoSource(0, "0", null, Guid.NewGuid(), 1));
        //    sourceList.Add(new AutoSourceLarge(2, "0", null, Guid.NewGuid(), 3, 13));
        //    sourceList.Add(new AutoSource(4, "0", null, Guid.NewGuid(), 5));

        //    List<Target> targetList = sourceList.AutoConvertList<IAutoSource, Target>();

        //    Assert.That(targetList.Count, Is.EqualTo(sourceList.Count));

        //    for (int i = 0; i < sourceList.Count; i++)
        //    {
        //        Assert.That(targetList[i].Target1, Is.EqualTo(sourceList[i].Target1));
        //        Assert.That(targetList[i].Target2, Is.EqualTo(sourceList[i].Target2));
        //        Assert.That(targetList[i].Target3, Is.EqualTo(sourceList[i].Target3));
        //        Assert.That(targetList[i].Target4, Is.EqualTo(sourceList[i].Target4));
        //        Assert.That(targetList[i].Target5, Is.EqualTo(sourceList[i].Target5));
        //    }
        //}

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
