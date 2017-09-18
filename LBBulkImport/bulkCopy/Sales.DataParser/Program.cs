using NLog;
using Sales.DataParser.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Process started. Please wait...........");
                new JobManager().RunJob();
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

        }

    }
}
