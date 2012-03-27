namespace datagridview
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLogIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSignOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRef = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSignIn = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.הזמנותToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRefreash = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOperate = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSaveTbl = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRead = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCheckTower = new System.Windows.Forms.ToolStripMenuItem();
            this.tabin = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabout = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabcount = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewAll = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabcount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "my-reports.png");
            this.imageList1.Images.SetKeyName(1, "cart.png");
            this.imageList1.Images.SetKeyName(2, "lorry.png");
            this.imageList1.Images.SetKeyName(3, "my-profile.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLogIn,
            this.toolStripButtonSignOut,
            this.toolStripButtonRef,
            this.toolStripButtonOrder,
            this.toolStripButtonSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1246, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonLogIn
            // 
            this.toolStripButtonLogIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLogIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLogIn.Image")));
            this.toolStripButtonLogIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLogIn.Name = "toolStripButtonLogIn";
            this.toolStripButtonLogIn.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLogIn.Text = "כניסה למערכת";
            this.toolStripButtonLogIn.Visible = false;
            this.toolStripButtonLogIn.Click += new System.EventHandler(this.toolStripButtonLogIn_Click);
            // 
            // toolStripButtonSignOut
            // 
            this.toolStripButtonSignOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSignOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSignOut.Image")));
            this.toolStripButtonSignOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSignOut.Name = "toolStripButtonSignOut";
            this.toolStripButtonSignOut.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSignOut.Text = "נעילת מערכת";
            this.toolStripButtonSignOut.Visible = false;
            this.toolStripButtonSignOut.Click += new System.EventHandler(this.toolStripButtonSignOut_Click);
            // 
            // toolStripButtonRef
            // 
            this.toolStripButtonRef.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRef.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRef.Image")));
            this.toolStripButtonRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRef.Name = "toolStripButtonRef";
            this.toolStripButtonRef.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRef.Text = "רענון";
            this.toolStripButtonRef.Click += new System.EventHandler(this.toolStripButtonRef_Click);
            // 
            // toolStripButtonOrder
            // 
            this.toolStripButtonOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOrder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOrder.Image")));
            this.toolStripButtonOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOrder.Name = "toolStripButtonOrder";
            this.toolStripButtonOrder.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonOrder.Text = "הבא פריט";
            this.toolStripButtonOrder.Click += new System.EventHandler(this.toolStripButtonOrder_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSave.Text = "שמור טבלה לאקסל";
            this.toolStripButtonSave.Visible = false;
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemTools,
            this.הזמנותToolStripMenuItem,
            this.ToolStripMenuItemFile,
            this.aboutToolStripMenuItem,
            this.ToolStripMenuItemCheckTower});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1246, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemTools
            // 
            this.ToolStripMenuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSignIn,
            this.ToolStripMenuItemSignOut,
            this.ToolStripMenuItemExit});
            this.ToolStripMenuItemTools.Name = "ToolStripMenuItemTools";
            this.ToolStripMenuItemTools.Size = new System.Drawing.Size(44, 20);
            this.ToolStripMenuItemTools.Text = "כלים";
            // 
            // ToolStripMenuItemSignIn
            // 
            this.ToolStripMenuItemSignIn.Image = global::datagridview.Properties.Resources.my_profile;
            this.ToolStripMenuItemSignIn.Name = "ToolStripMenuItemSignIn";
            this.ToolStripMenuItemSignIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ToolStripMenuItemSignIn.Size = new System.Drawing.Size(213, 22);
            this.ToolStripMenuItemSignIn.Text = "כניסה למערכת";
            this.ToolStripMenuItemSignIn.Click += new System.EventHandler(this.ToolStripMenuItemSignIn_Click);
            // 
            // ToolStripMenuItemSignOut
            // 
            this.ToolStripMenuItemSignOut.Image = global::datagridview.Properties.Resources.lock1;
            this.ToolStripMenuItemSignOut.Name = "ToolStripMenuItemSignOut";
            this.ToolStripMenuItemSignOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ToolStripMenuItemSignOut.Size = new System.Drawing.Size(213, 22);
            this.ToolStripMenuItemSignOut.Text = "נעילת מערכת";
            this.ToolStripMenuItemSignOut.Click += new System.EventHandler(this.ToolStripMenuItemSignOut_Click);
            // 
            // ToolStripMenuItemExit
            // 
            this.ToolStripMenuItemExit.Image = global::datagridview.Properties.Resources.cancel_48;
            this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            this.ToolStripMenuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ToolStripMenuItemExit.Size = new System.Drawing.Size(213, 22);
            this.ToolStripMenuItemExit.Text = "סגירת האפליקציה";
            this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
            // 
            // הזמנותToolStripMenuItem
            // 
            this.הזמנותToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemRefreash,
            this.ToolStripMenuItemOperate});
            this.הזמנותToolStripMenuItem.Name = "הזמנותToolStripMenuItem";
            this.הזמנותToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.הזמנותToolStripMenuItem.Text = "הזמנות";
            // 
            // ToolStripMenuItemRefreash
            // 
            this.ToolStripMenuItemRefreash.Image = global::datagridview.Properties.Resources.database_refresh;
            this.ToolStripMenuItemRefreash.Name = "ToolStripMenuItemRefreash";
            this.ToolStripMenuItemRefreash.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ToolStripMenuItemRefreash.Size = new System.Drawing.Size(183, 22);
            this.ToolStripMenuItemRefreash.Text = "רענן הזמנות";
            this.ToolStripMenuItemRefreash.Click += new System.EventHandler(this.ToolStripMenuItemRefreash_Click);
            // 
            // ToolStripMenuItemOperate
            // 
            this.ToolStripMenuItemOperate.Image = global::datagridview.Properties.Resources.shopping;
            this.ToolStripMenuItemOperate.Name = "ToolStripMenuItemOperate";
            this.ToolStripMenuItemOperate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.ToolStripMenuItemOperate.Size = new System.Drawing.Size(183, 22);
            this.ToolStripMenuItemOperate.Text = "הבא פריט";
            this.ToolStripMenuItemOperate.Click += new System.EventHandler(this.ToolStripMenuItemOperate_Click);
            // 
            // ToolStripMenuItemFile
            // 
            this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSaveTbl,
            this.ToolStripMenuItemRead});
            this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            this.ToolStripMenuItemFile.Size = new System.Drawing.Size(46, 20);
            this.ToolStripMenuItemFile.Text = "קובץ";
            this.ToolStripMenuItemFile.Visible = false;
            // 
            // ToolStripMenuItemSaveTbl
            // 
            this.ToolStripMenuItemSaveTbl.Image = global::datagridview.Properties.Resources.database_save;
            this.ToolStripMenuItemSaveTbl.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ToolStripMenuItemSaveTbl.Name = "ToolStripMenuItemSaveTbl";
            this.ToolStripMenuItemSaveTbl.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ToolStripMenuItemSaveTbl.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItemSaveTbl.Text = "שמירת הטבלה";
            this.ToolStripMenuItemSaveTbl.Click += new System.EventHandler(this.ToolStripMenuItemSaveTbl_Click);
            // 
            // ToolStripMenuItemRead
            // 
            this.ToolStripMenuItemRead.Name = "ToolStripMenuItemRead";
            this.ToolStripMenuItemRead.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.ToolStripMenuItemRead.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItemRead.Text = "קריאה מקובץ";
            this.ToolStripMenuItemRead.Click += new System.EventHandler(this.ToolStripMenuItemRead_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "המוצר";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemCheckTower
            // 
            this.ToolStripMenuItemCheckTower.Name = "ToolStripMenuItemCheckTower";
            this.ToolStripMenuItemCheckTower.Size = new System.Drawing.Size(90, 20);
            this.ToolStripMenuItemCheckTower.Text = "בדיקת מגדל";
            this.ToolStripMenuItemCheckTower.Click += new System.EventHandler(this.ToolStripMenuItemCheckTower_Click);
            // 
            // tabin
            // 
            this.tabin.Controls.Add(this.dataGridView2);
            this.tabin.ImageIndex = 2;
            this.tabin.Location = new System.Drawing.Point(4, 26);
            this.tabin.Name = "tabin";
            this.tabin.Padding = new System.Windows.Forms.Padding(3);
            this.tabin.Size = new System.Drawing.Size(1192, 463);
            this.tabin.TabIndex = 1;
            this.tabin.Text = "הכנסה למלאי";
            this.tabin.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView2.Location = new System.Drawing.Point(23, 21);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView2.Size = new System.Drawing.Size(1163, 436);
            this.dataGridView2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabout);
            this.tabControl1.Controls.Add(this.tabin);
            this.tabControl1.Controls.Add(this.tabcount);
            this.tabControl1.Font = new System.Drawing.Font("Miriam", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(12, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1200, 493);
            this.tabControl1.TabIndex = 0;
            // 
            // tabout
            // 
            this.tabout.Controls.Add(this.dataGridView1);
            this.tabout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabout.ImageIndex = 1;
            this.tabout.Location = new System.Drawing.Point(4, 26);
            this.tabout.Name = "tabout";
            this.tabout.Padding = new System.Windows.Forms.Padding(3);
            this.tabout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabout.Size = new System.Drawing.Size(1192, 463);
            this.tabout.TabIndex = 0;
            this.tabout.Text = "ניפוק סחורה";
            this.tabout.UseVisualStyleBackColor = true;
            this.tabout.Click += new System.EventHandler(this.tabout_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.Location = new System.Drawing.Point(16, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(1170, 441);
            this.dataGridView1.TabIndex = 1;
            // 
            // tabcount
            // 
            this.tabcount.Controls.Add(this.dataGridView3);
            this.tabcount.ImageIndex = 0;
            this.tabcount.Location = new System.Drawing.Point(4, 26);
            this.tabcount.Name = "tabcount";
            this.tabcount.Size = new System.Drawing.Size(492, 463);
            this.tabcount.TabIndex = 2;
            this.tabcount.Text = "ספירת מלאי";
            this.tabcount.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView3.Location = new System.Drawing.Point(7, 7);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView3.Size = new System.Drawing.Size(1383, 480);
            this.dataGridView3.TabIndex = 2;
            // 
            // dataGridViewAll
            // 
            this.dataGridViewAll.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridViewAll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewAll.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewAll.Location = new System.Drawing.Point(1192, 76);
            this.dataGridViewAll.Name = "dataGridViewAll";
            this.dataGridViewAll.ReadOnly = true;
            this.dataGridViewAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridViewAll.Size = new System.Drawing.Size(10, 10);
            this.dataGridViewAll.TabIndex = 4;
            this.dataGridViewAll.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 627);
            this.Controls.Add(this.dataGridViewAll);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מסך ראשי";
            this.Load += new System.EventHandler(this.Main_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabcount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLogIn;
        private System.Windows.Forms.ToolStripButton toolStripButtonRef;
        private System.Windows.Forms.ToolStripButton toolStripButtonOrder;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemTools;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveTbl;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSignIn;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSignOut;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExit;
        private System.Windows.Forms.TabPage tabin;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabout;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSignOut;
        private System.Windows.Forms.ToolStripMenuItem הזמנותToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRefreash;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOperate;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRead;
        private System.Windows.Forms.DataGridView dataGridViewAll;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tabcount;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCheckTower;
    }
}