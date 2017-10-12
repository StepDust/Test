using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Common {
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils {

        #region 字符串处理

        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string originalStr, string oldStr, string newStr) {
            if (string.IsNullOrEmpty(oldStr)) {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar) {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1) {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }

        #endregion

        #region URL处理

        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str) {
            if (string.IsNullOrEmpty(str)) {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str) {
            if (string.IsNullOrEmpty(str)) {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string _url, string _keys, params string[] _values) {
            StringBuilder urlParams = new StringBuilder();
            try {
                string[] keyArr = _keys.Split(new char[] { '&' });
                for (int i = 0; i < keyArr.Length; i++) {
                    if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "") {
                        _values[i] = UrlEncode(_values[i]);
                        urlParams.Append(string.Format(keyArr[i], _values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
                    urlParams.Insert(0, "?");
            }
            catch {
                return _url;
            }
            return _url + DelLastChar(urlParams.ToString(), "&");
        }

        #endregion

        #region 分页处理

        /// <summary>
        /// 返回分页页码
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="linkUrl">链接地址，__id__代表页码</param>
        /// <param name="centSize">中间页码数量</param>
        /// <returns></returns>
        public static string OutPageList(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize, string typeName = "") {

            string pageId = "__id__";
            if (string.IsNullOrEmpty(linkUrl))
                linkUrl = "";
            // 拼接PageIndex，PageSize

            // 判断URL是否有参数
            int index = linkUrl.LastIndexOf("?");
            // 若没有参数
            if (index == -1)
                linkUrl += "?";
            else
                linkUrl += "&";

            linkUrl += "pageSize=" + pageSize;
            linkUrl += "&pageindex=" + pageId;
            
            //计算页数
            if (totalCount < 1 || pageSize < 1) {
                return OutPagingHtml("", linkUrl, pageSize, typeName);
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1) {
                return OutPagingHtml("", linkUrl, pageSize, typeName);
            }
            if (totalCount % pageSize > 0) {
                pageCount += 1;
            }
            if (pageCount <= 1) {
                return OutPagingHtml("", linkUrl, pageSize, typeName);
            }

            // 开始拼接页码
            StringBuilder pageStr = new StringBuilder();


            // 拼接页码
            string firstBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«上一页</a>";
            string lastBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">下一页»</a>";

            string firstStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

            if (pageIndex <= 1) {
                firstBtn = "<span class=\"disabled\">«上一页</span>";
            }
            if (pageIndex >= pageCount) {
                lastBtn = "<span class=\"disabled\">下一页»</span>";
            }
            if (pageIndex == 1) {
                firstStr = "<span class=\"current\">1</span>";
            }
            if (pageIndex == pageCount) {
                lastStr = "<span class=\"current\">" + pageCount.ToString() + "</span>";
            }
            // 中间开始的页码
            int firstNum = pageIndex - (centSize / 2);
            if (pageIndex < centSize)
                firstNum = 2;
            // 中间结束的页码
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1);
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            pageStr.Append("<span>共" + totalCount + "记录</span>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize) {
                pageStr.Append("<span>...</span>\n");
            }
            for (int i = firstNum; i <= lastNum; i++) {
                if (i == pageIndex) {
                    pageStr.Append("<span class=\"current\">" + i + "</span>");
                }
                else {
                    pageStr.Append("<a href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2))) {
                pageStr.Append("<span>...</span>");
            }
            pageStr.Append(lastStr + lastBtn);

            return OutPagingHtml(pageStr.ToString(), linkUrl, pageSize, typeName);
        }

        /// <summary>
        /// 返回分页HTML
        /// </summary>
        /// <param name="PageList"></param>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        private static string OutPagingHtml(string PageList, string linkUrl, int pageSize, string typeName) {

            string repStr = "&pageindex";

            // 去掉pageIndex参数
            int index = linkUrl.LastIndexOf(repStr);

            if (index != -1)
                linkUrl = linkUrl.Substring(0, index);

            // 去掉pageSize的值
            repStr = "pageSize=";
            index = linkUrl.LastIndexOf(repStr);
            if (index != -1)
                linkUrl = linkUrl.Substring(0, index + repStr.Length);
            
            // Html部分
            StringBuilder pageStr = new StringBuilder();
            pageStr.Append("<br />");
            pageStr.Append("<div class=\"pagelist\">");
            pageStr.Append("    <div class=\"l-btns\">");
            pageStr.Append("        <span>显示</span>");
            pageStr.Append("        <input class=\"pagenum\" id=\"changePageSize\" value=\"" + pageSize + "\" />");
            pageStr.Append("        <span>条/页</span>");
            pageStr.Append("    </div>");
            pageStr.Append("    <div id=\"PageContent\" class=\"default\">" + PageList + "</div>");
            pageStr.Append("</div>");

            // js部分
            pageStr.Append("<script>");
            // 添加页数修改方法
            pageStr.Append(" $('#changePageSize').bind('change', function () {");
            pageStr.Append(" var val= $(this).val(); ");
            // 数据验证
            //pageStr.Append(" if(val<=0||val>100) ");
            //pageStr.Append("  val= 10; ");
            pageStr.Append("val=val<1?10:val;");
            pageStr.Append("val=val>100?100:val;");

            pageStr.Append("window.location.href = '" + linkUrl + "'+val; ");
            pageStr.Append("    });");

            // 添加下拉替换方法
            if (!string.IsNullOrEmpty(typeName)) {
                // 去掉所有参数，保留'?'
                linkUrl = linkUrl.Substring(0, linkUrl.LastIndexOf("?") + 1);
                // 拼接下拉类型参数
                linkUrl += "&" + typeName + "=";

                pageStr.Append(" $('select[name=" + typeName + "]').bind('change', function () {");
                pageStr.Append(" var val= $(this).val(); ");
                pageStr.Append("window.location.href = '" + linkUrl + "'+val; ");
                pageStr.Append("    });");

            }

            pageStr.Append("</script>");

            return pageStr.ToString();
        }

        #endregion


    }
}