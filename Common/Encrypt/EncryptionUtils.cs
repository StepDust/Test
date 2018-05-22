using System;
using System.Text;

namespace Common.Encrypt {
    /// <summary>
    /// 字符串加密解密
    /// </summary>
    public static class EncryptionUtils {

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="text">待加密字符串</param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Encrypt(string text, string key) {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
                return "";

            string md5Key = MD5Encrypt.Encrypt(key).Substring(8, 8);

            text = Base64Encrypt.Encrypt(text);

            string res = text.Substring(0, text.Length / 2) + md5Key + text.Substring(text.Length / 2);

            return Base64Encrypt.Encrypt(res);
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text">待解密字符串</param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string key) {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
                return "";

            string md5Key = MD5Encrypt.Encrypt(key).Substring(8, 8);

            text = Base64Encrypt.Decrypt(text);

            if (text.Substring((text.Length - 8) / 2, 8) != md5Key)
                return text;

            text = text.Substring(0, (text.Length - 8) / 2) + text.Substring((text.Length - 8) / 2 + 8);

            return Base64Encrypt.Decrypt(text);
        }

        #region JS相关

        /// <summary>
        /// 加密，类似js的Escape()方法
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Escape(string text) {
            StringBuilder sb = new StringBuilder();
            byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(text);

            for (int i = 0; i < byteArr.Length; i += 2) {
                sb.Append("%u");
                sb.Append(byteArr[i + 1].ToString("X2"));//把字节转换为十六进制的字符串表现形式
                sb.Append(byteArr[i].ToString("X2"));
            }
            return sb.ToString();

        }

        /// <summary>
        /// 解密，类似js的UnEscape()方法
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnEscape(string text) {
            string str = text.Remove(0, 2);//删除最前面两个”%u”
            string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//以子字符串”%u”分隔
            byte[] byteArr = new byte[strArr.Length * 2];
            for (int i = 0, j = 0; i < strArr.Length; i++, j += 2) {
                byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16);  //把十六进制形式的字串符串转换为二进制字节
                byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
            }
            str = System.Text.Encoding.Unicode.GetString(byteArr);　//把字节转为unicode编码
            return str;
        }

        #endregion

    }
}
