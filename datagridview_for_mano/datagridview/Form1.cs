using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
//using ConsumeTranstechWS.ServiceReference1;

using datagridview.Properties;

internal struct TheFontSize
{
    public static int Huge = 30;
    public static int Normal = 20;
    public static int Tiny = 8;
}


namespace datagridview
{
    [System.Runtime.InteropServices.GuidAttribute("734CBA2B-E6C0-4AE8-B761-42F0E00EBFC4")]
    public partial class Form1 : Form
    {
        //Example Ex = new Example();

        int cntr = 0; //used for custom sort toggle
        DataTable dt; // used as datasource of DataGridView
        MainMenu MyMenu;

        private int currFontSize = TheFontSize.Normal;
        private StatusBarPanel sbPnlPrompt = new StatusBarPanel();
        private StatusBarPanel sbPnlTime = new StatusBarPanel();


        /// <summary>
        /// Form1 Constructor that calls the intialize component and other custom initalization code
        /// </summary>
        public Form1()
        {
            InitializeComponent();


            // Function 1 PO
            //TranstechClient Client = new TranstechClient();
            //string MessageReturn = string.Empty;
            //int subrc = 0;
            //V_PO[] Po = new V_PO[0];
            //Po = Client.ZMM_TRANSTECH_GET_PO(subrc, ref MessageReturn);

            ReadConfig();

            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            //this.dataGridView1.Size = new System.Drawing.Size(405, 290);
            this.dataGridView1.TabIndex = 0;
            //Add the column header mouse cilck event handler
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DataGridView1_CellDoubleClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);

            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyDown);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyUp);
            


            // Creating the custom datatable for binding
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("מספר הזמנה", System.Type.GetType("System.Int32")));
            dt.Columns.Add(new DataColumn("מספר פריט", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("סוג מסמך", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("מק'ט", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("יחידת מידה", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("תיאור מק'ט", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("מספר מגדל", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("מחסן מקבל", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("כמות לניפוק", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("כמות נשארה לניפוק", System.Type.GetType("System.String")));


            //dataGridView1.DataSource = GetResultsTable();

            // Adding data to datatable
            DataRow dr = dt.NewRow();
            dr["מספר הזמנה"] = 1;
            dr["מספר פריט"] = "שמיכה";
            dr["סוג מסמך"] = 7;
            dr["מק'ט"] = 66;
            dr["יחידת מידה"] = 66;
            dr["תיאור מק'ט"] = 66;
            dr["מספר מגדל"] = 66;
            dr["מחסן מקבל"] = 66;
            dr["כמות לניפוק"] = 66;
            dr["כמות נשארה לניפוק"] = 66;                             
            dt.Rows.Add(dr);
                       
            dt.AcceptChanges();


            // Create a main menu object. 
            MyMenu = new MainMenu();

    

            MenuItem toolsMenu = new MenuItem("כלים");
            MyMenu.MenuItems.Add(toolsMenu);

            // Add top-level menu items to the menu. 
            MenuItem fileMenu = new MenuItem("קובץ");
            MyMenu.MenuItems.Add(fileMenu);


            toolsMenu.MenuItems.Add(new MenuItem("כניסה למערכת",  new EventHandler(this.MMLogInClick),Shortcut.CtrlL));
            toolsMenu.MenuItems.Add(new MenuItem("יציאה מהמערכת", new EventHandler(this.MMLogOut),    Shortcut.CtrlO));
            toolsMenu.MenuItems.Add(new MenuItem("סגירת האפליקציה",   new EventHandler(this.MMExitClick), Shortcut.CtrlX));

            fileMenu.MenuItems.Add(new MenuItem("שמירת הטבלה", new EventHandler(this.MMSave), Shortcut.CtrlS));



            // Assign the menu to the form. 
            Menu = MyMenu;

            BuildStatBar();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Bind the datagridview with datatable
            dataGridView1.DataSource = dt;
        }


        //public DataTable GetResultsTable()
        //{
        //    DataTable d = new DataTable();
        //    return d;
        //}

        /// <summary>
        /// Header click event handler that perform the sorting on DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (cntr % 2 == 0) //This condition applied for toggeling the Ascending and Descending sort
                dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], ListSortDirection.Ascending);
            else
                dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], ListSortDirection.Descending);
            cntr++;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "ColumnIndex", e.ColumnIndex);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", e.RowIndex);
            messageBoxCS.AppendLine();
            MessageBox.Show(messageBoxCS.ToString(), "CellDoubleClick Event");


            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                proceedOrder order = new proceedOrder();

                order._EBELN = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["מספר הזמנה"].Value);
                order._EBELP = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["מספר פריט"].Value);
                //order._BSART = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["סוג מסמך"].Value);
                order._MATNR = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["מק'ט"].Value);
                order._MEINS = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["יחידת מידה"].Value);
                order._MAKTX = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["תיאור מק'ט"].Value);
                //order._ID_LGPBE = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["מספר מגדל"].Value);
                order._LGORT = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["מחסן מקבל"].Value);
                order._MENGE = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["כמות לניפוק"].Value);
 
                       
                order.ShowDialog();

            }

        }


        private void dataGridView1_CellEnter(object sender,
           DataGridViewCellEventArgs e)
        {
            //dataGridView1[e.ColumnIndex, e.RowIndex].Style
            //     .SelectionBackColor = Color.Blue;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DataGridViewCell xx = dataGridView1.CurrentCell;

                System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
                messageBoxCS.AppendFormat("{0} = {1}", "ColumnIndex", xx.ColumnIndex);
                messageBoxCS.AppendLine();
                messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", xx.RowIndex);
                messageBoxCS.AppendLine();
                MessageBox.Show(messageBoxCS.ToString(), "CellDoubleClick Event");


            }
        }

        
        protected void MMLogInClick(object who, EventArgs e)
        {
    
            loginForm login = new loginForm();
            login.ShowDialog();
            if (login.DialogResult.Equals(DialogResult.OK))
                //this.Close();
            //else
                MessageBox.Show("Welcome " + login.ResultText, "!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        // Handler for main menu Change selection. 
        protected void MMLogOut(object who, EventArgs e)
        {
            //Width = Height = 200;
        }

        // Handler for main menu Restore selection. 
        protected void MMSave(object who, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Excel Documents (*.xls)|*.xls";

            sfd.FileName = "export.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                 ToCsV(dataGridView1, sfd.FileName); // Here dvwACH is your grid view name

            }
        }

 
        // Handler for main menu Exit selection. 
        protected void MMExitClick(object who, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Stop Program?",
                                    "Terminate",
                                     MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes) Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            string s = t.ToLongTimeString();
            sbPnlTime.Text = s;
        }




        private void BuildStatBar()
        {
            Timer timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);

            StatusBar statusBar = new StatusBar();

            statusBar.ShowPanels = true;
            statusBar.Panels.AddRange((StatusBarPanel[])new StatusBarPanel[] { sbPnlPrompt, sbPnlTime });

            sbPnlPrompt.BorderStyle = StatusBarPanelBorderStyle.None;
            sbPnlPrompt.AutoSize = StatusBarPanelAutoSize.Spring;
            //sbPnlPrompt.Width = 62;
            //sbPnlPrompt.Text = "Ready";

            sbPnlTime.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            sbPnlTime.Width = 76;

            try
            {
                //Icon i = new Icon("icon1.ico");
                //sbPnlPrompt.Icon = i;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            this.Controls.Add(statusBar);
        }


        private void ToCsV(DataGridView dGV, string filename)
        {

            string stOutput = "";

            // Export titles:

            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)

                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";

            stOutput += sHeaders + "\r\n";

            // Export data.

            for (int i = 0; i < dGV.RowCount - 1; i++)
            {

                string stLine = "";

                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)

                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";

                stOutput += stLine + "\r\n";

            }

            Encoding utf16 = Encoding.GetEncoding(1254);

            byte[] output = utf16.GetBytes(stOutput);

            FileStream fs = new FileStream(filename, FileMode.Create);

            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(output, 0, output.Length); //write the encoded file

            bw.Flush();

            bw.Close();

            fs.Close();

        }

 
        private void toolStripButtonLogIn_Click(object sender, EventArgs e)
        {
            loginForm login = new loginForm();
            login.ShowDialog();
            if (login.DialogResult.Equals(DialogResult.OK))
                //this.Close();
                //else
                MessageBox.Show("Welcome " + login.ResultText, "!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonOrder_Click(object sender, EventArgs e)
        {
            DataGridViewRow rowToSelect = this.dataGridView1.CurrentRow;

            foreach (DataGridViewCell cell in rowToSelect.Cells) 
            { 
                if (cell.Value == null || cell.Value.Equals("")) 
                { 
                   continue; 
                }
                string stLine1 = cell.Value.ToString(); 
            } 


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        static void ReadConfig()
        {
            Settings set = Settings.Default;
            string str = set.migdal.ToString();
        }

    }





}

