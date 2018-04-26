using System.Text.RegularExpressions;

namespace Common {

    /// <summary>
    /// 数据校验
    /// </summary>
    public static class Regular {
        
        #region 正则表达式

        /// <summary>
        /// 是否为数字，包含（整数，小数，负数）
        /// </summary>
        public static string Reg_Num => @"^[\-+]{0,1}[0-9]{1,}[.]{0,1}[0-9]*$";

        public static string Reg_Eng => @"[a-zA-Z]+";

        /// <summary>
        /// 是否为IP地址
        /// </summary>
        public static string Reg_IP => "" +
            "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\." +
            "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." +
            "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." +
            "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)";

        /// <summary>
        /// 匹配城市
        /// </summary>
        public static string Reg_City => @"address:'(.{0,})'";

        /// <summary>
        /// 指定字符之间的数据，要给两个参数
        /// </summary>
        public static string Reg_BetWeen => @".*{0}([\s\S]*) .*{1}";

        public static string Reg_Url => @"(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]";

        #endregion

        #region 校验字符，Check

        /// <summary>
        /// 校验字符是否匹配正则表达式
        /// </summary>
        /// <param name="con">初始字符</param>
        /// <param name="regStr">正则表达式</param>
        /// <param name="def">默认返回值</param>
        /// <returns></returns>
        public static string CheckReg(string con, string regStr, string def)
        {
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
        public static bool CheckReg(string con, string regStr)
        {
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
        public static bool CheckSQL(string contents)
        {
            bool bReturnValue = false;
            if (contents.Length > 0) {
                //convert to lower
                string sLowerStr = contents.ToLower();
                //RegularExpressions
                string sRxStr = "(/sand/s)|(/sand/s)|(/slike/s)|(select/s)|(insert/s)|" +
                    "(delete/s)|(update/s[/s/S].*/sset)|(create/s)|(/stable)|(<[iframe|/iframe|" +
                    "script|/script])|(')|(/sexec)|(/sdeclare)|(/struncate)|(/smaster)|(/sbackup)|(/smid)|(/scount)";
                //Match
                Regex sRx = new Regex(sRxStr);

                return sRx.IsMatch(sLowerStr, 0);
            }
            return bReturnValue;
        }

        #endregion

        #region 替换字符，Rep

        /// <summary>
        /// 替换中西文字符
        /// </summary>
        /// <param name="con">初始字符</param>
        /// <param name="ToWestern">是否替换至西文字符</param>
        /// <returns></returns>
        public static string RepLanguage(string con, bool ToWestern = true)
        {

            string[,] arr =  {
               // {"（", "(" },{ "）", ")" },// 括号
               // {"【", "[" },{ "】", "]" },// 中括号
                { "“", "\""},{ "”", "\"" },// 双引号
                {"‘", "\""},{ "’", "\"" },// 单引号
                {"！", "!"},// 感叹号
                {"？", "?"},// 问号
               // {"；", ";"},// 分号
                {"：", ":"},// 冒号
                {"，", ","},// 逗号
                {"。", "."}// 句号
            };

            int old = ToWestern ? 0 : 1;
            int now = 1 - old;

            for (int i = 0; i < arr.Length / 2; i++) {
                con = con.Replace(arr[i, old], arr[i, now]);
            }

            return con;
        }

        /// <summary>
        /// 替换指定格式字符串
        /// </summary>
        /// <param name="con">原字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="str">新字符</param>
        /// <returns></returns>
        public static string RepStr(string con, string regex, string str)
        {
            if (string.IsNullOrEmpty(con)) return "";
            return Regex.Replace(con, regex, str);
        }

        /// <summary>
        /// 字符串去空格，去回车
        /// </summary>
        /// <param name="con">原字符串</param>
        /// <param name="num">空格长度</param>
        /// <returns></returns>
        public static string RepTrim(string con, int num = 1)
        {
            num++;
            // 去空格
            con = RepStr(con, "[ ]{" + num + ",}", "");
            // 去回车和制表符
            con = RepStr(con, "[\n\r\t]*", "");
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
        public static int? GetNumInMinToMax(ref int? num, int min, int max, int? def = null)
        {
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
        public static string GetSecurityStr(ref string text, bool Ischeck = true, string def = "")
        {
            if (string.IsNullOrEmpty(text))
                return text = def;
            text = text.Trim();
            if (Ischeck)
                if (CheckSQL(text))
                    text = def;
            return text;
        }

        /// <summary>
        /// 返回原字符中，满足正则表达式的字符串数组
        /// </summary>
        /// <param name="text">原字符串</param>
        /// <param name="reg">正则表达式</param>
        /// <returns></returns>
        public static string[] GetRegStrArr(string text, string regStr)
        {
            Regex reg = new Regex(regStr);

            MatchCollection mc = reg.Matches(text);
            if (mc.Count <= 0)
                return new string[1];

            string[] str = new string[mc.Count];
            int index = 0;
            foreach (Match item in mc)
                str[index++] = item.Value;

            return str;
        }

        /// <summary>
        /// 返回原字符串中指定位置的字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="regStr"></param>
        /// <returns></returns>
        public static string GetRegStr(string text, string regStr)
        {
            Regex reg = new Regex(regStr);
            Match mc = reg.Match(text);
            if (mc.Length <= 0) return null;
            return mc.Groups[1].ToString();
        }

        #endregion

    }
}
