using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogViewerTest.Framework.Reflection;
using System.Linq.Expressions;
using FastDynamicPropertyAccessor;
using System.Reflection;

namespace LogViewerTest.Framework.DTO
{
    public class DtoMap<SOURCE, TARGET> where TARGET : new()
    {
        public static DtoMap<SOURCE, TARGET> New
        {
            get
            {
                return new DtoMap<SOURCE, TARGET>();
            }
        }

        List<MappingDefinition> m_map = new List<MappingDefinition>();
        public DtoMap<SOURCE, TARGET> AddMap(Expression<Func<SOURCE, object>> source, Expression<Func<TARGET, object>> target)
        {
            m_map.Add(new DtoMap<SOURCE, TARGET>.MappingDefinition(source, target));
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

    public static class DtoMapExtension
    {
        public static TARGET Map<SOURCE, TARGET>(this SOURCE source, DtoMap<SOURCE, TARGET> mappingDefinition) where TARGET : new()
        {
            TARGET target = new TARGET();
            mappingDefinition.Map(source, target);
            return target;
        }

        public static TARGET AutoConvert<SOURCE, TARGET>(this SOURCE source) where TARGET : new()
        {
            if (source == null) { return default(TARGET); }

            Type sourceType = typeof(SOURCE);
            Type targetType = typeof(TARGET);

            PropertyInfo[] sourcePropertyInfoListe = CreatePropertyInfoz(sourceType);
            PropertyInfo[] targetPropertyInfoListe = CreatePropertyInfoz(targetType);

            return AutoConvert<SOURCE, TARGET>(source, sourceType, targetType, sourcePropertyInfoListe, targetPropertyInfoListe);
        }

        private static TARGET AutoConvert<SOURCE, TARGET>(SOURCE source, Type sourceType, Type targetType, PropertyInfo[] sourcePropertyInfoListe, PropertyInfo[] targetPropertyInfoListe) where TARGET : new()
        {
            TARGET target = new TARGET();

            foreach (PropertyInfo targetProperty in targetPropertyInfoListe)
            {
                PropertyInfo sourceProperty = (from s in sourcePropertyInfoListe
                                               where s.Name == targetProperty.Name
                                               select s).FirstOrDefault();

                if (sourceProperty == null) { throw new TargetException(string.Format("Could not find property {0} on {1} to convert {2}", targetProperty.Name, targetType.Name, sourceType.Name)); }

                PropertyAccessor sourceAccessor = GetAccessor(sourceType, sourceProperty);
                PropertyAccessor targetAccessor = GetAccessor(targetType, targetProperty);

                targetAccessor.Set(target, sourceAccessor.Get(source));
            }

            return target;
        }

        private static Dictionary<string, PropertyAccessor> m_propertyAccessorz = new Dictionary<string, PropertyAccessor>();

        private static PropertyAccessor GetAccessor(Type type, PropertyInfo property)
        {
            string key = type.AssemblyQualifiedName + "#^#" + property.Name;

            PropertyAccessor accessor;
            if (!m_propertyAccessorz.TryGetValue(key, out accessor))
            {
                lock (m_propertyAccessorz)
                {
                    if (!m_propertyAccessorz.TryGetValue(key, out accessor))
                    {
                        accessor = new PropertyAccessor(type, property.Name);
                        m_propertyAccessorz[key] = accessor;
                    }
                }
            }

            return accessor;
        }

        public static List<TARGET>
            CastList<SOURCE, TARGET>(this IEnumerable<SOURCE> sourcez)
            where SOURCE : TARGET
        {
            return sourcez.Cast<TARGET>().ToList();
        }

        public static List<TARGET> AutoConvertList<SOURCE, TARGET>(this IEnumerable<SOURCE> sourcez) where TARGET : new()
        {
            Type sourceType = typeof(SOURCE);
            Type targetType = typeof(TARGET);

            PropertyInfo[] sourcePropertyInfoListe = CreatePropertyInfoz(sourceType);
            PropertyInfo[] targetPropertyInfoListe = CreatePropertyInfoz(targetType);

            List<TARGET> result = new List<TARGET>();

            foreach (var source in sourcez)
            {
                result.Add(AutoConvert<SOURCE, TARGET>(source, sourceType, targetType, sourcePropertyInfoListe, targetPropertyInfoListe));
            }

            return result;
        }

        private static PropertyInfo[] CreatePropertyInfoz(Type type)
        {
            return DeepInterfaceDiscoverage.Find(type);
            //return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic);
        }
    }
}
