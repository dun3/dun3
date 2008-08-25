using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.Automap
{
    [TestFixture]
    public class LinqMapTest
    {
        [Test]
        public void ManualMapGeneralTest()
        {
            LinqMap<LinqMapSource, LinqMapTarget> map = LinqMap<LinqMapSource, LinqMapTarget>.New
                .AddMap(a => a.Source1, (a, b) => a.Target1 = b)
                .AddMap(a => a.Source2, (a, b) => a.Target2 = b)
                .AddMap(a => a.Source3, (a, b) => a.Target3 = b)
                .AddMap(a => a.Source4, (a, b) => a.Target4 = b)
                .AddMap(a => a.Source5, (a, b) => a.Target5 = b);

            LinqMapSource source = new LinqMapSource(1, 2, 3, 4, 5);

            LinqMapTarget target = new LinqMapTarget();
            Assert.That(target.Target1, Is.EqualTo(default(int)));
            Assert.That(target.Target2, Is.EqualTo(default(int)));
            Assert.That(target.Target3, Is.EqualTo(default(int)));
            Assert.That(target.Target4, Is.EqualTo(default(int)));
            Assert.That(target.Target5, Is.EqualTo(default(int)));

            map.Map(source, target);

            Assert.That(target.Target1, Is.EqualTo(source.Source1));
            Assert.That(target.Target2, Is.EqualTo(source.Source2));
            Assert.That(target.Target3, Is.EqualTo(source.Source3));
            Assert.That(target.Target4, Is.EqualTo(source.Source4));
            Assert.That(target.Target5, Is.EqualTo(source.Source5));
        }

        public class LinqMapSource
        {
            /// <summary>
            /// Initializes a new instance of the Source class.
            /// </summary>
            public LinqMapSource(int source1, int source2, int source3, int source4, int source5)
            {
                Source1 = source1;
                Source2 = source2;
                Source3 = source3;
                Source4 = source4;
                Source5 = source5;
            }
            public int Source1 { get; set; }
            public int Source2 { get; set; }
            public int Source3 { get; set; }
            public int Source4 { get; set; }
            public int Source5 { get; set; }
        }


        public class LinqMapTarget
        {
            public int Target1 { get; set; }
            public int Target2 { get; set; }
            public int Target3 { get; set; }
            public int Target4 { get; set; }
            public int Target5 { get; set; }
        }
    }
}
