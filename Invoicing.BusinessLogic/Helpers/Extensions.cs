using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Invoicing.BusinessLogic.Helpers
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            return value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description
                ?? value.ToString();
        }

        public static string DisplayNipFormat(this string value)
        {
            var sb = new StringBuilder();
            if(value.Length == 10)
            {
                sb.Append(value.Substring(0,3));
                sb.Append("-");
                sb.Append(value.Substring(3,3));
                sb.Append("-");
                sb.Append(value.Substring(6,2));
                sb.Append("-");
                sb.Append(value.Substring(8,2));

                return sb.ToString();
            }

            return value;
        }
    }
}
