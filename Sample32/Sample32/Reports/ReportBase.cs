using Sample32.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32.Reports
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

        public virtual void ExportToDB()
        {

        }

        public ReportModel GetReport()
        {
            ReportModel report = new ReportModel();
            var dataTable = new CsvParser().ReadCsvFile(filePath);

            report.TotalSales = GetTotal(dataTable, "Sales");
            report.TotalGst = GetTotal(dataTable, "GST");
            report.TotalQuantity = GetTotal(dataTable, "Quantity");

            return report;
        }

        internal double GetTotal(DataTable datatable, string columnName)
        {
            var total = datatable.AsEnumerable().Sum(r =>
            {
                double val = 0;
                double.TryParse(r.Field<string>(columnName), out val);
                return val;
            });
            return total;
        }

        internal void AddBatchNumerColumn(DataTable table)
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



    }
}
