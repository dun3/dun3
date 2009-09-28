using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzBuzz
{
    public class FizzBuzzGenerator : IEnumerable<string>
    {

        #region IEnumerable<string> Members

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 1; i <= 100; i++)
            {
                if ((i % 15) == 0)
                {
                    yield return "FizzBuzz";
                }
                else if ((i % 3) == 0)
                {
                    yield return "Fizz";
                }
                else if ((i % 5) == 0)
                {
                    yield return "Buzz";
                }
                else
                {
                    yield return i.ToString();
                }
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
