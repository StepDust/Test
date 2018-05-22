using System;
using System.Text;

namespace Common.Encrypt {
    public static class Base64Encrypt {

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt(string text) {
            string encode = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            try {
                encode = Convert.ToBase64String(bytes);
            }
            catch {
                encode = text;
            }
            return encode;
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="text">待解密字符串</param>
        public static string Decrypt(string text) {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(text);
            try {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch {
                decode = text;
            }
            return decode;
        }

    }
}
