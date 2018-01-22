using BLL.DDS;
using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers
{
    public class AddCodeController : Manager
    {

        DT_DataItemService _DataItemService = new DT_DataItemService();
        DT_DataItemDetailService _DataItemDetailService = new DT_DataItemDetailService();

        // GET: WebFunction/AddCode
        public ActionResult Index()
        {
            string sel = ViewBag.sel;

            ViewBag.Type = new SelectList(_DataItemService.LoadEntities(c => true), "Id", "ItemName", sel);
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(int type)
        {
            
            DT_DataItem _DataItem = _DataItemService.FindEntity(type);

            List<DT_DataItemDetail> list = _DataItemDetailService.LoadEntities(c => c.DataItemId == type).ToList();


            string enumType = "D:\\Code\\enumType.txt";
            string classData = "D:\\Code\\classData.txt";

            // 开头
            FileAction.AppendStr(enumType, $"        #region {_DataItem.ItemName}\n\n");
            FileAction.AppendStr(classData, $"        #region {_DataItem.ItemName}\n\n");
            // 父级
            DT_DataItemDetail begin = new DT_DataItemDetail();
            begin.Id = _DataItem.Id;
            begin.ItemCode = _DataItem.ItemCode;
            begin.ByName = _DataItem.ByName;
            begin.ItemName = _DataItem.ItemName;

            // 添加父级
            FileAction.AppendStr(enumType, GetEnumType(begin));
            FileAction.AppendStr(classData, GetClassData(begin));

            // 添加详情
            foreach (var item in list)
            {
                FileAction.AppendStr(enumType, GetEnumType(item));
                FileAction.AppendStr(classData, GetClassData(item));
            }

            // 结束
            FileAction.AppendStr(enumType, $"        #endregion\n\n");
            FileAction.AppendStr(classData, $"        #endregion\n\n");

            ViewBag.sel = type+"";

            return Success("执行成功！", "AddCode/Index", false);
        }

        public string GetEnumType(DT_DataItemDetail _DataItemDetail)
        {
            string str = $"" +
                $"        /// <summary>\n" +
                $"        /// {_DataItemDetail.ItemName}\n" +
                $"        /// </summary>\n" +
                $"        [Description(\"{_DataItemDetail.ItemName}\")]\n" +
                $"        {_DataItemDetail.ByName} = {_DataItemDetail.Id},\n\n";
            return str;
        }

        public string GetClassData(DT_DataItemDetail _DataItemDetail)
        {
            string str = $"" +
                $"        /// <summary>\n" +
                $"        /// {_DataItemDetail.ItemName}\n" +
                $"        /// </summary>\n" +
                $"        /// {_DataItemDetail.ByName}= {_DataItemDetail.Id},\n" +
                $"        public static int {_DataItemDetail.ByName}\n" +
                "        {\n" +
                "            get\n" +
                "            {\n" +
                $"                return _DT_DataItem.GetDataItemDetail(EnumDataItem.{_DataItemDetail.ByName}).Id;\n" +
                "            }\n" +
                "            set { }\n" +
                "        }\n\n";
            return str;
        }

        public string GetByName(string byname)
        {
            if (string.IsNullOrEmpty(byname)) return null;
            List<string> str = byname.Split(' ').ToList();

            if (str.Count <= 1)
                return byname;

            for (int i = 0; i < str.Count; i++)
            {
                if (string.IsNullOrEmpty(str[i]))
                    continue;

                str[i] = (str[i][0] + "").ToUpper() + str[i].Substring(1);
            }
            byname = "";
            foreach (var item in str)
            {
                if (string.IsNullOrEmpty(item)) continue;
                byname += item;
            }
            return byname;
        }
    }
}