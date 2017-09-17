using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12.Reports
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

        public void AddBatchNumberColumn(DataTable table)
        {
            table.Columns.Add("BatchNumber", typeof(System.Int32));
            foreach (DataRow row in table.Rows)
            {
                row["BatchNumber"] = batchNumber;
            }
        }
    }
}
