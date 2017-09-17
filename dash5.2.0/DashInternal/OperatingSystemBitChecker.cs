using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashInternal
{
   public class OperatingSystemBitChecker
    {
        public static bool Is64BitOperatingSystem()
        {
            return Environment.Is64BitOperatingSystem;
        }
        public static string GetProgramFilesFolderName()
        {
            if (OperatingSystemBitChecker.Is64BitOperatingSystem())
            {
                return "Program Files (x86)";
            }
            return "Program Files";
        }
    }
}
