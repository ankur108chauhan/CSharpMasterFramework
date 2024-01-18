using System.Data;

namespace Mobile.Utils
{
    internal class TableExtensions
    {
        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }

        public static DataTable ToDataTable(Table table)
        {
            var dataTable = new DataTable();
            foreach (var header in table.Header)
            {
                dataTable.Columns.Add(header, typeof(string));
            }

            foreach (var row in table.Rows)
            {
                var newRow = dataTable.NewRow();
                foreach (var header in table.Header)
                {
                    newRow.SetField(header, row[header]);
                }
                dataTable.Rows.Add(newRow);
            }
            return dataTable;
        }

        public static List<string> ToStringList(Table table, int columnIndex)
        {
            var list = new List<string>();
            DataTable dataTable = ToDataTable(table);
            foreach (DataRow row in dataTable.Rows)
                list.Add(row.ItemArray[--columnIndex]!.ToString()!);
            return list;
        }

        public static List<string> ToStringList(Table table, string column)
        {
            return table.Rows.Select(row => row[column]).ToList();
        }

        public static string[,] ToStringArray(Table table)
        {
            int rowCount = table.RowCount + 1;
            string[,] arr = new string[rowCount, table.Header.Count];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < table.Header.Count; j++)
                {
                    if (i == 0)
                        arr[i, j] = table.Header.ElementAt(j).ToString().Trim();
                    else
                    {
                        int rowindex = i - 1;
                        arr[i, j] = table.Rows[rowindex].Values.ElementAt(j).ToString().Trim();
                    }
                }
            }
            return arr;
        }

        public static string[,] ToStringArray(List<string> list)
        {
            string[,] arr = new string[list.Count, 1];
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    arr[i, j] = list.ElementAt(i);
                }
            }
            return arr;
        }
    }
}
