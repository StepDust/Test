namespace WinForm {
    partial class F_Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_top = new System.Windows.Forms.Button();
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_min = new System.Windows.Forms.Button();
            this.bt_max = new System.Windows.Forms.Button();
            this.bt_close = new System.Windows.Forms.Button();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.bt_top);
            this.panel1.Controls.Add(this.lb_title);
            this.panel1.Controls.Add(this.bt_min);
            this.panel1.Controls.Add(this.bt_max);
            this.panel1.Controls.Add(this.bt_close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 30);
            this.panel1.TabIndex = 0;
            this.panel1.DoubleClick += new System.EventHandler(this.bt_max_Click);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.F_Main_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.F_Main_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.F_Main_MouseUp);
            // 
            // bt_top
            // 
            this.bt_top.BackColor = System.Drawing.Color.Transparent;
            this.bt_top.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_top.FlatAppearance.BorderSize = 0;
            this.bt_top.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_top.Font = new System.Drawing.Font("宋体", 7F);
            this.bt_top.Location = new System.Drawing.Point(747, 0);
            this.bt_top.Name = "bt_top";
            this.bt_top.Size = new System.Drawing.Size(30, 30);
            this.bt_top.TabIndex = 5;
            this.bt_top.Text = "_";
            this.tip.SetToolTip(this.bt_top, "最小化");
            this.bt_top.UseVisualStyleBackColor = false;
            // 
            // lb_title
            // 
            this.lb_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_title.Location = new System.Drawing.Point(0, 0);
            this.lb_title.Name = "lb_title";
            this.lb_title.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lb_title.Size = new System.Drawing.Size(777, 30);
            this.lb_title.TabIndex = 4;
            this.lb_title.Text = "API文档管理系统";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_min
            // 
            this.bt_min.BackColor = System.Drawing.Color.Transparent;
            this.bt_min.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_min.FlatAppearance.BorderSize = 0;
            this.bt_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_min.Font = new System.Drawing.Font("宋体", 7F);
            this.bt_min.Location = new System.Drawing.Point(777, 0);
            this.bt_min.Name = "bt_min";
            this.bt_min.Size = new System.Drawing.Size(30, 30);
            this.bt_min.TabIndex = 3;
            this.bt_min.Text = "_";
            this.tip.SetToolTip(this.bt_min, "最小化");
            this.bt_min.UseVisualStyleBackColor = false;
            this.bt_min.Click += new System.EventHandler(this.bt_min_Click);
            // 
            // bt_max
            // 
            this.bt_max.BackColor = System.Drawing.Color.Transparent;
            this.bt_max.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_max.FlatAppearance.BorderSize = 0;
            this.bt_max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_max.Font = new System.Drawing.Font("宋体", 7F);
            this.bt_max.Location = new System.Drawing.Point(807, 0);
            this.bt_max.Name = "bt_max";
            this.bt_max.Size = new System.Drawing.Size(30, 30);
            this.bt_max.TabIndex = 2;
            this.bt_max.Text = "□";
            this.tip.SetToolTip(this.bt_max, "最大化");
            this.bt_max.UseVisualStyleBackColor = false;
            this.bt_max.Click += new System.EventHandler(this.bt_max_Click);
            // 
            // bt_close
            // 
            this.bt_close.BackColor = System.Drawing.Color.Transparent;
            this.bt_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_close.FlatAppearance.BorderSize = 0;
            this.bt_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_close.Font = new System.Drawing.Font("宋体", 10F);
            this.bt_close.Location = new System.Drawing.Point(837, 0);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(30, 30);
            this.bt_close.TabIndex = 0;
            this.bt_close.Text = "✖";
            this.tip.SetToolTip(this.bt_close, "关闭");
            this.bt_close.UseVisualStyleBackColor = false;
            this.bt_close.MouseEnter += new System.EventHandler(this.bt_close_MouseEnter);
            this.bt_close.MouseLeave += new System.EventHandler(this.bt_close_MouseLeave);
            this.bt_close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_close_MouseUp);
            // 
            // tip
            // 
            this.tip.AutoPopDelay = 5000;
            this.tip.BackColor = System.Drawing.Color.Gainsboro;
            this.tip.InitialDelay = 100;
            this.tip.ReshowDelay = 100;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(316, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(316, 126);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(321, 91);
            this.textBox1.TabIndex = 2;
            // 
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 420);
            this.ControlBox = false;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_Main";
            this.Load += new System.EventHandler(this.F_Main_Load);
            this.DoubleClick += new System.EventHandler(this.bt_max_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.F_Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.F_Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.F_Main_MouseUp);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.Button bt_min;
        private System.Windows.Forms.Button bt_max;
        private System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.ToolTip tip;
        private System.Windows.Forms.Button bt_top;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}