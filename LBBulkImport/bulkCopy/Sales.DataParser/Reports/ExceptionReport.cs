using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class ExceptionReport
    {
        private int batchNumber;
        public ExceptionReport(int batchNumber)
        {
            this.batchNumber = batchNumber;
        }
        public void GenerateExceptionReport(string filePath, bool isAppendMode)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filePath, isAppendMode))
            {
                FilePathResolver pathResolver = new FilePathResolver(batchNumber);
                if (!isAppendMode)
                {
                    file.WriteLine("\"Batch#\",\"ProcessedDate\",\"ErrorCode\",\"ErrorDesc\",\"Details\",\"Diff\",\"Line Number\"");
                }

                if (ValidationErrorService.GetErrors().Count > 0)
                {
                    foreach (var item in ValidationErrorService.GetErrors())
                    {
                        file.WriteLine($"\"{item.BatchNumber}\",\"{item.ProcessedDate.ToString("MM/dd/yyyy")}\",\"{item.ErrorCode}\",\"{item.ErrorDescription}\",\"{item.Details}\",\"{string.Empty}\",\"{item.LineNumber}\"");
                    }
                }
            }

        }
        public void GenerateExceptionReport(ReportModel salesReport, ReportModel itemsReport, ReportModel journelsReport, string filePath)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filePath, false))
            {
                FilePathResolver pathResolver = new FilePathResolver(batchNumber);

                file.WriteLine("\"Batch#\",\"ProcessedDate\",\"ErrorCode\",\"ErrorDesc\",\"Details\",\"Diff\",\"Line Number\"");

                //double totalItemVsSales = salesReport.TotalSales - itemsReport.TotalSales;
                double totalItemsVsJournals = journelsReport.TotalSales - itemsReport.TotalSales;
                double totalISalesVsJournals = salesReport.TotalSales - journelsReport.TotalSales;


                //double gstItemsVsSales = salesReport.TotalGst - itemsReport.TotalGst;
                double gstItemsVsJournals = journelsReport.TotalGst - itemsReport.TotalGst;
                double gstSalesVsJournals = salesReport.TotalGst - journelsReport.TotalGst;

                //double qtyItemsVsSales = salesReport.TotalQuantity - itemsReport.TotalQuantity;
                double qtyItemsvsJournals = journelsReport.TotalQuantity - itemsReport.TotalQuantity;
                double qtySalesvsJournals = salesReport.TotalQuantity - journelsReport.TotalQuantity;

                if (ValidationErrorService.GetErrors().Count > 0)
                {
                    foreach (var item in ValidationErrorService.GetErrors())
                    {
                        file.WriteLine($"\"{item.BatchNumber}\",\"{item.ProcessedDate.ToString("MM/dd/yyyy")}\",\"{item.ErrorCode}\",\"{item.ErrorDescription}\",\"{item.Details}\",\"{string.Empty}\",\"{item.LineNumber}\"");
                    }
                }

                if (totalItemsVsJournals != 0)
                {
                    file.WriteLine($"\"{batchNumber}\",\"{DateTime.Now.ToString("MM/dd/yyyy")}\",\"{((int)ErrorCodes.SalesAmountNotMatchingItemsVsJournals).ToString()}\",\"{ErrorCodes.SalesAmountNotMatchingItemsVsJournals.GetEnumDescription()}\",\"{pathResolver.GetItemsFileName()}\",\"{totalItemsVsJournals.ToString("N3")}\",\"\"");
                }
                if (totalISalesVsJournals != 0)
                {
                    file.WriteLine($"\"{batchNumber}\",\"{DateTime.Now.ToString("MM/dd/yyyy")}\",\"{((int)ErrorCodes.SalesAmountNotMatchingSalesVsJournals).ToString()}\",\"{ErrorCodes.SalesAmountNotMatchingSalesVsJournals.GetEnumDescription()}\",\"{pathResolver.GetSalesFileName()}\",\"{totalISalesVsJournals.ToString("N3")}\",\"\"");
                }
                if (gstItemsVsJournals != 0)
                {
                    file.WriteLine($"\"{batchNumber}\",\"{DateTime.Now.ToString("MM/dd/yyyy")}\",\"{((int)ErrorCodes.GSTAmountNotMatchingItemsVsJournals).ToString()}\",\"{ErrorCodes.GSTAmountNotMatchingItemsVsJournals.GetEnumDescription()}\",\"{pathResolver.GetItemsFileName()}\",\"{gstItemsVsJournals.ToString("N3")}\",\"\"");
                }
                if (gstSalesVsJournals != 0)
                {
                    file.WriteLine($"\"{batchNumber}\",\"{DateTime.Now.ToString("MM/dd/yyyy")}\",\"{((int)ErrorCodes.GSTAmountNotMatchingSalesVsJournals).ToString()}\",\"{ErrorCodes.GSTAmountNotMatchingSalesVsJournals.GetEnumDescription()}\",\"{pathResolver.GetSalesFileName()}\",\"{gstSalesVsJournals.ToString("N3")}\",\"\"");
                }

                if (qtyItemsvsJournals != 0)
                {
                    file.WriteLine($"\"{batchNumber}\",\"{DateTime.Now.ToString("MM/dd/yyyy")}\",\"{((int)ErrorCodes.QuantityAmountNotMatchingItemsVsJournals).ToString()}\",\"{ErrorCodes.QuantityAmountNotMatchingItemsVsJournals.GetEnumDescription()}\",\"{pathResolver.GetItemsFileName()}\",\"{qtyItemsvsJournals.ToString("N3")}\",\"\"");
                }
                if (qtySalesvsJournals != 0)
                {
                    file.WriteLine($"\"{batchNumber}\",\"{DateTime.Now.ToString("MM/dd/yyyy")}\",\"{((int)ErrorCodes.QuantityAmountNotMatchingSalesVsJournals).ToString()}\",\"{ErrorCodes.QuantityAmountNotMatchingSalesVsJournals.GetEnumDescription()}\",\"{pathResolver.GetSalesFileName()}\",\"{qtySalesvsJournals.ToString("N3")}\",\"\"");
                }

                //file.WriteLine("Diff in total");
                //file.WriteLine("----------------------------------------------");
                //file.WriteLine($"Items vs Sales : {(salesReport.TotalSales - itemsReport.TotalSales).ToString("N3")}");
                //file.WriteLine($"Items vs Journals: {(journelsReport.TotalSales - itemsReport.TotalSales).ToString("N3")}");
                //file.WriteLine();
                //file.WriteLine();

                //file.WriteLine("Diff in GST");
                //file.WriteLine("----------------------------------------------");
                //file.WriteLine($"Items vs Sales: {(salesReport.TotalGst - itemsReport.TotalGst).ToString("N3")}");
                //file.WriteLine($"Items vs Journals: {(journelsReport.TotalGst - itemsReport.TotalGst).ToString("N3")}");
                //file.WriteLine();
                //file.WriteLine();

                //file.WriteLine("Diff in qty");
                //file.WriteLine("----------------------------------------------");
                //file.WriteLine($"Items vs Sales: {(salesReport.TotalQuantity - itemsReport.TotalQuantity).ToString("N3")}");
                //file.WriteLine($"Items vs Journals: {(journelsReport.TotalQuantity - itemsReport.TotalQuantity).ToString("N3")}");
                //file.WriteLine();
                //file.WriteLine();
            }

        }
    }
}
