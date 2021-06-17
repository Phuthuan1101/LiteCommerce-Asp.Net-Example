using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// Các hàm dùng cho mã hóa/giải mã
    /// </summary>
    public static class CryptHelper
    {
        /// <summary>
        /// Mã hóa MD5 chuỗi text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Md5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(text);
            Byte[] encodeBytes = md5.ComputeHash(originalBytes);
            text = BitConverter.ToString(encodeBytes).Replace("-", "");
            var result = text.ToLower();
            return result;
        }
    }
}