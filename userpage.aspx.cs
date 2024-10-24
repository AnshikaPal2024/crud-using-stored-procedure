using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyPractice
{
    public partial class userpage : System.Web.UI.Page
    {
        private string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent the page from being cached
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            // Ensure the user is logged in
            if (Session["name"] != null)
            {
                Response.Write("name");
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


        private void BindGridView()
        {
            if (Session["name"] != null)
            {
                string username = Session["name"].ToString();

                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "fetchuserdata");
                        cmd.Parameters.AddWithValue("@name", username);

                        try
                        {
                            con.Open();
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                gvdata.DataSource = rdr;
                                gvdata.DataBind();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the error (implement proper logging)
                            Response.Write("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            // Assuming `launchModal` is a JavaScript function for a modal
            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            // Assuming `launchModel` is a JavaScript function for a modal
            ClientScript.RegisterStartupScript(this.GetType(), "Key", "launchModel();", true);
        }

        protected void SaveButton_Click1(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "SpEdit");
                    cmd.Parameters.AddWithValue("@name", txtname1.Text.Trim());
                    cmd.Parameters.AddWithValue("@mobile", txtmobile2.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtemail3.Text.Trim());
                    cmd.Parameters.AddWithValue("@gender", DropDownList1.SelectedItem.Value);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Saved Successfully!!!');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed!!!');", true);
                        }

                        BindGridView();
                    }
                    catch (Exception ex)
                    {
                        // Log the error (implement proper logging)
                        Response.Write("Error: " + ex.Message);
                    }
                }
            }
        }

        protected void PasswordButton2_Click(object sender, EventArgs e)
        {
            // Logic to handle password change
            string id = txtid1.Text.Trim();
            string oldPassword = txtoldpassword.Text.Trim();
            string newPassword = txtpassword2.Text.Trim();

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "Changepassword");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@oldpassword", oldPassword);
                    cmd.Parameters.AddWithValue("@password", newPassword);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblSuccess.Text = "Password changed successfully.";
                            lblSuccess.Visible = true;
                        }
                        else
                        {
                            lblError.Text = "Password change failed.";
                            lblError.Visible = true;
                        }
                        BindGridView();
                    }

                    catch (Exception ex)
                    {
                        // Log the error (implement proper logging)
                        lblError.Text = "Error: " + ex.Message;
                        lblError.Visible = true;
                    }
                }
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            if (Session["name"] != null)
            {
                Session.Abandon();
                Response.Redirect("Mainpage.aspx");
            }
        }

        protected void btnpdf_Click(object sender, EventArgs e)
        {
            if (Session["name"] != null) // Check if the user is logged in
            {
                string username = Session["name"].ToString();
                string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "fetchuserdata");
                        cmd.Parameters.AddWithValue("@name", username);

                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
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

                                        Document pdfdoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);

                                        PdfWriter writer = PdfWriter.GetInstance(pdfdoc, Response.OutputStream);

                                        pdfdoc.Open();

                                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfdoc, sr);

                                        pdfdoc.Close();


                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                        Response.Write(pdfdoc);
                                        Response.End();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


     
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }


}
