using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using datagridview.TranstecWS;
using System.IO;
using System.Reflection;
using System.Configuration;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using CodeProject.Dialog;



using datagridview.Properties;

namespace datagridview
{
    public partial class orderItemsForm : Form
    {
        public List<order> LIST_ORDER = new List<order>();
        System.Data.DataTable dt_items; // used as datasource of DataGridView
        bool once = false;
        Dictionary<string, errorValues> dictionary = new Dictionary<string, errorValues>();
 

        //List<order> newList = new List<order>();
        string ORDER_NUMBER;
        string LOGIN_NAME;
        string LOGIN_PASSWORD;
        V_PO[] Po = new V_PO[0];
        string MessageReturn = string.Empty;


        string MACHINE_NO = "";
        string START_LU = "";
        int machine_no_int = 0;
        int start_lu_int = 0;
        bool OPERATE_MACHINE = false;
        string TOWER_STRING = "";
        int CLIENT_TYPE = 0;


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //DialogResult frmclose = MessageBox.Show("orderItemsForm?", "orderItemsForm", MessageBoxButtons.YesNo);

            simplelogfile.LogToFile("force closing orderitemsform");
            
            sendToMachineAccordingStatus();


        }
      
        public orderItemsForm()
        {
            InitializeComponent();

            this.dataGridViewWithItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Blue;
            columnHeaderStyle.Font = new System.Drawing.Font("Verdana", 8, FontStyle.Bold);
            dataGridViewWithItems.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            Settings set = Settings.Default;
            MACHINE_NO = set.machine_no.ToString();
            START_LU = set.started_lu.ToString();
            machine_no_int = Convert.ToInt16(MACHINE_NO);
            start_lu_int = Convert.ToInt16(START_LU);
            OPERATE_MACHINE = set.operate_machine;
            TOWER_STRING = set.migdal.ToString();
            CLIENT_TYPE = set.client_type;

        }

        public string _ORDER_NUMBER
        {
            get
            {
                return ORDER_NUMBER;
            }
            set
            {
                ORDER_NUMBER = value;
            }
        }

        public string _LOGIN_NAME
        {
            get
            {
                return LOGIN_NAME;
            }
            set
            {
                LOGIN_NAME = value;
            }
        }


        public string _LOGIN_PASSWORD
        {
            get
            {
                return LOGIN_PASSWORD;
            }
            set
            {
                LOGIN_PASSWORD = value;
            }
        }


