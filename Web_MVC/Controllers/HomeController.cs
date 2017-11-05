using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common;
using Models;
using BLL.Demo;
using EBuy;
using System.Text;

namespace Web_MVC.Controllers {
    /// <summary>
    /// 添加语言包数据
    /// </summary>
    public class HomeController : Manager {

        /// <summary>
        /// 父窗页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            LoginInfo info = Utils.GetLoginInfo();

            if (Session["Login"] == null) {
                LoginIPService bll_login = new LoginIPService();
                LoginIP login = bll_login.FindEntity(c =>
                c.ipv4 == info.IPv4 && c.extranetIP == info.ExtranetIP &&
                c.hostName == info.HostName && c.System == info.System && c.city == info.City);
                if (login == null) {
                    login = new LoginIP();
                    login.ipv4 = info.IPv4;
                    login.extranetIP = info.ExtranetIP;
                    login.hostName = info.HostName;
                    login.System = info.System;
                    login.city = info.City;
                    login.mac = info.Mac;
                    login.@operator = info.Operator;
                    login.counts = 1;
                    login.loginTime = DateTime.Now;
                    Session["Login"] = login;
                    bll_login.AddEntity(login);
                }
                else {
                    login.counts++;
                    login.loginTime = DateTime.Now;
                    bll_login.EditEntity(login);
                }
            }
            List<NavData> list = Serialize.DeSerializeNow<List<NavData>>("Nav.data");
            ViewBag.nav = GetNav(list);

            return View();
        }

        /// <summary>
        /// 返回导航栏
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetNav(List<NavData> list)
        {
            if (list == null) return "";
            StringBuilder builder = new StringBuilder();
            builder.Append("<ul class=\"layui-nav layui-nav-tree\" lay-filter=\"test\">");
            foreach (var item in list) {
                builder.Append(GetNavItem(item));
            }
            builder.Append("</ul>");

            return builder.ToString();
        }

        /// <summary>
        /// 返回每一项
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public string GetNavItem(NavData nav)
        {
            string url = "";
            if (!string.IsNullOrEmpty(nav.Url))
                url = "data-url=\"" + nav.Url + "\"";
            StringBuilder builder = new StringBuilder();
            builder.Append("<li class=\"layui-nav-item \" >");
            builder.Append("<a href=\"#\" " + url + " ><i class=\"fa " + nav.Icon + " fa-fw\"></i> " + nav.Title + "</a>");
            builder.Append(GetNavInfo(nav.Info));
            builder.Append(GetNavChild(nav.ChildrenList));
            builder.Append("</li>");
            return builder.ToString();
        }

        /// <summary>
        /// 返回下拉项
        /// </summary>
        /// <param name="childrenList"></param>
        /// <returns></returns>
        public string GetNavChild(List<NavData> list)
        {
            if (list == null)
                return "";
            StringBuilder builder = new StringBuilder();
            builder.Append("<dl class=\"layui-nav-child\">");
            for (int i = 0; i < list.Count; i++) {
                builder.Append("<dd>");
                builder.Append("    <a href=\"#\" data-url=\"" + list[i].Url + "\"> " + Utils.GetStrToLen((i + 1), 2, "0") + "、" + list[i].Title);
                builder.Append(GetNavInfo(list[i].Info));
                builder.Append("    </a>");
                builder.Append("</dd>");
            }
            builder.Append("</dl>");
            return builder.ToString();
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetNavInfo(string info)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(info)) {
                builder.Append("<i class=\"msg\">");
                builder.Append(info);
                builder.Append("</i>");
            }
            return builder.ToString();
        }


        /// <summary>
        /// 默认首页
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Default(ReqData<string> data)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            // 添加下拉框
            foreach (var item in Enum.GetValues(typeof(Icon)))
                dic.Add((int)item, item.ToString());

            ViewBag.icon = Utils.BingDrop(dic, data.Icon ?? -1, false);
            ViewBag.MsgStr = data.MsgStr;

            LoginInfo info = Utils.GetLoginInfo();

            ViewBag.m = Request.ServerVariables;

            return View(info);
        }

        [HttpPost]
        public ActionResult Default(string title, int? icon)
        {

            if (string.IsNullOrEmpty(title))
                title = "Msg";
            return Content(ResObj.LayerMsg(title, icon, Utils.GetPostUrlInfo()));
        }

    }
}