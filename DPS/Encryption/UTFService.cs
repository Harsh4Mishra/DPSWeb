using System;
using System.Text;

namespace DPS.Encryption
{
    public class UTFService
    {
        public string Encrypt(string text)
        {
            char key = 'K';
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                // XOR each character with the key
                result.Append((char)(c ^ key));
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(result.ToString()));
        }

        public string Decrypt(string text)
        {
            char key = 'K';
            // Decode the Base64 string first
            byte[] bytes = Convert.FromBase64String(text);
            string decodedText = Encoding.UTF8.GetString(bytes);

            StringBuilder result = new StringBuilder();
            foreach (char c in decodedText)
            {
                // XOR each character with the key to decrypt
                result.Append((char)(c ^ key));
            }
            return result.ToString();
        }
    }
}