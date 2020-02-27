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

        public static string DisplayDecimalValues(this string value, decimal amount)
        {
            const string PolishCurrency = "złote";
            var index = value?.IndexOf(PolishCurrency) ?? 0;

            var sb = new StringBuilder();

            sb.Append(value.Remove(index));
            
            var decimalValue = amount - Math.Truncate(amount);

            if(decimalValue != 0)
            {
                var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(decimalValue)[3])[2];
                sb.Append(" ");
                sb.Append((double)decimalValue * Math.Pow(10, (double)decimalPlaces == 1 ? 2 : 1));
                sb.Append("/");
                sb.Append("100");
            }

            sb.Append(" PLN");
            var s = sb.ToString();
            return s;
        }

        public static string DisplayBankAccountFormat(this string value)
        {
            if(value.Length == 26)
            {
                var sb = new StringBuilder();

                sb.Append(value.Substring(0, 2));
                sb.Append(" ");
                sb.Append(value.Substring(2, 4));
                sb.Append(" ");
                sb.Append(value.Substring(6, 4));
                sb.Append(" ");
                sb.Append(value.Substring(10, 4));
                sb.Append(" ");
                sb.Append(value.Substring(14, 4));
                sb.Append(" ");
                sb.Append(value.Substring(18, 4));
                sb.Append(" ");
                sb.Append(value.Substring(22, 4));

                return sb.ToString();
            }

            return value;
        }
    }
}
