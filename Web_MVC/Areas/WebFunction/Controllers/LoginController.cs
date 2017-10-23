using BLL.Demo;
using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers {
    public class LoginController : Manager {
        // GET: WebFunction/Login
        public ActionResult Index(ReqData<LoginIP> data) {
            LoginIPService bll_login = new LoginIPService();
            data.DataList = bll_login.LoadEntities(data, c => true, c => c.loginTime, false).ToList();
            data.PageUrl = Utils.GetUrlInfo();
            data.GetPageList();

            return View(data);
        }
    }
}