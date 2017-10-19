using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="arr">待排序数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="end">结束索引</param>
        /// <returns></returns>
        public static double[] Quicksort(double[] arr, int start = -1, int end = -1) {

            // 递归退出条件
            if (start < 0 && arr.Length <= start)
                return arr;
            if (end < 0 && arr.Length <= end)
                return arr;
            if (end < start)
                return arr;
            
            // 初始化值
            if (start == -1)
                start = 0;
            if (end == -1)
                end = arr.Length - 1;

            int i = start;
            int j = end;

            // 随机获取参考值
            Random ran = new Random();
            double k = arr[ran.Next(start, end + 1)];

            // 从后向前找
            for (; i < j; j--) {
                // 当后面的数小于参考值时
                if (arr[j] <= k) {
                    // 从前向后找
                    for (; i < j; i++) {
                        // 当前面的数大于参考值时
                        if (k <= arr[i]) {
                            // 交换前后值
                            arr[i] += arr[j];
                            arr[j] = arr[i] - arr[j];
                            arr[i] = arr[i] - arr[j];
                            break;
                        }
                    }
                }
            }

            // 排序右边数组
            arr = Quicksort(arr, start, i - 1);
            // 排序左边数组
            arr = Quicksort(arr, i + 1, end);

            return arr;
        }

    }
}
