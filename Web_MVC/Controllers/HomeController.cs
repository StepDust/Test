using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common;
using Models;
using BLL.Demo;
using EBuy;

namespace Web_MVC.Controllers {
    /// <summary>
    /// 添加语言包数据
    /// </summary>
    public class HomeController : Manager {
        
        /// <summary>
        /// 父窗页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
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

            return View();
        }

        /// <summary>
        /// 默认首页
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Default(ReqData<string> data) {
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
        public ActionResult Default(string title, int? icon) {

            if (string.IsNullOrEmpty(title))
                title = "Msg";
            return Content(ResObj.LayerMsg(title, icon, Utils.GetPostUrlInfo()));
        }

    }
}