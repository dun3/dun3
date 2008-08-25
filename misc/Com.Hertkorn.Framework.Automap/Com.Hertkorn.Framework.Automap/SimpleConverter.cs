using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Com.Hertkorn.Framework.Automap
{
    public class SimpleConverter
    {
        public void Map<SOURCE, TARGET>(SOURCE source, TARGET target)
        {
            foreach (var targetProperty in typeof(TARGET).GetProperties())
            {
                var sourceProperty = typeof(SOURCE).GetProperty(targetProperty.Name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

                if (sourceProperty == null) { throw new TargetException(string.Format("Could not find property {0} on {1} to convert {2}", targetProperty.Name, typeof(TARGET).Name, typeof(SOURCE).Name)); }

                targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
            }
        }
    }
}
