using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanSubversionTree
{
    public static class StringBuilderExtension
    {
        public static void RemoveLastCharacter(this StringBuilder builder)
        {
            builder.Remove(builder.Length - 1, 1); // remove last character
        }
    }
}
