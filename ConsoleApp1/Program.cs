using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            /*
             网址：https://www.sekai.co/trust/
             第一个信任游戏：
             规定，1为合作，0为欺骗
            */
            int com = 0;

            Init(1);
            FDJ(1, 0, 1);
            FDJ(0, 0, 1);
            com += Max;
            Console.WriteLine("复读机：" + Max);
            Console.WriteLine(ss + "\n");

            Init(0);
            LYT(1, 0, 1);
            LYT(0, 0, 1);
            com += Max;
            Console.WriteLine("老油条：" + Max);
            Console.WriteLine(ss + "\n");

            Init(1);
            XF(1, 0, 1);
            XF(0, 0, 1);
            com += Max;
            Console.WriteLine("小粉：" + Max);
            Console.WriteLine(ss + "\n");

            Init(1);
            bl = false;
            LT(1, 0, 1);
            LT(0, 0, 1);
            com += Max;
            Console.WriteLine("老铁：" + Max);
            Console.WriteLine(ss + "\n");

            Init(1);
            FEMS(1, 0, 1);
            FEMS(0, 0, 1);
            com += Max;
            Console.WriteLine("福尔摩斯：" + Max);
            Console.WriteLine(ss + "\n");

            Console.WriteLine(com);

            Console.ReadLine();
        }

        /// <summary>
        /// 福尔摩斯的默认投币方式
        /// </summary>
        static int[] model = { 1, 0, 1, 1, 0, 0, 0, 0 };

        
        static int dui = 0;
        static int Max = int.MinValue;
        static string ss = "";
        static bool bl = false;

        /// <summary>
        /// 复读机
        /// 哈喽！我第一次会出「合作」，但是之后，我会选和你之前一轮一模一样的选择喔～嘻嘻
        /// </summary>
        /// <param name="action">我方，1：合作，0欺骗</param>
        /// <param name="sum">当前金币</param>
        /// <param name="layer">游戏次数</param>
        private static void FDJ(int action, int sum, int layer, int maxlayer = 5, string str = "") {

            if (layer == maxlayer + 1) {
                if (Max <= sum) {
                    Max = sum;
                    ss = str;
                }
                return;
            }
            str += action + " ";
            sum = check(action, dui, sum);
            dui = action;
            FDJ(1, sum, layer + 1, maxlayer, str);
            FDJ(0, sum, layer + 1, maxlayer, str);
        }

        /// <summary>
        /// 老油条
        /// 永不合作，这是弱肉强食的世界
        /// </summary>
        /// <param name="action"></param>
        /// <param name="sum"></param>
        /// <param name="layer"></param>
        /// <param name="maxlayer"></param>
        /// <param name="str"></param>
        private static void LYT(int action, int sum, int layer, int maxlayer = 4, string str = "") {
            if (layer == maxlayer + 1) {
                if (Max <= sum) {
                    Max = sum;
                    ss = str;
                }
                return;
            }
            str += action + " ";
            sum = check(action, 0, sum);
            LYT(1, sum, layer + 1, maxlayer, str);
            LYT(0, sum, layer + 1, maxlayer, str);
        }

        /// <summary>
        /// 小粉
        /// 我们大家做朋友吧！
        /// </summary>
        /// <param name="action"></param>
        /// <param name="sum"></param>
        /// <param name="layer"></param>
        /// <param name="maxlayer"></param>
        /// <param name="str"></param>
        private static void XF(int action, int sum, int layer, int maxlayer = 4, string str = "") {
            if (layer == maxlayer + 1) {
                if (Max <= sum) {
                    Max = sum;
                    ss = str;
                }
                return;
            }
            str += action + " ";
            sum = check(action, 1, sum);
            XF(1, sum, layer + 1, maxlayer, str);
            XF(0, sum, layer + 1, maxlayer, str);
        }

        /// <summary>
        /// 老铁
        /// 我会先跟你「合作」，如果你听话，那咱们的生意就继续做下去。但是你要是敢「欺骗」我，死到临头我也不会再合作！
        /// </summary>
        /// <param name="action"></param>
        /// <param name="sum"></param>
        /// <param name="layer"></param>
        /// <param name="maxlayer"></param>
        /// <param name="str"></param>
        private static void LT(int action, int sum, int layer, int maxlayer = 5, string str = "") {
            if (layer == maxlayer + 1) {
                if (Max <= sum) {
                    Max = sum;
                    ss = str;
                }
                return;
            }
            str += action + " ";
            sum = check(action, bl == false ? 1 : 0, sum);

            if (action == 0)
                bl = true;

            LT(1, sum, layer + 1, maxlayer, str);
            LT(0, sum, layer + 1, maxlayer, str);
        }

        #region 福尔摩斯规则

        /*
         分析人是我的特长。游戏开始我会「合作」、「欺骗」、「合作」、「合作」。
         如果你反过来欺骗我，我就会像跟复读机那样跟着你出牌。
         如果你一直不欺骗回来，那我就会像千年老油条那样榨干你。
         这都是行走江湖最基本的套路啊，我亲爱的花生儿～
       */

        #endregion

        /// <summary>
        /// 福尔摩斯
        /// </summary>
        /// <param name="action"></param>
        /// <param name="sum"></param>
        /// <param name="layer"></param>
        /// <param name="str"></param>
        private static void FEMS(int action, int sum, int layer, string str = "") {

            if (layer == 8) {
                if (Max <= sum) {
                    Max = sum;
                    ss = str;
                }
                return;
            }
            str += action + " ";
            // 若一直未欺骗
            sum = check(action, model[layer - 1], sum);

            // 判断是否欺骗
            if (action == 0 && model[layer - 1] == 1)
                bl = true;

            if (layer >= 4 && bl) {
                dui = action;
                FDJ(1, sum, layer + 1, 7, str);
                FDJ(0, sum, layer + 1, 7, str);
                return;
            }

            FEMS(1, sum, layer + 1, str);
            FEMS(0, sum, layer + 1, str);



        }

        /// <summary>
        /// 计算自己当前得分
        /// </summary>
        /// <param name="me">我欺骗（0）或合作（1）</param>
        /// <param name="you"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static int check(int me, int you, int sum) {
            // 我合作
            if (me == 1) {
                // 对方合作
                if (you == 1)
                    sum += 2;
                // 对方欺骗
                else
                    sum -= 1;
            }
            // 我欺骗
            else {
                // 对方合作
                if (you == 1)
                    sum += 3;
                // 对方欺骗
                else
                    sum -= 0;
            }
            return sum;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init(int num) {
            bl = false;
            Max = int.MinValue;
            ss = "";
            dui = num;
        }
    }
}
