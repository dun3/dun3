// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Blog.Indexed
{
    [TestFixture]
    public class IndexedPropertyTest
    {
        [Test]
        public void SimpleTest()
        {
            SettingContainer c = new SettingContainer();

            var t = c.SettingByName["test"];
            Assert.That(t, Is.Null);

            c.SettingByName["test"] = new SettingContainer.Setting() { Name = "test" };

            Assert.That(c.Settingz.Count, Is.EqualTo(1));
        }
    }
}
