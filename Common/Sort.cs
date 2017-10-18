using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    /// <summary>
    /// 排序类
    /// </summary>
    public class Sort {

        /// <summary>
        /// 桶排序
        /// </summary>
        /// <param name="arr">带排序数组</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int[] BucketSort(double[] arr, int min, int max) {
            // 准备桶
            int[] num = new int[max - min + 1];
            // 差值为：  (最小值)100-(初始索引)0=100
            int diff = min - 0;
            List<int> list = new List<int>();

            // 开始排序
            for (int i = 0; i < arr.Length; i++) {
                // 满足条件，[100,900]
                if (min <= arr[i] && arr[i] <= max) {
                    // 对应索引加一，索引对应的值，表示当前数字出现的次数
                    num[(int)(arr[i] - diff)]++;
                }
            }
            // 输出
            for (int i = 0; i < num.Length; i++) {
                // 当 num[i] 为0时，表示 i 没有出现过
                if (num[i] <= 0) continue;
                // 输出 num[i] 个 i 
                for (int j = 0; j < num[i]; j++)
                    list.Add(i);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="arr">待排序数组</param>
        /// <returns></returns>
        public static double[] BubbleSort(double[] arr) {

            // 依次获取未排序的数字，最后一个不获取
            for (int i = 0; i < arr.Length - 1; i++) {
                // 循环获取未排序数字之后的数字
                for (int j = i + 1; j < arr.Length; j++) {
                    if (arr[i] > arr[j]) {
                        // 交换两个数字
                        arr[i] += arr[j];
                        arr[j] = arr[i] - arr[j];
                        arr[i] = arr[i] - arr[j];
                    }
                }
            }
            return arr;
        }



    }
}
