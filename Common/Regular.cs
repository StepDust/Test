using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common {

    /// <summary>
    /// 正则表达式
    /// </summary>
    public static class Regular {

        #region 正则表达式

        public static string Reg_PX { get { return @"^[0-9]{1,}px$"; } }


        #endregion

        #region 校验字符

        /// <summary>
        /// 校验字符是否匹配正则表达式
        /// </summary>
        /// <param name="con">初始字符</param>
        /// <param name="regStr">正则表达式</param>
        /// <param name="def">默认返回值</param>
        /// <returns></returns>
        public static string CheckReg(string con, string regStr, string def) {
            if (CheckReg(con, regStr))
                return con;

            if (string.IsNullOrEmpty(def))
                def = con;
            return def;
        }

        /// <summary>
        /// 校验字符是否匹配正则表达式
        /// </summary>
        /// <param name="con">初始字符</param>
        /// <param name="regStr">正则表达式</param>
        /// <returns></returns>
        public static bool CheckReg(string con, string regStr) {
            if (string.IsNullOrEmpty(con))
                con = "";
            if (string.IsNullOrEmpty(regStr))
                return false;
            return Regex.IsMatch(con, regStr);
        }

        #endregion

        #region 替换字符

        /// <summary>
        /// 替换中西文字符
        /// </summary>
        /// <param name="con">初始字符</param>
        /// <param name="ToWestern">是否替换至西文字符</param>
        /// <returns></returns>
        public static string ReplaceChar(string con, bool ToWestern = true) {

            char[,] arr =  {
                { '（', '(' },{ '）', ')' },// 括号
                {'【', '[' },{ '】', ']' },// 中括号
                { '“', '"'},{ '”', '"' },// 双引号
                {'‘', '\''},{ '’', '\'' },// 单引号
                {'！', '!'},// 感叹号
                {'？', '?'},// 问号
                {'；', ';'},// 分号
                {'：', ':'},// 冒号
                {'，', ','},// 逗号
                {'。', '.'}// 句号
            };

            int old = ToWestern ? 0 : 1;
            int now = 1 - old;

            for (int i = 0; i < arr.Length; i++) {
                con = con.Remove(arr[i, old], arr[1, now]);
            }

            return con;
        }
         
        #endregion
    }
}
