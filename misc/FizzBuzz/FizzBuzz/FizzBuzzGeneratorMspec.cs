using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzBuzz
{
    [Subject(typeof(FizzBuzzGenerator))]
    public class when_generating_the_sequence : with_output_as_list
    {
        Establish context = () =>
        {
            FizzBuzzGenerator fizzBuzzGenerator = new FizzBuzzGenerator();
            output = fizzBuzzGenerator.ToList();
        };

        It should_output__1__for_the_first_element = () =>
        {
            output.First().ShouldEqual("1");
        };

        It should_output__Buzz__for_the_last_element = () =>
        {
            output.Last().ShouldEqual("Buzz");
        };

        It should_output__Fizz__for_any_items__mod_3_and_not_mod_5__ = () =>
        {
            TestForAllItems(i => ((i % 3) == 0 && (i % 5) != 0), item => item.ShouldEqual("Fizz"));
        };

        It should_not_output__Fizz__for_any_items_not__mod_3_and_not_mod_5__ = () =>
        {
            TestForAllItems(i => !((i % 3) == 0 && (i % 5) != 0), item => item.ShouldNotEqual("Fizz"));
        };

        It should_output__Buzz__for_any_items__mod_5_and_not_mod_3__ = () =>
        {
            TestForAllItems(i => ((i % 5) == 0 && (i % 3) != 0), item => item.ShouldEqual("Buzz"));
        };

        It should_not_output__Buzz__for_any_items_not__mod_5_and_not_mod_3__ = () =>
        {
            TestForAllItems(i => !((i % 5) == 0 && (i % 3) != 0), item => item.ShouldNotEqual("Buzz"));
        };

        It should_output__FizzBuzz__for_any_items__mod_15__ = () =>
        {
            TestForAllItems(i => (i % 15) == 0, item => item.ShouldEqual("FizzBuzz"));
        };

        It should_not_output__FizzBuzz__for_any_items_not__mod_15__ = () =>
        {
            TestForAllItems(i => (i % 15) != 0, item => item.ShouldNotEqual("FizzBuzz"));
        };

        It should_output_numbers_for_any_items_not__mod_3_or_mod_5__ = () =>
        {
            TestForAllItems(i => !((i % 3) == 0 || (i % 5) == 0), (i, item) => item.ShouldEqual(i.ToString()));
        };

        It should_output_100_items = () =>
        {
            output.Count.ShouldEqual(100);
        };

        Cleanup after_each = () =>
            output = null;
    }

    public class with_output_as_list
    {
        public static IList<string> output;

        public static void TestForAllItems(Func<int, bool> testThisNumber, Action<string> assert)
        {
            for (int i = 1; i <= output.Count; i++)
            {
                if (testThisNumber(i))
                {
                    assert(output[i - 1]);
                }
            }
        }

        public static void TestForAllItems(Func<int, bool> testThisNumber, Action<int, string> assert)
        {
            for (int i = 1; i <= output.Count; i++)
            {
                if (testThisNumber(i))
                {
                    assert(i, output[i - 1]);
                }
            }
        }
    }
}
