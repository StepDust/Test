using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.Algorithm.Controllers {
    /// <summary>
    /// 数据结构
    /// </summary>
    public class StrucController : Manager {
        // GET: Algorithm/Struc
        public ActionResult Index() {
            return View();
        }

        #region 队列

        /// <summary>
        /// 队列解密
        /// </summary>
        /// <param name="input"></param>
        public void QDecrypt(string input) {
            ResWrite(QueueDecrypt(Utils.GetStrToDoubleArr(input)).ToString(" , "));
        }

        /// <summary>
        /// 队列解密
        /// </summary>
        /// <typeparam name="s">数组类型</typeparam>
        /// <param name="arr">待解密数组</param>
        /// <returns></returns>
        public s[] QueueDecrypt<s>(s[] arr) {
            // 创建队列
            Queue<s> q = new Queue<s>();
            // 循环将数据加入队列
            foreach (s item in arr)
                q.Enqueue(item);

            int index = 0;
            // 将数据取出
            while (q.Count > 0) {
                // 将第一条数据从队列取出，并放入数组
                arr[index++] = q.Dequeue();
                if (q.Count > 0)
                    // 将第二条数据放入队列末尾
                    q.Enqueue(q.Dequeue());
            }
            return arr;
        }

        #endregion

        #region 栈

        /// <summary>
        /// 判断是否为回文
        /// </summary>
        /// <param name="input">待判断字符串</param>
        public void IsPalindrome(string input) {

            Stack<char> stack = new Stack<char>();
            string msg = "";
            Icon icon = Icon.Success;

            // 字符串的一半入栈
            for (int i = 0; i < input.Length / 2; i++) {
                stack.Push(input[i]);
            }
            // 字符串最后一个值的索引
            int index = input.Length - 1;
            // 依次出栈
            while (stack.Count > 0) {
                // 若有不同的字符，结束出栈
                if (input[index--] != stack.Pop())
                    break;
            }
            // 若index的大小不是字符串长度的一半
            if (index != input.Length / 2) {
                msg = input + "，不是回文！";
                icon = Icon.Error;
            }
            else
                msg = input + "，是回文！";

            ResObj res = new ResObj("返回结果", msg, icon);
            Response.Write(Utils.ObjectToJson(res));
        }

        #endregion

        #region 链表

        /// <summary>
        /// 链表添加
        /// </summary>
        /// <param name="input"></param>
        public void AddNum(string input) {
            input = DataCheck.RepLanguage(input);
            string[] str = input.Split(';');
            double[] arr = Sort.BubbleSort(Utils.GetStrToDoubleArr(str[0]));
            double[] num = Utils.GetStrToDoubleArr(str[1]);
            ResWrite(LinkListAdd(arr,num).ToString(" , "));
        }

        /// <summary>
        /// 链表按顺序插入数字
        /// </summary>
        /// <param name="arr">已排序数组< /param>
        /// <param name="num">待插入数组< /param>
        public double[] LinkListAdd(double[] arr, double[] num) {

            LinkedList<double> link = new LinkedList<double>();

            // 将排序完毕的数组加入链表
            foreach (var item in arr)
                link.AddLast(item);

            // 将num按从小到大的顺序插入链表
            foreach (var item in num) {
                // 获取第一个节点
                LinkedListNode<double> now = link.First;
                // 寻找值比item大的节点
                while (now != null && now.Value < item)
                    now = now.Next;
                // 找遍整个链表，没有比item更大的
                if (now == null)
                    link.AddLast(item);
                else {
                    now = now.Previous;
                    // 找遍整个链表，没有比item更小的
                    if (now == null)
                        link.AddFirst(item);
                    // 将item插入指定位置
                    else
                        link.AddAfter(now, new LinkedListNode<double>(item));
                }
            }
            return link.ToArray();
        }
        
        #endregion

    }
}