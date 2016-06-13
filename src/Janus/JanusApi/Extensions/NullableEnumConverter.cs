using FluentSql.Support.Extensions;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Janus.Extensions
{
    public class NullableEnumConverter : JavaScriptPrimitiveConverter
    {
        private readonly IEnumerable<Type> types;

        public NullableEnumConverter(IEnumerable<Assembly> assemblies)
        {
            types = assemblies?.SelectMany(a => a.GetLoadableTypes()).Where(t => t.IsEnum).Select(t => typeof(Nullable<>).MakeGenericType(t)).ToArray();
        }

        public override object Deserialize(object value, Type type, JavaScriptSerializer serializer)
        {
            var enumType = Nullable.GetUnderlyingType(type);

            // Non-nullable enums pass through here even though only nullable enums are specified in the supported types.
            // Therefore, test for non-nullable before continuing.
            if (enumType == null)
                return value;

            if (value == null)
                return null;

            return Enum.ToObject(enumType, value);
        }

        public override object Serialize(object value, JavaScriptSerializer serializer)
        {
            return value;
        }

        public override IEnumerable<Type> SupportedTypes => types ?? Enumerable.Empty<Type>();
    }
}