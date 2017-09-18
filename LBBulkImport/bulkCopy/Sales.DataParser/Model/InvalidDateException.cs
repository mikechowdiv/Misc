using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser.Model
{
    public class InvalidDateException : Exception
    {
        public string LineNumber { get; set; }
        public InvalidDateException(string message, string lineNumber) : base(message)
        {
            this.LineNumber = lineNumber;
        }
    }
}
