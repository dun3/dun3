using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.DuckTypingLight
{
    public static class Duck
    {
        public static T AsDuck<T>(this object toBeDucked)
        {
            throw new NotImplementedException();
        }
    }
}
