using System;
using System.Data;
using System.Linq;

namespace Antech.Atlas
{
    /// <summary>
    /// Extending DataTable such as formats
    /// </summary>
    public static class DataTableExtension
    {
        public static DataTable ToDataTableFilterRowFormatted (this DataTable table, int rowNumber, string columnName, string newColumnValue, params string[] filters)
        {
            if (filters.Length != 3)
                throw new ArgumentOutOfRangeException();

            var query = table.AsEnumerable()
                            .Where(ss => ss.Field<string>("ID") == filters[0]
                                    || ss.Field<string>("ID") == filters[1]
                                    || ss.Field<string>("ID") == filters[2]);
            if(query.Count() > 0)
            {
                DataTable dt = query.CopyToDataTable<DataRow>();
                //if (dt.Rows.Count >= rowNumber && dt.Columns.Contains(columnName))
                //{
                //    dt.Rows[rowNumber][columnName] = "Other";
                //}

                return dt;
            }
            return table;
        }
    }
}