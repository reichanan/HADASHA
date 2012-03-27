using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using datagridview.Properties;
using System.IO;
using CodeProject.Dialog;
using System.Data.OleDb;
using datagridview.TranstecWS;
using System.Diagnostics;
using System.Threading;




namespace datagridview
{
    public partial class Main : Form, IMessageFilter 
    {

        #region vars
        int cntr = 0; //used for custom sort toggle
        DataTable dt_Onlyproduct; // used as datasource of DataGridView
        DataTable dt_Insertproduct;
        DataTable dt_Coutingproduct; 
        bool loginToSystem = false;
        bool displayonce = true;
        

        string loginName = "";
        string loginPassword = "";
        string TOWER_STRING = "";
        StatusBarPanel panelLoginName;
        StatusBar statusBar1;
        private int currFontSize = TheFontSize.Normal;
        private StatusBarPanel sbPnlPrompt = new StatusBarPanel();
        private StatusBarPanel sbPnlTime = new StatusBarPanel();
        #endregion
        #region member functions


        int subrc = 0;//Subrc EQ 0 = OK, Subrc EQ 4 = Error
        string MessageReturn = string.Empty;
        V_PO[] Po = new V_PO[0];
        V_HEADER_PO[] PoHeader = new V_HEADER_PO[0];
        V_MIGO[] Migo = new V_MIGO[1];
        V_BAPIRETURN[] Bapireturn = new V_BAPIRETURN[0];
        V_MATERIAL_IN[] Material_in = new V_MATERIAL_IN[0];
        V_MATERIAL[] MaterialCatalog = new V_MATERIAL[0];

        private System.Windows.Forms.Timer mTimer; 
        
        bool OPERATE_MACHINE = false;
        bool SHOW_TEST_MACHINE_MENU = false;
        string MACHINE_NO = "";
        string START_LU = "";
        int machine_no_int = 0;
        int start_lu_int = 0;
        int operation_delay_int = 0;
        int CLIENT_TYPE = 0;
        


        private void Grayed()
        {
            //toolStripButtonLogIn.Enabled = false;
            //toolStripButtonSignOut.Enabled = false;
            //toolStripButtonRef.Enabled = false;
            //toolStripButtonOrder.Enabled = false;

            //toolStripButtonSave.Enabled = false;

            //ToolStripMenuItemSignIn.Enabled = false;
            //ToolStripMenuItemSignOut.Enabled = false;
            //ToolStripMenuItemExit.Enabled = false;
            //ToolStripMenuItemRefreash.Enabled = false;
            //ToolStripMenuItemOperate.Enabled = false;
            //ToolStripMenuItemSaveTbl.Enabled = false;
            //ToolStripMenuItemRead.Enabled = false;


            ToolStripMenuItemCheckTower.Enabled = SHOW_TEST_MACHINE_MENU;
        }

        private void LogoutUser(object sender, EventArgs e) 
        {
            if (loginToSystem && displayonce)
            {

                displayonce = false;
                DialogResult res = AppBox.Show("המערכת זיהתה חוסר שימוש במשך 30 דקות האם ברצונך לצאת?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (res == DialogResult.Yes)
                {
                    
                   Application.Exit();             

                }
                else
                {
                    displayonce = true;

                }

            }
           
                 
        } 


        public bool PreFilterMessage(ref Message m) 
        {             
            // Monitor message for keyboard and mouse messages             
            bool active = m.Msg == 0x100 || m.Msg == 0x101;  // WM_KEYDOWN/UP             
            
            active = active || m.Msg == 0xA0 || m.Msg == 0x200;  // WM_(NC)MOUSEMOVE             
            active = active || m.Msg == 0x10;  // WM_CLOSE, in case dialog closes             
            if (active) 
            {                 
                //if (!mTimer.Enabled) label1.Text = "Wakeup";                 
                mTimer.Enabled = false;                 
                mTimer.Start();             
            }             return false;         
        } 


        public void f1_Shown(object sender, EventArgs e) 
        {

            logIn(sender, e);



        } 



        public Main()
        {


            simplelogfile.LogToFile("started application");
            
            InitializeComponent();
            killolemission();

            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);

            //////////////////////////////////

            //ExceptionLogger logger = new ExceptionLogger();
            //logger.AddLogger(new TextFileLogger());

            //Exception ex = new Exception("hanan");
            //logger.LogException(ex);

            //////////////////////////////////

            //1000 * 30 * 60;

            Settings set = Settings.Default;
            int postpone = set.postpone_auto_logout;
            SHOW_TEST_MACHINE_MENU = set.show_test_machine_menu;
            MACHINE_NO = set.machine_no.ToString();
            START_LU = set.started_lu.ToString();
            machine_no_int = Convert.ToInt16(MACHINE_NO);
            start_lu_int = Convert.ToInt16(START_LU);
            OPERATE_MACHINE = set.operate_machine;
            operation_delay_int = set.operation_delay;
            CLIENT_TYPE = set.client_type;


            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 1000 * postpone * 60;  // postpone auto-logout by 30 minutes 
            mTimer.Tick += LogoutUser; 
            mTimer.Enabled = true;
            Application.AddMessageFilter(this); 

            Grayed();

            ReadConfig();             
            tabControl1.TabPages.RemoveAt(2);


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
            //this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyDown);
            //this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyUp);

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Blue;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;


            this.Shown += new EventHandler(f1_Shown); 


            ///////////////////////////////////////////////////////////////////////////////////////// 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView2.Name = "dataGridView2";
            //this.dataGridView1.Size = new System.Drawing.Size(405, 290);
            this.dataGridView2.TabIndex = 1;
            //Add the column header mouse cilck event handler
            this.dataGridView2.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView2_ColumnHeaderMouseClick);
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DataGridView2_CellDoubleClick);
            //this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView2_KeyDown);
            //this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyUp);
            dataGridView2.ColumnHeadersDefaultCellStyle = columnHeaderStyle;


