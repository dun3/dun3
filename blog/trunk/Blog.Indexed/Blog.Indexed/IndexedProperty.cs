using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Indexed
{
    public class IndexedProperty<Index, Return>
    {
        private Func<Index, Return> m_getter;
        private Action<Index, Return> m_setter;

        public IndexedProperty(Func<Index, Return> getter, Action<Index, Return> setter)
        {
            if (getter == null) { throw new ArgumentNullException("getter", "getter is null."); }
            if (setter == null) { throw new ArgumentNullException("setter", "setter is null."); }

            m_getter = getter;
            m_setter = setter;
        }

        public Return this[Index index]
        {
            get { return m_getter(index); }
            set { m_setter(index, value); }
        }
    }
}
