namespace datagridview
{
    partial class checktower
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
            this.Machinetxtbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Exittxtbox = new System.Windows.Forms.TextBox();
            this.LU = new System.Windows.Forms.Label();
            this.LUtxtbox = new System.Windows.Forms.TextBox();
            this.Call = new System.Windows.Forms.Button();
            this.Dpick = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Verify = new System.Windows.Forms.Button();
            this.ClearTable = new System.Windows.Forms.Button();
            this.DBMAINT = new System.Windows.Forms.Button();
            this.DeleteAll = new System.Windows.Forms.Button();
            this.EndOfPicking = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lustatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Machinetxtbox
            // 
            this.Machinetxtbox.Location = new System.Drawing.Point(81, 21);
            this.Machinetxtbox.Name = "Machinetxtbox";
            this.Machinetxtbox.Size = new System.Drawing.Size(74, 20);
            this.Machinetxtbox.TabIndex = 0;
            this.Machinetxtbox.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Machine";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Exit";
            // 
            // Exittxtbox
            // 
            this.Exittxtbox.Location = new System.Drawing.Point(225, 21);
            this.Exittxtbox.Name = "Exittxtbox";
            this.Exittxtbox.Size = new System.Drawing.Size(74, 20);
            this.Exittxtbox.TabIndex = 3;
            this.Exittxtbox.Text = "1";
            // 
            // LU
            // 
            this.LU.AutoSize = true;
            this.LU.Location = new System.Drawing.Point(334, 28);
            this.LU.Name = "LU";
            this.LU.Size = new System.Drawing.Size(21, 13);
            this.LU.TabIndex = 4;
            this.LU.Text = "LU";
            // 
            // LUtxtbox
            // 
            this.LUtxtbox.Location = new System.Drawing.Point(373, 25);
            this.LUtxtbox.Name = "LUtxtbox";
            this.LUtxtbox.Size = new System.Drawing.Size(74, 20);
            this.LUtxtbox.TabIndex = 5;
            // 
            // Call
            // 
            this.Call.Location = new System.Drawing.Point(25, 81);
            this.Call.Name = "Call";
            this.Call.Size = new System.Drawing.Size(75, 23);
            this.Call.TabIndex = 6;
            this.Call.Text = "LU CALL";
            this.Call.UseVisualStyleBackColor = true;
            this.Call.Click += new System.EventHandler(this.Call_Click);
            // 
            // Dpick
            // 
            this.Dpick.Location = new System.Drawing.Point(228, 81);
            this.Dpick.Name = "Dpick";
            this.Dpick.Size = new System.Drawing.Size(75, 23);
            this.Dpick.TabIndex = 7;
            this.Dpick.Text = "Dpick";
            this.Dpick.UseVisualStyleBackColor = true;
            this.Dpick.Click += new System.EventHandler(this.Dpick_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(372, 81);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 8;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Verify
            // 
            this.Verify.Location = new System.Drawing.Point(147, 81);
            this.Verify.Name = "Verify";
            this.Verify.Size = new System.Drawing.Size(75, 23);
            this.Verify.TabIndex = 9;
            this.Verify.Text = "Read";
            this.Verify.UseVisualStyleBackColor = true;
            this.Verify.Click += new System.EventHandler(this.Verify_Click);
            // 
            // ClearTable
            // 
            this.ClearTable.Location = new System.Drawing.Point(463, 236);
            this.ClearTable.Name = "ClearTable";
            this.ClearTable.Size = new System.Drawing.Size(75, 23);
            this.ClearTable.TabIndex = 10;
            this.ClearTable.Text = "Clear Table";
            this.ClearTable.UseVisualStyleBackColor = true;
            this.ClearTable.Click += new System.EventHandler(this.ClearTable_Click);
            // 
            // DBMAINT
            // 
            this.DBMAINT.Location = new System.Drawing.Point(372, 236);
            this.DBMAINT.Name = "DBMAINT";
            this.DBMAINT.Size = new System.Drawing.Size(75, 23);
            this.DBMAINT.TabIndex = 11;
            this.DBMAINT.Text = "DB MAINT";
            this.DBMAINT.UseVisualStyleBackColor = true;
            this.DBMAINT.Click += new System.EventHandler(this.DBMAINT_Click);
            // 
            // DeleteAll
            // 
            this.DeleteAll.Location = new System.Drawing.Point(372, 110);
            this.DeleteAll.Name = "DeleteAll";
            this.DeleteAll.Size = new System.Drawing.Size(75, 23);
            this.DeleteAll.TabIndex = 12;
            this.DeleteAll.Text = "Delete All";
            this.DeleteAll.UseVisualStyleBackColor = true;
            this.DeleteAll.Click += new System.EventHandler(this.DeleteAll_Click);
            // 
            // EndOfPicking
            // 
            this.EndOfPicking.Location = new System.Drawing.Point(25, 124);
            this.EndOfPicking.Name = "EndOfPicking";
            this.EndOfPicking.Size = new System.Drawing.Size(91, 23);
            this.EndOfPicking.TabIndex = 13;
            this.EndOfPicking.Text = "End Of Picking";
            this.EndOfPicking.UseVisualStyleBackColor = true;
            this.EndOfPicking.Click += new System.EventHandler(this.EndOfPicking_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Release";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lustatus
            // 
            this.lustatus.Location = new System.Drawing.Point(147, 126);
            this.lustatus.Name = "lustatus";
            this.lustatus.ReadOnly = true;
            this.lustatus.Size = new System.Drawing.Size(100, 20);
            this.lustatus.TabIndex = 16;
            // 
            // checktower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 318);
            this.Controls.Add(this.lustatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.EndOfPicking);
            this.Controls.Add(this.DeleteAll);
            this.Controls.Add(this.DBMAINT);
            this.Controls.Add(this.ClearTable);
            this.Controls.Add(this.Verify);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Dpick);
            this.Controls.Add(this.Call);
            this.Controls.Add(this.LUtxtbox);
            this.Controls.Add(this.LU);
            this.Controls.Add(this.Exittxtbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Machinetxtbox);
            this.Name = "checktower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "checktower";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Machinetxtbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Exittxtbox;
        private System.Windows.Forms.Label LU;
        private System.Windows.Forms.TextBox LUtxtbox;
        private System.Windows.Forms.Button Call;
        private System.Windows.Forms.Button Dpick;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Verify;
        private System.Windows.Forms.Button ClearTable;
        private System.Windows.Forms.Button DBMAINT;
        private System.Windows.Forms.Button DeleteAll;
        private System.Windows.Forms.Button EndOfPicking;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox lustatus;
    }
}