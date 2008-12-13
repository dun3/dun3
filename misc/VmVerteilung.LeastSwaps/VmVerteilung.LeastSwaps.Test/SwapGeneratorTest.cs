using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace VmVerteilung.LeastSwaps
{
    [TestFixture]
    public class SwapGeneratorTest
    {
        private SwapGenerator m_swapGenerator;

        [SetUp]
        public void SetUp()
        {
            m_swapGenerator = new SwapGenerator();
        }

        [Test]
        public void ZeroSwapTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>(before[0].Vmz));
            targetDefinition.Add(new List<Vm>(before[1].Vmz));
            targetDefinition.Add(new List<Vm>(before[2].Vmz));
            targetDefinition.Add(new List<Vm>(before[3].Vmz));

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(0));
        }

        [Test]
        public void ZeroSwapScrambledTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>(before[1].Vmz));
            targetDefinition.Add(new List<Vm>(before[3].Vmz));
            targetDefinition.Add(new List<Vm>(before[2].Vmz));
            targetDefinition.Add(new List<Vm>(before[0].Vmz));

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(0));
        }

        [Test]
        public void ZeroSwapUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[0].Vmz[0], before[0].Vmz[3], before[0].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[2], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[2].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(0));
        }

        [Test]
        public void ZeroSwapScrambledUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[2], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[2].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[0].Vmz[0], before[0].Vmz[3], before[0].Vmz[2] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(0));
        }

        [Test]
        public void OneSwapUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[0].Vmz[0], before[0].Vmz[3], before[0].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[2].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0], before[1].Vmz[2] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(1));
            Assert.That(swaps[0].Vm, Is.EqualTo(before[1].Vmz[2]));
            Assert.That(swaps[0].Target, Is.EqualTo(before[3]));
        }

        [Test]
        public void OneSwapScrambledUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0], before[1].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[2].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[0].Vmz[0], before[0].Vmz[3], before[0].Vmz[2] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(1));
            Assert.That(swaps[0].Vm, Is.EqualTo(before[1].Vmz[2]));
            Assert.That(swaps[0].Target, Is.EqualTo(before[3]));
        }

        [Test]
        public void TwoSwapUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[0].Vmz[3], before[0].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[2].Vmz[0], before[1].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0], before[0].Vmz[0] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(2));
            Assert.That(swaps, Has.Some.Property("Vm").EqualTo(before[1].Vmz[2]));
            Assert.That(swaps, Has.Some.Property("Vm").EqualTo(before[0].Vmz[0]));
        }

        [Test]
        public void TwoSwapScrambledUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0], before[1].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[2].Vmz[0], before[0].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[0].Vmz[3], before[0].Vmz[2] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(2));
            Assert.That(swaps, Has.Some.Property("Vm").EqualTo(before[1].Vmz[2]));
            Assert.That(swaps, Has.Some.Property("Vm").EqualTo(before[0].Vmz[0]));
        }

        [Test]
        public void TwoSwapInplaceUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[0].Vmz[0], before[0].Vmz[3], before[0].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[2], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[2].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(2));
        }

        [Test]
        public void TwoSwapInplaceScrambledUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[2], before[2].Vmz[0], before[1].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[1].Vmz[1] });
            targetDefinition.Add(new List<Vm>() { before[0].Vmz[1], before[0].Vmz[0], before[0].Vmz[3], before[0].Vmz[2] });

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(2));
        }

        [Test]
        public void FullSwapScrambledUnorderedTest()
        {
            List<Store> before = GenerateBefore();
            List<List<Vm>> targetDefinition = new List<List<Vm>>();
            targetDefinition.Add(new List<Vm>() { before[1].Vmz[1], before[1].Vmz[2], before[1].Vmz[0], before[0].Vmz[1], before[0].Vmz[0], before[0].Vmz[3], before[0].Vmz[2] });
            targetDefinition.Add(new List<Vm>() { before[3].Vmz[0] });
            targetDefinition.Add(new List<Vm>() { before[2].Vmz[1], before[2].Vmz[0] });
            targetDefinition.Add(new List<Vm>());

            var swaps = m_swapGenerator.GenerateSwaps(before, targetDefinition);

            Assert.That(swaps.Count, Is.EqualTo(3));
            Assert.That(swaps, Has.Some.Property("Vm").EqualTo(before[1].Vmz[0]));
            Assert.That(swaps, Has.Some.Property("Vm").EqualTo(before[1].Vmz[1]));
            Assert.That(swaps, Has.Some.Property("Vm").EqualTo(before[1].Vmz[2]));
        }

        private static List<Store> GenerateBefore()
        {
            Store one = new Store("one");
            one.Vmz.Add(new Vm("1_1") { Store = one });
            one.Vmz.Add(new Vm("1_2") { Store = one });
            one.Vmz.Add(new Vm("1_3") { Store = one });
            one.Vmz.Add(new Vm("1_4") { Store = one });

            Store two = new Store("two");
            two.Vmz.Add(new Vm("2_5") { Store = two });
            two.Vmz.Add(new Vm("2_6") { Store = two });
            two.Vmz.Add(new Vm("2_7") { Store = two });

            Store three = new Store("three");
            three.Vmz.Add(new Vm("3_8") { Store = three });
            three.Vmz.Add(new Vm("3_9") { Store = three });

            Store four = new Store("four");
            four.Vmz.Add(new Vm("4_10") { Store = four });

            List<Store> before = new List<Store>() { one, two, three, four };

            return before;
        }
    }
}
