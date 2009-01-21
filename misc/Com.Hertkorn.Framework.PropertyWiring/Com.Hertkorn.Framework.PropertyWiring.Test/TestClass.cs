using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.PropertyWiring
{
    public class TestClass
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public object ObjectProperty { get; set; }

        public object ObjectMethod()
        {
            throw new NotImplementedException();
        }
        public int IntMethod()
        {
            throw new NotImplementedException();
        }
    }
}
