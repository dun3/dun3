using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.Flags
{
    public static class WeaklyTypedOps
    {
        public unsafe static object Or<T>(T flags, object flagToSet)
        {


            Type flagsType = null;
            Type flagToSetType = null;
            if (flagToSet.GetType().IsEnum)
            {
                flagToSet = flagToSet.GetType().UnderlyingSystemType;
            }
            if (!flags.GetType().UnderlyingSystemType.Equals(flagToSetType))
            {
                throw new ArgumentException(string.Format("Underlying system type is not matching ({0} vs {1})", flags.GetType().UnderlyingSystemType.Name, flagToSet.GetType().UnderlyingSystemType.Name));
            }

            switch (Type.GetTypeCode(typeof(T).UnderlyingSystemType))
            {
                // integral types except char
                case TypeCode.Byte:
                    break;
                case TypeCode.Char:
                    break;
                case TypeCode.Int16:
                    break;
                case TypeCode.Int32:
                    break;
                case TypeCode.Int64:
                    break;
                case TypeCode.SByte:
                    break;
                case TypeCode.Single:
                    break;
                case TypeCode.UInt16:
                    break;
                case TypeCode.UInt32:
                    break;
                case TypeCode.UInt64:
                    break;
                default:
                    throw new InvalidOperationException(string.Format("not an integral type: {0}", typeof(T).FullName));
            }



            //return (T)Convert.ChangeType(flags, typeof(T)) | (T)Convert.ChangeType(flagToSet, typeof(T));
            return (object)(Convert.ToUInt32(flags) | Convert.ToUInt32(flagToSet));

        }



    }
}
