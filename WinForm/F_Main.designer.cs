namespace WinForm {
    partial class F_Main {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Main));
            this.gb_UrlList = new System.Windows.Forms.GroupBox();
            this.pl_info = new System.Windows.Forms.Panel();
            this.lb_all = new System.Windows.Forms.Label();
            this.cb_all = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_con = new System.Windows.Forms.Label();
            this.lb_num = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_time = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_clear = new System.Windows.Forms.Button();
            this.bt_send = new System.Windows.Forms.Button();
            this.bt_Start = new System.Windows.Forms.Button();
            this.pl_UrlList = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_log = new System.Windows.Forms.RichTextBox();
            this.t_con = new System.Windows.Forms.Timer(this.components);
            this.t_clear = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gb_UrlList.SuspendLayout();
            this.pl_info.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pl_UrlList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_UrlList
            // 
            this.gb_UrlList.Controls.Add(this.pl_info);
            this.gb_UrlList.Controls.Add(this.panel1);
            this.gb_UrlList.Controls.Add(this.pl_UrlList);
            this.gb_UrlList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gb_UrlList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_UrlList.Location = new System.Drawing.Point(13, 13);
            this.gb_UrlList.Name = "gb_UrlList";
            this.gb_UrlList.Size = new System.Drawing.Size(290, 460);
            this.gb_UrlList.TabIndex = 0;
            this.gb_UrlList.TabStop = false;
            this.gb_UrlList.Text = "网址列表";
            // 
            // pl_info
            // 
            this.pl_info.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pl_info.Controls.Add(this.lb_all);
            this.pl_info.Controls.Add(this.cb_all);
            this.pl_info.Controls.Add(this.label3);
            this.pl_info.Controls.Add(this.lb_con);
            this.pl_info.Controls.Add(this.lb_num);
            this.pl_info.Controls.Add(this.label2);
            this.pl_info.Controls.Add(this.lb_time);
            this.pl_info.Controls.Add(this.label1);
            this.pl_info.Cursor = System.Windows.Forms.Cursors.Default;
            this.pl_info.Location = new System.Drawing.Point(7, 342);
            this.pl_info.Name = "pl_info";
            this.pl_info.Size = new System.Drawing.Size(277, 68);
            this.pl_info.TabIndex = 2;
            // 
            // lb_all
            // 
            this.lb_all.AutoSize = true;
            this.lb_all.Location = new System.Drawing.Point(192, 27);
            this.lb_all.Name = "lb_all";
            this.lb_all.Size = new System.Drawing.Size(47, 12);
            this.lb_all.TabIndex = 6;
            this.lb_all.Text = "共 0 个";
            // 
            // cb_all
            // 
            this.cb_all.AutoSize = true;
            this.cb_all.Cursor = System.Windows.Forms.Cursors.Default;
            this.cb_all.Location = new System.Drawing.Point(191, 49);
            this.cb_all.Name = "cb_all";
            this.cb_all.Size = new System.Drawing.Size(48, 16);
            this.cb_all.TabIndex = 5;
            this.cb_all.Text = "全选";
            this.cb_all.UseVisualStyleBackColor = true;
            this.cb_all.Click += new System.EventHandler(this.cb_all_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Location = new System.Drawing.Point(7, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "总 数 量：";
            // 
            // lb_con
            // 
            this.lb_con.AutoSize = true;
            this.lb_con.Cursor = System.Windows.Forms.Cursors.Default;
            this.lb_con.Location = new System.Drawing.Point(78, 50);
            this.lb_con.Name = "lb_con";
            this.lb_con.Size = new System.Drawing.Size(29, 12);
            this.lb_con.TabIndex = 3;
            this.lb_con.Text = "0 个";
            // 
            // lb_num
            // 
            this.lb_num.AutoSize = true;
            this.lb_num.Cursor = System.Windows.Forms.Cursors.Default;
            this.lb_num.Location = new System.Drawing.Point(78, 27);
            this.lb_num.Name = "lb_num";
            this.lb_num.Size = new System.Drawing.Size(29, 12);
            this.lb_num.TabIndex = 3;
            this.lb_num.Text = "0 个";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Location = new System.Drawing.Point(7, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "抓取数量：";
            // 
            // lb_time
            // 
            this.lb_time.AutoSize = true;
            this.lb_time.Cursor = System.Windows.Forms.Cursors.Default;
            this.lb_time.Location = new System.Drawing.Point(78, 4);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(29, 12);
            this.lb_time.TabIndex = 1;
            this.lb_time.Text = "0 秒";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "运行时间：";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.bt_clear);
            this.panel1.Controls.Add(this.bt_Start);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Location = new System.Drawing.Point(6, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 38);
            this.panel1.TabIndex = 1;
            // 
            // bt_clear
            // 
            this.bt_clear.Cursor = System.Windows.Forms.Cursors.Default;
            this.bt_clear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_clear.Location = new System.Drawing.Point(10, 7);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(75, 23);
            this.bt_clear.TabIndex = 0;
            this.bt_clear.Text = "清除日志";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // bt_send
            // 
            this.bt_send.Cursor = System.Windows.Forms.Cursors.Default;
            this.bt_send.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_send.Location = new System.Drawing.Point(191, 289);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(75, 23);
            this.bt_send.TabIndex = 0;
            this.bt_send.Text = "发送消息";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // bt_Start
            // 
            this.bt_Start.Cursor = System.Windows.Forms.Cursors.Default;
            this.bt_Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_Start.Location = new System.Drawing.Point(192, 7);
            this.bt_Start.Name = "bt_Start";
            this.bt_Start.Size = new System.Drawing.Size(75, 23);
            this.bt_Start.TabIndex = 0;
            this.bt_Start.Text = "开启服务";
            this.bt_Start.UseVisualStyleBackColor = true;
            this.bt_Start.Click += new System.EventHandler(this.bt_Start_Click);
            // 
            // pl_UrlList
            // 
            this.pl_UrlList.AutoScroll = true;
            this.pl_UrlList.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pl_UrlList.Controls.Add(this.textBox1);
            this.pl_UrlList.Controls.Add(this.bt_send);
            this.pl_UrlList.Cursor = System.Windows.Forms.Cursors.Default;
            this.pl_UrlList.Location = new System.Drawing.Point(7, 21);
            this.pl_UrlList.Name = "pl_UrlList";
            this.pl_UrlList.Size = new System.Drawing.Size(277, 315);
            this.pl_UrlList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_log);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Location = new System.Drawing.Point(310, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 460);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志";
            // 
            // txt_log
            // 
            this.txt_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_log.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_log.HideSelection = false;
            this.txt_log.Location = new System.Drawing.Point(3, 17);
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.Size = new System.Drawing.Size(575, 440);
            this.txt_log.TabIndex = 2;
            this.txt_log.Text = "";
            this.txt_log.TextChanged += new System.EventHandler(this.txt_log_TextChanged);
            // 
            // t_con
            // 
            this.t_con.Enabled = true;
            this.t_con.Interval = 1000;
            this.t_con.Tick += new System.EventHandler(this.t_con_Tick);
            // 
            // t_clear
            // 
            this.t_clear.Enabled = true;
            this.t_clear.Interval = 3600000;
            this.t_clear.Tick += new System.EventHandler(this.t_clear_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 291);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(176, 21);
            this.textBox1.TabIndex = 0;
            // 
            // F_Main
            // 
            this.AcceptButton = this.bt_Start;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(903, 485);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_UrlList);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "F_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据爬虫";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_Main_FormClosing);
            this.Load += new System.EventHandler(this.F_Main_Load);
            this.gb_UrlList.ResumeLayout(false);
            this.pl_info.ResumeLayout(false);
            this.pl_info.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pl_UrlList.ResumeLayout(false);
            this.pl_UrlList.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_UrlList;
        private System.Windows.Forms.Panel pl_UrlList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_Start;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.RichTextBox txt_log;
        private System.Windows.Forms.Panel pl_info;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_con;
        private System.Windows.Forms.Label lb_num;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_clear;
        private System.Windows.Forms.CheckBox cb_all;
        private System.Windows.Forms.Timer t_con;
        private System.Windows.Forms.Label lb_all;
        private System.Windows.Forms.Timer t_clear;
        private System.Windows.Forms.TextBox textBox1;
    }
}

