using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.Algorithm.Controllers
{
    public class ChartController : Manager
    {
        /// <summary>
        /// 图的遍历
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public void City(string input) {
            ResWrite(CityLong(Utils.GetStrToDoubleArr(input)));
        }

        public string CityLong(double[] arr) {
            return "";
        }
    }
}