using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12
{
    public class SalesDBContext : DbContext
    {
        protected string connectionName;
        public SalesDBContext(string connectionName = "SalesDB") : base(connectionName)
        {
            this.connectionName = connectionName;
        }
    }
}
