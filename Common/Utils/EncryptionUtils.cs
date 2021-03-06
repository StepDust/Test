﻿using System;
using System.Text;
using System.Security.Cryptography;

namespace Common.Utils {
    /// <summary>
    /// 字符串加密解密
    /// </summary>
    public sealed class EncryptionUtils
    {

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="text">待加密字符串</param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Encrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
                return "";

            string md5Key = EncryptMD5(key).Substring(8, 8);

            text = EncryptBase64(text);

            string res = text.Substring(0, text.Length / 2) + md5Key + text.Substring(text.Length / 2);

            return EncryptBase64(res);
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text">待解密字符串</param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
                return "";

            string md5Key = EncryptMD5(key).Substring(8, 8);

            text = DecryptBase64(text);

            if (text.Substring((text.Length - 8) / 2, 8) != md5Key)
                return text;

            text = text.Substring(0, (text.Length - 8) / 2) + text.Substring((text.Length - 8) / 2 + 8);

            text = DecryptBase64(text);

            return text;
        }

        #region 正规的加密解密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string EncryptMD5(string text)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            return pwd;
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string EncryptBase64(string text)
        {
            string encode = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = text;
            }
            return encode;
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="text">待解密字符串</param>
        public static string DecryptBase64(string text)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(text);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = text;
            }
            return decode;
        }

        #endregion

        #region JS相关

        /// <summary>
        /// 加密，类似js的Escape()方法
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Escape(string text)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(text);

            for (int i = 0; i < byteArr.Length; i += 2)
            {
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
        public static string UnEscape(string text)
        {
            string str = text.Remove(0, 2);//删除最前面两个”%u”
            string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//以子字符串”%u”分隔
            byte[] byteArr = new byte[strArr.Length * 2];
            for (int i = 0, j = 0; i < strArr.Length; i++, j += 2)
            {
                byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16);  //把十六进制形式的字串符串转换为二进制字节
                byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
            }
            str = System.Text.Encoding.Unicode.GetString(byteArr);　//把字节转为unicode编码
            return str;
        }

        #endregion

    }
}
