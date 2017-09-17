using sample12.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12.Services
{
    public class BatchSummaryService
    {
        public void SaveBatchSummary(List<BatchSummaryModel> summary)
        {
            if (summary != null)
            {
                foreach (var item in summary)
                {
                    using (SalesDBContext context = new SalesDBContext())
                    {
                        Save(item, context);
                    }
                }
            }
        }

        private void Save(BatchSummaryModel model, SalesDBContext context)
        {
            context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[BatchSummary]([BatchNumber],[ProcessedDate],[FileName],[SalesDate]) VALUES(@BatchNumber,@ProcessedDate,@FileName,@SalesDate)",
                
                new SqlParameter("@BatchNumber",model.BatchNumber),
                new SqlParameter("@ProcessedDate", model.ProcessedDate),
                new SqlParameter("@FileName", model.FileName),
                new SqlParameter("@SalesDate", model.SalesDate)
                );
        }
    }
}
