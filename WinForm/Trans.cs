using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common;
using Models;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace WinForm {
    public partial class Trans : Form {
        public Trans()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 写入日志委托
        /// </summary>
        /// <param name="msg"></param>
        private delegate void textInvoke(string msg, Color Color);

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="Color">文字颜色</param>
        private void SetLog(string msg, Color Color)
        {
            // 是否在其它线程调用此控件
            if (txt_log.InvokeRequired) {
                textInvoke ti = new textInvoke(SetLog);
                this.Invoke(ti, new { msg, Color });
            }
            else {
                msg += "\n";
                msg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff") + "\t" + msg;

                txt_log.SelectionColor = Color;

                txt_log.AppendText(msg);

            }
        }

     

        private void F_Main_Load(object sender, EventArgs e)
        {
            //List<GSQ_Web> list = _GSQ_WebService.LoadEntities(c => true).ToList();
            //for (int i = 0; i < list.Count; i++) {
            //    CheckBox box = new CheckBox();
            //    box.Name = "cb_" + list[i].Id;
            //    box.Tag = list[i].Url;
            //    box.Text = list[i].name;

            //    // 位置
            //    box.Padding = new Padding(3);
            //    box.AutoSize = true;
            //    box.Width = 35;
            //    box.Location = new Point(10, 25 * i);
            //    pl_UrlList.Controls.Add(box);
            //}

        }

        private void bt_Start_Click(object sender, EventArgs e)
        {
            //CheckBox temp;
            //Crawler crawler = new Crawler();

            //crawler.OnCompleted += Crawler_OnCompleted;
            //crawler.OnError += Crawler_OnError;


            //foreach (Control item in pl_UrlList.Controls) {
            //    if (item is CheckBox) {
            //        temp = item as CheckBox;
            //        if (temp.Checked) {

            //            SetLog("开始爬取：" + temp.Text, Color.Black);
            //            crawler.Start(temp.Tag + "", "<a[^>]+?href=[\"']?http://3g.163.com/all/article/([^\"']+)[\"']?[^>]*>(.+?)</a>").Wait();
            //        }
            //        temp.Enabled = true;
            //    }

            //}
            //SetLog("结束！", Color.OrangeRed);

            #region 翻译

            string ReadPath = @"F:\01.Porject\NanNing\050Coding\New_NaNing_SSC\WebFront\App_Data\Localization\en\orchard.module.po";
            string SavePath = @"F:\orchard.module.po";

            string content = FileAction.ReadToStr(ReadPath);


            string reg = "msgid \"(?<msgid>.*)\" \r\nmsgstr \"(?<msgstr>.*)\"";

            string[] strArr = DataCheck.GetRegStrArr(content, reg);
            Translate translate = new Translate("20171116000095832", "PuVyBlMqMOOjqfks4GJ7");

            // 记录运行时间
            Stopwatch watch = new Stopwatch();

            int con = 0;

            foreach (var str in strArr) {

                SetLog("开始翻译！", Color.Black);

                watch.Start();

                MatchCollection res = Regex.Matches(str, reg, RegexOptions.IgnoreCase);
                foreach (Match item in res) {
                  //  BaiduTransAPI transAPI = translate.BaiduTranslate(item.Groups["msgid"].Value, "zh", "en");

                    string context = "" +
                        "msgid \"" + item.Groups["msgid"].Value + "\"\n" +
                        "msgstr \"" + Utils.StrToUpper(item.Groups["msgstr"].Value, 2) + "\"\n\n";

                    FileAction.AppendStr(SavePath, context);
                }
                SetLog("翻译结束，用时：" + watch.ElapsedMilliseconds*0.1/1000 + " s，第"+(++con)+"条", Color.Brown);
                watch.Stop();
            }
            
            #endregion

            bt_Start.Enabled = true;
        }

        private void Crawler_OnError(object sender, Exception e)
        {
            SetLog("抓取失败：" + e.TargetSite, Color.Red);
        }

        private void Crawler_OnCompleted(object sender, OnCompletedEventArgs e)
        {
            string[] str = DataCheck.GetRegStrArr(e.PageSource, e.Regex);
            SetLog("抓取新闻数量：" + str.Length + "\t用时：" + e.Milliseconds, Color.Black);
        }




        private void bt_Stop_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            bt_Start.Enabled = true;
            CheckBox temp;
            foreach (Control item in pl_UrlList.Controls) {
                if (item is CheckBox) {
                    temp = item as CheckBox;
                    temp.Enabled = true;
                }

            }
        }
    }
}
