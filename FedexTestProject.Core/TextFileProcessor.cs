using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace FedexTestProject.Core
{
    public class TextFileProcessor
    {
        public IEnumerable<string> GetLines(Stream stream)
        {
            var trackingNumbers = new List<string>();
            using (var file = new StreamReader(stream))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    trackingNumbers.Add(line);
                }
            }

            return trackingNumbers;
        }

        public string BuildTxtFile(DataTable table)
        {
            var result = new StringBuilder();

            var columnNames = table.Columns.Cast<DataColumn>().
                Select(column => column.ColumnName);
            result.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in table.Rows)
            {
                var fields = row.ItemArray.Select(field => field.ToString());
                result.AppendLine(string.Join(",", fields));
            }

            return result.ToString();
        }
    }
}