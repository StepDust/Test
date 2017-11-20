using BLL.GSQ_PaChong;
using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace WinForm.WebUrl {
    /// <summary>
    /// 网易站点
    /// </summary>
    public class Web_WangYi {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url">网站链接</param>
        /// <param name="LinkReg">超链接正则</param>
        /// <param name="ConReg">内容正则</param>
        /// <param name="Interval">间隔时间(ms)</param>
        public Web_WangYi(string Url, string LinkReg, string ConReg, int Interval = 2000)
        {
            this.Url = Url;
            this.LinkReg = LinkReg;
            this.ConReg = ConReg;
            this.Interval = Interval;
            this.IsRead = true;
        }

        /// <summary>
        /// 写入日志委托
        /// </summary>
        /// <param name="msg"></param>
        public delegate void textInvoke(string msg, Color Color);

        /// <summary>
        /// 数量更新委托
        /// </summary>
        /// <param name="num"></param>
        public delegate void UpLabel(int num);

        /// <summary>
        /// 循环执行
        /// </summary>
        public System.Timers.Timer timer = new System.Timers.Timer();

        public textInvoke SetLog;

        public UpLabel UpNum;

        public UpLabel UpCon;

        public bool IsRead { get; set; }

        /// <summary>
        /// 网站链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 超链接正则
        /// </summary>
        public string LinkReg { get; set; }
        /// <summary>
        /// 内容正则
        /// </summary>
        public string ConReg { get; set; }
        /// <summary>
        /// 间隔时间(ms)
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="LinkReg"></param>
        /// <param name="ConReg"></param>
        /// <param name="Interval"></param>
        public void Begin()
        {
            // 设置间隔时间
            timer.Interval = Interval;
            // 设置循环调用
            timer.AutoReset = true;
            // 设置启动事件
            timer.Enabled = true;
            // 设置调用内容
            timer.Elapsed += (s, e) => {
                Timer_Elapsed();
            };
        }

        public void Stop()
        {
            IsRead = false;
            Thread.CurrentThread.Abort();
            SetLog($"已终止线程{Thread.CurrentThread.Name}，线程ID：{Thread.CurrentThread.ManagedThreadId}", Color.LightSalmon);
        }

        GSQ_NewsService _GSQ_NewsService = new GSQ_NewsService();

        private void Timer_Elapsed()
        {
            // 暂停抓取数据
            timer.Enabled = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            // 获取整个HTML文档，以XML格式读取
            XmlDocument doc = Utils.GetUrlXML(Url);


            //LinkReg = "/html/body/section/ul/li/h2/a";
            // 指定读取的节点集合
            XmlNodeList list = doc.SelectNodes(LinkReg);
            int count = 0;

            for (int i = 0; i < list.Count; i++) {

                // 获取标题
                string title = list[i].InnerText;
                // 获取超链接
                string href = "";

                // 循环所有属性，找到超链接
                foreach (XmlAttribute attr in list[i].Attributes) {
                    if (attr.Name.ToLower() == "href") {
                        href = attr.Value;
                        break;
                    }
                }

                // 判断此超链接是否已经读取
                if (_GSQ_NewsService.Exists(c => c.url == href))
                    continue;

                // 根据超链接读取内容
                doc = Utils.GetUrlXML(href);
                //ConReg = "/html/body/main/article/div[@class=\"content\"]";
                string con = doc.SelectSingleNode(ConReg).InnerXml;

                if (!string.IsNullOrEmpty(con)) {
                    GSQ_News _News = new GSQ_News();
                    _News.title = title;
                    _News.url = href;
                    _News.sourcewebsite = con;
                    _News.num = 0;
                    _GSQ_NewsService.AddEntity(_News);
                    UpCon(1);
                    UpNum(1);
                    count++;
                }

            }

            SetLog($"抓取新闻{count}条，用时：{watch.ElapsedMilliseconds} 毫秒", Color.Gray);
            watch.Reset();

            // 开始抓取数据
            timer.Enabled = true;
        }



    }
}
