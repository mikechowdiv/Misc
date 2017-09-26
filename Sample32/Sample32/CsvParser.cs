using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample32
{
    public class CsvParser
    {
        public DataTable ReadCsvFile(string filePath)
        {

            DataTable dtCsv = new DataTable();
            string Fulltext;
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        var rowTimmed = rows[i].Trim('\r');
                        string[] rowValues = rowTimmed.Split('|'); //split each row with comma to get individual values  
                        if (rowTimmed.Length > 0)
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    var trimmedValue = string.IsNullOrWhiteSpace(rowValues[j]) ? rowValues[j] : rowValues[j].Trim();
                                    dtCsv.Columns.Add(trimmedValue); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                var length = rowValues.Count() > dtCsv.Columns.Count ? dtCsv.Columns.Count : rowValues.Count();

                                for (int k = 0; k < length; k++)
                                {
                                    var trimmedValue = string.IsNullOrWhiteSpace(rowValues[k]) ? rowValues[k] : rowValues[k].Trim().Trim('"');
                                    dr[k] = trimmedValue.ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }
            return dtCsv;
        }
    }
}
