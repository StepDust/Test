using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common;
using Models;
using System.Threading;
using WinForm.WebUrl;
using System.IO;
using BLL.GSQ_PaChong;

namespace WinForm {
    public partial class F_Main : Form {
        public F_Main()
        {
            InitializeComponent();
        }

        #region 写入日志

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
                this.Invoke(ti, msg, Color);
            }
            else {
                msg += "\n";
                msg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff") + "\t" + msg;

                txt_log.SelectionColor = Color;

                txt_log.AppendText(msg);

            }
        }

        /// <summary>
        /// 写入日志文本
        /// </summary>
        /// <param name="context">内容</param>
        public void SetLogTxt(string context)
        {
            string path = Path.GetFullPath($"../../LogTxt/{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
            FileAction.AppendStr(path, context);
        }

        /// <summary>
        /// 清空日志显示框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_clear_Click(object sender, EventArgs e)
        {
            SetLogTxt(txt_log.Text);
            txt_log.Text = "";
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_log.Text))
                SetLogTxt(txt_log.Text);
            SetLogTxt($"" +
                "==========>\n" +
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff") + "\t关闭窗体！\n" +
                $"\t\t\t\t\t\t\t运行时间：{lb_time.Text}\t抓取数量：{lb_num.Text}\t总数量：{lb_con.Text}\n" +
                "==========>\n");
            Environment.Exit(0);
        }

        #endregion

        #region 更新数量

        /// <summary>
        /// 更新新闻抓取数量
        /// </summary>
        /// <param name="num"></param>
        public delegate void UpLabel(int num);

        /// <summary>
        /// 更新抓取数量
        /// </summary>
        /// <param name="num"></param>
        public void UpNum(int num)
        {
            if (lb_num.InvokeRequired) {
                UpLabel up = new UpLabel(UpNum);
                this.Invoke(up, num);
            }
            else {
                int n = 0;
                string text = lb_num.Text;
                int.TryParse(text.Substring(0, text.Length - 2), out n);
                n += num;
                lb_num.Text = n + " 个";
            }
        }

        /// <summary>
        /// 更新全部数量
        /// </summary>
        /// <param name="num"></param>
        public void UpCon(int num)
        {
            if (lb_num.InvokeRequired) {
                UpLabel up = new UpLabel(UpCon);
                this.Invoke(up, num);
            }
            else {
                int n = 0;
                string text = lb_con.Text;
                int.TryParse(text.Substring(0, text.Length - 2), out n);
                n += num;
                lb_con.Text = n + " 个";
            }
        }

        #endregion

        #region 运行时间

        int TimeCon = 0;

        private void t_con_Tick(object sender, EventArgs e)
        {
            TimeCon++;
            TimeSpan ts = new TimeSpan(0, 0, TimeCon);
            lb_time.Text = "";
            if ((int)ts.TotalHours > 0)
                lb_time.Text += (int)ts.TotalHours + " 时 ";
            if (ts.Minutes > 0 || (int)ts.TotalHours > 0)
                lb_time.Text += ts.Minutes + " 分 ";
            if (ts.Seconds > 0 || ts.Minutes > 0 || (int)ts.TotalHours > 0)
                lb_time.Text += ts.Seconds + " 秒";
        }

        #endregion

        #region 复选框加载&&联动

        GSQ_WebService _GSQ_WebService = new GSQ_WebService();
        GSQ_NewsService _GSQ_NewsService = new GSQ_NewsService();

        private void F_Main_Load(object sender, EventArgs e)
        {
            lb_con.Text = _GSQ_NewsService.LoadEntities(c => true).Count() + " 个";

            List<GSQ_Web> list = _GSQ_WebService.LoadEntities(c => true).ToList();
            for (int i = 0; i < list.Count; i++) {
                CheckBox box = new CheckBox();
                box.Name = "cb_" + list[i].Id;
                box.Tag = list[i].Url + "\t" + list[i].name;
                box.Text = list[i].name;

                // 位置
                box.Padding = new Padding(3);
                box.AutoSize = true;
                box.Width = 35;
                box.Location = new Point(10, 25 * i);
                box.Click += Box_CheckedChanged;
                pl_UrlList.Controls.Add(box);
            }
        }

        private void Box_CheckedChanged(object sender, EventArgs e)
        {
            bool check = (sender as CheckBox).Checked;

            if (check == false) {
                cb_all.Checked = false;
                return;
            }
            else
                foreach (Control Control in pl_UrlList.Controls) {
                    if (Control is CheckBox)
                        if ((Control as CheckBox).Checked != check) {
                            cb_all.Checked = false;
                            return;
                        }
                }
            cb_all.Checked = true;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            bool check = (sender as CheckBox).Checked;

            foreach (Control Control in pl_UrlList.Controls) {
                if (Control is CheckBox)
                    (Control as CheckBox).Checked = check;
            }
        }

        #endregion


        List<Thread> threadList = new List<Thread>();

        private void bt_Start_Click(object sender, EventArgs e)
        {
            List<GSQ_Web> list = _GSQ_WebService.LoadEntities(c => true).ToList();

            CheckBox check;
            GSQ_Web url;

            cb_all.Enabled = false;
            bt_Start.Enabled = false;
            foreach (Control Control in pl_UrlList.Controls) {
                // 是否为多选框
                if (Control is CheckBox) {
                    check = Control as CheckBox;
                    check.Enabled = false;
                    // 是否选中
                    if (check != null && check.Checked) {
                        url = list.Where(c => c.name == check.Text).FirstOrDefault();
                        // 是否存在此网站
                        if (url != null) {
                            if (check.Text.Contains("网易")) {
                                Web_WangYi wy = new Web_WangYi(url.Url, url.Reglx, url.ReglxContent);

                                // 委托方法
                                wy.SetLog += new Web_WangYi.textInvoke(SetLog);
                                wy.UpNum += new Web_WangYi.UpLabel(UpNum);
                                wy.UpCon += new Web_WangYi.UpLabel(UpCon);

                                Thread t1 = new Thread(new ThreadStart(wy.Begin));
                                t1.Name = url.name;

                                threadList.Add(t1);
                                t1.Start();
                                SetLog($"启动抓取：{url.name}（{url.Url}）", Color.Black);
                            }
                        }
                    }
                }
                check = null;
                url = null;
            }

            bt_Start.Enabled = true;
        }


        private void bt_Stop_Click(object sender, EventArgs e)
        {
            bt_Start.Enabled = true;
            cb_all.Enabled = true;
            CheckBox temp;
            foreach (Control item in pl_UrlList.Controls) {
                if (item is CheckBox) {
                    temp = item as CheckBox;
                    temp.Enabled = true;
                }
            }

            //while (threadList.Count > 0) {
            //    Web_WangYi t = threadList[0];
            //    try {
            //        threadList.RemoveAt(0);

            //        t.Stop();
            //        //SetLog($"已停止线程：{ t.Name}", Color.LightSalmon);
            //    }
            //    catch (Exception ex) {
            //        SetLog($"已停止线程：{ t.LinkReg}失败：{ex.Message}", Color.Red);
            //    }
            //}
        }



    }
}
