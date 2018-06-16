using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinForm {
    /// <summary>
    /// 父窗体类
    /// Date：2018-05-28 21:07:26
    /// </summary>
    public class BaseForm : Form {

        public BaseForm() {
            //SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            this.Location = new System.Drawing.Point(500, 500);
            this.MouseMove += Form_MouseDown;
            this.Paint += Form_Paint;
            //this.ResizeEnd += Form_Paint;
        }


        #region 窗体阴影

        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion

        #region 窗体移动

        //窗体移动API
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;

        protected void Form_MouseDown(object sender, MouseEventArgs e) {
            ReleaseCapture();

            Control control = (sender as Control);

            while (!(control is Form)) control = control.Parent;

            SendMessage(control.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

        private void Form_Paint(object sender, PaintEventArgs e) {

            Color color = Color.FromArgb(100, 202, 81, 0);

            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                                        color, 1, ButtonBorderStyle.Solid,
                                        color, 1, ButtonBorderStyle.Solid,
                                        color, 1, ButtonBorderStyle.Solid,
                                        color, 1, ButtonBorderStyle.Solid);
        }
    }
}
