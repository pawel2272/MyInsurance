using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Services.Cryptography
{
    public class Encryption : IDisposable
    {
        MD5 md5 = MD5.Create();
        private string GetMD5HASH(MD5 hash, string input)
        {
            byte[] tab = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < tab.Length; i++)
                sBuilder.Append(tab[i].ToString("x2"));
            return sBuilder.ToString();
        }

        public string Encrypt(string toBeEncrypted)
        {
            return GetMD5HASH(md5, toBeEncrypted);
        }

        public void Dispose()
        {
            md5.Dispose();
        }
    }
}
