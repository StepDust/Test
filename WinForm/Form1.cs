using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using BLL;
using BLL.CodeFirst;
using Models;
using System.Threading;

namespace WinForm {
    public partial class F_Main : Form {
        public F_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 写入日志委托
        /// </summary>
        /// <param name="msg"></param>
        private delegate void textInvoke(string msg, Color Color);

        GSQ_WebService _GSQ_WebService = new GSQ_WebService();

        private void F_Main_Load(object sender, EventArgs e)
        {
            List<GSQ_Web> list = _GSQ_WebService.LoadEntities(c => true).ToList();
            for (int i = 0; i < list.Count; i++) {
                CheckBox box = new CheckBox();
                box.Name = "cb_" + list[i].Id;
                box.Tag = list[i].Url;
                box.Text = list[i].name;

                // 位置
                box.Padding = new Padding(3);
                box.AutoSize = true;
                box.Width = 35;
                box.Location = new Point(10, 25 * i);
                pl_UrlList.Controls.Add(box);
            }
        }

        private void bt_Start_Click(object sender, EventArgs e)
        {
            CheckBox temp;
            Crawler crawler = new Crawler();

            foreach (Control item in pl_UrlList.Controls) {
                if (item is CheckBox) {
                    temp = item as CheckBox;
                    if (temp.Checked) {
                        //Thread thread = new Thread(null);
                    }
                }

            }



        }


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

    }
}