        private void Refreash(object sender, EventArgs e)
        {
  

            
            int subrc = 0;
            TranstechClient Client = new TranstechClient();
            try
            {
                Client.Open();
                Po = Client.ZMM_TRANSTECH_GET_PO(LOGIN_NAME, LOGIN_PASSWORD, ((TranstechlTower)Enum.Parse(typeof(TranstechlTower), TOWER_STRING)),
                    (TranstechlFields)Enum.Parse(typeof(TranstechlFields), "EBELN"), ORDER_NUMBER, ref subrc, ref MessageReturn, CLIENT_TYPE);
                //Session.Add("Po", Po);
                dataGridViewWithItems.DataSource = Po;

                if (!once)
                {

                    DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
                    col1.DataPropertyName = "ID_TYPE";
                    col1.HeaderText = " שגיאה";
                    col1.Name = "TYPE";
                    dataGridViewWithItems.Columns.Add(col1);


                    DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
                    col2.DataPropertyName = "ID_MESSAGE";
                    col2.HeaderText = "הודעת השגיאה";
                    col2.Name = "MESSAGE";
                    dataGridViewWithItems.Columns.Add(col2);

                    once = true;
                }








                if (subrc == 4)//Check for an error
                    throw new Exception(MessageReturn);



                dataGridViewWithItems.Columns["BSART"].Visible = false;
                dataGridViewWithItems.Columns["ERNAM"].Visible = false;
                //dataGridViewWithItems.Columns["TOWER"].Visible = false;
                //dataGridViewWithItems.Columns["TOWER"].Visible = false;

                dataGridViewWithItems.Columns["AEDAT"].DisplayIndex = 0;
                dataGridViewWithItems.Columns["EBELN"].DisplayIndex = 1;
                dataGridViewWithItems.Columns["EBELP"].DisplayIndex = 2;
                dataGridViewWithItems.Columns["LGOBE"].DisplayIndex = 3;
                dataGridViewWithItems.Columns["LGORT"].DisplayIndex = 4;
                dataGridViewWithItems.Columns["MATNR"].DisplayIndex = 5;
                dataGridViewWithItems.Columns["MAKTX"].DisplayIndex = 6;
                dataGridViewWithItems.Columns["MENGE"].DisplayIndex = 7;
                dataGridViewWithItems.Columns["MEINS"].DisplayIndex = 8;
                dataGridViewWithItems.Columns["AXISY"].DisplayIndex = 9;
                dataGridViewWithItems.Columns["AXISX"].DisplayIndex = 10;
                dataGridViewWithItems.Columns["TRAY"].DisplayIndex = 11;


                dataGridViewWithItems.Columns["TOWER"].DisplayIndex = 12;
                dataGridViewWithItems.Columns["MENGE2"].DisplayIndex = 13;
                dataGridViewWithItems.Columns["LABST"].DisplayIndex = 14;




                for (int i = 0; i < dataGridViewWithItems.RowCount; i++)
                {
                    dataGridViewWithItems.Rows[i].Cells["TYPE"].Value = "";
                    dataGridViewWithItems.Rows[i].Cells["MESSAGE"].Value ="";


                }


 
                //GridView.DataBind();
                //Response.Write(MessageReturn);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                //Client.Close();
            }





            //////////////////////////////////////

            dt_items.AcceptChanges();
            dataGridViewWithItems.AutoResizeColumns();
            LIST_ORDER.RemoveRange(0, LIST_ORDER.Count());


            ///////////////////////////////////////////////////////////////

            if (dataGridViewWithItems.RowCount ==0)
            {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();

            }


            for (int i = 0; i < dataGridViewWithItems.RowCount; i++)
            {

                order orderItem = new order();

                string orderNum = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["EBELN"].Value);

                orderItem._EBELN = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["EBELN"].Value); //order number
                orderItem._AEDAT = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["AEDAT"].Value);
                orderItem._AXISX = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["AXISX"].Value);
                orderItem._AXISY = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["AXISY"].Value);
                orderItem._BSART = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["BSART"].Value);
                orderItem._EBELN = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["EBELN"].Value);
                orderItem._EBELP = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["EBELP"].Value);
                orderItem._ERNAM = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["ERNAM"].Value);
                orderItem._LGOBE = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["LGOBE"].Value);
                orderItem._LGORT = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["LGORT"].Value);
                orderItem._MAKTX = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["MAKTX"].Value);

                orderItem._MATNR = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["MATNR"].Value);
                orderItem._MEINS = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["MEINS"].Value);
                orderItem._MENGE = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["MENGE"].Value);
                orderItem._MENGE2 = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["MENGE2"].Value);
                orderItem._TOWER = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["TOWER"].Value);
                orderItem._TRAY = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["TRAY"].Value);
                orderItem._INBOX = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["LABST"].Value);


                LIST_ORDER.Add(orderItem);
            }


            /////////////////////////////////////////////////////////////
   
        }



        private void displayOrderItems(object sender, EventArgs e)
        {

            dt_items = new System.Data.DataTable();

            dataGridViewWithItems.DataSource = dt_items;


            dt_items.Columns.Add(new DataColumn("AEDAT", System.Type.GetType("System.Int32"))); //AEDAT  //תאריך הזמנה";
            dt_items.Columns.Add(new DataColumn("EBELN", System.Type.GetType("System.String"))); //EBELN "מספר הזמנה";
            dt_items.Columns.Add(new DataColumn("EBELP", System.Type.GetType("System.String")));  //EBELP //"מספר שורה בהזמנה";
            dt_items.Columns.Add(new DataColumn("LGOBE", System.Type.GetType("System.String"))); //LGOBE  //"תיאור מחסן מזמין";
            dt_items.Columns.Add(new DataColumn("LGORT", System.Type.GetType("System.String"))); //LGORT  // "מספר מחסן מזמין";
            dt_items.Columns.Add(new DataColumn("MATNR", System.Type.GetType("System.String"))); //MATNR   //"מק'ט";
            dt_items.Columns.Add(new DataColumn("MAKTX", System.Type.GetType("System.String"))); //MAKTX  //"תיאור מק'ט";
            dt_items.Columns.Add(new DataColumn("MEINS", System.Type.GetType("System.String"))); //MEINS  //"יחידת מידה";
            dt_items.Columns.Add(new DataColumn("TRAY", System.Type.GetType("System.String"))); //TRAY   //"מגש";

            dt_items.Columns.Add(new DataColumn("AXISX", System.Type.GetType("System.String"))); //AXISX //"ציר X";
            dt_items.Columns.Add(new DataColumn("AXISY", System.Type.GetType("System.String"))); //AXISY //"ציר Y";
            dt_items.Columns.Add(new DataColumn("MENGE", System.Type.GetType("System.String"))); //MENGE  // "כמות לניפוק";
            dt_items.Columns.Add(new DataColumn("MENGE2", System.Type.GetType("System.String"))); //MENGE2 //"כמות נשארה לניפוק";


            dt_items.Columns.Add(new DataColumn("BSART", System.Type.GetType("System.String"))); //BSART //"סוג הזמנה";
            dt_items.Columns.Add(new DataColumn("ERNAM", System.Type.GetType("System.String"))); //ERNAM  //"שם משתמש מזמין";
            dt_items.Columns.Add(new DataColumn("TOWER", System.Type.GetType("System.String"))); //TOWER //"מגדל";
            dt_items.Columns.Add(new DataColumn("LABST", System.Type.GetType("System.String"))); //TOWER //"מגדל";
            //dt_items.Columns.Add(new DataColumn("READY", System.Type.GetType("System.String"))); //READY

 
 
            setDataGridHeader(dataGridViewWithItems);
            Refreash(sender,  e);

   
        }

        private void setDataGridHeader(DataGridView dg)
        {
            for (int j = 0; j < dg.Columns.Count; j++)
            {

                if (dg.Columns[j].Name == "BSART")
                    dg.Columns[j].HeaderText = "סוג הזמנה";

                if (dg.Columns[j].Name == "EBELN")
                    dg.Columns[j].HeaderText = "מספר הזמנה";

                if (dg.Columns[j].Name == "EBELP")
                    dg.Columns[j].HeaderText = "שורה";

                if (dg.Columns[j].Name == "LGOBE")
                    dg.Columns[j].HeaderText = "    מזמין";

                if (dg.Columns[j].Name == "LGORT")
                    dg.Columns[j].HeaderText = "אתר";

                if (dg.Columns[j].Name == "LGPBE")
                    dg.Columns[j].HeaderText = "מספר מגדל";

                if (dg.Columns[j].Name == "MAKTX")
                    dg.Columns[j].HeaderText = "תאור פריט";

                if (dg.Columns[j].Name == "MATNR")
                    dg.Columns[j].HeaderText = "מק'ט";

                if (dg.Columns[j].Name == "MEINS")
                    dg.Columns[j].HeaderText = "יחידת מידה";

                if (dg.Columns[j].Name == "MENGE")
                    dg.Columns[j].HeaderText = "כמות לניפוק";

                if (dg.Columns[j].Name == "MENGE2")
                    dg.Columns[j].HeaderText = "כמות נשארה";

                if (dg.Columns[j].Name == "READY")
                    dg.Columns[j].HeaderText = "בוצע";


               if (dg.Columns[j].Name == "AEDAT")
                    dg.Columns[j].HeaderText = "תאריך ";

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

              if (dg.Columns[j].Name == "LABST")
                  dg.Columns[j].HeaderText = "כמות במלאי";

  


             
            }

        }


        private void sendToMachineAccordingStatus()
        {

            simplelogfile.LogToFile("start sendToMachineAccordingStatus");
            List<int> LIST_TRAY = new List<int>();
            int RETRAY = 0;
            


            for (int i = 0; i < dataGridViewWithItems.RowCount; i++)
            {
                if (OPERATE_MACHINE)
                {
                    ///need here to send all messages to the tower
                    string tray = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["TRAY"].Value);
                    int trayNo = Convert.ToInt16(tray);

                    OperateTower.instance().Read((start_lu_int + trayNo));

                    Thread.Sleep(120);
                    string text = System.IO.File.ReadAllText(@"lu.txt");
                    string result = text.Remove(0, 4);
                    string machine = result.Substring(0, 4);
                    string status = result.Substring(4);

                    string strtrim = status.Trim();

                    if (strtrim == "W")
                    {

                        string s = String.Format("call tray {0} for machine {1}", (start_lu_int + trayNo), machine_no_int);

                        int ret = OperateTower.instance().Del((start_lu_int + trayNo));

                        if (ret == 1)
                        {
                            //LU IN TABLE
                            //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string strerror = "error: Order Item Del" + s;
                            simplelogfile.LogToFile(strerror);
                        }
                        else
                        {
                            //LU NOT IN TABLE

                            string strerror = "success: Order Item Del" + s;
                            simplelogfile.LogToFile(strerror);
                        }

                    }
 
                }

            }//END FOR

           
            for (int i = 0; i < dataGridViewWithItems.RowCount; i++)
            {
                if (OPERATE_MACHINE)
                {
                    ///need here to send all messages to the tower
                    string tray = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["TRAY"].Value);
                    int trayNo = Convert.ToInt16(tray);

                    OperateTower.instance().Read((start_lu_int + trayNo));

                    Thread.Sleep(120);
                    string text = System.IO.File.ReadAllText(@"lu.txt");
                    string result = text.Remove(0, 4);
                    string machine = result.Substring(0, 4);
                    string status = result.Substring(4);

                    string strtrim = status.Trim();

 
                    if (strtrim == "P")
                    {

                        string s1 = String.Format("call tray {0} for machine {1}", (start_lu_int + trayNo), machine_no_int);

                        int ret = OperateTower.instance().RemotePickingEnd((start_lu_int + trayNo), machine_no_int, 1);

                        if (ret == 1)
                        {
                            //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string strerror = "error: order item RemotePickingEnd" + s1;

                            simplelogfile.LogToFile(strerror);
                        }
                        else
                        {
                            string str = "success RemotePickingEnd" + s1;
                            simplelogfile.LogToFile(str);

                        }

                    }
                    else if (strtrim == "E")
                    {
                        LIST_TRAY.Add(trayNo);


                    }

 
                    

                 }

            }//END FOR


            foreach (int trayno in LIST_TRAY)
            {
                

               for (int i = 0; i < RETRAY; i++)
                {
                    OperateTower.instance().Read((start_lu_int + trayno));

                    Thread.Sleep(120);
                    string text = System.IO.File.ReadAllText(@"lu.txt");
                    string result = text.Remove(0, 4);
                    string machine = result.Substring(0, 4);
                    string status = result.Substring(4);

                    string strtrim = status.Trim();

                    if (strtrim == "W")
                    {
                        //nothing to do ok.
                    }
                    else if (strtrim == "E")
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    else if (strtrim == "P")
                    {


                        string s1 = String.Format("call tray {0} for machine {1}", (start_lu_int + trayno), machine_no_int);

                        int ret = OperateTower.instance().RemotePickingEnd((start_lu_int + trayno), machine_no_int, 1);

                        if (ret == 1)
                        {
                            //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string strerror = "error: order item RemotePickingEnd" + s1;

                            simplelogfile.LogToFile(strerror);
                        }
                        else
                        {
                            string str = "success RemotePickingEnd" + s1;
                            simplelogfile.LogToFile(str);

                        }                       
                        
                        
                        
                        
                        break;
                    }

                }

            }

            
            
                        
            simplelogfile.LogToFile("end sendToMachineAccordingStatus");

        }
        
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            int x = 0;
            int nextTray = 0;
            int currentTray = 0;

            simplelogfile.LogToFile("the user start working on order");


            for (int i = 0; i < dataGridViewWithItems.RowCount; i++)
            {
                if (OPERATE_MACHINE)
                {
                    ///need here to send all messages to the tower
                    string tray = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["TRAY"].Value);
                    int trayNo = Convert.ToInt16(tray);

                    int ret = OperateTower.instance().LUCall((start_lu_int + trayNo), machine_no_int, 1);

                    string s = String.Format("call tray {0} for machine {1}", (start_lu_int + trayNo), machine_no_int);

                    if (ret == 1)
                    {
                        //LU IN TABLE
                        //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string strerror = "error: order item LUCall" + s;

                        simplelogfile.LogToFile(strerror);
                    }
                    else
                    {
                        //LU NOT IN TABLE
                        string str = "success LUCall" + s;
                        simplelogfile.LogToFile(str);
                    }

                }

            }


            foreach (order _orderItem in LIST_ORDER) // Loop through List with foreach
            {
                
                proceedOrder order = new proceedOrder();

                //order._AEDAT = _orderItem._AEDAT;
                order._AXISX = _orderItem._AXISX;
                order._AXISY = _orderItem._AXISY;
                //order._BSART = _orderItem._BSART;
                order._EBELN = _orderItem._EBELN;
                order._EBELP = _orderItem._EBELP;
                //order._ERNAM = _orderItem._ERNAM;
                order._LGOBE = _orderItem._LGOBE;
                order._LGORT = _orderItem._LGORT;
                order._MAKTX = _orderItem._MAKTX;
                order._MATNR = _orderItem._MATNR;
                order._MEINS = _orderItem._MEINS;
                order._MENGE = _orderItem._MENGE;
                order._MENGE2 = _orderItem._MENGE2;
                order._MENGETextBox = _orderItem._MENGE2;
                order._TOWER = _orderItem._TOWER;
                order._TRAY = _orderItem._TRAY;
                //order._DONE = _orderItem._DONE;
                order._INBOX = _orderItem._INBOX;

                
                
               


                if (_orderItem._DONE == "OK")
                {
   
                    x++;
                    continue;
                    

                }

                order.ShowDialog();

                if (order.DialogResult.Equals(DialogResult.OK))
                {
                       //dataGridViewWithItems.Rows[x].Cells["READY"].Value = "OK";

                    //dataGridViewWithItems.Rows[x].Cells["EBELN"].Style.BackColor =
                     //           System.Drawing.Color.Green;

                    dataGridViewWithItems.Rows[x].Cells["EBELP"].Style.BackColor = System.Drawing.Color.Yellow;
                    dataGridViewWithItems.Rows[x].Cells["MENGE2"].Style.BackColor = System.Drawing.Color.Yellow;
                    decimal y = Convert.ToDecimal(dataGridViewWithItems.Rows[x].Cells["MENGE2"].Value) - Convert.ToDecimal(order._MENGETextBox);
                    dataGridViewWithItems.Rows[x].Cells["MENGE2"].Value = y.ToString();


                    decimal inboxafter = Convert.ToDecimal(dataGridViewWithItems.Rows[x].Cells["LABST"].Value) - Convert.ToDecimal(order._MENGETextBox);
                    dataGridViewWithItems.Rows[x].Cells["LABST"].Value = inboxafter;


 

                     currentTray = Convert.ToInt16(_orderItem._TRAY);

                     int listsize = LIST_ORDER.Count();
                     order _orderItem1;

                     if (x < (listsize - 1))
                     {

                         _orderItem1 = LIST_ORDER[x + 1];
                         nextTray = Convert.ToInt16(_orderItem1._TRAY);
                     }
                     else
                     {
                         nextTray = -1; //end of list
                     }


                     if (currentTray != nextTray)
                     {

                         if (OPERATE_MACHINE)
                         {
                             int tray = Convert.ToInt16(currentTray);

                             string s = String.Format("call tray {0} for machine {1}", (start_lu_int + tray), machine_no_int);

                             int ret = OperateTower.instance().RemotePickingEnd((start_lu_int + tray), machine_no_int, 1);

                             if (ret == 1)
                             {
                                 //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 string strerror = "error: order item RemotePickingEnd" + s;

                                 simplelogfile.LogToFile(strerror);
                             }
                             else
                             {
                                 string str = "success RemotePickingEnd" + s;
                                 simplelogfile.LogToFile(str);

                             }
                         }
                         
 

                     }

                     _orderItem._DONE = "OK";
                     _orderItem._MENGE2 = order._MENGETextBox;
                     x++;


                }

                if (order.DialogResult.Equals(DialogResult.Cancel))
                {

                    simplelogfile.LogToFile("cancel button ordewritem");
                    
                    if (OPERATE_MACHINE)
                    {
                        
                        int tray = Convert.ToInt16(order._TRAY);

                        string s = String.Format("call tray {0} for machine {1}", (start_lu_int + tray), machine_no_int);

                        int ret = OperateTower.instance().RemotePickingEnd((start_lu_int + tray), machine_no_int, 1);

                        if (ret == 1)
                        {
                            //MessageBox.Show("need to check lu and LU Status", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string strerror = "error: order item RemotePickingEnd" + s;

                            simplelogfile.LogToFile(strerror);
                        }
                        else
                        {
                            string str = "success RemotePickingEnd" + s;
                            simplelogfile.LogToFile(str);

                        }
                    }                   
                    
                    break;
                }

                

            }

            simplelogfile.LogToFile("the user finsih working on order");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            int listsize = 0;
            foreach (order _orderItem in LIST_ORDER) // Loop through List with foreach
            {

                if (_orderItem._DONE == "OK")
                    listsize++;
            }

            if (listsize > 0)
            {

                DialogResult res = AppBox.Show("? לפני היציאה , האם ברצונך לבצע עדכון ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (res == DialogResult.Yes)
                {
                    simplelogfile.LogToFile("btnCancel_Click - order items form save the order ");
                    update_Click(sender, e);

                }
  

            }
            
            simplelogfile.LogToFile("cancel button orderitemsform ");
            
            sendToMachineAccordingStatus();
              
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void orderItemsForm_Load(object sender, EventArgs e)
        {
            displayOrderItems(sender, e);
        }

        private void update_Click(object sender, EventArgs e)
        {

            simplelogfile.LogToFile("update button orderitemsform ");
            
            int subrc = 0;
            //check how many entries there are in the list. 
            int listsize = 0;
            foreach (order _orderItem in LIST_ORDER) // Loop through List with foreach
            {

                if (_orderItem._DONE == "OK")
                    listsize++;
            }


            TranstechClient Client = new TranstechClient();
            try
            {

                V_MIGO[] Migo = new V_MIGO[listsize];
                int counter = 0;

                foreach (order _orderItem in LIST_ORDER) // Loop through List with foreach
                {

                    if (_orderItem._DONE == "OK")
                    {
                        //decimal menge = Convert.ToDecimal(_orderItem._MENGE2);
                        //if (menge != 0)
                        //{

                            V_MIGO Migo_line = new V_MIGO();
                            //_orderItem._MENGE2 = "2";
                            Migo_line.MATERIAL = _orderItem._MATNR;
                            Migo_line.PO_ITEM = _orderItem._EBELP;
                            Migo_line.PO_NUMBER = _orderItem._EBELN;
                            Migo_line.ENTRY_QNT = Convert.ToDecimal(_orderItem._MENGE2);
                            Migo_line.STGE_LOC = _orderItem._LGORT;
                            Migo[counter] = Migo_line;
                            counter++;
                        //}


                    }
                }

                Client.Open();
                DateTime EndDate = DateTime.Now;
                V_BAPIRETURN[] Bapireturn = new V_BAPIRETURN[0];
                string MigoNumber = Client.ZMM_TRANSTECH_MIGO(LOGIN_NAME, LOGIN_PASSWORD, Migo, ref Bapireturn, "", EndDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), "Test rub", ref subrc, ref MessageReturn, CLIENT_TYPE);
                string xxx = MigoNumber;
                //Label.Text = MigoNumber;
                //if (!String.IsNullOrEmpty(MessageReturn))
                //  Label.Text = MessageReturn;

                string s = String.Format("create MIGO  subrc : {0}  MigoNumber: {1} ", (subrc), MigoNumber);
                simplelogfile.LogToFile(s);
                
                dictionary.Clear();
                

                if (subrc == 4)//Check for an error
                {
                    string text = ": הנפקת טובין נכשלה  ";
                    //MessageBox.Show(text, "Message", MessageBoxButtons.OK);
               
                    //MessageBox.Show(text, "Message", MessageBoxButtons.OK);

                    MyMessageBox.ShowBox(text, "Message");


                    foreach (V_BAPIRETURN BapiLine in Bapireturn)
                    {
                        errorValues error = new errorValues();
                        int row = BapiLine.ROW - 1;

                         error._MESSAGE = BapiLine.MESSAGE;
                        error._TYPE = BapiLine.TYPE;
                        error._ROW = dataGridViewWithItems.Rows[row].Cells["EBELP"].Value.ToString();
                        dictionary.Add(error._ROW, error);


                    }

                    //Refreash(sender, e);


                    for (int i = 0; i < dataGridViewWithItems.RowCount; i++)
                    {

                        string ebelp = Convert.ToString(dataGridViewWithItems.Rows[i].Cells["EBELP"].Value);

                        if (dictionary.ContainsKey(ebelp))
                        {

                            errorValues getError = dictionary[ebelp];

                            dataGridViewWithItems.Rows[i].Cells["MESSAGE"].Value = getError._MESSAGE;
                            dataGridViewWithItems.Rows[i].Cells["TYPE"].Value = getError._TYPE;
                            dataGridViewWithItems.Rows[i].Cells["TYPE"].Style.BackColor = System.Drawing.Color.Red;
                        }


                    }



                }

                else if (subrc == 0)
                {
                    string text =   "הנפקת טובין בוצעה בהצלחה . מספר:   " + MigoNumber  ;
                    //MessageBox.Show(text, "Message", MessageBoxButtons.OK);

                    MyMessageBox.ShowBox(text, "Message");
   
                    Refreash(sender, e);

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


            sendToMachineAccordingStatus();

 


        }


    public static void Excel(DataGridView dgv, string filename)
    {

        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

       
       xlApp.Application.Workbooks.Add(Type.Missing);
        xlApp.Columns.ColumnWidth = "15";
   

        // storing header part in Excel 

        for (int i = 1; i < dgv.Columns.Count + 1; i++)
        {

            xlApp.Cells[1, i] = dgv.Columns[i - 1].HeaderText;

        }


        // storing Each row and column value to excel sheet 

        for (int i = 0; i < dgv.Rows.Count - 1; i++)
        {

            if (dgv.Rows[i].Cells[0].Value.ToString() == "E")
            {

                for (int j = 0; j < dgv.Columns.Count ; j++)
                {


                    string str = dgv.Rows[i].Cells[j].Value.ToString();
                    xlApp.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();

                }

            }
 

        } 


      xlApp.ActiveWorkbook.SaveCopyAs(filename);
      xlApp.ActiveWorkbook.Saved = true;

      xlApp.Quit();
   }





        private void Savetosheet_Click(object sender, EventArgs e)
        {


            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Excel Documents (*.xls)|*.xls";

   
                sfd.FileName = "out.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    //ToCsV(dataGridViewWithItems, sfd.FileName);
                    Excel(dataGridViewWithItems, sfd.FileName);

                }

          }


        private void ToCsV(DataGridView dGV, string filename)
        {

            
            string stOutput = "";

            // Export titles:

            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)

                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText ) + "\t";

            stOutput += sHeaders + "\r\n";

            // Export data.

            for (int i = 0; i < dGV.RowCount; i++)
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



    }
}




//Posting only possible in periods 2012/01 and 2011/12 in company code 1000