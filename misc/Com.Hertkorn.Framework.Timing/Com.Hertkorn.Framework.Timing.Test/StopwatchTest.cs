using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.Timing
{
    [TestFixture]
    public class StopwatchTest
    {
        [Test]
        public void TimeActionTest()
        {
            Stopwatch s = Stopwatch.Time(() =>
            {
                System.Threading.Thread.Sleep(100);
            });

            Assert.That(s.IsRunning, Is.False);
            Assert.That(s.ElapsedMilliseconds, Is.GreaterThan(90));
        }

        [Test]
        public void TimeActionNamingTest()
        {
            string name = "Test";
            Stopwatch s = Stopwatch.Time(name, () =>
            {
                System.Threading.Thread.Sleep(100);
            });

            Assert.That(s.IsRunning, Is.False);
            Assert.That(s.ElapsedMilliseconds, Is.GreaterThan(90));
            Assert.That(s.Name, Is.EqualTo(name));
        }

    }
}
