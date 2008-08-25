using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Codegeneration.Classes
{
    public class ClassDefinition
    {
        public ClassDefinition(string name)
        {
            Name = name;
            IsPartial = false;
        }

        public ClassDefinition(string name, bool isPartial)
        {
            Name = name;
            IsPartial = isPartial;
        }

        public string Name { get; set; }

        public virtual bool IsClass { get { return true; } }

        public bool IsPartial { get; set; }
    }
}
