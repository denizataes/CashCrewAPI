using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Services.Util
{
    public static class Encryption
    {
        private static string DecryptKey(string encrypted, string key)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var keyhash = hashmd5.ComputeHash(Encoding.GetEncoding(1254).GetBytes(key));
            var des = new TripleDESCryptoServiceProvider { Key = keyhash, Mode = CipherMode.ECB };

            var buff = Convert.FromBase64String(encrypted);
            var decrypted = Encoding.GetEncoding(1254).GetString(des.CreateDecryptor().TransformFinalBlock(buff, 0, buff.Length));

            return decrypted;
        }
        private static string EncryptString(string original)
        {
            original = original.ToLower();
            var x = new MD5CryptoServiceProvider();
            // Özel kod sayfasını kaydet
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // Şimdi özel kod sayfanızı kullanabilirsiniz
            var bs = Encoding.GetEncoding(1254).GetBytes(original);

            bs = x.ComputeHash(bs);
            var s = new StringBuilder();
            foreach (var b in bs) { s.Append(b.ToString("x2").ToLower()); }
            return s.ToString();
        }
        private static string GenerateKey(string original, bool reverse)
        {
            var result = "";
            var reverseOriginal = "";
            if (!reverse)
            {
                for (var i = (original.Length - 1); i > -1; i--) reverseOriginal += original[i].ToString(CultureInfo.InvariantCulture);
                for (var i = 0; i < original.Length; i++) result += original[i] + reverseOriginal[i].ToString(CultureInfo.InvariantCulture);

                result = "!" + result + "!";
            }
            else
            {
                original = original.Replace("!", "");
                for (var i = 0; i < original.Length; i = i + 2) result += original[i];
            }

            return result;
        }

        public static string Decrypt(string encrypted, string userId)
        {
            const string key = "r4WC7pxNDfQsaFrb0F00YJqlOJezFhjZ014jhgT+A1mxahEXDTDHYwaToCPr/bs/c7flyZIkK1MkelcpAiwfT8s";

            var userPwdGenerated = encrypted;
            userPwdGenerated = DecryptKey(userPwdGenerated, key);

            var nickNameHash = EncryptString(userId.ToString(CultureInfo.InvariantCulture));

            userPwdGenerated = userPwdGenerated.Replace(nickNameHash, "");
            return GenerateKey(userPwdGenerated, true);
        }

        private static string Encrypt(string original, string key)
        {
            //System.Security.Cryptography.RSACryptoServiceProvider
            TripleDESCryptoServiceProvider des;
            //RSACryptoServiceProvider rsa;
            MD5CryptoServiceProvider hashmd5;

            byte[] keyhash, buff;
            string encrypted;

            hashmd5 = new MD5CryptoServiceProvider();
            keyhash = hashmd5.ComputeHash(Encoding.GetEncoding(1254).GetBytes(key));
            //keyhash = ASCIIEncoding.ASCII.GetBytes(key);
            hashmd5 = null;
            des = new TripleDESCryptoServiceProvider();
            //rsa = new RSACryptoServiceProvider();
            //rsa.Key = keyhash;
            des.Key = keyhash;
            des.Mode = CipherMode.ECB;

            buff = Encoding.GetEncoding(1254).GetBytes(original);
            encrypted = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buff, 0, buff.Length));

            return encrypted;
        }

        public static string EncryptPassword(string password, string userkey)
        {
            string key = "r4WC7pxNDfQsaFrb0F00YJqlOJezFhjZ014jhgT+A1mxahEXDTDHYwaToCPr/bs/c7flyZIkK1MkelcpAiwfT8s";

            string generatedStr = GenerateKey(password, false);

            string nickNameHash = EncryptString(userkey.ToString());
            generatedStr = nickNameHash + generatedStr + nickNameHash;

            return Encrypt(generatedStr, key);
        }
    }
}