using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common {

    /// <summary>
    /// 数据校验
    /// </summary>
    public static class DataCheck {

        #region 正则表达式



        #endregion

        #region 校验字符，Check

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

        /// <summary>
        /// 检测是否含有危险字符（防止Sql注入）
        /// </summary>
        /// <param name="contents">预检测的内容</param>
        /// <returns>返回True或false</returns>
        public static bool CheckSQL(string contents) {
            bool bReturnValue = false;
            if (contents.Length > 0) {
                //convert to lower
                string sLowerStr = contents.ToLower();
                //RegularExpressions
                string sRxStr = "(/sand/s)|(/sand/s)|(/slike/s)|(select/s)|(insert/s)|" +
                    "(delete/s)|(update/s[/s/S].*/sset)|(create/s)|(/stable)|(<[iframe|/iframe|" +
                    "script|/script])|(')|(/sexec)|(/sdeclare)|(/struncate)|(/smaster)|(/sbackup)|(/smid)|(/scount)";
                //Match
                bool bIsMatch = false;
                Regex sRx = new Regex(sRxStr);

                return sRx.IsMatch(sLowerStr, 0);
            }
            return bReturnValue;
        }

        #endregion

        #region 替换字符，Get...To

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

        #region 数据校验，Get

        /// <summary>
        /// 保证num的值范围为，[min,max]
        /// </summary>
        /// <param name="num">原始数据</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="def">默认值(不填时，默认值为最小值)</param>
        /// <returns></returns>
        public static int? GetNumInMinToMax(ref int? num, int min, int max, int? def = null) {
            // 若def没有值，将最小值给它
            if (!def.HasValue)
                def = min;
            // 若num没有值，将默认值给它
            if (!num.HasValue)
                num = def;
            // 若num的值小于最小值，或大于最大值，将默认值给它
            else if (num < min || max < num)
                num = def;

            return num;
        }

        /// <summary>
        /// 将字符串去掉空格，并进行敏感字符检测
        /// </summary>
        /// <param name="text">原字符串</param>
        /// <param name="Ischeck">是否开启敏感字符校验</param>
        /// <param name="def">默认的值，字符串为空时，赋此值</param>
        /// <returns></returns>
        public static string GetSecurityStr(ref string text, bool Ischeck = true, string def = "") {
            if (string.IsNullOrEmpty(text))
                return text = def;
            text = text.Trim();
            if (Ischeck)
                if (CheckSQL(text))
                    text = def;
            return text;
        }
        
        #endregion

    }
}
