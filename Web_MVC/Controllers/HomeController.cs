using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common;

namespace Web_MVC.Controllers {
    /// <summary>
    /// 添加语言包数据
    /// </summary>
    public class HomeController : Controller {

        /// <summary>
        /// 父窗页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {

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

            data.DropList = Utils.BingDrop(dic, data.Icon ?? -1, false);

            return View(data);
        }

        [HttpPost]
        public ActionResult Default(string title, int? icon) {

            if (string.IsNullOrEmpty(title))
                title = "Msg";
            return Content(ResObj.LayerMsg(title, icon, Utils.GetPostUrlInfo()));
        }

    }
}