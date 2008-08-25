using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.Automap
{
    [TestFixture]
    public class ManualMapTest
    {
        [Test]
        public void ManualMapGeneralTest()
        {
            ManualMap<Source, Target> map = ManualMap<Source, Target>.New
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


        public class Target
        {
            public int Target1 { get; set; }
            public string Target2 { get; set; }
            public object Target3 { get; set; }
            public Guid Target4 { get; set; }
            public int Target5 { get; set; }
        }
    }
}