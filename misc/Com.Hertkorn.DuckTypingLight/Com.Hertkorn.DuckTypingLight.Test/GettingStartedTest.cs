using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Com.Hertkorn.DuckTypingLight
{
    [TestFixture]
    public class GettingStartedTest
    {
        [Test]
        public void SimpleTest()
        {
            var test = new { Value = 1 };
            var result = CallSimpleDuck(test.AsDuck<ISimpleDuckInterface>());
        }

        [Test]
        public void MethodSimulationTest()
        {
            var test = new { Value = 1 };
            var result = CallSimpleDuck(test.AsDuck<ISimpleDuckInterface>());
        }

        private int CallSimpleDuck(ISimpleDuckInterface duck)
        {
            return duck.Value;
        }

    }

    public interface ISimpleDuckInterface
    {
        int Value { get; }
    }

    public interface IWithMethodsDuckInterface
    {
        Func<int, int> AddAndReport { get; }
        Func<int> IncrementAndReport { get; }
        Action<int> Add { get; }
        Action Increment { get; }
        int Current { get; }
    }
}
