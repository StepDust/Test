using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.Algorithm.Controllers {
    public class ChartController : Manager {
        /// <summary>
        /// 图的遍历
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            return View();
        }

        public void City(string input) {

            int[,] Arr = {
                {-1,-1,-1,-1,-1,-1 },
                {-1,0,2,-1,-1,10 },
                {-1,-1,0,3,-1,7 },
                {-1,4,-1,0,4,-1 },
                {-1,-1,-1,-1,0,5 },
                {-1,-1,-1,3,-1,0 }
            };
            double[] init = Utils.GetStrToDoubleArr(input);

            CityLong(Arr, (int)init[0], 0, (int)init[1]);

            ResWrite(Min + "");
        }

        int Min = -1;

        /// <summary>
        /// 深度查找城市路径
        /// </summary>
        /// <param name="Arr">路径地图</param>
        /// <param name="r">起点</param>
        /// <param name="sum">所用路径长度</param>
        /// <param name="c">终点</param>
        /// <returns></returns>
        public void CityLong(int[,] Arr, int r, int sum, int c) {
            // 当起点即为终点时，抵达目的地
            if (r == c) {
                // 若最小值大于当前的路径长度
                if (Min > sum||Min<0)
                    Min = sum;
                return;
            }
            int temp = 0;

            // 寻找能够从城市r去往的下一个城市
            for (int i = 0; i < Arr.GetLength(0); i++) {
                // 找到下一个城市
                if (Arr[r, i] > 0) {
                    // 标记下一个城市已经走过
                    temp = Arr[r, i];
                    Arr[r, i] = -1;
                    // 从下一个城市出发
                    CityLong(Arr, i, sum + temp, c);
                    // 还原
                    Arr[r, i] = temp;
                }
            }

        }
    }
}