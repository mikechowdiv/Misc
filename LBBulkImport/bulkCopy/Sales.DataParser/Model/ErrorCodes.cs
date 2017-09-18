using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DataParser
{
    public enum ErrorCodes
    {
        [Description("Missing File(s)")]
        MissingFiles = 1001,
        [Description("Empty File(s)")]
        EmptyFiles = 1002,
        [Description("File(s) not current")]
        FilesNotCurrent = 1003,

        [Description("Sales Amt not matching(Sales vs Journal)")]
        SalesAmountNotMatchingSalesVsJournals = 1004,
        [Description("Qty Amt not matching(Sales vs Journal)")]
        QuantityAmountNotMatchingSalesVsJournals = 1005,
        [Description("GST Amt not matching(Sales vs Journal)")]
        GSTAmountNotMatchingSalesVsJournals = 1006,

        [Description("Sales Amt not matching(Items vs Journal)")]
        SalesAmountNotMatchingItemsVsJournals = 1007,
        [Description("Qty Amt not matching(Items vs Journal)")]
        QuantityAmountNotMatchingItemsVsJournals = 1008,
        [Description("GST Amt not matching(Items vs Journal)")]
        GSTAmountNotMatchingItemsVsJournals = 1009,

        [Description("General Exception")]
        GeneralException = 2001,
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
