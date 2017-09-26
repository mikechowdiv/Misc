using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32
{
    public class FilePathResolver
    {
        private const string csvFilePrefix = "LBWeekly";
        private const string exceptionReportPrefix = "Exception_";
        private string filesRootPath = string.Empty;
        private int batchNumber;
        public FilePathResolver(int batchNumber)
        {
            filesRootPath = ConfigReader.FilesRootFolder;
            this.batchNumber = batchNumber;
        }

        //-------------------------------get path-------------------------------

        public string GetSalesCsvPath()
        {
            var path = $"{filesRootPath}{GetSalesFileName()}";
            return path;
        }

        //-----------------------------get filename---------------------------------
        public string GetSalesFileName()
        {
            var path = $"{csvFilePrefix}Sales_1_{GetDateFormatForCsv()}.csv";
            return path;
        }

        //-----------------------------get dateformat---------------------------------

        public string GetDateFormatForCsv()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }




    }
}
