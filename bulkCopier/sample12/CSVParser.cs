using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample12
{
    public class CSVParser
    {
        public DataTable ReadCSVFile(string filePath)
        {
            DataTable dtCsv = new DataTable();
            string FullText;

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    FullText = sr.ReadToEnd().ToString(); //read full file text
                    string[] rows = FullText.Split('\n');

                    //--------------------------------Reading Rows--------------------------------------
                    for (int i = 0; i < rows.Count() -1; i++)
                    {
                        var rowTrimmed = rows[i].Trim('\r');
                        string[] rowsValues = rowTrimmed.Split('|'); //split each row with comma to get individual values

                        //--------------------------------Reading Columns--------------------------------------
                        if (rowTrimmed.Length > 0)
                        {
                            //----------------Header----------------
                            if (i == 0)
                            {
                                for (int j = 0; j < rowsValues.Count(); j++)
                                {
                                    var trimmedValue = string.IsNullOrWhiteSpace(rowsValues[j]) ? rowsValues[j] : rowsValues[j].Trim();
                                    dtCsv.Columns.Add(trimmedValue);
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                var length = rowsValues.Count() > dtCsv.Columns.Count ? dtCsv.Columns.Count : rowsValues.Count();
                                for (int k = 0; k < length; k++)
                                {
                                    var trimmedValue = string.IsNullOrWhiteSpace(rowsValues[k]) ? rowsValues[k] : rowsValues[k].Trim().Trim('"');
                                    dr[k] = trimmedValue.ToString();
                                }
                                dtCsv.Rows.Add(dr); //add new rows
                            }
                        }
                        
                    }
                }
            }
            return dtCsv;
        }
    }
}
