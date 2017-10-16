using BLL.Demo;
using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Controllers {
    /// <summary>
    /// 添加字典
    /// Time：2017-10-16
    /// </summary>
    public class AddDicController : Controller {

        LangService bll_Lang = new LangService();

        public ActionResult Index(ReqData<Lang> data) {
            PageInfo info= new PageInfo();
            data.DataList = bll_Lang.LoadEntities(data, c => true, c => c.id, false).ToList();

            return View(data);
        }
        

    }
}