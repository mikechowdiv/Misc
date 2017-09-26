using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32
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


    }
}
