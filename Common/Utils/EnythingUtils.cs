using System.Collections.Generic;
using System.Management;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Reflection;

namespace Common.Utils {
    /// <summary>
    /// 工具类
    /// </summary>
    public static class EnythingUtils {


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

            return url + string.Join("&", dic.Select(c => $"{c.Key}={c.Value}"));
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
        /// 返回跳转路径
        /// </summary>
        /// <param name="url">待跳转路径</param>
        /// <returns></returns>
        public static string GetActionUrl(string url) {
            if (string.IsNullOrEmpty(url)) return "";

            int now = StringUtils.GetStrCount(url, "/");
            if (now == 0)
                now++;

            url = StringUtils.DelLastChar(GetUrl(), "/", now) + "/" + url;

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
            return _url + StringUtils.DelLastChar(urlParams.ToString(), '&');
        }

        #endregion

        #region 客户端信息

        /// <summary>
        /// 获取内网IP
        /// </summary>
        /// <returns></returns>
        public static string GetIPv4() {
            string userIP = "未获取用户IP";

            try {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                string CustomerIP = "";

                //CDN加速后取到的IP   
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP)) {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch { }

            return userIP;
        }

        /// <summary>
        /// 获取外网IP，以及城市和运营商
        /// </summary>
        /// <returns></returns>
        public static string GetExternalInfo() {
            /*
             * 不错的外网地址获取网址
             * http://www.3322.org/dyndns/getip
             * http://ip.chinaz.com/getip.aspx
             */
            string Html = GetUrlHtml("http://ip.chinaz.com/getip.aspx");
            string[] str = Regular.GetRegStrArr(Html, Regular.Reg_IP);
            string city = Regular.GetRegStr(Html, Regular.Reg_City);

            return str[0] + " " + city;
        }

        /// <summary>
        /// 返回主机名称
        /// </summary>
        /// <returns></returns>
        public static string GetHostName() {
            // 根据目标ip地址的获取ip对象
            IPAddress ip = IPAddress.Parse(HttpContext.Current.Request.UserHostAddress);
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            IPHostEntry ihe = Dns.GetHostEntry(ip);    //根据ip对象创建主机对象
            return ihe.HostName;
        }

        /// <summary>  
        /// 获取本机的Mac
        /// </summary>  
        /// <returns></returns>  
        public static string GetMac() {
            string madAddr = null;
            try {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc2 = mc.GetInstances();
                foreach (ManagementObject mo in moc2) {
                    if (Convert.ToBoolean(mo["IPEnabled"]) == true) {
                        madAddr = mo["MacAddress"].ToString();
                        madAddr = madAddr.Replace(':', '-');
                    }
                    mo.Dispose();
                }
                if (madAddr == null) {
                    return "unknown";
                }
                else {
                    return madAddr;
                }
            }
            catch (Exception) {
                return "unknown";
            }
        }

        /// <summary>  
        /// 获取操作系统名称  
        /// </summary>  
        /// <returns>操作系统名称</returns>  
        public static string GetSystemName() {
            try {
                string strSystemName = string.Empty;
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT PartComponent FROM Win32_SystemOperatingSystem");
                foreach (ManagementObject mo in mos.Get()) {
                    strSystemName = mo["PartComponent"].ToString();
                }
                mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption FROM Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.Get()) {
                    strSystemName = mo["Caption"].ToString();
                }
                return strSystemName;
            }
            catch {
                return "unknown";
            }
        }

