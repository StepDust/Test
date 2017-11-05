using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers {
    public class AddNavController : Manager {
        
        public ActionResult Index()
        {
            List<NavData> list = Serialize.DeSerializeNow<List<NavData>>(GTKey.File_Nav);

            return View(BindDrop(list));
        }

        public List<SelectListItem> BindDrop(List<NavData> list, string selVal = "")
        {
            List<SelectListItem> Drop = new List<SelectListItem>();
            // 添加顶级菜单
            SelectListItem item = new SelectListItem();
            item.Text = ">> 顶级菜单 <<";
            item.Value = "0";
            Drop.Add(item);
            // 添加侧边栏菜单
            if (list != null && list.Count > 0) {

                list = Utils.TreeToList(list, "ChildrenList");
                // 循环所有列表
                foreach (var nav in list) {
                    if (nav.Layer > 1)
                        nav.Title = ">&nbsp; " + nav.Title;
                    item = new SelectListItem() {
                        Text = Utils.StrAppendTo(nav.Title, "&nbsp;&nbsp;&nbsp; ", nav.Layer - 1),
                        Value = nav.ID,
                        Selected = nav.ID == selVal
                    };
                    Drop.Add(item);
                }
            }
            return Drop;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public ActionResult Insert(NavData nav)
        {
            if (string.IsNullOrEmpty(nav.Title))
                return Error("请输入标题！", "Index");
            nav.Title = nav.Title.Trim();

            List<NavData> list = Serialize.DeSerializeNow<List<NavData>>(GTKey.File_Nav);
            if (list != null && list.Count > 0 && nav.ID != "0") {
                NavData parent = Utils.TreeToList(list, "ChildrenList").Where(c => c.ID == nav.ID).FirstOrDefault();

                if (parent != null) {
                    if (parent.ChildrenList == null)
                        parent.ChildrenList = new List<NavData>();
                    nav.Layer = parent.Layer + 1;
                    nav.ID = Guid.NewGuid().ToString();
                    parent.ChildrenList.Add(nav);
                }
            }
            else {
                if (list == null || list.Count <= 0)
                    list = new List<NavData>();
                nav.ID = Guid.NewGuid().ToString();
                nav.Layer = 1;
                list.Add(nav);
            }
            Serialize.ToSerialize(list, GTKey.File_Nav);

            return Success(GTKey.Msg_Succes_Add, "Index");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public ActionResult Edit(NavData nav)
        {
            List<NavData> list = Serialize.DeSerializeNow<List<NavData>>(GTKey.File_Nav);
            EditNav(list, nav.ID, nav);
            Serialize.ToSerialize(list, GTKey.File_Nav);
            return Success(GTKey.Msg_Succes_Edit, "Index");
        }

        public void GetNav(string ID)
        {
            List<NavData> list = Serialize.DeSerializeNow<List<NavData>>(GTKey.File_Nav);
            NavData nav = Utils.TreeToList(list, "ChildrenList").Where(c => c.ID == ID).FirstOrDefault();
            Response.Write(Utils.ObjectToJson(nav));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public ActionResult Delete(NavData nav)
        {
            List<NavData> list = Serialize.DeSerializeNow<List<NavData>>(GTKey.File_Nav);
            EditNav(list, nav.ID);
            Serialize.ToSerialize(list, GTKey.File_Nav);
            return Success(GTKey.Msg_Succes_Del, "Index");
        }

        /// <summary>
        /// 删除左侧导航栏选项
        /// </summary>
        /// <param name="list"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public bool EditNav(List<NavData> list, string ID, NavData nav = null)
        {
            if (list == null) return false;
            NavData temp = list.Where(c => c.ID == ID).FirstOrDefault();
            if (!list.Remove(temp)) {
                foreach (var item in list) {
                    if (EditNav(item.ChildrenList, ID, nav))
                        break;
                }
            }
            else {
                if (nav != null)
                    list.Add(nav);
                return true;
            }
            return false;
        }


    }
}