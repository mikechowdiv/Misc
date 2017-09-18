using Sales.DataParser.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public abstract class ReportBase
    {
        protected int batchNumber;
        protected string filePath;
        protected string tableName;
        public ReportBase(int batchNumber, string filePath)
        {
            this.batchNumber = batchNumber;
            this.filePath = filePath;
        }
        public virtual void ExportToDatabase()
        {

        }
        public string BuildInsertQuery(List<TableSchemaModel> tableColumns, List<string> columnsToExclude, string tableName)
        {
            var columns = tableColumns.Where(e => !columnsToExclude.Contains(e.ColumnName));

            string query = string.Empty;
            query += "INSERT INTO " + tableName + "(";

            foreach (var item in columns)
            {
                query += $"[{item.ColumnName}],";
            }
            if (query.EndsWith(","))
            {
                query = query.TrimEnd(',');
            }
            query += ")Values(";

            foreach (var item in columns)
            {
                query += $"@{item.ColumnNameWithoutSpace},";
            }
            if (query.EndsWith(","))
            {
                query = query.TrimEnd(',');
            }

            query += ")";
            return query;
        }
        public ReportModel GetReport()
        {
            ReportModel report = new ReportModel();
            var dataTable = new CsvParser().ReadCsvFile(filePath);
            CheckIfCsvIsEmpty(dataTable);

            report.TotalSales = GetTotal(dataTable, "Sales");
            report.TotalGst = GetTotal(dataTable, "GST");
            report.TotalQuantity = GetTotal(dataTable, "Quantity");

            return report;
        }
        private void CheckIfCsvIsEmpty(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                ValidationErrorService.AddError(new ExceptionModel
                {
                    BatchNumber = batchNumber,
                    ErrorCode = ((int)ErrorCodes.EmptyFiles).ToString(),
                    ProcessedDate = DateTime.Now,
                    ErrorDescription = ErrorCodes.EmptyFiles.GetEnumDescription(),
                    Details = filePath
                });
            }
        }
        public void Delete()
        {
            string deleteQuery = $"DELETE FROM {tableName} WHERE BatchNumber={batchNumber}";
            SqlConnection connection = new SqlConnection(ConfigReader.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(deleteQuery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        internal double GetTotal(DataTable datatable, string columnName)
        {
            var total = datatable.AsEnumerable()
            .Sum(r =>
            {
                double val = 0;
                double.TryParse(r.Field<string>(columnName), out val);
                return val;
            });
            return total;
        }
        internal void UpdateEmptyCellsToZero(DataTable datatable, string columnName)
        {
            var index = 1;
            datatable.AsEnumerable().ToList().ForEach
           (r =>
           {
               index++;
               var value = r.Field<string>(columnName);
               if (string.IsNullOrWhiteSpace(value))
               {
                   r[columnName] = 0;
               }

               double val = 0;
               var result = double.TryParse(r.Field<string>(columnName).Trim(), out val);
               if (!result)
               {
                   throw new Exception($"Invalid decimal value in csv file :{filePath} in column: {columnName} at row number {index}");
               }
           });
        }

        ///// <summary>
        ///// Converts the data string with the passed in format to system date format
        ///// </summary>
        ///// <param name="datatable"></param>
        ///// <param name="columnName"></param>
        ///// <param name="inputFormat">eg: dd-MM-yyyy</param>
        //internal void UpdateDateFormat(DataTable datatable, string columnName, string inputFormat)
        //{
        //    var index = 1;
        //    datatable.AsEnumerable().ToList().ForEach
        //   (r =>
        //   {
        //       index++;
        //       var value = r.Field<string>(columnName);
        //       if (!string.IsNullOrWhiteSpace(value))
        //       {
        //           r[columnName] = value.Trim();
        //       }

        //       if (!string.IsNullOrWhiteSpace(value))
        //       {
        //           DateTime val;
        //           //var result = DateTime.TryParse(r.Field<string>(columnName), CultureInfo.InvariantCulture, DateTimeStyles.None, out val);
        //           var result = DateTime.TryParseExact(r.Field<string>(columnName), inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out val);
        //           if (!result)
        //           {
        //               throw new InvalidDateException($"Invalid date in csv file :{filePath} in column: {columnName} at row number {index}", index.ToString());
        //           }
        //           else
        //           {
        //               r[columnName] = val.ToString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
        //           }
        //       }

        //   });
        //}
        /// <summary>
        /// Converts the data string with the passed in format to system date format
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="columnName"></param>
        /// <param name="inputFormat">eg: dd-MM-yyyy</param>
        /// <param name="outFormat">eg: dd-MM-yyyy</param>
        /// 

        internal void UpdateDateFormat(DataTable datatable, string columnName, string inputFormat, string outFormat)
        {
            var index = 1;
            datatable.AsEnumerable().ToList().ForEach
           (r =>
           {
               index++;
               var value = r.Field<string>(columnName);
               if (!string.IsNullOrWhiteSpace(value))
               {
                   r[columnName] = value.Trim();
               }

               if (!string.IsNullOrWhiteSpace(value))
               {
                   DateTime val;
                   //var result = DateTime.TryParse(r.Field<string>(columnName), CultureInfo.InvariantCulture, DateTimeStyles.None, out val);
                   var result = DateTime.TryParseExact(r.Field<string>(columnName), inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out val);
                   if (!result)
                   {
                       ///throw new InvalidDateException($"Invalid date in csv file :{filePath} in column: {columnName} at row number {index}", index.ToString());
                   }
                   else
                   {
                       r[columnName] = val.ToString(outFormat);
                   }
               }

           });
        }
        internal void AddBatchNumberColumn(DataTable table)
        {
            table.Columns.Add("BatchNumber", typeof(System.Int32));
            foreach (DataRow row in table.Rows)
            {
                row["BatchNumber"] = batchNumber;
            }
        }

        public List<TableSchemaModel> GetDatabaseColumns(string tableName)
        {
            string query = $"SELECT TABLE_NAME AS TableName,COLUMN_NAME AS ColumnName,IS_NULLABLE AS IsNullable,DATA_TYPE AS DataType FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{tableName}'";

            using (SalesDBContext context = new SalesDBContext())
            {
                var columns = context.Database.SqlQuery<TableSchemaModel>(query);
                return columns.ToList();
            }
        }
    }
}
