using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class FilePathResolver
    {
        private const string csvFilePrefix = "LBWeekly";
        private const string exceptionReportPredfix = "Exception_";
        private const string demographicsCsvPrefix = "LBMemberDemographics_1_";
        private string filesRootPath = string.Empty;
        private int batchNumber;
        public FilePathResolver(int batchNumber)
        {
            filesRootPath = ConfigReader.FilesRootFolder;
            this.batchNumber = batchNumber;
        }
        public string GetSalesCsvPath()
        {
            var path = $"{filesRootPath}{GetSalesFileName()}";
            return path;
        }
        public string GetItemsCsvPath()
        {
            var path = $"{filesRootPath}{GetItemsFileName()}";
            return path;
        }
        public string GetJournalsCsvPath()
        {
            var path = $"{filesRootPath}{GetJournalsFileName()}";
            return path;
        }
        public string GetDemographicsCsvPath()
        {
            var path = $"{filesRootPath}{GetDemographicsFileName()}";
            return path;
        }

        public string GetExceptionReportPath()
        {
            var path = $"{filesRootPath}{GetExceptionReportFileName()}";
            return path;
        }

        public string GetSalesFileName()
        {
            var path = $"{csvFilePrefix}Sales_1_{GetDateFormatForCsv()}.csv";
            return path;
        }
        public string GetItemsFileName()
        {
            var path = $"{csvFilePrefix}Items_1_{GetDateFormatForCsv()}.csv";
            return path;
        }
        public string GetJournalsFileName()
        {
            var path = $"{csvFilePrefix}Journals_1_{GetDateFormatForCsv()}.csv";
            return path;
        }
        public string GetDemographicsFileName()
        {
            var path = $"{demographicsCsvPrefix}{GetDateFormatForCsv()}.csv";
            return path;
        }

        public string GetExceptionReportFileName()
        {
            var path = $"{exceptionReportPredfix}{GetDateFormatForCsv()}.csv";
            return path;
        }
        public string GetDateFormatForCsv()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        public bool ValidateExistanceOfCsvFiles()
        {
            var isAnyFilesMissing = false;
            //if(!System.IO.File.Exists(GetSalesCsvPath()))
            //{
            //    ValidationErrorService.AddError(new ExceptionModel
            //    {
            //        BatchNumber = batchNumber,
            //        ErrorCode = ((int)ErrorCodes.MissingFiles).ToString(),
            //        ProcessedDate = DateTime.Now,
            //        ErrorDescription = ErrorCodes.MissingFiles.GetEnumDescription(),
            //        Details = GetSalesCsvPath()
            //    });
            //    isAnyFilesMissing = true;
            //}
            if (!System.IO.File.Exists(GetItemsCsvPath()))
            {
                ValidationErrorService.AddError(new ExceptionModel
                {
                    BatchNumber = batchNumber,
                    ErrorCode = ((int)ErrorCodes.MissingFiles).ToString(),
                    ProcessedDate = DateTime.Now,
                    ErrorDescription = ErrorCodes.MissingFiles.GetEnumDescription(),
                    Details = GetItemsCsvPath()
                });
                isAnyFilesMissing = true;
            }
            if (!System.IO.File.Exists(GetJournalsCsvPath()))
            {
                ValidationErrorService.AddError(new ExceptionModel
                {
                    BatchNumber = batchNumber,
                    ErrorCode = ((int)ErrorCodes.MissingFiles).ToString(),
                    ProcessedDate = DateTime.Now,
                    ErrorDescription = ErrorCodes.MissingFiles.GetEnumDescription(),
                    Details = GetJournalsCsvPath()
                });
                isAnyFilesMissing = true;
            }
            //if (!System.IO.File.Exists(GetDemographicsCsvPath()))
            //{
            //    ValidationErrorService.AddError(new ExceptionModel
            //    {
            //        BatchNumber = batchNumber,
            //        ErrorCode = ((int)ErrorCodes.MissingFiles).ToString(),
            //        ProcessedDate = DateTime.Now,
            //        ErrorDescription = ErrorCodes.MissingFiles.GetEnumDescription(),
            //        Details = GetDemographicsCsvPath()
            //    });
            //    isAnyFilesMissing = true;
            //}
            return isAnyFilesMissing;
        }
    }
}
