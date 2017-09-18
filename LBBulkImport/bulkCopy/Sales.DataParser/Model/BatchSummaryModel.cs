using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class BatchSummaryModel
    {
        public int BatchNumber { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string FileName { get; set; }
        public DateTime SalesDate { get; set; }
    }
}
