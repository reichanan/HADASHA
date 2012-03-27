namespace datagridview
{
    partial class orderItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(orderItemsForm));
            this.dataGridViewWithItems = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.Savetosheet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWithItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewWithItems
            // 
            this.dataGridViewWithItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridViewWithItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewWithItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewWithItems.Location = new System.Drawing.Point(12, 15);
            this.dataGridViewWithItems.Name = "dataGridViewWithItems";
            this.dataGridViewWithItems.ReadOnly = true;
            this.dataGridViewWithItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridViewWithItems.Size = new System.Drawing.Size(1152, 465);
            this.dataGridViewWithItems.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(346, 498);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(104, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "התחל עבודה";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(722, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "יציאה";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(483, 498);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 6;
            this.update.Text = "בצע עדכון";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // Savetosheet
            // 
            this.Savetosheet.Location = new System.Drawing.Point(593, 498);
            this.Savetosheet.Name = "Savetosheet";
            this.Savetosheet.Size = new System.Drawing.Size(97, 23);
            this.Savetosheet.TabIndex = 7;
            this.Savetosheet.Text = "שמור שגיאה";
            this.Savetosheet.UseVisualStyleBackColor = true;
            this.Savetosheet.Click += new System.EventHandler(this.Savetosheet_Click);
            // 
            // orderItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 533);
            this.Controls.Add(this.Savetosheet);
            this.Controls.Add(this.update);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dataGridViewWithItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "orderItemsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "מסך שורות בהזמנה";
            this.Load += new System.EventHandler(this.orderItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWithItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewWithItems;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button Savetosheet;

    }
}