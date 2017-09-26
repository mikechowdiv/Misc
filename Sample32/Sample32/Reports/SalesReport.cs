using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32.Reports
{
    public class SalesReport : ReportBase
    {
        string fileName = "";

        public SalesReport(int batchNumber, string filePath) : base(batchNumber, filePath)
        {
            fileName = new FilePathResolver(batchNumber).GetSalesFileName();
            tableName = "tblSalesTemp";
        }

        public override void ExportToDB()
        {
            var dataTable = new CsvParser().ReadCsvFile(filePath);
            var columnsInTable = GetDatabaseColumns(tableName);
            List<string> columnsToExcludeFromInsert = new List<string> {
                "DataFrom",
                "SalesFileName",
                "ImportedDate",
                "tblId"
            };

            List<string> columnsToExcludeFromCsv = new List<string>
            {
            };

            columnsToExcludeFromInsert.ForEach(s => { s = s.ToLower(); });
            columnsInTable.ForEach(s => { s.ColumnName = s.ColumnName.ToLower(); });
            columnsToExcludeFromCsv.ForEach(s => { s = s.ToLower(); });
            SqlConnection connection = new SqlConnection(ConfigReader.ConnectionString);

            string insertQuery = BuildInsertQuery(GetDatabaseColumns(tableName), columnsToExcludeFromInsert, tableName);
            int index = 1;
            connection.Open();
            foreach (DataRow row in dataTable.Rows)
            {
                index++;
                try
                {
                    SqlCommand cmd = new SqlCommand(insertQuery, connection);
                    foreach (var item in dataTable.Columns)
                    {
                        var columnName = item.ToString().ToLower();
                        var columnNameWithoutSpace = item.ToString().Replace(" ", "").ToLower();
                        if (!columnsToExcludeFromCsv.Contains(columnName))
                        {
                            SqlParameter parameter = new SqlParameter($"@{columnNameWithoutSpace}", row[columnName]);
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    SqlParameter batchNumberParameter = new SqlParameter($"@batchnumber", batchNumber);
                    cmd.Parameters.Add(batchNumberParameter);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            connection.Close();
            
            base.ExportToDB();
        }
    }
}
