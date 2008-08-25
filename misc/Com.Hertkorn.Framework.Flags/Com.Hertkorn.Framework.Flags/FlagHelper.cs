using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.Flags
{
    public static class FlagHelper
    {
        public static bool IsSet(this Enum toBeTested, Enum flag)
        {
            return ((Convert.ToUInt32(toBeTested) & Convert.ToUInt32(flag)) == Convert.ToUInt32(flag));
        }

        public static bool IsNoneSet(this Enum toBeTested)
        {
            return Convert.ToUInt32(toBeTested) == Convert.ToUInt32(0);
        }

        public static bool IsAnySet(this Enum toBeTested)
        {
            return Convert.ToUInt32(toBeTested) != Convert.ToUInt32(0);
        }

        public static T Set<T>(this Enum flags, T flagToSet)
        {
            return (T)(object)(Convert.ToUInt32(flags) | Convert.ToUInt32(flagToSet));
        }

        public static T Unset<T>(this Enum flags, T flagToUnset)
        {
            return (T)(object)(Convert.ToUInt32(flags) & (~Convert.ToUInt32(flagToUnset)));
        }

        public static T Toggle<T>(this Enum flags, T flagToToggle)
        {
            return (T)(object)(Convert.ToUInt32(flags) ^ Convert.ToUInt32(flagToToggle));
        }

        public static bool IsComposable<T>(object value)
        {
            if (value == null) { throw new ArgumentNullException("value"); }
            if (!typeof(T).IsEnum) { throw new ArgumentException("The generic type parameter is not an enum", "T"); }
            if (value.GetType().IsEnum)
            {
                
                if (!Enum.GetUnderlyingType(value.GetType()).Equals(Enum.GetUnderlyingType(typeof(T)))) { throw new ArgumentException(string.Format("{0} and {1} don't have matching underlying system types ({0}'s is {2})", typeof(T).FullName, value.GetType().FullName, typeof(T).UnderlyingSystemType.FullName), "value"); }
            }
            else
            {
                if (!value.GetType().Equals(Enum.GetUnderlyingType(typeof(T)))) { throw new ArgumentException(string.Format("Can't match {0} to {1}'s underlying system types {2}", value.GetType().FullName, typeof(T).FullName, typeof(T).UnderlyingSystemType.FullName), "value"); }
            }

            if (Enum.IsDefined(typeof(T), value))
            {
                return true;
            }

            if (Convert.ChangeType(0, Enum.GetUnderlyingType(typeof(T))).Equals(value))
            {
                // is "None" and not directly defined -> false
                return false;
            }

            var t = Enum.GetValues(typeof(T));

            uint array = 0;

            foreach (T item in t)
            {
                array |= Convert.ToUInt32(item);
            }
            return (Convert.ToUInt32(value) | array) == array;
        }


        private static Type GetUnderlyingSystemType(object value)
        {
            throw new NotImplementedException();
        }
        static bool IsBitSet(Enum testEnum, int position)
        {
            throw new NotImplementedException();
        }
    }
}
