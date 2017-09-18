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
    public class ItemsReport : ReportBase
    {
        string fileName = "";
        public ItemsReport(int batchNumber, string filePath) : base(batchNumber, filePath)
        {
            fileName = new FilePathResolver(batchNumber).GetItemsFileName();
            tableName = "tblItemTemp";
        }

        public override void ExportToDatabase()
        {
            var dataTable = new CsvParser().ReadCsvFile(filePath);
            AddBatchNumberColumn(dataTable);

            UpdateEmptyCellsToZero(dataTable, "Site");
            UpdateEmptyCellsToZero(dataTable, "Location");
            UpdateEmptyCellsToZero(dataTable, "Department");
            UpdateEmptyCellsToZero(dataTable, "Supplier");
            UpdateEmptyCellsToZero(dataTable, "Brand");

            UpdateEmptyCellsToZero(dataTable, "Sales");
            UpdateEmptyCellsToZero(dataTable, "GST");
            UpdateEmptyCellsToZero(dataTable, "Quantity");
            UpdateEmptyCellsToZero(dataTable, "Cost");
            UpdateEmptyCellsToZero(dataTable, "Item Discount");
            UpdateEmptyCellsToZero(dataTable, "Sales Discount Quantity");

            UpdateDateFormat(dataTable, "Date", "yyyy-MM-dd", ConfigReader.DatabaseDefaultDateFormat);

            List<string> columnsToExcludeFromCsv = new List<string>
            {
            };

            using (var bulkCopy = new SqlBulkCopy(ConfigReader.ConnectionString, SqlBulkCopyOptions.Default))
            {
                foreach (DataColumn item in dataTable.Columns)
                {
                    if (!columnsToExcludeFromCsv.Any(s => s.ToString() == item.ColumnName))
                    {
                        bulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                    }
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
        //        "ItemNoOrLF",
        //        "LFDesc",
        //        "ItemFileName",
        //        "ImportedDate",
        //        "tblId"
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
        //                Details =$"Error in csv file {fileName} at line number:{index}. Error Message : {ex.Message}",
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
