using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12
{
    public class FilesPathResolver
    {
        private const string csvFilePrefix = "LBWeekly";

        private string filesRootPath = string.Empty;
        private int batchNumber;

        public FilesPathResolver(int batchNumber)
        {
            filesRootPath = ConfigReader.FilesRootFolder;
            this.batchNumber = batchNumber;
        }

        public string GetTest1CSVPath()
        {
            var path = $"{filesRootPath}{GetTest1FileName()}";
            return path;
        }

        public string GetTest1FileName()
        {
            var path = $"{csvFilePrefix}Test1_1_{GetDateFormatForCSV()}.csv";
            return path;
        }

        public string GetDateFormatForCSV()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
