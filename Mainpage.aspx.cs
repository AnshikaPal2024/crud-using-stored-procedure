using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Configuration;

namespace MyPractice
{
    public partial class SignIn : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;
        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#";
        private static byte[] iv = new byte[16];

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        if (Request.Cookies["name"] != null)
        //            txtname.Text = Request.Cookies["name"].Value;

        //        if (Request.Cookies["password"] != null)
        //            txtPassword.Attributes["value"] = Request.Cookies["password"].Value; // Prevent password from showing in plain text

        //        chkRememberMe.Checked = (Request.Cookies["name"] != null && Request.Cookies["password"] != null);
        //    }
        //}

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

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    string username = txtname.Text.Trim();
        //    string plainPassword = txtPassword.Text;

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Action", "login");
        //            cmd.Parameters.AddWithValue("@name", username);

        //            try
        //            {
        //                con.Open();
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.Read())
        //                    {
        //                        string encryptedPassword = dr["password"].ToString();
        //                        string decryptedPassword = Decrypt(encryptedPassword);

        //                        if (decryptedPassword == plainPassword)
        //                        {
        //                            string role = dr["usertype"].ToString();
        //                            Session["usertype"] = role;
        //                            Session["name"] = username;

        //                            if (chkRememberMe.Checked)
        //                            {
        //                                Response.Cookies["name"].Value = username;
        //                                Response.Cookies["password"].Value = plainPassword;
        //                                Response.Cookies["name"].Expires = DateTime.Now.AddMinutes(30);
        //                                Response.Cookies["password"].Expires = DateTime.Now.AddMinutes(30);
        //                                Response.Cookies["name"].HttpOnly = true; // Secure cookie
        //                                Response.Cookies["password"].HttpOnly = true; // Secure cookie
        //                            }
        //                            else
        //                            {
        //                                Response.Cookies["name"].Expires = DateTime.Now.AddMinutes(-30);
        //                                Response.Cookies["password"].Expires = DateTime.Now.AddMinutes(-30);
        //                            }

        //                            // Redirect based on user role
        //                            Response.Redirect(role == "Admin" ? "Adminpage.aspx" : "userpage.aspx");
        //                        }
        //                        else
        //                        {
        //                            lblmsg.Text = "Login failed: Invalid username or password";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblmsg.Text = "Login failed: Invalid username or password";
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                lblmsg.Text = "Error: " + ex.Message; // Consider logging instead
        //            }
        //        }
        //    }
        //}

        //protected void Submitbtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(cs))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@Action", "GetEmailAndPassword");
        //                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());

        //                con.Open();
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.Read())
        //                    {
        //                        string email = dr["email"].ToString();
        //                        string encryptedPassword = dr["password"].ToString();
        //                        string decryptedPassword = Decrypt(encryptedPassword); // Decrypt the password

        //                        // Create and send email
        //                        StringBuilder sb = new StringBuilder();
        //                        sb.AppendLine("Email: " + email);
        //                        sb.AppendLine("Password: " + decryptedPassword); // Sending decrypted password

        //                        using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
        //                        {
        //                            client.EnableSsl = true;
        //                            client.UseDefaultCredentials = false;
        //                            client.Credentials = new NetworkCredential("palanshika452@gmail.com", "bmws kvak atfx nkwt");

        //                            MailMessage msg = new MailMessage();
        //                            msg.To.Add(txtEmail.Text.Trim());
        //                            msg.From = new MailAddress("palanshika452@gmail.com");
        //                            msg.Subject = "Your password"; // Consider using a more secure method for password recovery
        //                            msg.Body = sb.ToString();
        //                            client.Send(msg);

        //                            lblmsg.Text = "Your password has been sent to your email.";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblmsg.Text = "Invalid Email Id";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblmsg.Text = "Error: " + ex.Message; // Consider logging instead
        //    }
        //}

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login1.aspx");
        }
    }
}

    
