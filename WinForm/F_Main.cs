using Factory;
using System;
using System.Drawing;
using Interface.DataBase.BLL;
using System.Windows.Forms;
using Common.Utils;
using System.Linq;
using System.Runtime.InteropServices;
using CCWin.SkinControl;
using Common;

namespace WinForm {
    public partial class F_Main : BaseForm {

        public F_Main() {
            InitializeComponent();
            bt_max.Hide();

            lb_info_context.MouseDown += Form_MouseDown;
            lb_info_ico.MouseDown += Form_MouseDown;
            lb_sys_ico.MouseDown += Form_MouseDown;
            lb_sys_title.MouseDown += Form_MouseDown;
            tv_menu.MouseDown += Form_MouseDown;
                       
        }


        private void F_Main_Load(object sender, EventArgs e) {
            string AppPath = Application.StartupPath;

            IDT_UserService _UserService = DataBaseFactory.CreateService<IDT_UserService>();

            // 开启事务
            _UserService.BeginTrans();

            // 回调事务
            _UserService.Rollback();


            //   comboBox1.DataSource = WinFont.GetFontList();
            //comboBox1.DisplayMember = "Name";


            // textBox1.SetFontIco(FontFreeIco.Flag, 99);

            WinFont.GetIco();

            //textBox1.Text = "F024";
            //textBox1.Font = new Font("Font Awesome 5 Free", 7);
            //textBox1.ForeColor = Color.Black;

        }

        #region 主窗体操作

        /// <summary>
        /// 关闭按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_close_MouseUp(object sender, MouseEventArgs e) {
            Application.Exit();//退出系统
        }

        /// <summary>
        /// 窗体最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_min_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 窗体最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_max_Click(object sender, EventArgs e) {

            if (this.WindowState == FormWindowState.Maximized) {
                this.WindowState = FormWindowState.Normal;
                // 设置字体大小
                bt_max.Font = new Font(bt_max.Font.FontFamily.Name, 7, FontStyle.Regular);
                bt_max.Text = "□";
                tip.SetToolTip(bt_max, "最大化");
            }
            else {
                this.WindowState = FormWindowState.Maximized;
                // 设置字体大小
                bt_max.Font = new Font(bt_max.Font.FontFamily.Name, 12, FontStyle.Regular);
                bt_max.Text = "❐";
                tip.SetToolTip(bt_max, "恢复");
            }

        }

        #endregion

    }
}