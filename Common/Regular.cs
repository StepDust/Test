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

        public static string Reg_ { get { return @""; } }


        #endregion

        #region 校验字符

        /// <summary>
        /// 校验字符是否匹配正则表达式
        /// </summary>
        /// <param name="con">待校验字符</param>
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
        /// <param name="con">待校验字符</param>
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
        
    }
}
