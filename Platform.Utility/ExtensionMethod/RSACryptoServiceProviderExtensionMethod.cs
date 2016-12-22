using System.Security.Cryptography;
using System.Text;

namespace SHWDTech.Platform.Utility.ExtensionMethod
{
    public static class RsaCryptoServiceProviderExtensionMethod
    {
        public static string DecryptString(this RSACryptoServiceProvider rsaCryptoServiceProvider, string sourceString)
        {
            var byteEn = rsaCryptoServiceProvider.Encrypt(Encoding.ASCII.GetBytes("a"), false);
            var sBytes = sourceString.Split(',');

            for (var j = 0; j < sBytes.Length; j++)
            {
                if (sBytes[j] != "")
                {
                    byteEn[j] = byte.Parse(sBytes[j]);
                }
            }
            var plaintbytes = rsaCryptoServiceProvider.Decrypt(byteEn, false);
            return Encoding.ASCII.GetString(plaintbytes);
        }

        public static string EncryptString(this RSACryptoServiceProvider rsaCryptoServiceProvider, string sourceString)
        {
            var plaintext = sourceString;
            var cipherbytes = rsaCryptoServiceProvider.Encrypt(Encoding.ASCII.GetBytes(plaintext), false);

            var sbString = new StringBuilder();
            foreach (var t in cipherbytes)
            {
                sbString.Append(t + ",");
            }

            return sbString.ToString();
        }
    }
}
