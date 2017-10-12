using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBuy.Models;
using Newtonsoft.Json;

namespace EBuy.Controllers {
    /// <summary>
    /// 添加语言包数据
    /// </summary>
    public class HomeController : Controller {

        DemoEntities1 db = new DemoEntities1();

        public ActionResult Index(ReqData<string> data) {
            
            return View(data);
        }

        public void GetData(ReqData<Lang> data) {
            
            data.DataList = db.Lang.OrderByDescending(c => c.id).ToList();

            Response.Write(JsonConvert.SerializeObject(data));
        }

        [HttpPost]
        public ActionResult Index(Lang lang) {

            if (string.IsNullOrEmpty(lang.msgid)) {
                ViewBag.Msg = "layer.msg('Msgid不能为空');";
                return View(lang);
            }

            if (string.IsNullOrEmpty(lang.msgstr)) {
                ViewBag.Msg = "layer.msg('Msgstr不能为空');";
                return View(lang);
            }

            string pageindex = Request["pageindex"];
            if (string.IsNullOrEmpty(pageindex))
                ViewBag.index = "1";
            else
                ViewBag.index = pageindex;

            lang.msgid = lang.msgid.ToLower();

            db.Lang.Add(lang);

            db.SaveChanges();

            ViewBag.Msg = "layer.msg('添加成功');";
            return View(new ReqData<string>());
        }

        public void Delete(int id) {
            Lang lang = db.Lang.Where(c => c.id == id).FirstOrDefault();

            try {
                db.Lang.Remove(lang);
                int num = db.SaveChanges();
                if (num > 0)
                    Response.Write(1);
                else
                    Response.Write(0);
            }
            catch {

                Response.Write(0);
            }

        }

        public void GetCount() {
            string msgid = Request["msgid"];
            string msgstr = Request["msgstr"];

            if (!string.IsNullOrEmpty(msgid)) {
                Response.Write(db.Lang.Where(c => c.msgid == msgid).Count());
            }
            if (!string.IsNullOrEmpty(msgstr)) {
                Response.Write(db.Lang.Where(c => c.msgstr == msgstr).Count());
            }
        }

    }
}