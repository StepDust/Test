using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Common {
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utils {

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
        /// 删除指定字符后的字符
        /// </summary>
        /// <param name="str">原字符</param>
        /// <param name="strchar">指定字符</param>
        /// <param name="num">倒数第几个指定字符</param>
        /// <returns></returns>
        public static string DelLastChar(string str, string strchar, int num = 1) {
            if (string.IsNullOrEmpty(str))
                return "";

            int index = str.Length;

            while (num-- > 0 && !string.IsNullOrEmpty(str)) {
                index = str.LastIndexOf(strchar);
                if (0 <= index && index <= str.Length - 1)
                    str = str.Substring(0, str.LastIndexOf(strchar));
            }

            return str;
        }

        /// <summary>
        /// 拼接键值对
        /// </summary>
        /// <param name="dic">存放键值对的字典</param>
        /// <param name="format">拼接格式，默认为Json格式</param>
        /// <param name="Split">分割符号</param>
        /// <returns></returns>
        public static string MosaicKeyVal(Dictionary<object, object> dic, string format = " '{0}':'{1}' ", string Split = ",") {

            StringBuilder str = new StringBuilder();

            // 是否包含{0}和{1}
            if (format.IndexOf("{0}") < 0 || format.IndexOf("{1}") < 0)
                return "";
            format = format.Trim();
            foreach (string item in dic.Keys) {
                if (string.IsNullOrEmpty(item + ""))
                    continue;
                if (string.IsNullOrEmpty(dic[item] + ""))
                    continue;
                // 添加逗号
                if (str.Length > 0)
                    str.Append(Split);
                // 添加键值对
                str.Append(string.Format(format, item.ToString(), dic[item].ToString()));
            }

            return str.ToString(); ;
        }

        #endregion

        #region URL处理

        /// <summary>
        /// 返回当前请求的Url，包含参数
        /// </summary>
        /// <returns></returns>
        public static string GetUrlInfo() {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 返回当前请求的Url，包含Post参数
        /// </summary>
        /// <returns></returns>
        public static string GetPostUrlInfo() {
            string url = GetUrlInfo();
            if (url.Contains('?'))
                url += "&";
            else
                url += "?";

            Dictionary<object, object> dic = new Dictionary<object, object>();
            // 遍历获取post参数和值
            foreach (string item in HttpContext.Current.Request.Form)
                dic.Add(item, HttpContext.Current.Request[item]);

            return url + MosaicKeyVal(dic, "{0}={1}", "&");
        }

        /// <summary>
        /// 返回当前请求的Url，不包含参数
        /// </summary>
        /// <returns></returns>
        public static string GetUrl() {
            string url = GetUrlInfo();
            int index = url.IndexOf("?");
            if (index > 0)
                return url.Substring(0, index);
            return url;
        }

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

        #region 创建下拉框

        /// <summary>
        /// 创建下拉框
        /// </summary>
        /// <param name="dic">数据源，(Val,Text)</param>
        /// <param name="selVal">默认选中值</param>
        /// <param name="isAll">是否有“全部”</param>
        /// <param name="allVal">“全部”的值</param>
        /// <param name="allStr">“全部”文本</param>
        /// <returns></returns>
        public static List<SelectListItem> BingDrop<k, v>(Dictionary<k, v> dic, k selVal, bool isAll = true, string allVal = "-1", string allStr = "全  部") {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (k key in dic.Keys) {
                list.Add(CreatSelectListItem(dic[key] + "", key + "", key + "" == selVal + ""));
            }

            if (isAll)
                list.Insert(0, CreatSelectListItem(allStr, allVal, allVal == selVal + ""));

            return list;
        }

        /// <summary>
        /// 创建下拉框，默认以索引作为值
        /// </summary>
        /// <param name="Arr">数组，作为数据源</param>
        /// <param name="SelVal">默认选中的值</param>
        /// <param name="StarVal">初始值，默认为索引的开始：0</param>
        /// <returns></returns>
        public static List<SelectListItem> BingDrop(object[] Arr, int SelVal, int StarVal = 0) {
            List<SelectListItem> list = new List<SelectListItem>();

            for (int i = 0; i < Arr.Length; i++) {
                int val = StarVal + i;
                list.Add(CreatSelectListItem(Arr[i].ToString(), val.ToString(), val == SelVal));
            }

            return list;
        }

        /// <summary>
        /// 创建下拉项
        /// </summary>
        /// <param name="Text">文本</param>
        /// <param name="Val">值</param>
        /// <param name="IsSel">是否选中</param>
        /// <returns></returns>
        private static SelectListItem CreatSelectListItem(string Text, string Val, bool IsSel) {
            SelectListItem item = new SelectListItem() {
                Text = Text,
                Value = Val,
                Selected = IsSel
            };
            return item;
        }

        #endregion

    }
}