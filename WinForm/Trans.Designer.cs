namespace WinForm {
    partial class Trans {
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
            this.gb_UrlList = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_Stop = new System.Windows.Forms.Button();
            this.bt_Start = new System.Windows.Forms.Button();
            this.pl_UrlList = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_log = new System.Windows.Forms.RichTextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.gb_UrlList.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pl_UrlList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_UrlList
            // 
            this.gb_UrlList.Controls.Add(this.panel1);
            this.gb_UrlList.Controls.Add(this.pl_UrlList);
            this.gb_UrlList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_UrlList.Location = new System.Drawing.Point(13, 13);
            this.gb_UrlList.Name = "gb_UrlList";
            this.gb_UrlList.Size = new System.Drawing.Size(290, 460);
            this.gb_UrlList.TabIndex = 0;
            this.gb_UrlList.TabStop = false;
            this.gb_UrlList.Text = "网址列表";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.bt_Stop);
            this.panel1.Controls.Add(this.bt_Start);
            this.panel1.Location = new System.Drawing.Point(6, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 48);
            this.panel1.TabIndex = 1;
            // 
            // bt_Stop
            // 
            this.bt_Stop.Cursor = System.Windows.Forms.Cursors.Default;
            this.bt_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_Stop.Location = new System.Drawing.Point(173, 12);
            this.bt_Stop.Name = "bt_Stop";
            this.bt_Stop.Size = new System.Drawing.Size(75, 23);
            this.bt_Stop.TabIndex = 0;
            this.bt_Stop.Text = "停止抓取";
            this.bt_Stop.UseVisualStyleBackColor = true;
            this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // bt_Start
            // 
            this.bt_Start.Cursor = System.Windows.Forms.Cursors.Default;
            this.bt_Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_Start.Location = new System.Drawing.Point(27, 12);
            this.bt_Start.Name = "bt_Start";
            this.bt_Start.Size = new System.Drawing.Size(75, 23);
            this.bt_Start.TabIndex = 0;
            this.bt_Start.Text = "开始抓取";
            this.bt_Start.UseVisualStyleBackColor = true;
            this.bt_Start.Click += new System.EventHandler(this.bt_Start_Click);
            // 
            // pl_UrlList
            // 
            this.pl_UrlList.AutoScroll = true;
            this.pl_UrlList.Controls.Add(this.radioButton1);
            this.pl_UrlList.Location = new System.Drawing.Point(7, 21);
            this.pl_UrlList.Name = "pl_UrlList";
            this.pl_UrlList.Size = new System.Drawing.Size(277, 379);
            this.pl_UrlList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_log);
            this.groupBox1.Location = new System.Drawing.Point(310, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 460);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "抓取消息";
            // 
            // txt_log
            // 
            this.txt_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_log.HideSelection = false;
            this.txt_log.Location = new System.Drawing.Point(3, 17);
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.Size = new System.Drawing.Size(575, 440);
            this.txt_log.TabIndex = 2;
            this.txt_log.Text = "";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(67, 169);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // Trans
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
            this.MaximizeBox = false;
            this.Name = "Trans";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "翻译";
            this.Load += new System.EventHandler(this.F_Main_Load);
            this.gb_UrlList.ResumeLayout(false);
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
        private System.Windows.Forms.Button bt_Stop;
        private System.Windows.Forms.RichTextBox txt_log;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

