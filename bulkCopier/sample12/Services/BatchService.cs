using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12.Services
{
    public class BatchService
    {
        public int GetBatchNumber()
        {
            int BatchNumber = 0;

            using (SalesDBContext context = new SalesDBContext())
            {
                BatchNumber = (int)context.Database.SqlQuery<decimal>("INSERT INTO [dbo].[Batch]([Date]) VALUES(getdate()) SELECT @@IDENTITY").FirstOrDefault<decimal>();
            }
            return BatchNumber;
        }
    }
}
