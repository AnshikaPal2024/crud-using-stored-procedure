//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Net;
//using System.Net.Mail;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web.UI;
//using System.Xml.Linq;

//namespace MyPractice
//{
//    public partial class Login :System.Web.UI. Page
//    {
//        string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;
//        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#";
//        private static byte[] iv = new byte[16];

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                if (Request.Cookies["name"] != null)
//                    txtname.Text = Request.Cookies["name"].Value;

//                if (Request.Cookies["password"] != null)
//                    txtPassword.Text = Request.Cookies["password"].Value;

//                chkRememberMe.Checked = (Request.Cookies["name"] != null && Request.Cookies["password"] != null);
//            }
//        }

//        private string Encrypt(string plainText)
//        {
//            using (Aes aes = Aes.Create())
//            {
//                aes.Key = Encoding.UTF8.GetBytes(secretKey);
//                aes.IV = iv;
//                aes.Mode = CipherMode.CBC;
//                aes.Padding = PaddingMode.PKCS7;

//                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
//                {
//                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
//                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
//                    return Convert.ToBase64String(encryptedBytes);
//                }
//            }
//        }

//        protected void btnSave_Click(object sender, EventArgs e)
//        {
//            string username = txtname.Text.Trim();
//            string plainPassword = txtPassword.Text;
//            string encryptedPassword = Encrypt(plainPassword);

//            using (SqlConnection con = new SqlConnection(cs))
//            {
//                using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@Action", "login");
//                    cmd.Parameters.AddWithValue("@name", username);
//                    cmd.Parameters.AddWithValue("@password", encryptedPassword);

//                    try
//                    {
//                        con.Open();
//                        using (SqlDataReader dr = cmd.ExecuteReader())
//                        {
//                            if (dr.Read())
//                            {
//                                string role = dr["usertype"].ToString();
//                                Session["usertype"] = role;
//                                Session["name"] = username;

//                                if (chkRememberMe.Checked)
//                                {
//                                    Response.Cookies["name"].Value = username;
//                                    Response.Cookies["password"].Value = plainPassword;
//                                    Response.Cookies["name"].Expires = DateTime.Now.AddMinutes(30);
//                                    Response.Cookies["password"].Expires = DateTime.Now.AddMinutes(30);
//                                }
//                                else
//                                {
//                                    Response.Cookies["name"].Expires = DateTime.Now.AddMinutes(-30);
//                                    Response.Cookies["password"].Expires = DateTime.Now.AddMinutes(-30);
//                                }

//                                // Redirect based on user role
//                                if (role == "Admin")
//                                    Response.Redirect("Adminpage.aspx");
//                                else if (role == "User")
//                                    Response.Redirect("userpage.aspx");
//                                else
//                                    Response.Write("Unknown user type");
//                            }
//                            else
//                            {
//                                Response.Write("Login failed: Invalid username or password");
//                            }
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        Response.Write("Error: " + ex.Message);
//                    }
//                }
//            }
//        }

//        protected void Submitbtn_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (SqlConnection con = new SqlConnection(cs))
//                {
//                    using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
//                    {
//                        cmd.CommandType = CommandType.StoredProcedure;
//                        cmd.Parameters.AddWithValue("@Action", "GetEmailAndPassword");
//                        cmd.Parameters.AddWithValue("@email", email.Text.Trim());

//                        con.Open();

//                        using (SqlDataReader dr = cmd.ExecuteReader())
//                        {
//                            if (dr.Read())
//                            {
//                                string emailAddr = dr["email"].ToString();
//                                string password = dr["password"].ToString(); // Sending raw password is a security risk.

//                                // Avoid sending plain text passwords; instead, send a password reset link
//                                StringBuilder sb = new StringBuilder();
//                                sb.AppendLine("Hi,");
//                                sb.AppendLine("To reset your password, please click the link below:");
//                                sb.AppendLine("https://yourwebsite.com/reset-password?email=" + emailAddr);

//                                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
//                                {
//                                    client.EnableSsl = true;
//                                    client.UseDefaultCredentials = false;
//                                    client.Credentials = new NetworkCredential("your-email@gmail.com", "your-email-password");

//                                    MailMessage msg = new MailMessage
//                                    {
//                                        From = new MailAddress("your-email@gmail.com"),
//                                        Subject = "Password Reset Request",
//                                        Body = sb.ToString()
//                                    };
//                                    msg.To.Add(emailAddr);

//                                    client.Send(msg);
//                                    labelmsg.Text = "A password reset link has been sent to your email.";
//                                }
//                            }
//                            else
//                            {
//                                labelmsg.Text = "Invalid Email Id";
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                labelmsg.Text = "Error: " + ex.Message;
//            }
//        }
//    }
//}
