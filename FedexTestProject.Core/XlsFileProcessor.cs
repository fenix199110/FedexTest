using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace FedexTestProject.Core
{
    public class XlsFileProcessor
    {
        public IEnumerable<string> GetLines(Stream stream)
        {
            var trackingNumbers = new List<string>();
            using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                do
                {
                    while (reader.Read())
                    {
                        trackingNumbers.Add(reader.GetString(0));
                    }
                } while (reader.NextResult());
            }
            return trackingNumbers;
        }

        public IWorkbook BuildExcelFile(DataTable table)
        {
            var workbook = new XSSFWorkbook();
            var sheet1 = workbook.CreateSheet("Sheet 1");

            //make a header row
            var row1 = sheet1.CreateRow(0);

            for (var j = 0; j < table.Columns.Count; j++)
            {
                var cell = row1.CreateCell(j);
                var columnName = table.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            //loops through data
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var row = sheet1.CreateRow(i + 1);
                for (var j = 0; j < table.Columns.Count; j++)
                {

                    var cell = row.CreateCell(j);
                    var columnName = table.Columns[j].ToString();
                    cell.SetCellValue(table.Rows[i][columnName].ToString());
                    sheet1.AutoSizeColumn(j);
                }
            }

            return workbook;
        }
    }
}