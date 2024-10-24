using Org.BouncyCastle.Utilities;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyPractice
{
    public partial class Register : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        //method
        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#";
        private static byte[] iv = new byte[16]; // AES block size is 16 bytes

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


        protected void btnSave_Click1(object sender, EventArgs e)
        {
            string plainpassword=txtPassword.Text;
            //encrypt the password
            string encryptedpassword=Encrypt(plainpassword);

            string usertype = rbAdmin.Checked ? "Admin" : "User";
            string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", encryptedpassword);
                    cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@usertype", usertype);
                  

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Save Successfully');</script>");
                        Response.Redirect("Login1.aspx");

                        // Clear fields after save
                        txtName.Text = "";
                        txtEmail.Text = "";
                        txtMobile.Text = "";
                        txtPassword.Text = "";
                        ddlGender.SelectedIndex = -1;
                        rbAdmin.Checked = false;
                        rbUser.Checked = false;
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string usertype = rbAdmin.Checked ? "Admin" : "User";
            string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Pass only the parameters that are necessary
                    cmd.Parameters.AddWithValue("@Action", "update");
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@usertype", usertype);
                  
                    try
                    {
                        con.Open();
                        int updaterow = cmd.ExecuteNonQuery();

                        // Clear fields after update
                        txtName.Text = "";
                        txtEmail.Text = "";
                        txtMobile.Text = "";
                        txtPassword.Text = "";
                        ddlGender.SelectedIndex = -1;
                        rbAdmin.Checked = false;
                        rbUser.Checked = false;

                        if (updaterow > 0)
                        {
                            Response.Write("<script>alert('Update Successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Record Not Found');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "delete");
                    cmd.Parameters.AddWithValue("@name", txtName.Text);

                    try
                    {
                        con.Open();
                        int rowdelete = cmd.ExecuteNonQuery();

                        if (rowdelete > 0)
                        {
                            Response.Write("<script>alert('Delete Successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Record for this name not found');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
        }

        protected void btnRead_Click1(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "read");
                    cmd.Parameters.AddWithValue("@name", txtName.Text);

                    try
                    {
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    txtName.Text = rd["name"].ToString();
                                    txtEmail.Text = rd["email"].ToString();
                                    txtPassword.Text = rd["password"].ToString();
                                    txtMobile.Text = rd["mobile"].ToString();
                                    ddlGender.SelectedValue = rd["gender"].ToString();
                                    if (rd["usertype"].ToString() == "Admin")
                                    {
                                        rbAdmin.Checked = true;
                                        rbUser.Checked = false;
                                    }
                                    else
                                    {
                                        rbAdmin.Checked = false;
                                        rbUser.Checked = true;
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('No Record Found');</script>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
        }
    }
}
