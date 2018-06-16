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
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("节点4", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("节点2");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Main));
            this.pl_sys = new System.Windows.Forms.Panel();
            this.bt_top = new System.Windows.Forms.Button();
            this.bt_min = new System.Windows.Forms.Button();
            this.bt_max = new System.Windows.Forms.Button();
            this.bt_close = new System.Windows.Forms.Button();
            this.lb_sys_title = new System.Windows.Forms.Label();
            this.lb_sys_ico = new System.Windows.Forms.Label();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.pl_info = new System.Windows.Forms.Panel();
            this.lb_info_context = new System.Windows.Forms.Label();
            this.lb_info_ico = new System.Windows.Forms.Label();
            this.tv_menu = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            
            this.pl_sys.SuspendLayout();
            this.pl_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pl_sys
            // 
            this.pl_sys.BackColor = System.Drawing.Color.Transparent;
            this.pl_sys.Controls.Add(this.bt_top);
            this.pl_sys.Controls.Add(this.bt_min);
            this.pl_sys.Controls.Add(this.bt_max);
            this.pl_sys.Controls.Add(this.bt_close);
            this.pl_sys.Controls.Add(this.lb_sys_title);
            this.pl_sys.Controls.Add(this.lb_sys_ico);
            this.pl_sys.Dock = System.Windows.Forms.DockStyle.Top;
            this.pl_sys.Location = new System.Drawing.Point(1, 1);
            this.pl_sys.Name = "pl_sys";
            this.pl_sys.Size = new System.Drawing.Size(998, 30);
            this.pl_sys.TabIndex = 0;
            this.pl_sys.DoubleClick += new System.EventHandler(this.bt_max_Click);
            // 
            // bt_top
            // 
            this.bt_top.BackColor = System.Drawing.Color.Transparent;
            this.bt_top.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_top.FlatAppearance.BorderSize = 0;
            this.bt_top.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.bt_top.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bt_top.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.bt_top.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_top.Font = new System.Drawing.Font("宋体", 7F);
            this.bt_top.ForeColor = System.Drawing.Color.White;
            this.bt_top.Location = new System.Drawing.Point(878, 0);
            this.bt_top.Name = "bt_top";
            this.bt_top.Size = new System.Drawing.Size(30, 30);
            this.bt_top.TabIndex = 5;
            this.bt_top.Text = "_";
            this.tip.SetToolTip(this.bt_top, "置顶");
            this.bt_top.UseVisualStyleBackColor = false;
            // 
            // bt_min
            // 
            this.bt_min.BackColor = System.Drawing.Color.Transparent;
            this.bt_min.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_min.FlatAppearance.BorderSize = 0;
            this.bt_min.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.bt_min.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bt_min.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.bt_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_min.Font = new System.Drawing.Font("宋体", 7F);
            this.bt_min.ForeColor = System.Drawing.Color.White;
            this.bt_min.Location = new System.Drawing.Point(908, 0);
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
            this.bt_max.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.bt_max.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bt_max.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.bt_max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_max.Font = new System.Drawing.Font("宋体", 7F);
            this.bt_max.ForeColor = System.Drawing.Color.White;
            this.bt_max.Location = new System.Drawing.Point(938, 0);
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
            this.bt_close.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.bt_close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.bt_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_close.Font = new System.Drawing.Font("宋体", 10F);
            this.bt_close.ForeColor = System.Drawing.Color.White;
            this.bt_close.Location = new System.Drawing.Point(968, 0);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(30, 30);
            this.bt_close.TabIndex = 0;
            this.bt_close.Text = "✖";
            this.tip.SetToolTip(this.bt_close, "关闭");
            this.bt_close.UseVisualStyleBackColor = false;
            this.bt_close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_close_MouseUp);
            // 
            // lb_sys_title
            // 
            this.lb_sys_title.BackColor = System.Drawing.Color.Transparent;
            this.lb_sys_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_sys_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(129)))), ((int)(((byte)(131)))));
            this.lb_sys_title.Location = new System.Drawing.Point(30, 0);
            this.lb_sys_title.Name = "lb_sys_title";
            this.lb_sys_title.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lb_sys_title.Size = new System.Drawing.Size(968, 30);
            this.lb_sys_title.TabIndex = 4;
            this.lb_sys_title.Text = "首页";
            this.lb_sys_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_sys_ico
            // 
            this.lb_sys_ico.BackColor = System.Drawing.Color.Transparent;
            this.lb_sys_ico.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_sys_ico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(129)))), ((int)(((byte)(131)))));
            this.lb_sys_ico.Location = new System.Drawing.Point(0, 0);
            this.lb_sys_ico.Name = "lb_sys_ico";
            this.lb_sys_ico.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lb_sys_ico.Size = new System.Drawing.Size(30, 30);
            this.lb_sys_ico.TabIndex = 4;
            this.lb_sys_ico.Text = "@";
            this.lb_sys_ico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tip
            // 
            this.tip.AutoPopDelay = 5000;
            this.tip.BackColor = System.Drawing.Color.Gainsboro;
            this.tip.InitialDelay = 100;
            this.tip.ReshowDelay = 100;
            // 
            // pl_info
            // 
            this.pl_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.pl_info.Controls.Add(this.lb_info_context);
            this.pl_info.Controls.Add(this.lb_info_ico);
            this.pl_info.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pl_info.Location = new System.Drawing.Point(1, 567);
            this.pl_info.Name = "pl_info";
            this.pl_info.Size = new System.Drawing.Size(998, 32);
            this.pl_info.TabIndex = 6;
            // 
            // lb_info_context
            // 
            this.lb_info_context.BackColor = System.Drawing.Color.Transparent;
            this.lb_info_context.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_info_context.ForeColor = System.Drawing.Color.White;
            this.lb_info_context.Location = new System.Drawing.Point(30, 0);
            this.lb_info_context.Name = "lb_info_context";
            this.lb_info_context.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lb_info_context.Size = new System.Drawing.Size(968, 32);
            this.lb_info_context.TabIndex = 5;
            this.lb_info_context.Text = "就绪";
            this.lb_info_context.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_info_ico
            // 
            this.lb_info_ico.BackColor = System.Drawing.Color.Transparent;
            this.lb_info_ico.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_info_ico.ForeColor = System.Drawing.Color.White;
            this.lb_info_ico.Location = new System.Drawing.Point(0, 0);
            this.lb_info_ico.Name = "lb_info_ico";
            this.lb_info_ico.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lb_info_ico.Size = new System.Drawing.Size(30, 32);
            this.lb_info_ico.TabIndex = 6;
            this.lb_info_ico.Text = "@";
            this.lb_info_ico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tv_menu
            // 
            this.tv_menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.tv_menu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tv_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_menu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.tv_menu.HotTracking = true;
            this.tv_menu.Indent = 19;
            this.tv_menu.ItemHeight = 17;
            this.tv_menu.LineColor = System.Drawing.Color.Gray;
            this.tv_menu.Location = new System.Drawing.Point(0, 0);
            this.tv_menu.Name = "tv_menu";
            treeNode9.Name = "节点0";
            treeNode9.Text = "节点0";
            treeNode10.Name = "节点3";
            treeNode10.Text = "节点3";
            treeNode11.Name = "节点6";
            treeNode11.Text = "节点6";
            treeNode12.Name = "节点7";
            treeNode12.Text = "节点7";
            treeNode13.Name = "节点4";
            treeNode13.Text = "节点4";
            treeNode14.Name = "节点5";
            treeNode14.Text = "节点5";
            treeNode15.Name = "节点1";
            treeNode15.Text = "节点1";
            treeNode16.Name = "节点2";
            treeNode16.Text = "节点2";
            this.tv_menu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode15,
            treeNode16});
            this.tv_menu.Size = new System.Drawing.Size(332, 536);
            this.tv_menu.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(1, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.tv_menu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(998, 536);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.TabIndex = 9;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
          
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.splitContainer2.Size = new System.Drawing.Size(662, 536);
            this.splitContainer2.SplitterDistance = 220;
            this.splitContainer2.TabIndex = 0;
            
            // 
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pl_sys);
            this.Controls.Add(this.pl_info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_Main";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_Main";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.F_Main_Load);
            this.pl_sys.ResumeLayout(false);
            this.pl_info.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_sys;
        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.Button bt_min;
        private System.Windows.Forms.Button bt_max;
        private System.Windows.Forms.Label lb_sys_ico;
        private System.Windows.Forms.ToolTip tip;
        private System.Windows.Forms.Panel pl_info;
        private System.Windows.Forms.Label lb_sys_title;
        private System.Windows.Forms.Label lb_info_context;
        private System.Windows.Forms.Label lb_info_ico;
        private System.Windows.Forms.TreeView tv_menu;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button bt_top;
    }
}