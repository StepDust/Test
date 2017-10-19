using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace EBuy.Areas.Algorithm.Controllers {
    /// <summary>
    /// 排序
    /// </summary>
    public class SortController : Manager {

        public ActionResult Index() {

            return View();
        }
        
        /// <summary>
        /// 桶排序
        /// </summary>
        /// <param name="input"></param>
        public void BucketSort(string input) {
            ResWrite(Sort.BucketSort(Utils.GetStrToDoubleArr(input), 100, 800).ToString(" , "));
        }
        
        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="input"></param>
        public void BubbleSort(string input) {
            ResWrite(Sort.BubbleSort(Utils.GetStrToDoubleArr(input)).ToString(" , "));
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="input"></param>
        public void Quicksort(string input) {
            ResWrite(Sort.Quicksort(Utils.GetStrToDoubleArr(input)).ToString(" , "));
        }

    }
}