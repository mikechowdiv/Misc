using sample12.Models;
using sample12.Reports;
using sample12.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12
{
    public class JobManager
    {
        public void Run()
        {
            int batchNumber = new BatchService().GetBatchNumber();

            try
            {
                FilesPathResolver fpr = new FilesPathResolver(batchNumber);
                var test1FilePath = fpr.GetTest1CSVPath();

                new Test1Report(batchNumber, test1FilePath).ExportToDB();

                new BatchSummaryService().SaveBatchSummary(new List<Models.BatchSummaryModel>{
                    new BatchSummaryModel
                    {
                        BatchNumber = batchNumber,
                        FileName = fpr.GetTest1FileName(),
                        ProcessedDate = DateTime.Now,
                        SalesDate = DateTime.Now
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