        /// <summary>  
        /// 获取操作系统类型  
        /// </summary>  
        /// <returns>操作系统类型</returns>  
        public static string GetSystemType() {
            try {
                string strSystemType = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc) {
                    strSystemType = mo["SystemType"].ToString();
                }
                moc = null;
                mc = null;
                return strSystemType;
            }
            catch {
                return "unknown";
            }
        }

        /// <summary>
        /// 返回指定网址的HTML代码
        /// </summary>
        /// <param name="Url"></param>
        public static string GetUrlHtml(string Url) {
            string Html = "";
            WebRequest request = WebRequest.Create(Url);

            // 返回Response
            WebResponse response = request.GetResponse();
            // 读取对应网址HTML源码
            StreamReader stream = new StreamReader(response.GetResponseStream());
            Html = stream.ReadToEnd();

            // 关闭资源响应对象
            response.Close();
            response.Dispose();

            // 关闭文件流
            stream.Close();
            stream.Dispose();

            return Html;
        }

        ///// <summary>
        ///// 从URL读取HTML，并转为XML格式
        ///// </summary>
        ///// <param name="Url"></param>
        ///// <returns></returns>
        //public static XmlDocument GetUrlXML(string Url) {
        //    SgmlReader reader = null;

        //    reader = new SgmlReader {
        //        DocType = "HTML",
        //        InputStream = new StringReader(GetUrlHtml(Url))
        //    };

        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(reader);

        //    return doc;
        //}

        ///// <summary>
        ///// 将字符串转为Xml格式
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public static XmlDocument GetXml(string context) {
        //    SgmlReader reader = null;

        //    reader = new SgmlReader {
        //        DocType = "HTML",
        //        InputStream = new StringReader(context)
        //    };

        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(reader);

        //    return doc;
        //}

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
                return OutPagingHtml("", linkUrl, pageSize, typeName, totalCount);
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1) {
                return OutPagingHtml("", linkUrl, pageSize, typeName, totalCount);
            }
            if (totalCount % pageSize > 0) {
                pageCount += 1;
            }
            if (pageCount <= 1) {
                return OutPagingHtml("", linkUrl, pageSize, typeName, totalCount);
            }

            // 开始拼接页码
            StringBuilder pageStr = new StringBuilder();


            // 拼接页码
            string firstBtn = "<a href=\"" + StringUtils.ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«</a>";
            string lastBtn = "<a href=\"" + StringUtils.ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">»</a>";

            string firstStr = "<a href=\"" + StringUtils.ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<a href=\"" + StringUtils.ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

            if (pageIndex <= 1) {
                firstBtn = "<span class=\"disabled\">«</span>";
            }
            if (pageIndex >= pageCount) {
                lastBtn = "<span class=\"disabled\">»</span>";
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

            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize) {
                pageStr.Append("<span>...</span>\n");
            }
            for (int i = firstNum; i <= lastNum; i++) {
                if (i == pageIndex) {
                    pageStr.Append("<span class=\"current\">" + i + "</span>");
                }
                else {
                    pageStr.Append("<a href=\"" + StringUtils.ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2))) {
                pageStr.Append("<span>...</span>");
            }
            pageStr.Append(lastStr + lastBtn);

            return OutPagingHtml(pageStr.ToString(), linkUrl, pageSize, typeName, totalCount);
        }

        /// <summary>
        /// 返回分页HTML
        /// </summary>
        /// <param name="PageList"></param>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        private static string OutPagingHtml(string PageList, string linkUrl, int pageSize, string typeName, int totalCount) {

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

            pageStr.Append("<div class=\"pagelist\">");
            pageStr.Append("<span>共 ：" + totalCount + " 条</span>");
            pageStr.Append("    <div class=\"l-btns\">");
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

        #region 创建树

        /// <summary>
        /// 创建层级列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data">数据源</param>
        /// <param name="MainAttr">主属性</param>
        /// <param name="FromAttr">从属性</param>
        /// <param name="TopVal">顶级值</param>
        /// <param name="IsTree">是否为树结构</param>
        /// <param name="MaxLayer">最深层级</param>
        /// <returns></returns>
        public static List<TreeView<T>> GetTree<T>(List<T> Data, string MainAttr, string FromAttr, string TopVal, bool IsTree = true, int MaxLayer = 5) {

            // 判断数据源是否有数据
            if (Data == null || Data.Count <= 0)
                return null;

            List<TreeView<T>> tree = new List<TreeView<T>>();

            Type type = Data[0].GetType();
            // 获取主属性
            PropertyInfo mainPro = type.GetProperty(MainAttr);
            // 获取从属性
            PropertyInfo fromPro = type.GetProperty(FromAttr);
            // 判断主从属性是否均存在
            if (mainPro == null || fromPro == null)
                return null;

            Dictionary<object, TreeView<T>> dic = new Dictionary<object, TreeView<T>>();
            // 最大循环次数
            int maxCon = MaxLayer * Data.Count;
            while (Data.Count > 0 && maxCon > 0) {
                maxCon--;
                T t = Data[0];
                Data.RemoveAt(0);

                TreeView<T> leaf = new TreeView<T>();
                leaf.Node = t;

                // 获取值
                object mainVal = mainPro.GetValue(t);
                object fromVal = fromPro.GetValue(t);

                // 若为顶级节点
                if (fromVal + "" == TopVal) {
                    leaf.Layer = 1;
                    if (leaf.Layer > MaxLayer)
                        continue;
                    tree.Add(leaf);
                    dic.Add(mainVal, leaf);
                }
                // 若存在父级节点
                else if (dic.Keys.Contains(fromVal)) {
                    // 将当前对象插入
                    TreeView<T> temp = dic[fromVal];
                    leaf.Layer = temp.Layer + 1;
                    if (leaf.Layer > MaxLayer)
                        continue;
                    // 是否为树结构
                    if (IsTree) {
                        if (temp.Children == null)
                            temp.Children = new List<TreeView<T>>();
                        temp.Children.Add(leaf);
                        dic.Add(mainVal, leaf);
                    }
                    else {
                        // 获取父级的索引
                        int index = tree.IndexOf(temp) + 1;
                        // 查找父级的最后一个子级
                        while (index < tree.Count) {
                            // 若当前节点的层级更小
                            if (tree[index++].Layer < leaf.Layer) {
                                index--;
                                break;
                            }
                        }
                        // 防止超出索引
                        if (index < tree.Count)
                            tree.Insert(index, leaf);
                        else
                            tree.Add(leaf);
                        dic.Add(mainVal, leaf);
                    }
                }
                // 否则
                else {
                    Data.Add(t);
                }
            }

            return tree;
        }

        #endregion

        #region List处理

        /// <summary>
        /// 将Tree结构转为平铺
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> TreeToList<T>(List<T> tree, string field) {
            if (tree == null)
                return null;
            List<T> list = new List<T>();

            foreach (T item in tree) {
                list.Add(item);

                List<T> child = Reflex.GetValByField<List<T>>(item, field);

                List<T> temp = TreeToList(child, field);
                if (temp != null && temp.Count > 0)
                    list.AddRange(temp);
            }
            return list;
        }


        #endregion
    }

    /// <summary>
    /// 树视图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeView<T> {
        /// <summary>
        /// 当前对象
        /// </summary>
        public T Node { get; set; }
        /// <summary>
        /// 当前层级
        /// </summary>
        public int Layer { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<TreeView<T>> Children { get; set; }
    }
}
