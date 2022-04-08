using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace PhoneWebShop.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DataRowExtensions
    {
        public static int GetInt(this DataRow row, string item)
        {
            return Convert.ToInt32(row[item]);
        }

        public static string GetString(this DataRow row, string item)
        {
            return Convert.ToString(row[item]);
        }

        public static double GetDouble(this DataRow row, string item)
        {
            return Convert.ToDouble(row[item]);
        }
    }
}
