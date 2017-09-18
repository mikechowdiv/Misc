using Sales.DataParser.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class JournalsReport : ReportBase
    {
        string fileName = "";

        public JournalsReport(int batchNumber, string filePath) : base(batchNumber, filePath)
        {
            fileName = new FilePathResolver(batchNumber).GetJournalsFileName();
            tableName = "tblJournalTemp";
        }

        public override void ExportToDatabase()
        {
            var dataTable = new CsvParser().ReadCsvFile(filePath);
            AddBatchNumberColumn(dataTable);

            UpdateEmptyCellsToZero(dataTable, "Site");
            UpdateEmptyCellsToZero(dataTable, "Location");
            UpdateEmptyCellsToZero(dataTable, "Terminal");
            UpdateEmptyCellsToZero(dataTable, "Journal");
            UpdateEmptyCellsToZero(dataTable, "Sequence");
            UpdateEmptyCellsToZero(dataTable, "Department");

            UpdateEmptyCellsToZero(dataTable, "Sales");
            UpdateEmptyCellsToZero(dataTable, "GST");
            UpdateEmptyCellsToZero(dataTable, "Quantity");
            UpdateEmptyCellsToZero(dataTable, "Cost");
            UpdateEmptyCellsToZero(dataTable, "Item Discount");
            UpdateEmptyCellsToZero(dataTable, "Loyalty Discount");
            UpdateEmptyCellsToZero(dataTable, "Offer Discount");
            UpdateEmptyCellsToZero(dataTable, "Price");

            UpdateEmptyCellsToZero(dataTable, "Original Price");
            UpdateEmptyCellsToZero(dataTable, "Sales Discount Quantity");
            UpdateEmptyCellsToZero(dataTable, "Covers");
            UpdateEmptyCellsToZero(dataTable, "Card Type");

            UpdateEmptyCellsToZero(dataTable, "Total Points");
            UpdateEmptyCellsToZero(dataTable, "Bonus Points");
            UpdateEmptyCellsToZero(dataTable, "Redeemed Points");
            UpdateEmptyCellsToZero(dataTable, "Redeemed Amount");
            UpdateEmptyCellsToZero(dataTable, "Expired Points");
            UpdateEmptyCellsToZero(dataTable, "Home Site");


            UpdateDateFormat(dataTable, "Date", "yyyy-MM-dd", ConfigReader.DatabaseDefaultDateFormat);
            UpdateDateFormat(dataTable, "Time", "yyyy-MM-dd HH:mm:ss", ConfigReader.DatabaseDefaultDateFormat);
            //2017-08-08 09:04:56

            using (var bulkCopy = new SqlBulkCopy(ConfigReader.ConnectionString, SqlBulkCopyOptions.Default))
            {
                foreach (DataColumn item in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                }
                bulkCopy.BulkCopyTimeout = 600;
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(dataTable);
            }

        }

        //public override void ExportToDatabase()
        //{
        //    var dataTable = new CsvParser().ReadCsvFile(filePath);
        //    try
        //    {
        //        UpdateDateFormat(dataTable, "Date", "yyyy-MM-dd", ConfigReader.DatabaseDefaultDateFormat);
        //        UpdateDateFormat(dataTable, "Time", "yyyy-MM-dd HH:mm:ss", ConfigReader.DatabaseDefaultDateFormat);
        //        //2017-08-08 09:04:56
        //    }
        //    catch (InvalidDateException ex)
        //    {
        //        ValidationErrorService.AddError(new DataParser.ExceptionModel
        //        {
        //            BatchNumber = batchNumber,
        //            Details = ex.Message,
        //            ProcessedDate = DateTime.Now,
        //            LineNumber = ex.LineNumber,
        //            ErrorCode = ((int)ErrorCodes.GeneralException).ToString(),
        //            ErrorDescription = ErrorCodes.GeneralException.GetEnumDescription()
        //        });
        //    }
        //    var columnsInTable = GetDatabaseColumns(tableName);
        //    List<string> columnsToExcludeFromInsert = new List<string> {
        //        "JournalFileName",
        //        "ImportedDate",
        //        "tblId",
        //        "ItemNoOrLF",
        //        "LFDesc"
        //    };
        //    List<string> columnsToExcludeFromCsv = new List<string>
        //    {
        //    };

        //    columnsToExcludeFromInsert.ForEach(s => { s = s.ToLower(); });
        //    columnsInTable.ForEach(s => { s.ColumnName = s.ColumnName.ToLower(); });
        //    columnsToExcludeFromCsv.ForEach(s => { s = s.ToLower(); });
        //    SqlConnection connection = new SqlConnection(ConfigReader.ConnectionString);

        //    string insertQuery = BuildInsertQuery(GetDatabaseColumns(tableName), columnsToExcludeFromInsert, tableName);
        //    int index = 1;
        //    connection.Open();
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        index++;
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand(insertQuery, connection);
        //            foreach (var item in dataTable.Columns)
        //            {
        //                var columnName = item.ToString().ToLower();
        //                var columnNameWithoutSpace = item.ToString().Replace(" ", "").ToLower();
        //                if (!columnsToExcludeFromCsv.Contains(columnName))
        //                {
        //                    SqlParameter parameter = new SqlParameter($"@{columnNameWithoutSpace}", row[columnName]);
        //                    cmd.Parameters.Add(parameter);
        //                }
        //            }
        //            SqlParameter batchNumberParameter = new SqlParameter($"@batchnumber", batchNumber);
        //            cmd.Parameters.Add(batchNumberParameter);

        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            ValidationErrorService.AddError(new DataParser.ExceptionModel
        //            {
        //                BatchNumber = batchNumber,
        //                Details = $"Error in csv file {fileName} at line number:{index}. Error Message : {ex.Message}",
        //                ProcessedDate = DateTime.Now,
        //                LineNumber = index.ToString(),
        //                ErrorCode = ((int)ErrorCodes.GeneralException).ToString(),
        //                ErrorDescription = ErrorCodes.GeneralException.GetEnumDescription()
        //            });
        //        }
        //    }

        //    connection.Close();

        //    base.ExportToDatabase();
        //}


    }
}
