using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace PhoneWebShop.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SqlDataReaderExtensions
    {
        public static object GetColumnValue(this SqlDataReader reader, string colName)
        {
            return reader.GetValue(reader.GetOrdinal(colName));
        }
    }
}
