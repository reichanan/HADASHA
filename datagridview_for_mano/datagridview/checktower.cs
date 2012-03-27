using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace datagridview
{
    public partial class checktower : Form
    {
          
        public checktower()
        {
            InitializeComponent();            

 

        }

        private void Call_Click(object sender, EventArgs e)
        {
            int x = OperateTower.instance().LUCall(Convert.ToInt16(LUtxtbox.Text), Convert.ToInt16(Machinetxtbox.Text), Convert.ToInt16(Exittxtbox.Text));

            if(x==1)
                MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
        }

        private void Verify_Click(object sender, EventArgs e)
        {

            //string m = "gggg";
            OperateTower.instance().Read(Convert.ToInt16(LUtxtbox.Text));
            Thread.Sleep(200);
            string text = System.IO.File.ReadAllText(@"lu.txt");           
            string result = text.Remove(0,4);
            string machine = result.Substring(0, 4);
            string status = result.Substring(4);
            lustatus.Text = status;

    
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int x = OperateTower.instance().Del(Convert.ToInt16(LUtxtbox.Text));

            if (x == 1)
                MessageBox.Show("need to check lu ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
        }

        private void Dpick_Click(object sender, EventArgs e)
        {

        }

        private void EndOfPicking_Click(object sender, EventArgs e)
        {
            int x = OperateTower.instance().RemotePickingEnd(Convert.ToInt16(LUtxtbox.Text), Convert.ToInt16(Machinetxtbox.Text), Convert.ToInt16(Exittxtbox.Text));

            if (x == 1)
                MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
        }

        private void DeleteAll_Click(object sender, EventArgs e)
        {
            int x = OperateTower.instance().Delall(Convert.ToInt16(Machinetxtbox.Text));

            if (x == 1)
                MessageBox.Show("check table", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DBMAINT_Click(object sender, EventArgs e)
        {
            int x = OperateTower.instance().DBMaint();

            if (x == 1)
                MessageBox.Show("db maint failed", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
        }

        private void ClearTable_Click(object sender, EventArgs e)
        {
            int x = OperateTower.instance().ClearTable();

            if (x == 1)
                MessageBox.Show("clear table failed", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            OperateTower.ReleaseNew();
 
        }

        private void EndPicking_Click(object sender, EventArgs e)
        {
            int x = OperateTower.instance().RemotePickingEnd(Convert.ToInt16(LUtxtbox.Text), Convert.ToInt16(Machinetxtbox.Text), Convert.ToInt16(Exittxtbox.Text));

             if (x == 1)
                 MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OperateTower.ReleaseNew();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
