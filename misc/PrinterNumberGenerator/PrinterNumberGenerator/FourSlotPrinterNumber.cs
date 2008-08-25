using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PrinterNumberGenerator
{
    public class FourSlotPrinterNumber : IComparable, IComparable<FourSlotPrinterNumber>
    {
        private static readonly int SLOT_COUNT = 4;

        public FourSlotPrinterNumber(string stringRepresentation)
        {
            if (stringRepresentation == null) { throw new ArgumentNullException("stringRepresentation"); }
            if (stringRepresentation.Length != SLOT_COUNT) { throw new ArgumentException("Not four slots", "stringRepresentation"); }
            if (!CheckStringRepresentation(stringRepresentation)) { throw new ArgumentException("Failed check", "stringRepresentation"); }



            m_stringRepresentation = stringRepresentation;
            m_longRepresentation = GenerateLongRepresentation();
        }

        private static readonly char[] VALID_CHARS;
        static FourSlotPrinterNumber()
        {
            List<char> validChars = new List<char>();

            for (char i = (char)0; i < 10; i++)
            {
                validChars.Add(i);
            }
            for (char i = (char)10; i < 36; i++)
            {
                validChars.Add(i);
            }

            VALID_CHARS = validChars.ToArray();
        }

        private bool CheckStringRepresentation(string stringRepresentation)
        {
            foreach (var item in stringRepresentation)
            {
                if (!VALID_CHARS.Contains(item)) { return false; }
            }
            return true;
        }

        public FourSlotPrinterNumber(long longRepresentation)
        {
            if (longRepresentation < 0) { throw new ArgumentException(string.Format("{0} does not handle negative numbers", this.GetType().Name), "longRepresentation"); }

            m_longRepresentation = longRepresentation;
            m_stringRepresentation = GenerateStringRepresentation();
        }

        private long m_longRepresentation;
        public long ToLong()
        {
            return m_longRepresentation;
        }

        private string m_stringRepresentation;
        public override string ToString()
        {
            return m_stringRepresentation;
        }

        private long GenerateLongRepresentation()
        {
            for (int i = 0; i < SLOT_COUNT; i++)
            {
                char current = m_stringRepresentation[i];
            }
            throw new NotImplementedException();
        }

        private string GenerateStringRepresentation()
        {
            return string.Format("100{0}", m_longRepresentation);

        }

        private static readonly int CONVERT_BASE = 36;
        private static readonly int CHAR_SHIFT_A = (int)'a' - 10;
        private static readonly int CHAR_SHIFT_ZERO = (int)'0';

        public static string ConvertToBase36(int number)
        {
            int carry;
            StringBuilder stringRepresentation = new StringBuilder();

            do
            {
                carry = number % CONVERT_BASE;

                stringRepresentation.Insert(0, ConvertToChar(carry));

                number = (int)(number / CONVERT_BASE);
            }
            while (!(number == 0));

            return stringRepresentation.ToString();
        }

        public static char ConvertToChar(int base36Int)
        {
            if (base36Int < 0) { throw new ArgumentException("Can't be negative", "base36Int"); }

            if (base36Int < 10)
            {
                return (char)(base36Int + CHAR_SHIFT_ZERO);
            }
            else
            {
                return (char)(base36Int + CHAR_SHIFT_A);
            }
        }

        public static int ConvertToInt(char base36Char)
        {
            if ((base36Char >= 'a') && (base36Char <= 'z'))
            {
                return (int)base36Char - CHAR_SHIFT_A;
            }
            else if ((base36Char >= '0') && (base36Char <= '9'))
            {
                return (int)base36Char - CHAR_SHIFT_ZERO;
            }
            else
            {
                throw new ArgumentException("not a valid char", "base36Char");
            }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return CompareTo(obj as FourSlotPrinterNumber);
        }

        #endregion

        #region IComparable<FourSlotPrinterNumber> Members

        public int CompareTo(FourSlotPrinterNumber other)
        {
            if (other == null) { throw new ArgumentNullException("other"); }
            return m_longRepresentation.CompareTo(other.m_longRepresentation);
        }

        #endregion
    }
}
