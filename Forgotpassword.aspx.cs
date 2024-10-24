using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;

namespace MyPractice
{
    public partial class Forgotpassword : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Submitbtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString; // Stored procedure name
                    using (SqlCommand cmd = new SqlCommand("insertintosptable", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "GetEmailAndPassword");
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string email = dr["email"].ToString();
                                string password = dr["password"].ToString();

                                // Create and send email
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine("Email: " + email);
                                sb.AppendLine("Password: " + password);

                                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                                {
                                    client.EnableSsl = true;
                                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    client.UseDefaultCredentials = false;
                                    client.Credentials = new NetworkCredential("palanshika452@gmail.com", "bmws kvak atfx nkwt");

                                    MailMessage msg = new MailMessage();
                                    msg.To.Add(txtEmail.Text.Trim());
                                    msg.From = new MailAddress("palanshika452@gmail.com");
                                    msg.Subject = "Your password";
                                    msg.Body = sb.ToString();
                                    client.Send(msg);

                                    lblmsg.Text = "Your password has been sent to your email.";
                                }
                            }
                            else
                            {
                                lblmsg.Text = "Invalid Email Id";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Error: " + ex.Message;
            }
        }
    }
}
