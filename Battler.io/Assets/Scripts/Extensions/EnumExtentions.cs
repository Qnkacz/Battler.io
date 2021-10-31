using System;
using System.Linq;

namespace Extensions
{
    public static class EnumExtentions
    {
        public static Enum GetRandomEnumValue(this Type t)
        {
            return Enum.GetValues(t)
                .OfType<Enum>()
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}