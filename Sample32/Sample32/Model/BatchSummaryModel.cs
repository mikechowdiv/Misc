using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32.Model
{
    public class BatchSummaryModel
    {
        public int BatchNumber { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string FileName { get; set; }
        public DateTime SalesDate { get; set; }
    }
}
