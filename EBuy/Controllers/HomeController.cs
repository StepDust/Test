using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBuy.Models;
using Newtonsoft.Json;
using Models;
using Common;

namespace EBuy.Controllers {
    /// <summary>
    /// 添加语言包数据
    /// </summary>
    public class HomeController : Controller {

        public ActionResult Index() {

            return View();
        }

        public ActionResult Default() {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (var item in Enum.GetValues(typeof(Icon)))
                dic.Add((int)item, item.ToString());

            ViewBag.icon = Utils.BingDrop(dic, s, false);

            return View();
        }
        static int s = 0;
        [HttpPost]
        public ActionResult Default(string title, int icon) {
            s = icon;
            return Content(ResObj.LayerMsg("Msg", "", (Icon)s));
        }

    }
}