using BLL.Demo;
using Common;
using Models;
using System.Linq;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers {
    /// <summary>
    /// 添加字典
    /// Time：2017-10-16
    /// </summary>
    public class AddDicController : Controller {

        LangService bll_Lang = new LangService();

        /// <summary>
        /// 首页路径
        /// </summary>
        string indexUrl { get { return Url.Action("Index", "AddDic", new { area = "WebFunction" }); } }

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
            return Content(ResObj.LayerMsg("删除成功！", Icon.Success, indexUrl));
        }

        public ActionResult Edit(Lang model) {

            if (bll_Lang.Exists(c => c.msgid == model.msgid))
                return Content(ResObj.LayerMsg("请勿重复添加！", Icon.Error, indexUrl));

            model.msgstr = model.msgid;

            bll_Lang.EditEntity(model);

            return Content(ResObj.LayerMsg("修改成功！", Icon.Success, indexUrl));
        }
    }

}