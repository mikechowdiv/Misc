using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser.Model
{
    public class TableSchemaModel
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public string ColumnNameWithoutSpace {

            get
            {
                if(!string.IsNullOrWhiteSpace(ColumnName))
                {
                    return ColumnName.Replace(" ", "").ToLower();
                }
                return string.Empty;
            }
        }
    }
}
