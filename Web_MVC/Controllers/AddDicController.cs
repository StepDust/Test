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
            PageInfo info = new PageInfo();
            data.DataList = bll_Lang.LoadEntities(data, c => true, c => c.id, false).ToList();
            data.PageUrl = Utils.GetUrl();
            data.GetPageList();

            return View(data);
        }

        [HttpPost]
        public ActionResult Index(Lang model) {

            if (bll_Lang.Exists(c => c.msgid == model.msgid))
                return Content(ResObj.LayerMsg("请勿重复添加！", Icon.Error));

            model.msgstr = model.msgid;

            bll_Lang.AddEntity(model);

            return Content(ResObj.LayerMsg("添加成功！", Icon.Success));
        }

        public ActionResult Delete(int id) {
            bll_Lang.DeleteEntity(id);
            return Content(ResObj.LayerMsg("删除成功！", Icon.Success, "/AddDic/Index"));
        }

        public ActionResult Edit(Lang model) {

            if (bll_Lang.Exists(c => c.msgid == model.msgid))
                return Content(ResObj.LayerMsg("请勿重复添加！", Icon.Error, "/AddDic/Index"));

            model.msgstr = model.msgid;

            bll_Lang.EditEntity(model);

            return Content(ResObj.LayerMsg("修改成功！", Icon.Success, "/AddDic/Index"));
        }
    }

}