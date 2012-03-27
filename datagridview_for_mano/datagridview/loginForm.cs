using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using datagridview.TranstecWS;
using datagridview.Properties;

namespace datagridview
{
    public partial class loginForm : Form
    {
        private string _resultText;
        private string _resultuser;
        private string _resultpassword;
        int client_type = 0;

        
        
        public loginForm()
        {
            InitializeComponent();
            Settings set = Settings.Default;
            client_type = set.client_type;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            //UserName.Text = "manou";
            //UserPassword.Text = "123456QAS";

            
            string MessageReturn = string.Empty;      
            if (UserName.Text != "" && UserPassword.Text != "")
            {


                bool isLogin = false;
                TranstechClient TraCl = new TranstechClient();
                try
                {
                    TraCl.Open();
                    isLogin = TraCl.ZMM_TRANSTECH_AUTHENTICATION(UserName.Text, UserPassword.Text, ref MessageReturn, client_type);


                   
                    
                    if (!isLogin)//Check for an error
                        throw new Exception(MessageReturn);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("שם המשתמש או הססמה שגויים!", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
                }
                finally
                {
                    TraCl.Close();
                }
                if (isLogin)
                {
                    ResultText = UserName.Text;
                    ResultPassword = UserPassword.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            
            }
            else
            {
                MessageBox.Show("אנא מלא את שם המשתמש והססמה!",
                "מידע חסר",MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
  
        }


        public string ResultText
        {
            get{return _resultText;} 
            private set{_resultText = value;}
        }

        public string ResultPassword
        {
            get { return _resultpassword; }
            private set { _resultpassword = value; }
        }

        public string ResultUser
        {
            get { return _resultuser; }
            private set { _resultuser = value; }
        }



        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.DialogResult.Equals(DialogResult.OK))
                this.DialogResult = DialogResult.Cancel;
        } 

  
    }
}
