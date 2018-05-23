using Factory;
using System;
using System.Drawing;
using Interface.DataBase.BLL;
using System.Windows.Forms;
using System.Data.Entity;
using Common;
using Models.CodeFirst;

namespace WinForm {
    public partial class F_Main : Form {
        public F_Main() {
            InitializeComponent();
        }

        private void F_Main_Load(object sender, EventArgs e) {

            IDT_UserService _UserService = DataBaseFactory.CreateService<IDT_UserService>();

            _UserService.BeginTrans();
            _UserService.AddEntity();
            _UserService.Commit();

        }

        #region 主窗体操作

        #region 移动无边框窗体

        /// <summary>
        /// 获取鼠标坐标
        /// </summary>
        Point MouseXY;

        /// <summary>
        /// 判断是否移动窗体
        /// </summary>
        bool ForMov = false;

        private void F_Main_MouseDown(object sender, MouseEventArgs e) {
            // 当按下的是鼠标左键时
            if (e.Button == MouseButtons.Left) {
                // 获取当前鼠标坐标
                MouseXY = new Point(-e.X, -e.Y);
                // 允许拖动
                ForMov = true;
            }
        }

        private void F_Main_MouseMove(object sender, MouseEventArgs e) {
            if (ForMov) {
                if (this.WindowState == FormWindowState.Maximized) {
                    this.WindowState = FormWindowState.Normal;
                    // Location = MouseXY;
                }

                //获取鼠标在屏幕中位置
                Point MouseSet = MousePosition;
                MouseSet.Offset(MouseXY.X, MouseXY.Y);
                //设置当前窗体位置
                Location = MouseSet;
            }
        }

        private void F_Main_MouseUp(object sender, MouseEventArgs e) {
            ForMov = false;
        }
        #endregion

        #region 关闭无边框窗体

        /// <summary>
        /// 关闭按钮鼠标移入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_close_MouseEnter(object sender, EventArgs e) {
            // 父级容器背景颜色改为红色
            bt_close.BackColor = Color.FromArgb(100, 255, 0, 0);
            // 父级容器背景颜色改为白色
            bt_close.ForeColor = Color.FromArgb(100, 255, 255, 255);
        }

        /// <summary>
        /// 关闭按钮鼠标移开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_close_MouseLeave(object sender, EventArgs e) {
            // 父级容器背景颜色透明度改为全透明
            bt_close.BackColor = Color.FromArgb(0, 255, 0, 0);
            // 父级容器背景颜色改为白色
            bt_close.ForeColor = Color.FromArgb(100, 0, 0, 0);
        }

        /// <summary>
        /// 关闭按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_close_MouseUp(object sender, MouseEventArgs e) {
            Application.Exit();//退出系统
        }

        #endregion

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
            MouseXY = new Point(this.Location.X, this.Location.Y);

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