using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.Algorithm.Controllers {
    public class SearchController : Manager {

        /// <summary>
        /// 搜索算法
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            Water();
            return View();
        }

        #region 广度优先

        /// <summary>
        /// 广度优先
        /// </summary>
        /// <param name="input"></param>
        public void Breadth(string input) {
            // 初始地图
            int[,] Arr = new int[10, 10] {
                { 1,2,1,0,0,0,0,0,2,3 },
                { 3,0,3,0,1,2,1,0,1,2 },
                { 4,0,1,0,1,2,3,2,0,1 },
                { 3,2,0,0,0,1,2,4,0,0 },
                { 0,0,0,0,0,0,1,5,3,0 },
                { 0,1,2,1,0,1,5,4,3,0 },
                { 0,1,2,3,1,3,6,2,1,0 },
                { 0,0,3,4,8,9,7,5,0,0 },
                { 0,0,0,3,7,8,6,0,1,2 },
                { 0,0,0,0,0,0,0,0,1,0 }
            };

            // 降落地点
            double[] t = Utils.GetStrToDoubleArr(input);
            int[] init = { (int)t[0], (int)t[1] };
            Arr.GetLength(0);

            ResWrite(BreadthFirst(Arr, init) + "");
        }

        /// <summary>
        /// 存储地图中的单元格
        /// </summary>
        public class MapCell {
            public MapCell(int r, int c, int v) {
                R = r; C = c; Val = v;
            }

            /// <summary>
            /// 所在行数
            /// </summary>
            public int R;
            /// <summary>
            /// 所在列数
            /// </summary>
            public int C;
            /// <summary>
            /// 对应值
            /// </summary>
            public int Val;
        }

        /// <summary>
        /// 广度优先搜索
        /// </summary>
        /// <param name="Arr">地图数组</param>
        /// <param name="init">初始降落坐标</param>
        /// <param name="Rows">总行数</param>
        /// <param name="Cells">总列数</param>
        public int BreadthFirst(int[,] Arr, int[] init) {
            // 创建队列
            Queue<MapCell> queue = new Queue<MapCell>();
            List<string> list = new List<string>();

            // 根据坐标创建第一个“地图格子”，
            MapCell first = new MapCell(init[0], init[1], Arr[init[0], init[1]]);
            if (first.Val <= 0)
                return 0;

            // 加入到队列中
            queue.Enqueue(first);
            list.Add(first.R + "," + first.C);

            int Sum = 0;

            // 循环队列中的值
            while (queue.Count > 0) {
                // 取出第一个格子
                MapCell temp = queue.Dequeue();
                Sum += temp.Val;
                // 修改状态，表示已经找过
                Arr[temp.R, temp.C] = -1;

                // 规定，上(1)下(2)左(3)右(4)
                for (int i = 1; i <= 4; i++) {
                    // 返回对应的格子
                    MapCell Next = FindCell(Arr, temp.R, temp.C, i);
                    if (Next != null)
                        // 防止重复添加
                        if (!list.Contains(Next.R + "," + Next.C)) {
                            queue.Enqueue(Next);
                            list.Add(Next.R + "," + Next.C);
                        }
                }
            }
            return Sum;
        }

        /// <summary>
        /// 寻找岛屿格子
        /// </summary>
        /// <param name="Arr">地图数组</param>
        /// <param name="r">当前行</param>
        /// <param name="c">当前列</param>
        /// <param name="d">寻找方向</param>
        /// <param name="Rows">总行数</param>
        /// <param name="Cells">总列数</param>
        /// <returns></returns>
        public MapCell FindCell(int[,] Arr, int r, int c, int d) {
            // 向上寻找
            if (d == 1) c--;
            // 向下寻找
            else if (d == 2) c++;
            // 向左寻找
            else if (d == 3) r--;
            // 向右寻找
            else if (d == 4) r++;

            // 防止超出数组下限
            if (r < 0 || c < 0)
                return null;
            // 防止超出数组上限
            if (r >= Arr.GetLength(0) || c >= Arr.GetLength(1))
                return null;

            // 只返回陆地，
            int val = Arr[r, c];
            if (val > 0)
                return new MapCell(r, c, val);
            return null;
        }

        #endregion

        #region 深度优先

        /// <summary>
        /// 深度优先
        /// </summary>
        /// <param name="input"></param>
        public void Depth(string input) {
            // 初始地图
            int[,] Arr = new int[10, 10] {
                { 1,2,1,0,0,0,0,0,2,3 },
                { 3,0,3,0,1,2,1,0,1,2 },
                { 4,0,1,0,1,2,3,2,0,1 },
                { 3,2,0,0,0,1,2,4,0,0 },
                { 0,0,0,0,0,0,1,5,3,0 },
                { 0,1,2,1,0,1,5,4,3,0 },
                { 0,1,2,3,1,3,6,2,1,0 },
                { 0,0,3,4,8,9,7,5,0,0 },
                { 0,0,0,3,7,8,6,0,1,2 },
                { 0,0,0,0,0,0,0,0,1,0 }
            };
            // 降落地点
            double[] t = Utils.GetStrToDoubleArr(input);

            ResWrite(DepthFirst(Arr, (int)t[0], (int)t[1]) + "");
        }

        /// <summary>
        /// 深度优先搜索
        /// </summary>
        /// <param name="Arr">地图数组</param>
        /// <param name="r">当前行</param>
        /// <param name="c">当前列</param>
        /// <returns></returns>
        public int DepthFirst(int[,] Arr, int r, int c) {
            // 退出条件
            if (r < 0 || c < 0)
                return 0;
            if (r >= Arr.GetLength(0) || c >= Arr.GetLength(1))
                return 0;
            if (Arr[r, c] <= 0)
                return 0;

            int Sum = Arr[r, c];
            Arr[r, c] = -1;

            // 向上找
            Sum += DepthFirst(Arr, r, c - 1);
            // 向下找
            Sum += DepthFirst(Arr, r, c + 1);
            // 向左找
            Sum += DepthFirst(Arr, r - 1, c);
            // 向右找
            Sum += DepthFirst(Arr, r + 1, c);

            return Sum;
        }

        #endregion

        #region 水管工

        public void Water() {
            int[,] Arr = new int[5, 4] {
                {5,3,5,3},
                {1,5,3,0},
                {2,3,5,1},
                {6,1,1,5},
                {1,5,5,4},
            };

            Stack<string> stack = WaterPipe(Arr, new Stack<string>(), 0, 0, 3);
        }

        bool IsEnd = false;

        /// <summary>
        /// 水管工游戏
        /// </summary>
        /// <param name="Arr">地图数组</param>
        /// <param name="list">记录路径</param>
        /// <param name="r">当前行</param>
        /// <param name="c">当前列</param>
        /// <param name="o">入水方向</param>
        /// <returns></returns>
        public Stack<string> WaterPipe(int[,] Arr, Stack<string> list, int r = 0, int c = 0, int o = 3) {

            // 防止索引超出
            if (r < 0 || c < 0) return list;
            if (r >= Arr.GetLength(0) || c >= Arr.GetLength(1)) return list;

            // 如果当前位置为0，即为树，走不通
            if (Arr[r, c] == 0) return list;
            // 判断当前位置管道是否为弯的
            bool IsCurved = Arr[r, c] / 5 < 1;

            string str = r + "," + c;
            // 若当前路径已经走过，返回
            if (list.Contains(str))
                return list;

            list.Push(str);

            // 规定入水方向：上(1)，下(2)，左(3)，右(4)             
            int top = 1;
            int up = 2;
            int left = 3;
            int right = 4;

            // 当到达最后一个格子时
            if (r == Arr.GetLength(0) - 1 && c == Arr.GetLength(1) - 1) {
                if ((IsCurved && o == top) || (!IsCurved && o == left))
                    IsEnd = true;
            }
            // 从上方入水
            else if (o == top) {
                if (IsCurved) {
                    // 向左走，入水口在右边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r, c - 1, right);
                    // 向右走，入水口在左边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r, c + 1, left);
                }
                else {
                    // 向下走，入水口在上边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r + 1, c, top);
                }
            }
            // 从下方入水
            else if (o == up) {
                if (IsCurved) {
                    // 向左走，入水口在右边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r, c - 1, right);
                    // 向右走，入水口在左边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r, c + 1, left);
                }
                else {
                    // 向上走，入水口在下边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r + 1, c, up);
                }
            }
            // 从左方入水
            else if (o == left) {
                if (IsCurved) {
                    // 向上走，入水口在下边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r - 1, c, up);
                    // 向下走，入水口在上边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r + 1, c, top);
                }
                else {
                    // 向右走，入水口在左边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r, c + 1, left);
                }
            }
            // 从右方入水
            else if (o == right) {
                if (IsCurved) {
                    // 向上走，入水口在下边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r - 1, c, up);
                    // 向下走，入水口在上边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r + 1, c, top);
                }
                else {
                    // 向左走，入水口在右边
                    if (!IsEnd)
                        WaterPipe(Arr, list, r, c + 1, right);
                }
            }
            // 若尚未结束，移除第一个数据
            if (!IsEnd)
                list.Pop();
            return list;
        }

        #endregion
    }
}