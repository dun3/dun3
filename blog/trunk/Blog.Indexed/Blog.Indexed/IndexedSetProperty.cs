// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Indexed
{
    public class IndexedSetProperty<Index, Return>
    {
        private Action<Index, Return> m_setter;

        public IndexedSetProperty(Action<Index, Return> setter)
        {
            if (setter == null) { throw new ArgumentNullException("setter", "setter is null."); }

            m_setter = setter;
        }

        public Return this[Index index]
        {
            set { m_setter(index, value); }
        }
    }
}
