using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FastDynamicPropertyAccessor;
using Com.Hertkorn.Framework.Automap.Reflection;

namespace Com.Hertkorn.Framework.Automap
{
    public static class AutoConverter
    {
        public static void AutoConvert<SOURCE, TARGET>(SOURCE source, TARGET target)
        {
            Type sourceType = typeof(SOURCE);
            Type targetType = typeof(TARGET);

            string[] sourceTypePropertyNamez = PropertyInfoFactory.GetPropertyNamez(sourceType);
            string[] targetTypePropertyNamez = PropertyInfoFactory.GetPropertyNamez(targetType);

            AutoConvert<SOURCE, TARGET>(source, sourceType, target, targetType, sourceTypePropertyNamez, targetTypePropertyNamez);
        }

        internal static void AutoConvert<SOURCE, TARGET>(SOURCE source, Type sourceType, TARGET target, Type targetType, string[] sourcePropertyNamez, string[] targetPropertyNamez)
        {
            Dictionary<string, PropertyAccessor> sourceAccessorz = PropertyAccessorFactory.GetAccessorz(sourceType, sourcePropertyNamez);
            Dictionary<string, PropertyAccessor> targetAccessorz = PropertyAccessorFactory.GetAccessorz(targetType, targetPropertyNamez);

            foreach (string targetProperty in targetPropertyNamez)
            {
                PropertyAccessor targetAccessor = targetAccessorz[targetProperty];

                PropertyAccessor sourceAccessor;
                if (!sourceAccessorz.TryGetValue(targetProperty, out sourceAccessor)) { throw new TargetException(string.Format("Could not find property {0} on {1} to convert {2}", targetProperty, targetType.Name, sourceType.Name)); }

                targetAccessor.Set(target, sourceAccessor.Get(source));
            }
        }
    }


    public static class AutoConverterExtension
    {
        public static TARGET AutoConvert<SOURCE, TARGET>(this SOURCE source) where TARGET : new()
        {
            if (source == null) { return default(TARGET); }

            TARGET target = new TARGET();

            AutoConverter.AutoConvert(source, target);

            return target;
        }

        public static List<TARGET> CastList<SOURCE, TARGET>(this IEnumerable<SOURCE> sourcez)
            where SOURCE : TARGET
        {
            return sourcez.Cast<TARGET>().ToList();
        }

        public static List<TARGET> AutoConvertList<SOURCE, TARGET>(this IEnumerable<SOURCE> sourcez) where TARGET : new()
        {
            List<TARGET> result = new List<TARGET>();

            AutoConvertList<SOURCE, TARGET>(sourcez, result);

            return result;
        }

        public static List<TARGET> AutoConvertList<SOURCE, TARGET>(this ICollection<SOURCE> sourcez) where TARGET : new()
        {
            List<TARGET> result = new List<TARGET>(sourcez.Count);

            AutoConvertList<SOURCE, TARGET>(sourcez, result);

            return result;
        }

        private static void AutoConvertList<SOURCE, TARGET>(IEnumerable<SOURCE> sourcez, List<TARGET> result) where TARGET : new()
        {
            Type sourceType = typeof(SOURCE);
            Type targetType = typeof(TARGET);

            string[] sourcePropertyNamez = PropertyInfoFactory.GetPropertyNamez(sourceType);
            string[] targetPropertyNamez = PropertyInfoFactory.GetPropertyNamez(targetType);

            foreach (var source in sourcez)
            {
                TARGET target = new TARGET();

                AutoConverter.AutoConvert<SOURCE, TARGET>(source, sourceType, target, targetType, sourcePropertyNamez, targetPropertyNamez);

                result.Add(target);
            }
        }
    }
}
