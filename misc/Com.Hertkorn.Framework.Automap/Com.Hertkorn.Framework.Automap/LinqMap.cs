using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Com.Hertkorn.Framework.Automap
{
    public class LinqMap<SOURCE, TARGET> where TARGET : class, new()
    {
        public static LinqMap<SOURCE, TARGET> New
        {
            get
            {
                return new LinqMap<SOURCE, TARGET>();
            }
        }

        private List<MappingDefinition<int>> intMap = new List<MappingDefinition<int>>();

        public LinqMap<SOURCE, TARGET> AddMap(Func<SOURCE, int> source, Action<TARGET, int> target)
        {
            intMap.Add(new LinqMap<SOURCE, TARGET>.MappingDefinition<int>(source, target));

            return this;
        }

        public void Map(SOURCE source, TARGET target)
        {
            foreach (var item in intMap)
            {
                item.Target(target, item.Source(source));
            }
        }

        class MappingDefinition<TARGETTYPE>
        {
            public Func<SOURCE, TARGETTYPE> Source { get; set; }
            public Action<TARGET, TARGETTYPE> Target { get; set; }

            /// <summary>
            /// Initializes a new instance of the Map class.
            /// </summary>
            public MappingDefinition(Func<SOURCE, TARGETTYPE> source, Action<TARGET, TARGETTYPE> target)
            {
                Source = source;
                Target = target;
            }
        }
    }
}
