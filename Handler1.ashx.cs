using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPractice
{

    public class Handler1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int id = 0;
            int.TryParse(context.Request.QueryString["id"], out id);
            if (id > 0)
            {
                string constr = ConfigurationManager.ConnectionStrings["newtable"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT image FROM sptable WHERE id =  @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        byte[] imgdata = sdr["image"] as byte[];
                        if(imgdata != null)
                        {
                            context.Response.ContentType = "image/jpg";
                            context.Response.BinaryWrite(imgdata);
                            context.Response.End();
                        }

                    }
                }

            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }

}