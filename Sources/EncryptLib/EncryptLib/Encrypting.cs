using System.Security.Cryptography;
using System.Text;

namespace EncryptLib
{
    public class Encrypting
    {
        private const string _key = "somesecretkey123456";

        //Сделать шифрацию
        public async static Task<byte[]> Encrypt(string clearText, string password, string key = _key)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using Aes encryptor = Aes.Create();
            Rfc2898DeriveBytes rfcKey = new(password, Encoding.ASCII.GetBytes(key), 10, HashAlgorithmName.SHA256);
            encryptor.Key = rfcKey.GetBytes(32);
            encryptor.IV = rfcKey.GetBytes(16);

            try
            {
                using MemoryStream ms = new();
                using CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
                await cs.WriteAsync(clearBytes);
                cs.Close();

                clearText = Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return [0];
                //return string.Empty;
            }

            return Encoding.ASCII.GetBytes(clearText);
            //return clearText;
        }

        public async static Task<string> Decrypt(byte[] cipherText, string password, string key = _key)
        {
            var decoded = Encoding.ASCII.GetString(cipherText);
            //var decoded = cipherText;

            decoded = decoded.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(decoded);

            using Aes encryptor = Aes.Create();
            Rfc2898DeriveBytes rfcKey = new(password, Encoding.ASCII.GetBytes(key), 10, HashAlgorithmName.SHA256);
            encryptor.Key = rfcKey.GetBytes(32);
            encryptor.IV = rfcKey.GetBytes(16);

            try
            {
                using MemoryStream ms = new();
                using CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write);
                await cs.WriteAsync(cipherBytes);
                cs.Close();

                decoded = Encoding.Unicode.GetString(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }

            return decoded;
        }
    }
}
