using System;
using System.Security.Cryptography;
using System.Text;

namespace MyInsurance.BusinessLogic.Services
{
    /// <summary>
    /// usługa szyfrująca dane
    /// </summary>
    public class CryptoService : IDisposable
    {
        /// <summary>
        /// klucz szyfrujący
        /// </summary>
        private readonly byte[] keys;
        /// <summary>
        /// dostawca szyfrowania
        /// </summary>
        private readonly MD5CryptoServiceProvider md5;

        /// <summary>
        /// konstruktor inicjalizujący dostawcę szyfrowania i klucz sztfrujący
        /// </summary>
        /// <param name="cryptoKey">klucz szyfrujący podany jako string</param>
        public CryptoService(string cryptoKey)
        {
            md5 = new MD5CryptoServiceProvider();
            keys = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
        }

        /// <summary>
        /// metoda szyfrująca tekst podany jako argument
        /// </summary>
        /// <param name="toBeEncrypted">tekst do zaszyfrowania</param>
        /// <returns>zaszyfrowany tekst</returns>
        public string Encrypt(string toBeEncrypted)
        {
            try
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
            catch (Exception ex)
            {
                //LOGGER
            }
            return string.Empty;
        }

        /// <summary>
        /// metoda odszyfrowująca tekst podany jako argument
        /// </summary>
        /// <param name="toDecrypt">zaszyfrowany tekst</param>
        /// <returns>odszyfrowany tekst</returns>
        public string Decrypt(string toDecrypt)
        {
            try
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
            catch (Exception ex)
            {
                //LOGGER
            }
            return string.Empty;
        }

        /// <summary>
        /// implementacja interfejsu IDisposable
        /// </summary>
        public void Dispose()
        {
            md5.Dispose();
        }
    }
}
