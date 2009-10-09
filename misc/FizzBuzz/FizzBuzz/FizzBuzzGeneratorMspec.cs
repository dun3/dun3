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
    public class when_generating_the_sequence
    {
        Establish context = () =>
        {
            fizzBuzzGenerator = new FizzBuzzGenerator();
        };

        Because of = () =>
        {
            enumerator = fizzBuzzGenerator.GetEnumerator();
        };

        It should_output__1__for_the_first_element = () =>
        {
            enumerator.MoveNext();
            enumerator.Current.ShouldEqual("1");
        };

        It should_output__Buzz__for_the_last_element = () =>
        {
            string current = null;
            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
            }
            current.ShouldEqual("Buzz");
        };

        It should_output_something_containing__Fizz__for_any_items_mod_3 = () =>
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 3) == 0)
                {
                    Assert.That(enumerator.Current.Contains("Fizz"), Is.True);
                }
            }
        };

        It should_not_output_something_containing__Fizz__for_any_items_mod_3 = () =>
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 3) != 0)
                {
                    Assert.That(enumerator.Current.Contains("Fizz"), Is.False);
                }
            }
        };

        It should_output_something_containing__Buzz__for_any_items_mod_5 = () =>
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 5) == 0)
                {
                    Assert.That(enumerator.Current.Contains("Buzz"), Is.True);
                }
            }
        };

        It should_not_output_something_containing__Buzz__for_any_items_mod_5 = () =>
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 5) != 0)
                {
                    Assert.That(enumerator.Current.Contains("Buzz"), Is.False);
                }
            }
        };

        It should_output__FizzBuzz__for_any_items_mod_15 = () =>
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 15) == 0)
                {
                    Assert.That(enumerator.Current.Contains("FizzBuzz"), Is.True);
                }
            }
        };

        It should_not_output__FizzBuzz__for_any_items_mod_15 = () =>
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if ((i % 15) != 0)
                {
                    Assert.That(enumerator.Current.Contains("FizzBuzz"), Is.False);
                }
            }
        };

        It should_output_numbers_for_any_items_not_mod_3_and_not_mod_5 = () =>
        {
            for (int i = 1; enumerator.MoveNext(); i++)
            {
                if (((i % 3) != 0) && ((i % 5) != 0))
                {
                    enumerator.Current.ShouldEqual(i.ToString());
                }
            }
        };

        It should_output_100_items = () =>
        {
            int i = 0;
            while (enumerator.MoveNext())
            {
                i++;
            }
            i.ShouldEqual(100);
        };

        Cleanup after_each = () =>
            enumerator = null;

        public static IEnumerator<string> enumerator;
        private static FizzBuzzGenerator fizzBuzzGenerator;
    }
}
