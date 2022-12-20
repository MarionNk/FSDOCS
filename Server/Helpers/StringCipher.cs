using System.Security.Cryptography;
using System.Text;

namespace FSDOCS.Server.Helpers
{
    public static class StringCipher
    {
        public static string Encrypt(string plainText)
        {
            string EncryptionKey = "é'fç(èà)fb,kls&x^$mrj";
            var transformPlainText = "clesecurisee" + plainText + DateTime.Now.ToString() + "securedkey";
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(transformPlainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cs.Close();
                    }
                    transformPlainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return transformPlainText;

        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "é'fç(èà)fb,kls&x^$mrj";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string HashPwd(string plainText)
        {
            string EncryptionKey = "é'fç(èà)fb,kls&x^$mrj";
            var transformPlainText = plainText;
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(transformPlainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cs.Close();
                    }
                    transformPlainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return transformPlainText;

        }
    }

}
