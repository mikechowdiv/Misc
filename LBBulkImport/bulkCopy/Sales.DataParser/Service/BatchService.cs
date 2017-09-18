using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public class BatchService
    {
        public int GetBatchNumber()
        {
            int batchNumber = 0;

            using (SalesDBContext context = new SalesDBContext())
            {
                batchNumber = (int)context.Database.SqlQuery<decimal>("INSERT INTO [dbo].[Batch]([Date]) VALUES(getdate()) SELECT @@IDENTITY").FirstOrDefault<decimal>();
            }

            return batchNumber;
        }
    }
}
