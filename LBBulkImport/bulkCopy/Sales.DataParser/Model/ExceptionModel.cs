using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class ExceptionModel
    {
        public int BatchNumber { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string Details { get; set; }
        public string LineNumber { get; set; }
    }
}
