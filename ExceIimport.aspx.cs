using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace MyPractice
{
    public partial class ExceIimport : System.Web.UI.Page
    {
       
        //protected void btnupload_Click(object sender, EventArgs e)
        //{
        //    if (fileupload1.HasFile)
        //    {
        //        string fileExtension = Path.GetExtension(fileupload1.FileName);

        //        // Validate file extension
        //        if (fileExtension.ToLower() != ".xls" && fileExtension.ToLower() != ".xlsx")
        //        {
        //            lblError.Text = "Please upload a valid Excel file (.xls or .xlsx).";
        //            return;
        //        }

        //        // Process the file
        //        using (var stream = fileupload1.FileContent)
        //        {
        //            IExcelDataReader excelReader = null;

        //            try
        //            {
        //                // Reset the stream position to ensure correct reading
        //                stream.Position = 0;

        //                // Check file extension and load accordingly
        //                if (fileExtension.ToLower() == ".xls")
        //                {
        //                    // Excel 97-03
        //                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        //                }
        //                else if (fileExtension.ToLower() == ".xlsx")
        //                {
        //                    // Excel 2007+
        //                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //                }

        //                // Read the Excel file into a DataSet
        //                using (excelReader)
        //                {
        //                    var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
        //                    {
        //                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                        {
        //                            UseHeaderRow = true // Treat first row as header
        //                        }
        //                    });

        //                    // Bind data to GridView
        //                    GridView1.DataSource = result.Tables[0];
        //                    GridView1.DataBind();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                lblError.Text = "Error: " + ex.Message;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        lblError.Text = "Please select a file to upload.";
        //    }
        //}
    }

}
