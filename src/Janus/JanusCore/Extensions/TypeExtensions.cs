using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusCore.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetUnderlyingType(this Type t)
        {
            return !t.IsGenericType ? t : t.GetGenericArguments().Single();
        }

        public static bool IsTypeOrNullableType<T>(this Type t)
        {
            return t.IsTypeOrNullableType(typeof(T));
        }

        public static bool IsTypeOrNullableType(this Type t, Type targetType)
        {
            return t == targetType || (t.IsNullable() && Nullable.GetUnderlyingType(t) == targetType);
        }

        public static bool IsEnumOrNullableEnum(this Type t)
        {
            return t.IsEnum || (t.IsNullable() && Nullable.GetUnderlyingType(t).IsEnum);
        }

        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static object GetDefaultValue(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        public static Type[] GetDirectInterfaces(this Type t)
        {
            if (t == null)
                return new Type[] { };

            var interfaces = t.GetInterfaces().ToList();

            if (t.BaseType != null)
                interfaces.RemoveRange(t.BaseType.GetInterfaces());

            interfaces.RemoveRange(interfaces.SelectMany(i => i.GetInterfaces()).ToArray());

            return interfaces.ToArray();
        }

        public static bool Implements<TInterface>(this Type concreteType)
        {
            return Implements(concreteType, typeof(TInterface));
        }

        public static bool Implements(this Type concreteType, Type interfaceType)
        {
            if (concreteType == null || interfaceType == null)
                return false;

            if (!interfaceType.IsGenericTypeDefinition)
                return interfaceType.IsAssignableFrom(concreteType);

            var baseType = concreteType;

            while (baseType != null && baseType != typeof(object))
            {
                if (baseType == interfaceType || (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == interfaceType))
                    return true;

                if (baseType.GetInterfaces().Any(i => (i.IsGenericType ? i.GetGenericTypeDefinition() : i) == interfaceType))
                    return true;

                baseType = baseType.BaseType;
            }

            return false;
        }        
       
    }
}
