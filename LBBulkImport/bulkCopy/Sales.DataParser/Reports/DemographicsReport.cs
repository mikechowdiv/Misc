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
    public class DemographicsReport : ReportBase
    {
        string fileName = "";
        public DemographicsReport(int batchNumber, string filePath) : base(batchNumber, filePath)
        {
            fileName = new FilePathResolver(batchNumber).GetDemographicsFileName();
            tableName = "tblDemographicsTemp";
        }
        public override void ExportToDatabase()
        {

            var dataTable = new CsvParser().ReadCsvFile(filePath);
            try
            {
                UpdateDateFormat(dataTable, "DateOfBirth", "dd/MM/yyyy", ConfigReader.DatabaseDefaultDateFormat);
                UpdateDateFormat(dataTable, "RegistrationDate", "dd/MM/yyyy", ConfigReader.DatabaseDefaultDateFormat);
                UpdateDateFormat(dataTable, "LastUpdatedDate", "dd/MM/yyyy", ConfigReader.DatabaseDefaultDateFormat);
            }
            catch (InvalidDateException ex)
            {
                ValidationErrorService.AddError(new DataParser.ExceptionModel
                {
                    BatchNumber = batchNumber,
                    Details = ex.Message,
                    ProcessedDate = DateTime.Now,
                    LineNumber = ex.LineNumber,
                    ErrorCode = ((int)ErrorCodes.GeneralException).ToString(),
                    ErrorDescription = ErrorCodes.GeneralException.GetEnumDescription()
                });
            }
            catch (Exception ex)
            {
                ValidationErrorService.AddError(new DataParser.ExceptionModel
                {
                    BatchNumber = batchNumber,
                    Details = ex.Message,
                    ProcessedDate = DateTime.Now,
                    ErrorCode = ((int)ErrorCodes.GeneralException).ToString(),
                    ErrorDescription = ErrorCodes.GeneralException.GetEnumDescription()
                });
            }
            var columnsInTable = GetDatabaseColumns(tableName);
            List<string> columnsToExcludeFromInsert = new List<string>
            {
            };
            List<string> columnsToExcludeFromCsv = new List<string>
            {
            };

            columnsToExcludeFromInsert.ForEach(s => { s = s.ToLower(); });
            columnsInTable.ForEach(s => { s.ColumnName = s.ColumnName.ToLower(); });
            columnsToExcludeFromCsv.ForEach(s => { s = s.ToLower(); });
            SqlConnection connection = new SqlConnection(ConfigReader.ConnectionString);

            var databaseColumns = GetDatabaseColumns(tableName);

            string insertQuery = BuildInsertQuery(databaseColumns, columnsToExcludeFromInsert, tableName);
            string updateQuery = BuildDemographicUpdateQuery(databaseColumns, new List<string> { "MemberID" }, tableName);
            int index = 1;
            connection.Open();
            foreach (DataRow row in dataTable.Rows)
            {
                index++;
                try
                {
                    SqlCommand cmd;
                    if (!CheckIfMemberAlreadyExists(row))
                    {
                        cmd = new SqlCommand(insertQuery, connection);
                    }
                    else
                    {
                        //do update
                        cmd = new SqlCommand(updateQuery, connection);
                    }
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
                    ValidationErrorService.AddError(new DataParser.ExceptionModel
                    {
                        BatchNumber = batchNumber,
                        Details = $"Error in csv file {fileName} at line number:{index}. Error Message : {ex.Message}",
                        ProcessedDate = DateTime.Now,
                        LineNumber = index.ToString(),
                        ErrorCode = ((int)ErrorCodes.GeneralException).ToString(),
                        ErrorDescription = ErrorCodes.GeneralException.GetEnumDescription()
                    });
                }
            }

            connection.Close();

            base.ExportToDatabase();

        }

        public bool CheckIfMemberAlreadyExists(DataRow row)
        {
            var result = false;
            SqlConnection connection = new SqlConnection(ConfigReader.ConnectionString);

            string query = $"Select 1 from {tableName} where MemberID ={row["MemberID"]}";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            result = cmd.ExecuteReader().HasRows;
            connection.Close();

            return result;
        }
        public string BuildDemographicUpdateQuery(List<TableSchemaModel> tableColumns, List<string> columnsToExclude, string tableName)
        {
            var columns = tableColumns.Where(e => !columnsToExclude.Contains(e.ColumnName)).ToList();

            string query = string.Empty;
            query += "UPDATE " + tableName;          

            for (int i = 0; i < columns.Count; i++)
            {
                var item = columns[i];
                if (i == 0)
                {
                    query += $" SET [{item.ColumnName}]=@{item.ColumnNameWithoutSpace},";
                }
                else if (i == columns.Count - 1)
                {
                    query += $" [{item.ColumnName}]=@{item.ColumnNameWithoutSpace}";
                }
                else
                {
                    query += $" [{item.ColumnName}]=@{item.ColumnNameWithoutSpace},";
                }

            }
            query += $" where MemberID=@MemberID";
            return query;
        }
    }
}
