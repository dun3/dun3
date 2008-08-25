using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.Flags.Test
{
    [TestFixture]
    public class FlagHelperTest
    {
        [Test]
        public void Flag_is_set()
        {
            Assert.That(TestEnum.None.IsSet(TestEnum.None), Is.True);

            Assert.That(TestEnum.Flag1.IsSet(TestEnum.Flag1), Is.True);
            Assert.That(TestEnum.Flag2.IsSet(TestEnum.Flag2), Is.True);
            Assert.That(TestEnum.Flag3.IsSet(TestEnum.Flag3), Is.True);

            Assert.That((TestEnum.Flag1 | TestEnum.Flag2).IsSet(TestEnum.Flag1), Is.True);
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2).IsSet(TestEnum.Flag2), Is.True);

            Assert.That((TestEnum.Flag1 | TestEnum.Flag3).IsSet(TestEnum.Flag1), Is.True);
            Assert.That((TestEnum.Flag1 | TestEnum.Flag3).IsSet(TestEnum.Flag3), Is.True);

            Assert.That((TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag2), Is.True);
            Assert.That((TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag3), Is.True);

            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag1), Is.True);
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag2), Is.True);
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag3), Is.True);

            // Combined

            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag1 | TestEnum.Flag2), Is.True);
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag1 | TestEnum.Flag3), Is.True);
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag2 | TestEnum.Flag3), Is.True);

            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).IsSet(TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3), Is.True);
        }

        [Test]
        public void Flag_not_is_set()
        {
            Assert.That(FlagHelper.IsSet(TestEnum.None, TestEnum.Flag1), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.None, TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.None, TestEnum.Flag3), Is.False);

            Assert.That(FlagHelper.IsSet(TestEnum.Flag1, TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag1, TestEnum.Flag3), Is.False);

            Assert.That(FlagHelper.IsSet(TestEnum.Flag2, TestEnum.Flag1), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag2, TestEnum.Flag3), Is.False);

            Assert.That(FlagHelper.IsSet(TestEnum.Flag3, TestEnum.Flag1), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag3, TestEnum.Flag2), Is.False);

            Assert.That(FlagHelper.IsSet(TestEnum.Flag1 | TestEnum.Flag2, TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag1 | TestEnum.Flag3, TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag2 | TestEnum.Flag3, TestEnum.Flag1), Is.False);

            // Combined

            Assert.That(FlagHelper.IsSet(TestEnum.Flag1, TestEnum.Flag1 | TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag1, TestEnum.Flag1 | TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag1, TestEnum.Flag2 | TestEnum.Flag3), Is.False);

            Assert.That(FlagHelper.IsSet(TestEnum.Flag2, TestEnum.Flag1 | TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag2, TestEnum.Flag1 | TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag2, TestEnum.Flag2 | TestEnum.Flag3), Is.False);

            Assert.That(FlagHelper.IsSet(TestEnum.Flag3, TestEnum.Flag1 | TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag3, TestEnum.Flag1 | TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag3, TestEnum.Flag2 | TestEnum.Flag3), Is.False);

            Assert.That(FlagHelper.IsSet(TestEnum.Flag1 | TestEnum.Flag2, TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag1 | TestEnum.Flag3, TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsSet(TestEnum.Flag2 | TestEnum.Flag3, TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3), Is.False);
        }

        [Test]
        public void Flag_is_none_set()
        {
            Assert.That(FlagHelper.IsNoneSet(TestEnum.None), Is.True);
            Assert.That(FlagHelper.IsNoneSet(TestEnum.Flag1), Is.False);
            Assert.That(FlagHelper.IsNoneSet(TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsNoneSet(TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsNoneSet(TestEnum.Flag1 | TestEnum.Flag2), Is.False);
            Assert.That(FlagHelper.IsNoneSet(TestEnum.Flag1 | TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsNoneSet(TestEnum.Flag2 | TestEnum.Flag3), Is.False);
            Assert.That(FlagHelper.IsNoneSet(TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3), Is.False);
        }

        [Test]
        public void Flag_is_any_set()
        {
            Assert.That(FlagHelper.IsAnySet(TestEnum.None), Is.False);
            Assert.That(FlagHelper.IsAnySet(TestEnum.Flag1), Is.True);
            Assert.That(FlagHelper.IsAnySet(TestEnum.Flag2), Is.True);
            Assert.That(FlagHelper.IsAnySet(TestEnum.Flag3), Is.True);
            Assert.That(FlagHelper.IsAnySet(TestEnum.Flag1 | TestEnum.Flag2), Is.True);
            Assert.That(FlagHelper.IsAnySet(TestEnum.Flag1 | TestEnum.Flag3), Is.True);
            Assert.That(FlagHelper.IsAnySet(TestEnum.Flag2 | TestEnum.Flag3), Is.True);
            Assert.That(FlagHelper.IsAnySet(TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3), Is.True);
        }

        [Test]
        public void Flag_set()
        {
            Assert.That(TestEnum.None.Set(TestEnum.None), Is.EqualTo(TestEnum.None));

            Assert.That(TestEnum.None.Set(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag1));
            Assert.That(TestEnum.None.Set(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag2));
            Assert.That(TestEnum.None.Set(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag3));

            Assert.That(TestEnum.Flag1.Set(TestEnum.None), Is.EqualTo(TestEnum.Flag1));
            Assert.That(TestEnum.Flag1.Set(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag1));
            Assert.That(TestEnum.Flag1.Set(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag2));
            Assert.That(TestEnum.Flag1.Set(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag3));

            Assert.That(TestEnum.Flag2.Set(TestEnum.None), Is.EqualTo(TestEnum.Flag2));
            Assert.That(TestEnum.Flag2.Set(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag2 | TestEnum.Flag1));
            Assert.That(TestEnum.Flag2.Set(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag2));
            Assert.That(TestEnum.Flag2.Set(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag2 | TestEnum.Flag3));

            Assert.That(TestEnum.Flag3.Set(TestEnum.None), Is.EqualTo(TestEnum.Flag3));
            Assert.That(TestEnum.Flag3.Set(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag3 | TestEnum.Flag1));
            Assert.That(TestEnum.Flag3.Set(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag3 | TestEnum.Flag2));
            Assert.That(TestEnum.Flag3.Set(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag3));

            Assert.That(TestEnum.Flag1.Set(TestEnum.Flag2).Set(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3));
        }

        [Test]
        public void Flag_unset()
        {
            Assert.That(TestEnum.None.Unset(TestEnum.None), Is.EqualTo(TestEnum.None));

            Assert.That(TestEnum.None.Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.None));
            Assert.That(TestEnum.None.Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.None));
            Assert.That(TestEnum.None.Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.None));

            Assert.That(TestEnum.Flag1.Unset(TestEnum.None), Is.EqualTo(TestEnum.Flag1));
            Assert.That(TestEnum.Flag1.Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.None));
            Assert.That(TestEnum.Flag1.Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag1));
            Assert.That(TestEnum.Flag1.Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag1));

            Assert.That(TestEnum.Flag2.Unset(TestEnum.None), Is.EqualTo(TestEnum.Flag2));
            Assert.That(TestEnum.Flag2.Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag2));
            Assert.That(TestEnum.Flag2.Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.None));
            Assert.That(TestEnum.Flag2.Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag2));

            Assert.That(TestEnum.Flag3.Unset(TestEnum.None), Is.EqualTo(TestEnum.Flag3));
            Assert.That(TestEnum.Flag3.Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag3));
            Assert.That(TestEnum.Flag3.Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag3));
            Assert.That(TestEnum.Flag3.Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.None));

            Assert.That((TestEnum.Flag1 | TestEnum.Flag2).Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag2));
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2).Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag1));
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2).Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag2));

            Assert.That((TestEnum.Flag1 | TestEnum.Flag3).Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag3));
            Assert.That((TestEnum.Flag1 | TestEnum.Flag3).Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag3));
            Assert.That((TestEnum.Flag1 | TestEnum.Flag3).Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag1));

            Assert.That((TestEnum.Flag2 | TestEnum.Flag3).Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag2 | TestEnum.Flag3));
            Assert.That((TestEnum.Flag2 | TestEnum.Flag3).Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag3));
            Assert.That((TestEnum.Flag2 | TestEnum.Flag3).Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag2));

            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).Unset(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag2 | TestEnum.Flag3));
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).Unset(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag3));
            Assert.That((TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3).Unset(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag2));
        }

        [Test]
        public void Flag_toggle()
        {
            Assert.That(TestEnum.None.Toggle(TestEnum.None), Is.EqualTo(TestEnum.None));

            Assert.That(TestEnum.None.Toggle(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag1));
            Assert.That(TestEnum.None.Toggle(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag2));
            Assert.That(TestEnum.None.Toggle(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag3));

            Assert.That(TestEnum.Flag1.Toggle(TestEnum.None), Is.EqualTo(TestEnum.Flag1));
            Assert.That(TestEnum.Flag1.Toggle(TestEnum.Flag1), Is.EqualTo(TestEnum.None));
            Assert.That(TestEnum.Flag1.Toggle(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag2));
            Assert.That(TestEnum.Flag1.Toggle(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag3));

            Assert.That(TestEnum.Flag2.Toggle(TestEnum.None), Is.EqualTo(TestEnum.Flag2));
            Assert.That(TestEnum.Flag2.Toggle(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag2));
            Assert.That(TestEnum.Flag2.Toggle(TestEnum.Flag2), Is.EqualTo(TestEnum.None));
            Assert.That(TestEnum.Flag2.Toggle(TestEnum.Flag3), Is.EqualTo(TestEnum.Flag2 | TestEnum.Flag3));

            Assert.That(TestEnum.Flag3.Toggle(TestEnum.None), Is.EqualTo(TestEnum.Flag3));
            Assert.That(TestEnum.Flag3.Toggle(TestEnum.Flag1), Is.EqualTo(TestEnum.Flag1 | TestEnum.Flag3));
            Assert.That(TestEnum.Flag3.Toggle(TestEnum.Flag2), Is.EqualTo(TestEnum.Flag2 | TestEnum.Flag3));
            Assert.That(TestEnum.Flag3.Toggle(TestEnum.Flag3), Is.EqualTo(TestEnum.None));
        }

        [Test]
        public void Flag_is_composable()
        {
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.None), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.Flag1), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.Flag2), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.Flag3), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.Flag1 | TestEnum.Flag2), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.Flag1 | TestEnum.Flag3), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.Flag2 | TestEnum.Flag3), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(TestEnum.Flag1 | TestEnum.Flag2 | TestEnum.Flag3), Is.True);

            Assert.That(Enum.IsDefined(typeof(TestEnum), (uint)0), Is.True);
            Assert.That(Enum.IsDefined(typeof(TestEnum), (uint)1), Is.True);

            Assert.That(FlagHelper.IsComposable<TestEnum>((uint)0), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>((uint)1), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(Convert.ToUInt32(6)), Is.True);

            Assert.That(FlagHelper.IsComposable<TestEnum>(Convert.ToUInt32(7)), Is.True);
            Assert.That(FlagHelper.IsComposable<TestEnum>(Convert.ToUInt32(8)), Is.False);

            Assert.That(Enum.IsDefined(typeof(TestWithoutNoneEnum), (uint)0), Is.False);
            Assert.That(FlagHelper.IsComposable<TestWithoutNoneEnum>((uint)0), Is.False);
        }

        [Test]
        public void is_n_th_bit_set()
        {
            //Assert.That(FlagHelper.IsBitSet(TestEnum.Flag2, 2));
            Assert.Fail("not implemented");
        }

        [Flags]
        public enum TestEnum : uint
        {
            None = 0x0,
            Flag1 = 0x1 << 0,
            Flag2 = 0x1 << 1,
            Flag3 = 0x1 << 2
        }

        [Flags]
        public enum TestWithoutNoneEnum : uint
        {
            Flag1 = 0x1 << 0,
            Flag2 = 0x1 << 1,
            Flag3 = 0x1 << 2
        }
    }
}
