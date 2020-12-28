using System;
using System.Security.Cryptography;
using System.Text;

namespace MyInsurance.BusinessLogic.Services
{
    public class CryptoService : IDisposable
    {
        private readonly byte[] keys;
        private readonly MD5CryptoServiceProvider md5;

        public CryptoService(string cryptoKey)
        {
            md5 = new MD5CryptoServiceProvider();
            keys = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
        }

        public string Encrypt(string toBeEncrypted)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(toBeEncrypted);
            string encryptedText;
            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                encryptedText = Convert.ToBase64String(results, 0, results.Length);
            }
            return encryptedText;
        }

        public string Decrypt(string toDecrypt)
        {
            byte[] data = Convert.FromBase64String(toDecrypt);
            string decryptedText;
            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = tripDes.CreateDecryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                decryptedText = UTF8Encoding.UTF8.GetString(results);
            }
            return decryptedText;
        }

        public void Dispose()
        {
            md5.Dispose();
        }
    }
}
