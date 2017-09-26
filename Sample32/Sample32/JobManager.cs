using Sample32.Model;
using Sample32.Reports;
using Sample32.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32
{
    public class JobManager
    {
        public void RunJob()
        {
            int batchNumber = new BatchService().GetBatchNumber();
            FilePathResolver filePathResolver = new FilePathResolver(batchNumber);
            var salesFilePath = filePathResolver.GetSalesCsvPath();
            var salesReport = new SalesReport(batchNumber, salesFilePath).GetReport();

            new SalesReport(batchNumber, salesFilePath).ExportToDB();

            new BatchSummaryService().SaveBatchSummary(new List<BatchSummaryModel>
            {
                new BatchSummaryModel {
                            BatchNumber = batchNumber,
                            FileName = filePathResolver.GetSalesFileName(),
                            ProcessedDate = DateTime.Now,
                            SalesDate = DateTime.Now
                        },
            });
        }
    }
}
