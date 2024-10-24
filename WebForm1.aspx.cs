using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyPractice
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Secret Key (must be 16, 24, or 32 bytes long)
        private static readonly string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#"; // 24 characters long

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // AES Encryption Method
        private string Encrypt(string plainText)
        {
            // Secret Bytes.
            byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);

            // Plain Text Bytes.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Encrypt with AES Algorithm using Secret Key.
            using (Aes aes = Aes.Create())
            {
                aes.Key = secretBytes;
                aes.Mode = CipherMode.ECB; // Using ECB mode as per your request (but CBC is more secure)
                aes.Padding = PaddingMode.PKCS7; // Padding for block size

                byte[] encryptedBytes;
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                }

                // Return the encrypted data as Base64 encoded string.
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // Save Button Click Event
        protected void btnsave_Click1(object sender, EventArgs e)
        {
            // Get plain text password from textbox
            string plainPassword = txtpassword.Text;

            // Encrypt the password using the AES encryption method
            string encryptedPassword = Encrypt(plainPassword);

            // Display encrypted password in Label (not in TextBox)
            encryptedpassword.Text = "Encrypted Password: " + encryptedPassword;

           
        }
    }
}