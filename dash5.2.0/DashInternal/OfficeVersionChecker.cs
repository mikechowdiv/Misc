using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashInternal
{
   public class OfficeVersionChecker
    {
        public static string GetOfficeVersion()
        {
            string _version = string.Empty;
            Microsoft.Office.Interop.Word.Application appVersion = new Microsoft.Office.Interop.Word.Application();
            appVersion.Visible = false;
            switch (appVersion.Version.ToString())
            {
                case "7.0":
                    _version = "95";
                    break;
                case "8.0":
                    _version = "97";
                    break;
                case "9.0":
                    _version = "2000";
                    break;
                case "10.0":
                    _version = "2002";
                    break;
                case "11.0":
                    _version = "2003";
                    break;
                case "12.0":
                    _version = "2007";
                    break;
                case "14.0":
                    _version = "2010";
                    break;
                case "15.0":
                    _version = "2013";
                    break;
                case "16.0":
                    _version = "2016";
                    break;
                default:
                    _version = "Too Old!";
                    break;
            }
            return _version;
        }

        public static string GetOfficeFolder()
        {
            string officeVersion = OfficeVersionChecker.GetOfficeVersion();
            string officeFolder = string.Empty;
            switch (officeVersion)
            {
                case "2010":
                    officeFolder = "Office14";
                    break;
                case "2013":
                    officeFolder = "Office15";
                    break;
                case "2016":
                    officeFolder = @"root\Office16";
                    break;
                default:
                    officeFolder = string.Empty;
                    break;
            }
            return officeFolder;
        }

        public static string GetOfficeExePath()
        {
            string exePath = @"C:\{0}\Microsoft Office\{1}\MSACCESS.EXE ";
            exePath = string.Format(exePath,
                OperatingSystemBitChecker.GetProgramFilesFolderName(),
                OfficeVersionChecker.GetOfficeFolder());
            return exePath;
        }
    }
}
