using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DashInternal
{
    public partial class Dashform : Form
    {
        public Dashform()
        {
            InitializeComponent();
        }


        //----------------------------------------MAIN MENU------------------------------------------------
        private void btnLiquologyID_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\internal\Liquology_MemID_Prog.accdb";
            OpenFile(fileName);
        }

        private void btnLiquologyImport_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\Liquology-Iris\Liquology_Import_Prog.accdb";
            OpenFile(fileName);
        }

        private void btnLFAdmin_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\internal\LiquorFileAdmin.accdb";
            OpenFile(fileName);
        }

        private void btnLiquorfileCloud_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\Program Files\LMG Marketing\LiquorfileCloud\LiquorfileCloud.exe";
            //OpenFile(fileName);
            //MessageBox.Show(fileName, "Message");
            if (!File.Exists(fileName))
            {
                MessageBox.Show("File not found.", "Error");
                return;
            }
            Process.Start(new ProcessStartInfo(AddQuotesAtStartAndEndOfPath(fileName)));
        }

        private void btnLiquologyPOS_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\internal\Liquology_POSLinking_Prog.accdb";
            OpenFile(fileName);
        }

        private void btnLiquology_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\internal\Liquology_Promotion_Prog.accdb";
            OpenFile(fileName);
        }

        private void btnLiquorfile_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\internal\LF_Prog.accdb";
            OpenFile(fileName);
        }

        private void btnCoreSuper_Click(object sender, EventArgs e)
        {
            string fileName = @"R:\GIMS\Rebate DB\Supplier_Member_Rebate_Prog.accdb";
            OpenFile(fileName);
        }

        private void btnDateGenN_Click(object sender, EventArgs e)
        {
            string fileName = @"R:\GIMS\Report DB\GIMS_Report_Date_Generator_Prog .accdb";
            OpenFile(fileName);
        }

        private void btn_Contacts_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\internal\LMG_Contacts_Prog.accdb";
            OpenFile(fileName);
        }

        private void btn_GIMS_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\LMGMcontainer\internal\GIMS_Product_Link_Prog.accdb";
            OpenFile(fileName);
        }

        //-------------------------------------OPTIONS MENU-----------------------------------------

        //------------------------------UPDATE DASHBOARD--------------------------------
        //private void upgradeDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (!AskConfirmation()) return;

        //    string exePath = @"O:\Liquology_Setup\LMGDash\LMGDashUpgradex64.exe";
        //    if (!File.Exists(exePath))
        //    {
        //        MessageBox.Show("File not found.", "Error");
        //        return;
        //    }

        //    Process.Start(new ProcessStartInfo(AddQuotesAtStartAndEndOfPath(exePath), "/SILENT"));
        //}

        //------------------------------UPDATE DATABASE--------------------------------
        private void updateDatabasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskConfirmation())
            {
                return;
            }
            else
            {
                MessageBox.Show("This will take about 1 minute to run.  Please wait until 12 confirmation messages appear");
            }
                    
            string sourceFile1 = @"R:\Liquology\Member DB\Liquology_MemID_Prog.accdb";
            string destinationFile1 = @"C:\LMGMcontainer\internal\Liquology_MemID_Prog.accdb";
            
            string sourceFile2 = @"R:\LiquorFile\Product DB\LF_Prog.accdb";
            string destinationFile2 = @"C:\LMGMcontainer\internal\LF_Prog.accdb";
         
            string sourceFile3 = @"R:\Liquorfile\Liquorfile Admin DB\LiquorFileAdmin.accdb";
            string destinationFile3 = @"C:\LMGMcontainer\internal\LiquorFileAdmin.accdb";
            
            string sourceFile4 = @"R:\LMGM\Case DB\LMG_CaseDB_Prog.accdb";
            string destinationFile4 = @"C:\LMGMcontainer\internal\LMG_CaseDB_Prog.accdb";
            
            string sourceFile5 = @"R:\LMGM\Contact DB\LMG_Contacts_Prog.accdb";
            string destinationFile5 = @"C:\LMGMcontainer\internal\LMG_Contacts_Prog.accdb";
            
            string sourceFile6 = @"R:\LMGM\Quote DB\LMG_Quotes_Prog.accdb";
            string destinationFile6 = @"C:\LMGMcontainer\internal\LMG_Quotes_Prog.accdb";
            
            string sourceFile7 = @"R:\LMGM\TimeSheet DB\LMG_Timesheet_Prog.accdb";
            string destinationFile7 = @"C:\LMGMcontainer\internal\LMG_Timesheet_Prog.accdb";
            
            string sourceFile8 = @"R:\GIMS\Member DB\LMG_Mem_Prog.accdb";
            string destinationFile8 = @"C:\LMGMcontainer\internal\LMG_Mem_Prog.accdb";
            
            string sourceFile9 = @"R:\GIMS\Member DB\UIAL_Mem_Prog.accdb";
            string destinationFile9 = @"C:\LMGMcontainer\internal\UIAL_Mem_Prog.accdb";
            
            string sourceFile10 = @"R:\Liquology\Product Link DB\Liquology_POSLinking_Prog.accdb";
            string destinationFile10 = @"C:\LMGMcontainer\internal\Liquology_POSLinking_Prog.accdb";
            
            string sourceFile11 = @"R:\GIMS\Product DB\GIMS_Product_Link_Prog.accdb";
            string destinationFile11 = @"C:\LMGMcontainer\internal\GIMS_Product_Link_Prog.accdb";
            
            string sourceFile12 = @"R:\Liquology\Promotion DB\Liquology_Promotion_Prog.accdb";
            string destinationFile12 = @"C:\LMGMcontainer\internal\Liquology_Promotion_Prog.accdb";

            

            string[] arr = new string[12];
            arr[0] = sourceFile1;
            arr[1] = sourceFile2;
            arr[2] = sourceFile3;
            arr[3] = sourceFile4;
            arr[4] = sourceFile5;
            arr[5] = sourceFile6;
            arr[6] = sourceFile7;
            arr[7] = sourceFile8;
            arr[8] = sourceFile9;
            arr[9] = sourceFile10;
            arr[10] = sourceFile11;
            arr[11] = sourceFile12;

            File.Copy(sourceFile1, destinationFile1, true);
            File.Copy(sourceFile2, destinationFile2, true);
            File.Copy(sourceFile3, destinationFile3, true);
            File.Copy(sourceFile4, destinationFile4, true);
            File.Copy(sourceFile5, destinationFile5, true);
            File.Copy(sourceFile6, destinationFile6, true);
            File.Copy(sourceFile7, destinationFile7, true);
            File.Copy(sourceFile8, destinationFile8, true);
            File.Copy(sourceFile9, destinationFile9, true);
            File.Copy(sourceFile10, destinationFile10, true);
            File.Copy(sourceFile11, destinationFile11, true);
            File.Copy(sourceFile12, destinationFile12, true);

            foreach (string s in arr)
            {
                if (!File.Exists(s))
                {
                    MessageBox.Show("Source file not found in R drive.  Please check. ", "Error");
                    return;
                }
                else
                {
                    try
                    {
                        //MessageBox.Show(Convert.ToString(s) + " has been updated to your local C drive location.");
                        MessageBox.Show("Files have been updated to your local C drive successfully.");
                        break;
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        MessageBox.Show("A file cannot be found in your destination folder in C drive.  Please check with IT. ");
                        MessageBox.Show(string.Concat("{0}", ex));
                        break;
                    }
                    catch (FileNotFoundException ex1)
                    {
                        MessageBox.Show("A file cannot be found in your source folder in G drive.  Please check with IT. ");
                        MessageBox.Show(string.Concat("{0}", ex1));
                        break;
                    }
                }
            }      
        }

        //------------------------------COMPILE DASHBOARD 64--------------------------------
        private void compileDashboardx64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskConfirmation())
            {
                return;
            }
            string exePath = @"C:\Program Files\Inno Setup 5\CompileDashx64.bat";

            if (!File.Exists(exePath))
            {
                MessageBox.Show("File not found.", "Error");
                return;
            }
            Process.Start(new ProcessStartInfo(AddQuotesAtStartAndEndOfPath(exePath)));
        }


        //------------------------------UPGRADE LIQUIDOGY IMPORT--------------------------------
        private void upgradeLiquologyImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskConfirmation()) return;
            else
            {
                MessageBox.Show("This will take about 30 seconds to run.  Please wait until 2 confirmation messages appear");
            }

            string sourceFile1 = @"R:\Liquology\Import DB\Liquology_Import_Prog.accdb";
            string destinationFile1 = @"C:\LMGMcontainer\Liquology-Iris\Liquology_Import_Prog.accdb";
                           
            string sourceFile2 = @"R:\Liquology\Import DB\Liquology_Import_Temp.accdb";
            string destinationFile2 = @"C:\LMGMcontainer\Liquology-Iris\Liquology_Import_Temp.accdb";

            string[] arr = new string[2];
            arr[0] = sourceFile1;
            arr[1] = sourceFile2;
          
            foreach (string s in arr)
            {
                if (!File.Exists(s))
                {
                    MessageBox.Show("Source file not found in R drive. Please check. ", "Error");
                    return;
                }
                else
                {
                    try
                    {
                        File.Copy(sourceFile1, destinationFile1, true);
                        File.Copy(sourceFile2, destinationFile2, true);
                        //MessageBox.Show(Convert.ToString(s) + " has been updated to your local C drive location.");
                        MessageBox.Show("Files have been updated to your local C drive successfully.");
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        MessageBox.Show("A file cannot be found in your destination folder in C drive.  Please check with IT. ");
                        MessageBox.Show(string.Concat("{0}", ex));
                        break;
                    }
                    catch (FileNotFoundException ex1)
                    {
                        MessageBox.Show("A file cannot be found in your source folder in R drive.  Please check with IT. ");
                        MessageBox.Show(string.Concat("{0}", ex1));
                        break;
                    }
                }
            }      
        }


        //---------------------------------------TEST--------------------------------------------

        private void compileTest1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskConfirmation()) return;
           
            string exePath = @"G:\Temp\Mike\SayHello.exe";
            if (!File.Exists(exePath))
            {
                MessageBox.Show("File not found.", "Error");
                return;
            }
            Process.Start(new ProcessStartInfo(AddQuotesAtStartAndEndOfPath(exePath)));
        }

        
        private void upgradeTest1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (!AskConfirmation()) return;
            
            string sourceFile1 = @"G:\Temp\Mike\test1.accdb";
            string destinationFile1 = @"C:\ProgramData\LMGMarketing\Test\test1.accdb";
            string sourceFile2 = @"G:\Temp\Mike\test2.accdb";
            string destinationFile2 = @"C:\ProgramData\LMGMarketing\Test2\test2.accdb";

            string[] arr = new string[2];
            arr[0] = sourceFile1;
            arr[1] = sourceFile2;         

            foreach (string s in arr)
            {
                if (!File.Exists(s))
                {
                    MessageBox.Show("File not found.", "Error");
                    return;
                }
                else
                {
                    //MessageBox.Show(Convert.ToString(s) + " has been updated to your local C drive location.");
                    try
                    {
                        File.Copy(sourceFile1, destinationFile1, true);
                        File.Copy(sourceFile2, destinationFile2, true);
                        MessageBox.Show(Convert.ToString(s) + " has been updated to your local C drive location.");
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        MessageBox.Show("Cannot find your destination folder in C drive.  Please check with IT. ");
                        MessageBox.Show(string.Concat("{0}", ex));
                        break;
                    }
                
                }              
            }      
        }
                    

        //-----------------------------------------Utilities------------------------------------------
        private bool AskConfirmation()
        {
            DialogResult result = MessageBox.Show("Are you sure to update the file(s) in your local C drive?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.Cancel))
            {
                return false;
            }
            return true;
        }

        private string AddQuotesAtStartAndEndOfPath(string path)
        {
            return string.Format("\"{0}\"", path);
        }


        private void OpenFile(string fileName)
        {
            string exePath = OfficeVersionChecker.GetOfficeExePath();
            //MessageBox.Show(exePath + fileName, "Message");
            if (!File.Exists(fileName))
            {
                MessageBox.Show("File not found.", "Error");
                return;
            }
            Process.Start(new ProcessStartInfo(exePath, AddQuotesAtStartAndEndOfPath(fileName)));
        }

        
    }
}
