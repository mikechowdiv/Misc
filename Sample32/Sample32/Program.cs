using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32
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
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }
        }
    }
}
