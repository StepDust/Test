using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Common.Utils {
    /// <summary>
    /// 字符串工具类
    /// Date：2018-05-28 14:03:27
    /// </summary>
    public static class StringUtils {

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="con">单词个数，默认：-1，即全部</param>
        /// <returns></returns>
        public static string StrToUpper(string str, int con = -1) {
            if (string.IsNullOrEmpty(str))
                return "";

            StringBuilder builder = new StringBuilder();

            string[] Arr = str.Split(' ');

            if (Arr.Length <= con)
                con = -1;

            for (int i = 0; i < Arr.Length; i++) {
                string t = Arr[i];

                if (string.IsNullOrEmpty(t))
                    continue;

                if (con == -1)
                    t = t.Substring(0, 1).ToUpper() + t.Substring(1);
                else {
                    str = str.ToLower();

                    return str.Substring(0, 1).ToUpper() + str.Substring(1);
                }

                builder.Append(t + " ");
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string str, string oldStr, string newStr) {
            if (string.IsNullOrEmpty(oldStr)) {
                return "";
            }
            return str.Replace(oldStr, newStr);
        }

        /// <summary>
        /// 删除指定字符后的字符
        /// </summary>
        /// <param name="str">原字符</param>
        /// <param name="strChar">指定字符</param>
        /// <param name="num">倒数第几个指定字符</param>
        /// <returns></returns>
        public static string DelLastChar(string str, string strChar, int num = 1) {
            if (string.IsNullOrEmpty(str))
                return "";

            int index = str.Length;

            while (num-- > 0 && !string.IsNullOrEmpty(str)) {
                index = str.LastIndexOf(strChar);
                if (0 <= index && index <= str.Length - 1)
                    str = str.Substring(0, str.LastIndexOf(strChar));
            }

            return str;
        }

        /// <summary>
        /// 删除指定字符结尾的指定字符
        /// </summary>
        /// <param name="str">原字符</param>
        /// <param name="c">指定字符</param>
        /// <returns></returns>
        public static string DelLastChar(string str, char c) {
            if (string.IsNullOrEmpty(str))
                return "";
            int index = str.LastIndexOf(c);

            if (index == str.Length - 1)
                return str.Substring(0, index);
            return str;
        }

        /// <summary>
        /// 路径的反斜杠统一
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PathHandle(string path) {
            path = ReplaceStr(path, "/", "\\");
            if (path.IndexOf("\\") == 0)
                path = path.Substring(1);
            path = DelLastChar(path, '\\');
            return path;
        }

        /// <summary>
        /// 在字符串后添加指定长度的指定字符
        /// </summary>
        /// <param name="str">原字符</param>
        /// <param name="s">指定字符串</param>
        /// <param name="count">指定长度</param>
        /// <returns></returns>
        public static string Append(string oldStr, string str, int count) {
            StringBuilder sb = new StringBuilder(oldStr);
            for (int i = 0; i < count; i++)
                sb.Append(str);
            return sb.ToString();
        }

        /// <summary>
        /// 在字符串前添加指定长度的指定字符
        /// </summary>
        /// <param name="str">原字符</param>
        /// <param name="s">指定字符串</param>
        /// <param name="count">指定长度</param>
        /// <returns></returns>
        public static string AppendTo(string oldStr, string str, int count) {
            StringBuilder sb = new StringBuilder(oldStr);
            for (int i = 0; i < count; i++)
                sb.Insert(0, str);
            return sb.ToString();
        }

        /// <summary>
        /// 计算指定字符串中，指定字符出现次数
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="s">要计数的字符</param>
        /// <returns></returns>
        public static int GetStrCount(string str, string s) {
            int count = 0;
            for (int i = 0; i < str.Length - s.Length;) {
                if (s == str.Substring(i, s.Length)) {
                    count++;
                    i += s.Length;
                }
                else
                    i++;
            }
            return count;
        }

        #region Json

        /// <summary>
        ///  对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertJson(object obj) {
            return obj == null ? null : obj is string ? obj.ToString() : JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Json转对象
        /// </summary>
        /// <param name="json"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertObject<T>(string json) {
            return json == null ? default : JsonConvert.DeserializeObject<T>(json);
        }

        #endregion

        #region 字符串编码

        /// <summary>    
        /// 字符串转为UniCode码字符串    
        /// </summary>    
        /// <param name="s"></param>    
        /// <returns></returns>    
        public static string StringToUnicode(string s) {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++) {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("\\u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        /// <summary>    
        /// Unicode字符串转为正常字符串    
        /// </summary>    
        /// <param name="srcText"></param>    
        /// <returns></returns>    
        public static string UnicodeToString(string srcText) {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;
            for (int i = 0; i <= len - 1; i++) {
                string str = "";
                str = src.Substring(0, 6).Substring(2);
                src = src.Substring(6);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        }

        /// <summary>
        /// 转为UTF-8编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToUtf8(string str) {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            string res = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            return res;
        }

        #endregion

    }
}