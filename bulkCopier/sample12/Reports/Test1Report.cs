using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12.Reports
{
    public class Test1Report : ReportBase
    {
        string fileName = "";

        public Test1Report(int batchNumber, string filePath) : base(batchNumber, filePath)
        {
            fileName = new FilesPathResolver(batchNumber).GetTest1FileName();
            tableName = "tblTest1Temp";
        }

        public override void ExportToDB()
        {
            var dataTable = new CSVParser().ReadCSVFile(filePath);
            AddBatchNumberColumn(dataTable);

            using (var bulkcopy = new SqlBulkCopy(ConfigReader.ConnectionString, SqlBulkCopyOptions.Default))
            {
                foreach (DataColumn item in dataTable.Columns)
                {
                    bulkcopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                }
                bulkcopy.BulkCopyTimeout = 600;
                bulkcopy.DestinationTableName = tableName;
                bulkcopy.WriteToServer(dataTable);
            }
        }
    }
}
