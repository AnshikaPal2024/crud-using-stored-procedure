using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel; // Add reference to Microsoft.Office.Interop.Excel

namespace MyPractice
{
    public partial class Uploadfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Method to read the Excel data and return it as a DataTable
        public static System.Data.DataTable read(Range excelRange)
        {
            DataRow row;
            System.Data.DataTable dt = new System.Data.DataTable();
            int rowCount = excelRange.Rows.Count;
            int colCount = excelRange.Columns.Count;

            for (int j = 1; j <= colCount; j++)
            {
                var cell = (Range)excelRange.Cells[1, j]; // Cast to Range

                if (cell != null && cell.Value2 != null)
                {
                    dt.Columns.Add(cell.Value2.ToString());
                }
                else
                {
                    dt.Columns.Add("Column" + j); // Default column name if the cell is empty
                }
            }

            // Add rows to DataTable
            for (int i = 2; i <= rowCount; i++)
            {
                row = dt.NewRow();
                for (int j = 1; j <= colCount; j++)
                {
                    var cell = (Range)excelRange.Cells[i, j]; // Cast to Range

                    if (cell != null && cell.Value2 != null)
                    {
                        row[j - 1] = cell.Value2.ToString();
                    }
                    else
                    {
                        row[j - 1] = ""; // If the cell is empty, add an empty string
                    }
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        // Handle the file upload and display the Excel data in GridView
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    // Save the uploaded file to a temporary folder
                    string filePath = Server.MapPath("~/Import/" + FileUpload1.FileName);
                    FileUpload1.SaveAs(filePath);

                    // Initialize Excel application
                    Application excelApp = new Application();
                    Workbook excelWorkbook = excelApp.Workbooks.Open(filePath);
                    Worksheet excelWorksheet = (Worksheet)excelWorkbook.Sheets[1];
                    Range excelRange = excelWorksheet.UsedRange;

                    // Bind the DataTable to GridView
                    gvExcelData.DataSource = read(excelRange);
                    gvExcelData.DataBind();

                    // Clean up Excel objects
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Marshal.ReleaseComObject(excelRange);
                    Marshal.ReleaseComObject(excelWorksheet);
                    excelWorkbook.Close(false);
                    Marshal.ReleaseComObject(excelWorkbook);
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);

                    // Delete the file after processing
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    // Display any errors that occur
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file.');</script>");
            }
        }
    }
}
