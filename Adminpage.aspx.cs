using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using OfficeOpenXml;
using System.Drawing;
using iTextSharp.tool.xml;
using static System.Net.WebRequestMethods;
using Image = System.Web.UI.WebControls.Image;
using System.Net;
using File = System.IO.File;
using System.Net.Mail;
using System.EnterpriseServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;



namespace MyPractice
{
    public partial class Adminpage : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;
        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#";
        private static byte[] iv = new byte[16];

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (Session["name"] != null)
            {
            }
            else
            {
                Response.Redirect("Mainpage.aspx");
            }

            if (!IsPostBack)
            {
                BindGridView();
            }

        }


        private string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }


        private void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "showdetails");

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);

                    // Check if the DataTable contains the Password column
                    if (dt.Columns.Contains("Password"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Password"] != DBNull.Value)
                            {
                                // Store the encrypted password in a separate variable
                                string encryptedPassword = row["Password"].ToString();
                                row["Password"] = Decrypt(encryptedPassword); // Replace with decrypted for display
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Password column is missing in the result set.");
                    }

                    gvdata.DataSource = dt;
                    gvdata.DataBind();
                }
            }
        }


        protected void Buttonpdf_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
            Response.Charset = "";
            Response.ContentType = "application/pdf";

            gvdata.AllowPaging = false;
            System.Data.DataTable dt = GetDataForPdf(); // Fetch data with encrypted passwords
            gvdata.DataSource = dt; // Optionally bind for visual inspection
            gvdata.DataBind(); // Not necessary for PDF generation

            using (MemoryStream ms = new MemoryStream())
            {
                using (Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                    pdfDoc.Open();

                    // Render GridView to HTML and convert to PDF
                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                        {
                            // Hide the Actions column if present
                            for (int i = 0; i < gvdata.Columns.Count; i++)
                            {
                                if (gvdata.Columns[i].HeaderText == "Actions")
                                {
                                    gvdata.Columns[i].Visible = false;
                                    break;
                                }
                            }

                            gvdata.RenderControl(hw); // Render the GridView to HTML
                            string htmlContent = sw.ToString();

                            using (StringReader sr = new StringReader(htmlContent))
                            {
                                // Use XMLWorker to parse the HTML and create the PDF
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                            }
                        }
                    }

                    pdfDoc.Close();
                    Response.OutputStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        private System.Data.DataTable GetDataForPdf()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "showdetails");

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            return dt;
        }


        protected void gvdata_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvdata.Rows[e.RowIndex];
            int id = Convert.ToInt32(gvdata.DataKeys[e.RowIndex].Value);

            var txtname = (System.Web.UI.WebControls.TextBox)row.FindControl("txtname");
            var txtemail = (System.Web.UI.WebControls.TextBox)row.FindControl("txtemail");
            var txtPassword = (System.Web.UI.WebControls.TextBox)row.FindControl("txtPassword");
            var txtmobile = (System.Web.UI.WebControls.TextBox)row.FindControl("txtmobile");
            var ddlgender = (DropDownList)row.FindControl("ddlgender");
            var ddlusertype = (DropDownList)row.FindControl("ddlusertype");
            var fileUpload = (FileUpload)row.FindControl("FileUpload2");
            var importpdf = (FileUpload)row.FindControl("importFile");

            // Validate controls
            if (txtname == null || txtemail == null || txtPassword == null ||
                txtmobile == null || ddlgender == null || ddlusertype == null ||
                fileUpload == null || importpdf == null)
            {
                Response.Write("<script>alert('Error: Some controls are missing.');</script>");
                return;
            }

            // Handle PDF upload
            string pdfLink = null;
            if (importpdf.HasFile)
            {
                try
                {
                    string pdfFileName = Path.GetFileName(importpdf.FileName);
                    string pdfPath = Server.MapPath("~/Import/") + pdfFileName;
                    importpdf.SaveAs(pdfPath);
                    pdfLink = "~/Import/" + pdfFileName; // Save relative path for hyperlink
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error saving PDF: {ex.Message}');</script>");
                    return; // Stop further execution if PDF import fails
                }
            }


            // Extract other data
            string name = txtname.Text;
            string email = txtemail.Text;
            string password = txtPassword.Text;
            string mobile = txtmobile.Text;
            string gender = ddlgender.SelectedValue;
            string usertype = ddlusertype.SelectedValue;

            byte[] imageData = null;

            // Retrieve existing image from the database
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT image FROM sptable WHERE id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        imageData = (byte[])result; // Existing image data
                    }
                }
            }

            // Check if a new image file is uploaded
            if (fileUpload.HasFile)
            {
                string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    using (BinaryReader reader = new BinaryReader(fileUpload.PostedFile.InputStream))
                    {
                        imageData = reader.ReadBytes(fileUpload.PostedFile.ContentLength); // Update with new image data
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only .jpg, .jpeg, and .png files are allowed.');</script>");
                    return;
                }
            }

            // Update database
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "updateuser");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", Encrypt(password));
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@usertype", usertype);
                    cmd.Parameters.AddWithValue("@image", (object)imageData ?? DBNull.Value); // Use existing image if new one not provided
                    cmd.Parameters.AddWithValue("@pdf", (object)pdfLink ?? DBNull.Value); // Save PDF path

                    try
                    {
                        con.Open();
                        int rowsAffected = (int)cmd.ExecuteScalar();

                        if (rowsAffected > 0)
                        {
                            gvdata.EditIndex = -1;
                            BindGridView();
                            Response.Write("<script>alert('Record Updated Successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Record Update Failed.');</script>");
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        string message = sqlEx.Message.Replace("'", "\\'");
                        Response.Write($"<script>alert('Database error: {message}');</script>");
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("'", "\\'");
                        Response.Write($"<script>alert('An error occurred: {message}');</script>");
                    }
                }
            }
        }






        private string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        protected void gvdata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvdata.EditIndex = -1;
            BindGridView();
        }

        protected void gvdata_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvdata.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "deleteuser");
                    cmd.Parameters.AddWithValue("@id", id);
                    try

                    {

                        con.Open();
                        int rowsAffected = (int)cmd.ExecuteScalar();
                        if (rowsAffected > 0)
                        {
                            BindGridView(); // Rebind the GridView to reflect changes
                            Response.Write("<script>alert('Record deleted successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Delete failed');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("'", "\\'");
                        Response.Write($"<script>alert('{message}');</script>");
                    }
                }
            }
        }

        protected void gvdata_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvdata.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        public string GetImageUrl(object imageData)
        {
            if (imageData != null && imageData is byte[])
            {
                string base64String = Convert.ToBase64String((byte[])imageData);
                return $"data:image/png;base64,{base64String}"; // Adjust image type as necessary
            }
            return "~/Images/default.png"; // Placeholder for no image
        }

        private string EncryptPassword(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }
        protected void InsertButton_Click(object sender, EventArgs e)
        {

            FileUpload fileUpload = (FileUpload)gvdata.FooterRow.FindControl("FileUpload1");

            System.Web.UI.WebControls.TextBox txtname = (System.Web.UI.WebControls.TextBox)gvdata.FooterRow.FindControl("Name1");
            System.Web.UI.WebControls.TextBox txtemail = (System.Web.UI.WebControls.TextBox)gvdata.FooterRow.FindControl("Email2");
            System.Web.UI.WebControls.TextBox txtpassword = (System.Web.UI.WebControls.TextBox)gvdata.FooterRow.FindControl("Password3");
            System.Web.UI.WebControls.TextBox txtmobile = (System.Web.UI.WebControls.TextBox)gvdata.FooterRow.FindControl("Mobile4");
            DropDownList ddlgender = (DropDownList)gvdata.FooterRow.FindControl("DropDownList1");
            DropDownList ddlusertype = (DropDownList)gvdata.FooterRow.FindControl("DropDownList2");

            string name = txtname.Text;
            string email = txtemail.Text;
            string password = txtpassword.Text;
            string mobile = txtmobile.Text;
            string gender = ddlgender.SelectedValue;
            string usertype = ddlusertype.SelectedValue;

            byte[] imageData = null;

            if (fileUpload != null && fileUpload.HasFile)
            {
                string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    using (BinaryReader reader = new BinaryReader(fileUpload.PostedFile.InputStream))
                    {
                        imageData = reader.ReadBytes(fileUpload.PostedFile.ContentLength);
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only .jpg, .jpeg, and .png files are allowed.');</script>");
                    return;
                }
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                string plainpassword = txtpassword.Text;
                string encryptedpassword = EncryptPassword(plainpassword);
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", encryptedpassword);
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@usertype", usertype);

                    if (imageData != null)
                    {
                        cmd.Parameters.AddWithValue("@image", imageData);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@image", DBNull.Value);
                    }

                    try
                    {
                        con.Open();
                        int rowsAffected = (int)cmd.ExecuteScalar();
                        if (rowsAffected > 0)
                        {
                            gvdata.EditIndex = -1; // Exit edit mode
                            BindGridView(); // Rebind the GridView to reflect changes
                            Response.Write("<script>alert('Data has been inserted');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Insert Failed');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("'", "\\'");
                        Response.Write($"<script>alert('{message}');</script>");
                    }
                }
            }

        }




        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear the session
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();

                // Clear session cookies
                HttpCookie sessionCookie = Request.Cookies[Session.SessionID];
                if (sessionCookie != null)
                {
                    sessionCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(sessionCookie);
                }

                // Redirect to login page
                Response.Redirect("Mainpage.aspx", true);
            }
            catch (Exception ex)
            {
                // Log exception if needed
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
            }
        }

        protected void gvdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Fetch the Current URL.
                Uri uri = Request.Url;

                //Generate Absolute Path for the Images folder.
                string applicationUrl = string.Format("{0}://{1}{2}", uri.Scheme, uri.Authority, uri.AbsolutePath);
                applicationUrl = applicationUrl.Replace(Request.Url.Segments[Request.Url.Segments.Length - 1], "");

                //Fetch the Image ID from the DataKeyNames property.
                int id = Convert.ToInt32(gvdata.DataKeys[e.Row.RowIndex].Values[0]);

                //Generate and set the Handler Absolute URL in the Image control.
                string imageUrl = string.Format("{0}Handler1.ashx?id={1}", applicationUrl, id);
                (e.Row.FindControl("Image1") as System.Web.UI.WebControls.Image).ImageUrl = imageUrl;
            }
        }

        protected void btnexcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel"; // Correct content type for Excel

            // Fetch data with encrypted passwords
            System.Data.DataTable dt = GetDataForexcel();
            gvdata.DataSource = dt;
            gvdata.DataBind(); // Bind the data directly without decrypting

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // Hide the Actions column if present
                for (int i = 0; i < gvdata.Columns.Count; i++)
                {
                    if (gvdata.Columns[i].HeaderText == "Actions")
                    {
                        gvdata.Columns[i].Visible = false;
                        break;
                    }
                }

                gvdata.AllowPaging = false;

                // Hide the FooterRow if present
                if (gvdata.FooterRow != null)
                {
                    gvdata.FooterRow.Visible = false;
                }

                // Set GridView styles
                gvdata.Width = Unit.Percentage(100); // Set GridView width to 100%

                // Loop through the GridView columns to find the "Images" column
                for (int i = 0; i < gvdata.Columns.Count; i++)
                {
                    // Check if the column's HeaderText is "Images"
                    if (gvdata.Columns[i].HeaderText == "Images")
                    {
                        // Loop through each row in the GridView to resize images in the "Images" column
                        foreach (GridViewRow row in gvdata.Rows)
                        {
                            // Locate the Image control in the GridView (assuming the Image control ID is "Image1")
                            Image img = (Image)row.FindControl("Image1");

                            if (img != null)
                            {
                                // Resize the image's width and height for better display in Excel
                                string imgTag = $"<img src='{img.ImageUrl}' width='100' height='100' style='display:inline-block;vertical-align:middle;' />";
                                row.Cells[i].Text = imgTag; // Set the cell text to the image tag
                            }
                        }
                    }
                }

                // Set alternating row colors for better visibility
                foreach (GridViewRow row in gvdata.Rows)
                {
                    row.Height = Unit.Pixel(100);
                    if (row.RowIndex % 2 == 0)
                    {
                        row.BackColor = System.Drawing.Color.LightGray; // Even rows
                    }
                    else
                    {
                        row.BackColor = System.Drawing.Color.White; // Odd rows
                    }
                }

                // Render the GridView with modified settings

                gvdata.RenderControl(hw);

                // Write the content in the response stream to generate the Excel file
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        private System.Data.DataTable GetDataForexcel()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "showdetails"); // Ensure this action returns the encrypted password

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            return dt;
        }


        protected void ExportAdminDetails_Click(object sender, EventArgs e)
        {
            if (Session["name"] != null)
            {
                string username = Session["name"].ToString();
                string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

                try
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Action", "fetchuserdata");
                            cmd.Parameters.AddWithValue("@name", username);

                            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                            {
                                System.Data.DataTable dt = new System.Data.DataTable();
                                ad.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    gvdata.AllowPaging = false;
                                    gvdata.DataSource = dt;
                                    gvdata.DataBind();

                                    using (StringWriter sw = new StringWriter())
                                    {
                                        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                                        {
                                            gvdata.RenderControl(hw);
                                            StringReader sr = new StringReader(sw.ToString());

                                            Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

                                            pdfDoc.Open();
                                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                                            pdfDoc.Close();

                                            // Set response headers for PDF download
                                            Response.ContentType = "application/pdf";
                                            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                            Response.Flush(); // Send the response buffer to the client
                                            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Ends the request properly
                                        }
                                    }
                                }
                                else
                                {
                                    Response.Write("No data available to export.");
                                }
                            }
                        }
                    }
                }
                //catch (Exception ex)
                //{
                //    string message = ex.Message.Replace("'", "\\'");
                //    Response.Write($"<script>alert('{message}');</script>");
                //}
                catch (Exception ex)
                {
                    string message = ex.Message.Replace("'", "\\'");
                    Response.Write($"<script>alert('{message}');</script>");
                }
            }
            else
            {
                Response.Write("Session expired, please log in again.");
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }


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


        //protected void btnUpload_Click(object sender, EventArgs e)
        //    {
        //        if (FileUpload1.HasFile)
        //        {
        //            try
        //            {
        //                // Save the uploaded file to a temporary folder
        //                string filePath = Server.MapPath("~/Import/" + FileUpload1.FileName);
        //                FileUpload1.SaveAs(filePath);

        //                // Initialize Excel application
        //                Application excelApp = new Application();
        //                Workbook excelWorkbook = excelApp.Workbooks.Open(filePath);
        //                Worksheet excelWorksheet = (Worksheet)excelWorkbook.Sheets[1];
        //                Range excelRange = excelWorksheet.UsedRange;

        //                // Bind the DataTable to GridView
        //                gvExcelData.DataSource = read(excelRange);
        //                gvExcelData.DataBind();

        //                // Clean up Excel objects
        //                GC.Collect();
        //                GC.WaitForPendingFinalizers();
        //                Marshal.ReleaseComObject(excelRange);
        //                Marshal.ReleaseComObject(excelWorksheet);
        //                excelWorkbook.Close(false);
        //                Marshal.ReleaseComObject(excelWorkbook);
        //                excelApp.Quit();
        //                Marshal.ReleaseComObject(excelApp);

        //                // Delete the file after processing
        //                File.Delete(filePath);
        //            }
        //            catch (Exception ex)
        //            {
        //                // Display any errors that occur
        //                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        //            }
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Please select a file.');</script>");
        //        }
        //    }

     
    }
    }






