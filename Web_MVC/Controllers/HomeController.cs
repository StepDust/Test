using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Models;
using Common;

namespace Web_MVC.Controllers {
    /// <summary>
    /// 添加语言包数据
    /// </summary>
    public class HomeController : Controller {

        public ActionResult Index() {

            return View();
        }

        public ActionResult Default(ReqData<string> data) {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (var item in Enum.GetValues(typeof(Icon)))
                dic.Add((int)item, item.ToString());

            data.DropList = Utils.BingDrop(dic, data.Icon ?? -1, false);

            return View(data);
        }

        [HttpPost]
        public ActionResult Default(string title, int? icon) {

            if (string.IsNullOrEmpty(title))
                title = "Msg";
            return Content(ResObj.LayerMsg(title, Utils.GetPostUrlInfo(), (Icon)icon));
        }

    }
}