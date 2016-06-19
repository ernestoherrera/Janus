using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusCore.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue<T>(this Enum enm, int selection)
        {
            return Enum.GetName(typeof(T), selection);
        }
    }
}
