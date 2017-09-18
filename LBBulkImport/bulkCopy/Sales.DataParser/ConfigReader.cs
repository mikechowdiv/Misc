using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class ConfigReader
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;
            }
        }
        public static string FilesRootFolder
        {
            get
            {
                var rootFolder = ConfigurationManager.AppSettings["FilesRootFolder"];
                if (!rootFolder.EndsWith(@"\"))
                {
                    rootFolder += @"\";
                }
                return rootFolder;
            }
        }
        public static string DatabaseDefaultDateFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["DatabaseDefaultDateFormat"];
            }
        }
        public static string DatabaseDefaultDateTimeFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["DatabaseDefaultDateTimeFormat"];
            }
        }
    }
}
