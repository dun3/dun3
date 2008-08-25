using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using Com.Hertkorn.Framework.Automap.Reflection;
using FastDynamicPropertyAccessor;

namespace Com.Hertkorn.Framework.Automap
{
    public class ManualMap<SOURCE, TARGET> where TARGET : new()
    {
        public static ManualMap<SOURCE, TARGET> New
        {
            get
            {
                return new ManualMap<SOURCE, TARGET>();
            }
        }

        List<MappingDefinition> m_map = new List<MappingDefinition>();
        public ManualMap<SOURCE, TARGET> AddMap(Expression<Func<SOURCE, object>> source, Expression<Func<TARGET, object>> target)
        {
            m_map.Add(new ManualMap<SOURCE, TARGET>.MappingDefinition(source, target));
            return this;
        }

        public void Map(SOURCE source, TARGET target)
        {
            foreach (MappingDefinition mappingDefinition in m_map)
            {
                mappingDefinition.Target.SetValue(target, mappingDefinition.Source.GetValue(source));
            }
        }

        class MappingDefinition
        {
            public Accessor Source { get; set; }
            public Accessor Target { get; set; }

            /// <summary>
            /// Initializes a new instance of the Map class.
            /// </summary>
            public MappingDefinition(Expression<Func<SOURCE, object>> source, Expression<Func<TARGET, object>> target)
            {

                Source = ReflectionHelper.GetAccessor(source);
                Target = ReflectionHelper.GetAccessor(target);
            }
        }
    }

    public static class ManualMapExtension
    {
        public static TARGET Map<SOURCE, TARGET>(this SOURCE source, ManualMap<SOURCE, TARGET> mappingDefinition) where TARGET : new()
        {
            TARGET target = new TARGET();
            mappingDefinition.Map(source, target);
            return target;
        }

        public static List<TARGET> MapList<SOURCE, TARGET>(this IEnumerable<SOURCE> sourcez, ManualMap<SOURCE, TARGET> mappingDefinition) where TARGET : new()
        {
            List<TARGET> result = new List<TARGET>();

            foreach (var source in sourcez)
            {
                TARGET target = new TARGET();
                mappingDefinition.Map(source, target);
                result.Add(target);
            }

            return result;
        }
    }
}