            ///////////////////////////////////////////////////////////////////////////////////////// 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView3.Name = "dataGridView3";
            //this.dataGridView1.Size = new System.Drawing.Size(405, 290);
            this.dataGridView3.TabIndex = 2;
            //Add the column header mouse cilck event handler
            this.dataGridView3.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView3_ColumnHeaderMouseClick);
            this.dataGridView3.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DataGridView3_CellDoubleClick);
            //this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);
            this.dataGridView3.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView3_KeyDown);
            //this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyUp);
            dataGridView3.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            //#region create grid column - Creating the custom datatable for binding
 
            //#endregion 

            CreateMyStatusBar();
            
            OPERATE_MACHINE = set.operate_machine;
            

            //logger.LogToFile("ggg");

            if (OPERATE_MACHINE)
            {
                int ret = 0;

                OperateTower.instance().SetTimeOut(operation_delay_int);
                
                //int ret = OperateTower.instance().ClearTable();

                if (ret == 1)
                {
                    //MessageBox.Show("can not clear the table", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    simplelogfile.LogToFile("error - can not clear olemission table");
                }
            }

 
            

            

        }


        private void killolemission()
        {

            foreach (System.Diagnostics.Process myProc in System.Diagnostics.Process.GetProcesses())
            {
                if (myProc.ProcessName == "olemission")
                {
                    
                    myProc.Kill();
                }
            }
        }
        
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //OperateTower.ReleaseNew();
            //killolemission();

            //DialogResult frmclose = MessageBox.Show("Do you really want to exit?", "Closing", MessageBoxButtons.YesNo);
            
            
            //if (frmclose == DialogResult.Yes)
            //{

            int ret = OperateTower.instance().ClearTable();
            Thread.Sleep(1000);
            OperateTower.instance().Release();
            Thread.Sleep(1000);
            killolemission();
                //Thread.Sleep(5000);
             //   Application.Exit();
            //}
           // else
            //{
            //    e.Cancel = true;
            //}



            

        }



        private void Main_Load(object sender, EventArgs e)
        {
                
            //AEDAT BSART EBELN ERNAM LGOBE LGORT NUMBER TOWER USERALIAS 
            dt_Onlyproduct = new DataTable(); 
            dt_Onlyproduct.Columns.Add(new DataColumn("AEDAT", System.Type.GetType("System.Int32")));       //AEDAT 
            dt_Onlyproduct.Columns.Add(new DataColumn("EBELN", System.Type.GetType("System.String")));      //EBELN
            dt_Onlyproduct.Columns.Add(new DataColumn("NUMBER", System.Type.GetType("System.String")));     //NUMBER
            dt_Onlyproduct.Columns.Add(new DataColumn("LGOBE", System.Type.GetType("System.String")));      //LGOBE 
            dt_Onlyproduct.Columns.Add(new DataColumn("LGORT", System.Type.GetType("System.String")));      //LGORT
            dt_Onlyproduct.Columns.Add(new DataColumn("TOWER", System.Type.GetType("System.String")));      //TOWER
            
            
            dt_Onlyproduct.Columns.Add(new DataColumn("BSART", System.Type.GetType("System.String")));      //BSART            
            //dt_Onlyproduct.Columns.Add(new DataColumn("ERNAM", System.Type.GetType("System.String")));      //ERNAM           
            //dt_Onlyproduct.Columns.Add(new DataColumn("USERALIAS", System.Type.GetType("System.String")));  //USERALIAS
            
            dataGridView1.DataSource = dt_Onlyproduct;
            setDataGridHeader1(dataGridView1);

            ///////////////////////////////////////////////////////////////////////////

            //AXISX AXISY CPUDT MAKTX MATNR MBLNR MEINS MENGE MJAHR TOWER TRAY ZEILE 
            dt_Insertproduct = new DataTable();
            dt_Insertproduct.Columns.Add(new DataColumn("CPUDT", System.Type.GetType("System.String")));      //CPUDT
            
            dt_Insertproduct.Columns.Add(new DataColumn("MBLNR", System.Type.GetType("System.String")));      //MBLNR מספר מסמך
            dt_Insertproduct.Columns.Add(new DataColumn("ZEILE", System.Type.GetType("System.String")));  //ZEILE מספר שורה
            dt_Insertproduct.Columns.Add(new DataColumn("MATNR", System.Type.GetType("System.String")));      //MATNR  מק'ט
            dt_Insertproduct.Columns.Add(new DataColumn("MAKTX", System.Type.GetType("System.String")));      //MAKTX תיאור מק'ט
            dt_Insertproduct.Columns.Add(new DataColumn("MENGE", System.Type.GetType("System.String")));      //MENGE
            dt_Insertproduct.Columns.Add(new DataColumn("MEINS", System.Type.GetType("System.String")));     //MEINS
            dt_Insertproduct.Columns.Add(new DataColumn("AXISY", System.Type.GetType("System.String")));      //AXISY 
            dt_Insertproduct.Columns.Add(new DataColumn("AXISX", System.Type.GetType("System.Int32")));       //AXISX 

                              
            dt_Insertproduct.Columns.Add(new DataColumn("TRAY", System.Type.GetType("System.String")));  //TRAY
      
            //dt_Insertproduct.Columns.Add(new DataColumn("MJAHR", System.Type.GetType("System.String")));        //MJAHR
            dt_Insertproduct.Columns.Add(new DataColumn("TOWER", System.Type.GetType("System.String")));        //TOWER
            dt_Insertproduct.Columns.Add(new DataColumn("LABST", System.Type.GetType("System.String")));      //LABST
            
           
            dataGridView2.DataSource = dt_Insertproduct;
            setDataGridHeader2(dataGridView2);

            ///////////////////////////////////////////////////////////////////////////

            //AXISX AXISY LABST LGOBE LGORT MAKTX MATKL MATNR MEINS MSEHT TOWER TRAY WGBEZ60 
            dt_Coutingproduct = new DataTable();
            dt_Coutingproduct.Columns.Add(new DataColumn("AXISX", System.Type.GetType("System.Int32")));       //AXISX 
            dt_Coutingproduct.Columns.Add(new DataColumn("AXISY", System.Type.GetType("System.String")));      //AXISY 
            dt_Coutingproduct.Columns.Add(new DataColumn("LABST", System.Type.GetType("System.String")));      //LABST
            dt_Coutingproduct.Columns.Add(new DataColumn("LGOBE", System.Type.GetType("System.String")));      //LGOBE
            dt_Coutingproduct.Columns.Add(new DataColumn("LGORT", System.Type.GetType("System.String")));      //LGORT 
            dt_Coutingproduct.Columns.Add(new DataColumn("MAKTX", System.Type.GetType("System.String")));      //MAKTX
            dt_Coutingproduct.Columns.Add(new DataColumn("MATKL", System.Type.GetType("System.String")));     //MATKL
            dt_Coutingproduct.Columns.Add(new DataColumn("MATNR", System.Type.GetType("System.String")));      //MATNR
            dt_Coutingproduct.Columns.Add(new DataColumn("MEINS", System.Type.GetType("System.String")));  //MEINS
            dt_Coutingproduct.Columns.Add(new DataColumn("MSEHT", System.Type.GetType("System.String")));  //MSEHT
            dt_Coutingproduct.Columns.Add(new DataColumn("TOWER", System.Type.GetType("System.String")));  //TOWER
            dt_Coutingproduct.Columns.Add(new DataColumn("TRAY", System.Type.GetType("System.String")));  //TRAY
            dt_Coutingproduct.Columns.Add(new DataColumn("WGBEZ60", System.Type.GetType("System.String")));  //WGBEZ60
            dataGridView3.DataSource = dt_Coutingproduct;
            setDataGridHeader3(dataGridView3);

            //Refreash(sender, e,0);
            //Refreash(sender, e, 1);

            


        }



        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (cntr % 2 == 0) //This condition applied for toggeling the Ascending and Descending sort
            //    dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], ListSortDirection.Ascending);
            //else
            //    dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], ListSortDirection.Descending);
            //cntr++;

            //dataGridView1.AutoResizeColumns();
        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           DisplayOrder(e.ColumnIndex, e.RowIndex);
           //Refreash(sender, e);
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DataGridViewCell currCell = dataGridView1.CurrentCell;
                DisplayOrder(currCell.ColumnIndex, currCell.RowIndex);
                e.Handled = true;
            }
        }


        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (cntr % 2 == 0) //This condition applied for toggeling the Ascending and Descending sort
            //    dataGridView2.Sort(dataGridView2.Columns[e.ColumnIndex], ListSortDirection.Ascending);
            //else
            //    dataGridView2.Sort(dataGridView2.Columns[e.ColumnIndex], ListSortDirection.Descending);
            //cntr++;

            //dataGridView2.AutoResizeColumns();
        }
        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            simplelogfile.LogToFile("start InsertProduct");
            
            string traystr = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells["TRAY"].Value);
            int trayNo = Convert.ToInt16(traystr);
            string s = String.Format("tray {0} for machine {1}", (start_lu_int + trayNo), machine_no_int);

            if (OPERATE_MACHINE)
            {

                int ret = OperateTower.instance().LUCall((start_lu_int + trayNo), machine_no_int, 1);

                if (ret == 1)
                {
                    
                    //LU IN TABLE
                    //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string strerror = "error: supply item LUCall" + s;
                    simplelogfile.LogToFile(strerror);
                }
                else
                {
                    string str = "success LUCall" + s;
                    simplelogfile.LogToFile(str);
                }

            }          

            
            InsertProduct(e.ColumnIndex, e.RowIndex);


            if (OPERATE_MACHINE)
            {

                simplelogfile.LogToFile("try RemotePickingEnd");
                
                int tray = Convert.ToInt16(trayNo);

                int ret = OperateTower.instance().RemotePickingEnd((start_lu_int + tray), machine_no_int, 1);

                if (ret == 1)
                {
                    string strerror = "RemotePickingEnd failed" + s;

                    simplelogfile.LogToFile(strerror);
                    //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string str = "success RemotePickingEnd" + s;
                    simplelogfile.LogToFile(str);

                }
            }

            //del item anyway when exit supply window
            if (OPERATE_MACHINE)
            {
                //simplelogfile.LogToFile("try del");

                int ret = OperateTower.instance().Del((start_lu_int + trayNo));

                if (ret == 1)
                {
                    //LU IN TABLE
                    //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //simplelogfile.LogToFile("del failed");
                }
                else
                {
                    //LU NOT IN TABLE
                }
            }

            simplelogfile.LogToFile("end InsertProduct");



        }
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
           
            int trayNo = 0;
            if (e.KeyData == Keys.Enter)
            {
                //DataGridViewCell currCell = dataGridView2.CurrentCell;
                //InsertProduct(currCell.ColumnIndex, currCell.RowIndex);
                
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        string traystr = row.Cells["TRAY"].Value.ToString();
                        trayNo = Convert.ToInt16(traystr);

                        if (OPERATE_MACHINE)
                        {                   

                            int ret = OperateTower.instance().LUCall((start_lu_int + trayNo), machine_no_int, 1);

                            string s = String.Format("call tray {0} for machine {1}", (start_lu_int + trayNo), machine_no_int);

                            if (ret == 1)
                            {
                                //LU IN TABLE
                                //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                string strerror = "error: supply item LUCall" + s;

                                simplelogfile.LogToFile(strerror);
                            }
                            else
                            {
                                //LU NOT IN TABLE
                            }

                        }                       
                        
                        
                        InsertProduct(row.Index, row.Index);
                    }
                       
                }


                if (OPERATE_MACHINE)
                {
                    int tray = Convert.ToInt16(trayNo);

                    int ret = OperateTower.instance().RemotePickingEnd((start_lu_int + tray), machine_no_int, 1);

                    string s = String.Format("call tray {0} for machine {1}", (start_lu_int + trayNo), machine_no_int);

                    if (ret == 1)
                    {
                        //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string strerror = "error: supply item RemotePickingEnd" + s;

                        simplelogfile.LogToFile(strerror);

                    }
                }


                //del the item anyway when exit the from supply window
                if (OPERATE_MACHINE)
                {

                    int ret = OperateTower.instance().Del((start_lu_int + trayNo));

                    if (ret == 1)
                    {
                        //LU IN TABLE
                        //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //LU NOT IN TABLE
                    }
                }


                e.Handled = true;
            }
        }



        private void dataGridView3_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (cntr % 2 == 0) //This condition applied for toggeling the Ascending and Descending sort
                dataGridView3.Sort(dataGridView3.Columns[e.ColumnIndex], ListSortDirection.Ascending);
            else
                dataGridView3.Sort(dataGridView3.Columns[e.ColumnIndex], ListSortDirection.Descending);
            cntr++;

            dataGridView3.AutoResizeColumns();
        }
        private void DataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayOrder(e.ColumnIndex, e.RowIndex);
        }
        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DataGridViewCell currCell = dataGridView3.CurrentCell;
                DisplayOrder(currCell.ColumnIndex, currCell.RowIndex);
                e.Handled = true;
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            string s = t.ToLongTimeString();
            sbPnlTime.Text = s;
        }
        private void CreateMyStatusBar()
        {
            // Create a StatusBar control.
            statusBar1 = new StatusBar();
            // Create two StatusBarPanel objects to display in the StatusBar.
            StatusBarPanel panel1 = new StatusBarPanel();
            StatusBarPanel panel2 = new StatusBarPanel();



            panelLoginName = new StatusBarPanel();

            panelLoginName.Style = StatusBarPanelStyle.OwnerDraw;

            // Display the first panel with a sunken border style.
            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            // Initialize the text of the panel.
            //panel1.Text = "Ready...";
            // Set the AutoSize property to use all remaining space on the StatusBar.
            panel1.AutoSize = StatusBarPanelAutoSize.Spring;
            // Display the second panel with a raised border style.
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            // Create ToolTip text that displays the current time.
            panel2.ToolTipText = System.DateTime.Now.ToShortTimeString();
            // Set the text of the panel to the current date.
            panel2.Text = System.DateTime.Today.ToLongDateString();
            // Set the AutoSize property to size the panel to the size of the contents.
            panel2.AutoSize = StatusBarPanelAutoSize.Contents;



            // Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1);
            statusBar1.Panels.Add(panel2);
            statusBar1.Panels.Add(panelLoginName);

            // Display panels in the StatusBar control.
            statusBar1.ShowPanels = true;

            // Associate the event-handling method with the DrawItem event 
            // for the owner-drawn panel.
            statusBar1.DrawItem +=
                new StatusBarDrawItemEventHandler(DrawCustomStatusBarPanel);

            // Add the StatusBar to the form.
            this.Controls.Add(statusBar1);
        }
        private void DrawCustomStatusBarPanel(object sender,StatusBarDrawItemEventArgs e)
        {

            // Draw a blue background in the owner-drawn panel.
            e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);

            // Create a StringFormat object to align text in the panel.
            StringFormat textFormat = new StringFormat();

            // Center the text in the middle of the line.
            textFormat.LineAlignment = StringAlignment.Center;

            // Align the text to the left.
            textFormat.Alignment = StringAlignment.Far;

            // Draw the panel's text in dark blue using the Panel 
            // and Bounds properties of the StatusBarEventArgs object 
            // and the StringFormat object.
            e.Graphics.DrawString(e.Panel.Text, statusBar1.Font,
                Brushes.DarkBlue, new RectangleF(e.Bounds.X,
                e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), textFormat);

        }
        private void BuildStatBar()
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
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

            for (int i = 0; i < dGV.RowCount ; i++)
            {

                string stLine = "";

                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)

                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";

                stOutput += stLine + "\r\n";

            }

            Encoding utf16 = Encoding.GetEncoding(1255);

            byte[] output = utf16.GetBytes(stOutput);

            FileStream fs = new FileStream(filename, FileMode.Create);

            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(output, 0, output.Length); //write the encoded file

            bw.Flush();

            bw.Close();

            fs.Close();

        }
        private void logIn(object sender, EventArgs e)
        {
            loginForm login = new loginForm();
            login.ShowDialog();
            if (login.DialogResult.Equals(DialogResult.OK))
            {
                //MessageBox.Show("Welcome " + login.ResultText, "!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AppBox.Show(login.ResultText + " :ברוך הבא משתמש ", MessageBoxButtons.OK, MessageBoxIcon.Information);


                loginToSystem = true;
                loginName = login.ResultText;
                loginPassword = login.ResultPassword;
                panelLoginName.Text = login.ResultText;

                Refreash(sender, e,0);
                Refreash(sender, e, 1);


            }
            else
            {
                //the user was not log in.
            }
        }
        private void ToolStripMenuItemSignIn_Click(object sender, EventArgs e)
        {
            logIn(sender, e);
        }
        private void toolStripButtonLogIn_Click(object sender, EventArgs e)
        {
            logIn(sender, e);
        }
        private void Refreash(object sender, EventArgs e,int refTab = -1)
        {
            //loginToSystem = true;

            int x = tabControl1.SelectedIndex;
            
            if (loginToSystem) //for test
            {
                TranstechClient Client = new TranstechClient();
                try
                {
                    
                    Client.Open();

                    if (tabControl1.SelectedIndex == 0 || refTab ==0)
                    {
                        // Function 2 PO header                            

                        PoHeader = Client.ZMM_TRANSTECH_GET_HEADER_PO(loginName, loginPassword, ((TranstechlTower)Enum.Parse(typeof(TranstechlTower), TOWER_STRING)),
                            (TranstechlFields)Enum.Parse(typeof(TranstechlFields), "TOWER"), TOWER_STRING, ref subrc, ref MessageReturn, CLIENT_TYPE);
                        dataGridView1.DataSource = PoHeader;

                        if (subrc == 4)//Check for an error
                            throw new Exception(MessageReturn);


                        //dataGridView1.Columns["BSART"].Visible = false;
                        dataGridView1.Columns["ERNAM"].Visible = false;
                        dataGridView1.Columns["USERALIAS"].Visible = false;


                        dataGridView1.Columns["AEDAT"].DisplayIndex = 0;
                        dataGridView1.Columns["EBELN"].DisplayIndex = 1;
                        dataGridView1.Columns["NUMBER"].DisplayIndex = 2;
                        dataGridView1.Columns["LGOBE"].DisplayIndex = 3;
                        dataGridView1.Columns["LGORT"].DisplayIndex = 4;
                        dataGridView1.Columns["TOWER"].DisplayIndex = 5;
                        dataGridView1.Columns["BSART"].DisplayIndex = 6;

  

                    }
                    
                    if(tabControl1.SelectedIndex == 1 || refTab ==1)
                    {
                        Material_in = Client.ZMM_TRANSTECH_MATERIAL_IN(loginName, loginPassword, ((TranstechlTower)Enum.Parse(typeof(TranstechlTower), TOWER_STRING)),
                            (TranstechlFields)Enum.Parse(typeof(TranstechlFields), "TOWER"), TOWER_STRING, ref subrc, ref MessageReturn, CLIENT_TYPE);
                        dataGridView2.DataSource = Material_in;

                        if (subrc == 4)//Check for an error
                            throw new Exception(MessageReturn);


                        //dataGridView2.Columns["CPUDT"].Visible = false;
                        dataGridView2.Columns["MJAHR"].Visible = false;
                        //dataGridView2.Columns["TOWER"].Visible = false;

                        int v = 0;
                        dataGridView2.Columns["CPUDT"].DisplayIndex = v++;
                        dataGridView2.Columns["MBLNR"].DisplayIndex = v++;
                        dataGridView2.Columns["ZEILE"].DisplayIndex = v++;
                        dataGridView2.Columns["MATNR"].DisplayIndex = v++;
                        dataGridView2.Columns["MAKTX"].DisplayIndex = v++;
                        dataGridView2.Columns["MENGE"].DisplayIndex = v++;
                        dataGridView2.Columns["MEINS"].DisplayIndex = v++;
                        dataGridView2.Columns["AXISY"].DisplayIndex = v++;
                        dataGridView2.Columns["AXISX"].DisplayIndex = v++;
                        dataGridView2.Columns["TRAY"].DisplayIndex = v++;
               
                        dataGridView2.Columns["TOWER"].DisplayIndex = v++;
                        dataGridView2.Columns["LABST"].DisplayIndex = v++;


                        


                    }
                    
                    if (tabControl1.SelectedIndex == 2 || refTab == 2)
                    {
                        //MaterialCatalog = Client.ZMM_TRANSTECH_MATERIAL(loginName,loginPassword,(TranstechlFields)Enum.Parse(typeof(TranstechlFields), "NotSet"), "", ref subrc, ref MessageReturn);
                        //dataGridView3.DataSource = MaterialCatalog;

                        //if (subrc == 4)//Check for an error
                        //    throw new Exception(MessageReturn);
                    }
 
                }
                catch (Exception Ex)
                {
                   
                    MessageBox.Show(Ex.Message);
                }

                finally
                {
                    Client.Close();
                }

            }
            else
            {
                string err =  "!" + "יש להכנס למערכת תחילה" ;
                MessageBox.Show(err, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ErrBox.Show(err);
            }

        }


        private void clearalltrick(object sender, EventArgs e, int refTab = -1)
        {
               TranstechClient Client = new TranstechClient();
                try
                {

                    Client.Open();

                    if (tabControl1.SelectedIndex == 0 || refTab == 0)
                    {
                        // Function 2 PO header                            

                        PoHeader = Client.ZMM_TRANSTECH_GET_HEADER_PO(loginName, loginPassword, ((TranstechlTower)Enum.Parse(typeof(TranstechlTower), TOWER_STRING)),
                            (TranstechlFields)Enum.Parse(typeof(TranstechlFields), "TOWER"), "STAM", ref subrc, ref MessageReturn, CLIENT_TYPE);
                        dataGridView1.DataSource = PoHeader;

                        if (subrc == 4)//Check for an error
                            throw new Exception(MessageReturn);


                        //dataGridView1.Columns["BSART"].Visible = false;
                        dataGridView1.Columns["ERNAM"].Visible = false;
                        dataGridView1.Columns["USERALIAS"].Visible = false;


                        dataGridView1.Columns["AEDAT"].DisplayIndex = 0;
                        dataGridView1.Columns["EBELN"].DisplayIndex = 1;
                        dataGridView1.Columns["NUMBER"].DisplayIndex = 2;
                        dataGridView1.Columns["LGOBE"].DisplayIndex = 3;
                        dataGridView1.Columns["LGORT"].DisplayIndex = 4;
                        dataGridView1.Columns["TOWER"].DisplayIndex = 5;
                        dataGridView1.Columns["BSART"].DisplayIndex = 6;



                    }

                    if (tabControl1.SelectedIndex == 1 || refTab == 1)
                    {
                        Material_in = Client.ZMM_TRANSTECH_MATERIAL_IN(loginName, loginPassword, ((TranstechlTower)Enum.Parse(typeof(TranstechlTower), TOWER_STRING)),
                            (TranstechlFields)Enum.Parse(typeof(TranstechlFields), "TOWER"), "STAM", ref subrc, ref MessageReturn, CLIENT_TYPE);
                        dataGridView2.DataSource = Material_in;

                        if (subrc == 4)//Check for an error
                            throw new Exception(MessageReturn);


                        //dataGridView2.Columns["CPUDT"].Visible = false;
                        dataGridView2.Columns["MJAHR"].Visible = false;
                        //dataGridView2.Columns["TOWER"].Visible = false;

                        int v = 0;
                        dataGridView2.Columns["CPUDT"].DisplayIndex = v++;
                        dataGridView2.Columns["MBLNR"].DisplayIndex = v++;
                        dataGridView2.Columns["ZEILE"].DisplayIndex = v++;
                        dataGridView2.Columns["MATNR"].DisplayIndex = v++;
                        dataGridView2.Columns["MAKTX"].DisplayIndex = v++;
                        dataGridView2.Columns["MENGE"].DisplayIndex = v++;
                        dataGridView2.Columns["MEINS"].DisplayIndex = v++;
                        dataGridView2.Columns["AXISY"].DisplayIndex = v++;
                        dataGridView2.Columns["AXISX"].DisplayIndex = v++;
                        dataGridView2.Columns["TRAY"].DisplayIndex = v++;

                        dataGridView2.Columns["TOWER"].DisplayIndex = v++;
                        dataGridView2.Columns["LABST"].DisplayIndex = v++;





                    }

                    if (tabControl1.SelectedIndex == 2 || refTab == 2)
                    {
                        //MaterialCatalog = Client.ZMM_TRANSTECH_MATERIAL(loginName,loginPassword,(TranstechlFields)Enum.Parse(typeof(TranstechlFields), "NotSet"), "STAM", ref subrc, ref MessageReturn);
                        //dataGridView3.DataSource = MaterialCatalog;

                        //if (subrc == 4)//Check for an error
                        //    throw new Exception(MessageReturn);
                    }

                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }

                finally
                {
                    Client.Close();
                }

       }



        private void ToolStripMenuItemRefreash_Click(object sender, EventArgs e)
        {
            Refreash(sender, e);
        }
        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            Refreash(sender, e);
        }
        private void operateOrder(object sender, EventArgs e)
        {
            DataGridViewRow rowToSelect = this.dataGridView1.CurrentRow;

            //foreach (DataGridViewCell cell in rowToSelect.Cells)
            //{
            //    if (cell.Value == null || cell.Value.Equals(""))
             //   {
             //       continue;
             //   }
             //   string stLine1 = cell.Value.ToString();
            //}
            DataGridViewCell currCell = dataGridView1.CurrentCell;
            DisplayOrder(currCell.ColumnIndex, currCell.RowIndex);

        }
        private void ToolStripMenuItemOperate_Click(object sender, EventArgs e)
        {
            operateOrder(sender, e);
        }
        private void toolStripButtonOrder_Click(object sender, EventArgs e)
        {
            operateOrder(sender, e);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void ReadConfig()
        {
            Settings set = Settings.Default;
            TOWER_STRING = set.migdal.ToString();
        }

 

        private void signOut(object sender, EventArgs e)
        {

            if (loginToSystem)
            {
                DialogResult res = AppBox.Show("האם ברצונך לצאת מהמערכת?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (res == DialogResult.Yes)
                {
                    clearalltrick(sender, e, 0);
                    clearalltrick(sender, e, 1);                    
                    
                    loginToSystem = false;
                    loginName = ""; 
                    loginPassword ="";
                    panelLoginName.Text = "";

 
                    //need to delete tables.
                }
                else
                {
                    //dont need to do...
                }

            }
            else
            {
                ErrBox.Show("אין משתמש רשום במערכת");

            }
        }
        private void ToolStripMenuItemSignOut_Click(object sender, EventArgs e)
        {
            signOut(sender, e);
        }
        private void toolStripButtonSignOut_Click(object sender, EventArgs e)
        {
            signOut(sender, e);
        }
        private void tabout_Click(object sender, EventArgs e)
        {

        }
        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("Stop Program?",
            //             "Terminate",
             //             MessageBoxButtons.YesNo);

            DialogResult res = AppBox.Show("האם ברצונך לסגור את האפליקציה?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);



            if (res == DialogResult.Yes) Application.Exit();
        }
        private void Save(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Excel Documents (*.xls)|*.xls";

            if (tabControl1.SelectedIndex == 0)
            {
                sfd.FileName = "out.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV(dataGridView1, sfd.FileName); // Here dvwACH is your grid view name

                }

            }
            else if(tabControl1.SelectedIndex == 1)
            {
                sfd.FileName = "in.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV(dataGridView2, sfd.FileName); // Here dvwACH is your grid view name

                }

            }
            else if (tabControl1.SelectedIndex == 2)
            {
                sfd.FileName = "catalog.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV(dataGridView3, sfd.FileName); // Here dvwACH is your grid view name

                }

            }
            
            
            

  
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            Save(sender, e);
        }
        private void ToolStripMenuItemSaveTbl_Click(object sender, EventArgs e)
        {
            Save(sender, e);
        }
        private string BuildExcelConnectionString(string Filename, bool FirstRowContainsHeaders) 
        { 
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=\"Excel 8.0;HDR={1};\"", Filename.Replace("'", "''"), FirstRowContainsHeaders ? "Yes" : "No"); 
        }
        private void ReadFromFile()
        {
            DataTable dt_all;
            dt_all = new DataTable();


            OpenFileDialog sfd = new OpenFileDialog();

            sfd.Filter = "Excel Documents (*.xls)|*.xls";

            if (sfd.ShowDialog() == DialogResult.OK);
  

            string fileName = sfd.FileName; ;
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Cannot find file");
                return;
            }

            string connStr = BuildExcelConnectionString(fileName, true);
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand("Select * From [1$]", conn))
                {
                    using (OleDbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dt_all != null)
                            dt_all.Dispose();
                        dt_all = new DataTable();
                        dt_all.Load(dr);
                    }
                }
            }

            if (tabControl1.SelectedIndex == 0)
            {
                dataGridView1.AutoResizeColumns();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                dataGridView2.AutoResizeColumns();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                dataGridView3.AutoResizeColumns();
            }

           
 
        }
        private void ToolStripMenuItemRead_Click(object sender, EventArgs e)
        {
            ReadFromFile();
        }

        private void setDataGridHeader1(DataGridView dg)
        {
            for (int j = 0; j < dg.Columns.Count; j++)
            {

  

                if (dg.Columns[j].Name == "EBELN")
                    dg.Columns[j].HeaderText = "מספר הזמנה";

                if (dg.Columns[j].Name == "LGOBE")
                    dg.Columns[j].HeaderText = "                מזמין";

                if (dg.Columns[j].Name == "LGORT")
                    dg.Columns[j].HeaderText = "אתר";


                if (dg.Columns[j].Name == "AEDAT")
                    dg.Columns[j].HeaderText = "  תאריך ";


                if (dg.Columns[j].Name == "NUMBER")
                    dg.Columns[j].HeaderText = "    שורות";

                if (dg.Columns[j].Name == "TOWER")
                    dg.Columns[j].HeaderText = "מגדל";

                if (dg.Columns[j].Name == "BSART")
                    dg.Columns[j].HeaderText = "סוג הזמנה";



            }

        }
        private void setDataGridHeader2(DataGridView dg)
        {
            for (int j = 0; j < dg.Columns.Count; j++)
            {

                if (dg.Columns[j].Name == "BSART")
                    dg.Columns[j].HeaderText = "סוג מסמך";

                if (dg.Columns[j].Name == "EBELN")
                    dg.Columns[j].HeaderText = "מספר הזמנה";

                if (dg.Columns[j].Name == "EBELP")
                    dg.Columns[j].HeaderText = "מספר פריט";

                if (dg.Columns[j].Name == "LGOBE")
                    dg.Columns[j].HeaderText = "תיאור מחסן מקבל";

                if (dg.Columns[j].Name == "LGORT")
                    dg.Columns[j].HeaderText = "מחסן מקבל";

                if (dg.Columns[j].Name == "LGPBE")
                    dg.Columns[j].HeaderText = "מספר מגדל";

                if (dg.Columns[j].Name == "MAKTX")
                    dg.Columns[j].HeaderText = "                              תיאור פריט";

                if (dg.Columns[j].Name == "MATNR")
                    dg.Columns[j].HeaderText = "            מק'ט";

                if (dg.Columns[j].Name == "MEINS")
                    dg.Columns[j].HeaderText = "יחידת מידה";

                if (dg.Columns[j].Name == "MENGE")
                    dg.Columns[j].HeaderText = "כמות לאיחסון";

                if (dg.Columns[j].Name == "MENGE2")
                    dg.Columns[j].HeaderText = "כמות נשארה לניפוק";

                if (dg.Columns[j].Name == "AEDAT")
                    dg.Columns[j].HeaderText = "תאריך ";

                if (dg.Columns[j].Name == "ERNAM")
                    dg.Columns[j].HeaderText = "שם משתמש מזמין";

                if (dg.Columns[j].Name == "NUMBER")
                    dg.Columns[j].HeaderText = "מספר שורות בהזמנה";

                if (dg.Columns[j].Name == "TOWER")
                    dg.Columns[j].HeaderText = "מגדל";


                if (dg.Columns[j].Name == "AXISX")
                    dg.Columns[j].HeaderText = "ציר X";

                if (dg.Columns[j].Name == "AXISY")
                    dg.Columns[j].HeaderText = "ציר Y";


                if (dg.Columns[j].Name == "TRAY")
                    dg.Columns[j].HeaderText = "מגש";


                if (dg.Columns[j].Name == "CPUDT")
                    dg.Columns[j].HeaderText = "תאריך כניסה למלאי";


                if (dg.Columns[j].Name == "ZEILE")
                    dg.Columns[j].HeaderText = " שורה ";


                if (dg.Columns[j].Name == "MJAHR")
                    dg.Columns[j].HeaderText = "שנה";

                
                if (dg.Columns[j].Name == "MBLNR")
                    dg.Columns[j].HeaderText = "מספר מסמך";

                if (dg.Columns[j].Name == "LABST")
                    dg.Columns[j].HeaderText = "יתרה במלאי";
  

            }

        }
        private void setDataGridHeader3(DataGridView dg)
        {
            for (int j = 0; j < dg.Columns.Count; j++)
            {

                if (dg.Columns[j].Name == "BSART")
                    dg.Columns[j].HeaderText = "סוג מסמך";

                if (dg.Columns[j].Name == "EBELN")
                    dg.Columns[j].HeaderText = "מספר הזמנה";

                if (dg.Columns[j].Name == "EBELP")
                    dg.Columns[j].HeaderText = "מספר פריט";

                if (dg.Columns[j].Name == "LGOBE")
                    dg.Columns[j].HeaderText = "תיאור מחסן מקבל";

                if (dg.Columns[j].Name == "LGORT")
                    dg.Columns[j].HeaderText = "מחסן מקבל";

                if (dg.Columns[j].Name == "LGPBE")
                    dg.Columns[j].HeaderText = "מספר מגדל";

                if (dg.Columns[j].Name == "MAKTX")
                    dg.Columns[j].HeaderText = "תיאור מק'ט";

                if (dg.Columns[j].Name == "MATNR")
                    dg.Columns[j].HeaderText = "מק'ט";

                if (dg.Columns[j].Name == "MEINS")
                    dg.Columns[j].HeaderText = "יחידת מידה";

                if (dg.Columns[j].Name == "MENGE")
                    dg.Columns[j].HeaderText = "כמות לניפוק";

                if (dg.Columns[j].Name == "MENGE2")
                    dg.Columns[j].HeaderText = "כמות נשארה לניפוק";

                if (dg.Columns[j].Name == "AEDAT")
                    dg.Columns[j].HeaderText = "תאריך הזמנה";

                if (dg.Columns[j].Name == "AXISX")
                    dg.Columns[j].HeaderText = "ציר X";

                if (dg.Columns[j].Name == "AXISY")
                    dg.Columns[j].HeaderText = "ציר Y";

                if (dg.Columns[j].Name == "ERNAM")
                    dg.Columns[j].HeaderText = "שם משתמש מזמין";

                if (dg.Columns[j].Name == "TOWER")
                    dg.Columns[j].HeaderText = "מגדל";

                if (dg.Columns[j].Name == "TRAY")
                    dg.Columns[j].HeaderText = "מגש";


                if (dg.Columns[j].Name == "READY")
                    dg.Columns[j].HeaderText = "בוצע";


                if (dg.Columns[j].Name == "WGBEZ60")
                    dg.Columns[j].HeaderText = "תיאור קבוצת חומרים";

                if (dg.Columns[j].Name == "LABST")
                    dg.Columns[j].HeaderText = "יתרה במלאי";

                if (dg.Columns[j].Name == "MATKL")
                    dg.Columns[j].HeaderText = "קבוצת חומרים";


                if (dg.Columns[j].Name == "MSEHT")
                    dg.Columns[j].HeaderText = "תיאור יחידת מידה";

            }

        }


        private void DisplayOrder(int ColumnIndex, int RowIndex)
        {
            if (ColumnIndex > -1 && RowIndex > -1)
            {


                //////////////////////////////////////////////////////////////////////////////////////////////////////
                string orderNum = Convert.ToString(dataGridView1.Rows[RowIndex].Cells["EBELN"].Value);
                orderItemsForm form = new orderItemsForm();
                form._ORDER_NUMBER = orderNum;


                form._LOGIN_NAME = loginName;
                form._LOGIN_PASSWORD = loginPassword;       

                form.ShowDialog();


                //NEED TO CALL REFREASH
                object sender = new object();
                EventArgs e = new EventArgs();
                Refreash(sender, e);

            }

            

        }


        private int InsertProduct(int ColumnIndex, int RowIndex)
        {
           
            
            int retCode = 0;

            if (ColumnIndex > -1 && RowIndex > -1)
            {

                insertsupply supply = new insertsupply();


                supply._MBLNR = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["MBLNR"].Value);
                supply._ZEILE = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["ZEILE"].Value);
                supply._MATNR = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["MATNR"].Value);
                supply._MAKTX = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["MAKTX"].Value);
                supply._MEINS = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["MEINS"].Value);
                supply._MENGE = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["MENGE"].Value);
                supply._MENGETextBox = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["MENGE"].Value);
                supply._TRAY = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["TRAY"].Value);
                supply._AXISX = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["AXISX"].Value);
                supply._AXISY = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["AXISY"].Value);
                supply._INBOX = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["LABST"].Value);
                supply._TOWER = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["TOWER"].Value);


                supply.ShowDialog();


                if (supply.DialogResult.Equals(DialogResult.OK))
                {
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    V_MATERIAL_IN[] Material_send = new V_MATERIAL_IN[1];
                    V_MATERIAL_IN Mateirla_line = new V_MATERIAL_IN();
                    Mateirla_line.MATNR = supply._MATNR;
                    Mateirla_line.MBLNR = supply._MBLNR;
                    Mateirla_line.MJAHR = Convert.ToString(dataGridView2.Rows[RowIndex].Cells["MJAHR"].Value);
                    Mateirla_line.ZEILE = supply._ZEILE;
                    Mateirla_line.MENGE = Convert.ToDecimal(supply._MENGETextBox);
                    Material_send[0] = Mateirla_line;




                    TranstechClient Client = new TranstechClient();
                    try
                    {

                        retCode = 1; ;
                        
                        Client.Open();

                        Bapireturn = Client.ZMM_TRANSTECH_MATERIAL_REPORT(loginName, loginPassword, (TranstechlFields)Enum.Parse(typeof(TranstechlFields), "NotSet"), "", Material_send, ref subrc, ref MessageReturn, CLIENT_TYPE);

                        if (subrc == 4)//Check for an error
                            throw new Exception(MessageReturn);

                        string text = Bapireturn[0].MESSAGE;
                        //MessageBox.Show(text, "Message", MessageBoxButtons.OK);

                        MyMessageBox.ShowBox(text, "Message");


 


                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                    finally
                    {
                        Client.Close();
                    }


                    //NEED TO CALL REFREASH
                    object sender = new object();
                    EventArgs e = new EventArgs();
                    Refreash(sender, e);

                }


                if (supply.DialogResult.Equals(DialogResult.Cancel))
                {
                    retCode = 2;                    
                }


            }
            else
            {
                retCode = 0;

            }

            return retCode;


        }


         #endregion end class member functions

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
            AboutBox1 login = new AboutBox1();
            login.ShowDialog();


        }

 

        private void ToolStripMenuItemCheckTower_Click(object sender, EventArgs e)
        {

            checktower tower = new checktower();
            tower.ShowDialog();

        }

 


    }
}
