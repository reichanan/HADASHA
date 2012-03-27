using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeProject.Dialog;

using System.Configuration;


using datagridview.Properties;
using System.Threading;

namespace datagridview
{
    public partial class proceedOrder : Form
    {
        NumericTextBox numericTextBox1 = new NumericTextBox();
        private string MENGE2 = string.Empty;

        string MACHINE_NO = "";
        string START_LU = "";
        int machine_no_int = 0;
        int start_lu_int = 0;
        bool OPERATE_MACHINE = false;
        int RETRAY = 0;


        public proceedOrder()
        {
            InitializeComponent();

            this.Shown += new EventHandler(f1_Shown); 

            numericTextBox1.Parent = this;
            //Draw the bounds of the NumericTextBox.
            numericTextBox1.Bounds = new Rectangle(345, 200, 53, 15);

            Settings set = Settings.Default;
            MACHINE_NO = set.machine_no.ToString();
            START_LU = set.started_lu.ToString();
            machine_no_int = Convert.ToInt16(MACHINE_NO);
            start_lu_int = Convert.ToInt16(START_LU);
            OPERATE_MACHINE = set.operate_machine;
            RETRAY = set.operation_retray;



        }

        private void callMigdal_Click(object sender, EventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
            simplelogfile.LogToFile("save button - proceed order ");

            string todo = _MENGE2;
            string current = _MENGETextBox;

            decimal todoNumber = Convert.ToDecimal(todo);
            decimal currentNumber = Convert.ToDecimal(current);


            if (currentNumber < 0)
            {

                DialogResult res = AppBox.Show("! אין אפשרות להכניס כמות השווה או הקטנה מאפס    ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                this.DialogResult = DialogResult.Cancel;

            }
            else if (currentNumber != todoNumber)
            {

                DialogResult res = AppBox.Show("הכמות המנופקת בפועל שונה מהכמות המבוקשת. \n? האם ברצונך לשמור את ההזמנה ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (res == DialogResult.Yes)
                {

                    this.DialogResult = DialogResult.OK;
                    simplelogfile.LogToFile("the user happy to save the order ");


                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    simplelogfile.LogToFile("the user refuse to save the order ");
                }



            }
            else
            {
                //nothing to do.
                this.DialogResult = DialogResult.OK;
            }

  

            
        }


        public void f1_Shown(object sender, EventArgs e)
        {

            /*
            save.Enabled = false;
            exit.Enabled = false;

            
            
            if (OPERATE_MACHINE)
            {

                string tray = Convert.ToString(_TRAY);
                int trayNo = Convert.ToInt16(tray);

                for (int i = 0; i < RETRAY; i++)
                {
                    
                    
                    OperateTower.instance().Read((start_lu_int + trayNo));

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
                        save.Enabled = true;
                        exit.Enabled = true;
                        break;
                    }

                    Thread.Sleep(10);

                }

            }

            save.Enabled = true;
            exit.Enabled = true;
            */


        } 


        private void proceedOrder_Load(object sender, EventArgs e)
        {

               //this.Show();
               //this.Select();


 
        
        
        }

        ///



        public string _EBELN
        {
            get
            {
                return EBELN.Text;
            }
            set
            {
                EBELN.Text = value;
            }
        }

        public string _AXISX
        {
            get
            {
                return AXISX.Text;
            }
            set
            {
                AXISX.Text = value;
            }
        }
        public string _AXISY
        {
            get
            {
                return AXISY.Text;
            }
            set
            {
                AXISY.Text = value;
            }
        }

        public string _EBELP
        {
            get
            {
                return EBELP.Text;
            }
            set
            {
                EBELP.Text = value;
            }
        }
 
        public string _LGOBE
        {
            get
            {
                return LGOBE.Text;
            }
            set
            {
                LGOBE.Text = value;
            }
        }
        public string _LGORT
        {
            get
            {
                return LGORT.Text;
            }
            set
            {
                LGORT.Text = value;
            }
        }
        public string _MAKTX
        {
            get
            {
                return MAKTX.Text;
            }
            set
            {
                MAKTX.Text = value;
            }
        }
        public string _MATNR
        {
            get
            {
                return MATNR.Text;
            }
            set
            {
                MATNR.Text = value;
            }
        }        
        public string _MEINS
        {
            get
            {
                return MEINS.Text;
            }
            set
            {
                MEINS.Text = value;
            }
        }
 
        public string _TRAY
        {
            get
            {
                return TRAY.Text;
            }
            set
            {
                TRAY.Text = value;
            }
        }
        public string _MENGE
        {
            get
            {
                return MENGE.Text;
            }
            set
            {
                MENGE.Text = value;
            }
        }

        public string _MENGE2
        {
            get
            {
                return MENGE2;
            }
            set
            {
                MENGE2 = value;
            }
        }

        public string _MENGETextBox
        {
            get
            {
                return numericTextBox1.Text;
            }
            set
            {
                numericTextBox1.Text = value;
            }
        }


        public string _INBOX
        {
            get
            {
                return inbox.Text;
            }
            set
            {
                inbox.Text = value;
            }
        }


        public string _TOWER
        {
            get
            {
                return TOWER.Text;
            }
            set
            {
                TOWER.Text = value;
            }
        }


        private void exit_Click(object sender, EventArgs e)
        {
            simplelogfile.LogToFile("exit button - proceed order ");


            
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }





    }
}
