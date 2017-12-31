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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.pl_UrlList.Controls.Add(this.comboBox2);
            this.pl_UrlList.Controls.Add(this.comboBox1);
            this.pl_UrlList.Controls.Add(this.label3);
            this.pl_UrlList.Controls.Add(this.label2);
            this.pl_UrlList.Controls.Add(this.label4);
            this.pl_UrlList.Controls.Add(this.label1);
            this.pl_UrlList.Controls.Add(this.textBox2);
            this.pl_UrlList.Controls.Add(this.textBox1);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(152, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "语言包路径";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(26, 100);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(26, 136);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "源语言";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "目标语言";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(26, 43);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(152, 21);
            this.textBox2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "保存路径";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
    }
}

