using Sales.DataParser.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class JobManager
    {
        public void RunJob()
        {
            ValidationErrorService.ClearErrors();
            int batchNumber = new BatchService().GetBatchNumber();
            try
            {
                FilePathResolver filePathResolver = new FilePathResolver(batchNumber);
                bool validationResult = filePathResolver.ValidateExistanceOfCsvFiles();
                if (!validationResult)
                {
                    //var salesFilePath = filePathResolver.GetSalesCsvPath();
                    var itemsFilePath = filePathResolver.GetItemsCsvPath();
                    var journalsFilePath = filePathResolver.GetJournalsCsvPath();
                    //var demographicsFilePath = filePathResolver.GetDemographicsCsvPath();
                    //var exceptionReportPath = filePathResolver.GetExceptionReportPath();

                    //var salesReport = new SalesReport(batchNumber, salesFilePath).GetReport();
                    //var itemsReport = new ItemsReport(batchNumber, itemsFilePath).GetReport();
                    //var journalsReport = new JournalsReport(batchNumber, journalsFilePath).GetReport();

                    //new ExceptionReport(batchNumber).GenerateExceptionReport(salesReport, itemsReport, journalsReport, exceptionReportPath);

                    if (ValidationErrorService.GetErrors().Count == 0)
                    {
                        //new SalesReport(batchNumber, salesFilePath).ExportToDatabase();
                        new ItemsReport(batchNumber, itemsFilePath).ExportToDatabase();
                        new JournalsReport(batchNumber, journalsFilePath).ExportToDatabase();
                        //new DemographicsReport(batchNumber, demographicsFilePath).ExportToDatabase();
                    }

                    new BatchSummaryService().SaveBatchSummary(new List<BatchSummaryModel> {

                        new BatchSummaryModel {
                            BatchNumber = batchNumber,
                            FileName = filePathResolver.GetSalesFileName(),
                            ProcessedDate = DateTime.Now,
                            SalesDate = DateTime.Now
                        },

                        new BatchSummaryModel {
                            BatchNumber = batchNumber,
                            FileName = filePathResolver.GetItemsFileName(),
                            ProcessedDate = DateTime.Now,
                            SalesDate = DateTime.Now
                        },

                        new BatchSummaryModel {
                            BatchNumber = batchNumber,
                            FileName = filePathResolver.GetJournalsFileName(),
                            ProcessedDate = DateTime.Now,
                            SalesDate = DateTime.Now
                        },

                        new BatchSummaryModel {
                            BatchNumber = batchNumber,
                            FileName = filePathResolver.GetExceptionReportFileName(),
                            ProcessedDate = DateTime.Now,
                            SalesDate = DateTime.Now
                        },
                    });
                }

                if (ValidationErrorService.GetErrors().Count > 0)
                {
                    new ExceptionReport(batchNumber).GenerateExceptionReport(filePathResolver.GetExceptionReportPath(), !validationResult);
                    new ExceptionService().SaveException(ValidationErrorService.GetErrors());
                }

                if (ValidationErrorService.GetErrors().Any(s => s.ErrorCode == ((int)ErrorCodes.GeneralException).ToString()))
                {
                    //DeleteThisBatchRecords(batchNumber);
                }
                ValidationErrorService.ClearErrors();

            }

            catch (Exception ex)
            {
                new ExceptionService().SaveException(new ExceptionModel
                {
                    BatchNumber = batchNumber,
                    Details = ex.Message,
                    ProcessedDate = DateTime.Now,
                    ErrorCode = ((int)ErrorCodes.GeneralException).ToString(),
                    ErrorDescription = ErrorCodes.GeneralException.GetEnumDescription()
                });
                //DeleteThisBatchRecords(batchNumber);
                throw;
            }
        }

        //private static void DeleteThisBatchRecords(int batchNumber)
        //{
        //    new SalesReport(batchNumber, "").Delete();
        //    new ItemsReport(batchNumber, "").Delete();
        //    new JournalsReport(batchNumber, "").Delete();
        //}
    }
}
