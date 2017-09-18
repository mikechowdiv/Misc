using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser.Service
{
    public class ExceptionService
    {
        public void SaveException(ExceptionModel model)
        {
            using (SalesDBContext context = new SalesDBContext())
            {
                context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[Execptions]([BatchNumber],[ProcessedDate],[ErrorCode],[ErrorDescription],[Details]) VALUES(@BatchNumber,@ProcessedDate,@ErrorCode,@ErrorDescription,@Details)",
                    new SqlParameter("@BatchNumber", model.BatchNumber),
                    new SqlParameter("@ProcessedDate", model.ProcessedDate),
                    new SqlParameter("@ErrorCode", model.ErrorCode),
                    new SqlParameter("@ErrorDescription", model.ErrorDescription),
                    new SqlParameter("@Details", model.Details));
            }
        }
        public void SaveException(List<ExceptionModel> exceptions)
        {
            foreach (var item in exceptions)
            {
                SaveException(item);
            }
        }
    }
}
