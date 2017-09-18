using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public static class ValidationErrorService
    {
        private static List<ExceptionModel> Errors = new List<ExceptionModel>();

        public static void ClearErrors()
        {
            Errors = new List<ExceptionModel>();
        }

        public static void AddError(ExceptionModel error)
        {
            Errors.Add(error);
        }
        public static List<ExceptionModel> GetErrors()
        {
            return Errors;
        }
    }
}
