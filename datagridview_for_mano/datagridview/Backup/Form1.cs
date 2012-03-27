using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace datagridview
{
    public partial class Form1 : Form
    {
        int cntr = 0; //used for custom sort toggle
        DataTable dt; // used as datasource of DataGridView
        /// <summary>
        /// Form1 Constructor that calls the intialize component and other custom initalization code
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(405, 290);
            this.dataGridView1.TabIndex = 0;
            //Add the column header mouse cilck event handler
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_ColumnHeaderMouseClick);

            // Creating the custom datatable for binding
             dt = new DataTable();
             dt.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
             dt.Columns.Add(new DataColumn("token_no", System.Type.GetType("System.String")));

            // Adding data to datatable
             DataRow dr = dt.NewRow();
             dr["ID"] = 1;
             dr["token_no"] = "Amit";
             dt.Rows.Add(dr);
             dr = dt.NewRow();
             dr["ID"] = 2;
             dr["token_no"] = "Ajit";
             dt.Rows.Add(dr);
             dr = dt.NewRow();
             dr["ID"] = 3;
             dr["token_no"] = "Rahul";
             dt.Rows.Add(dr);
             dr = dt.NewRow();
             dr["ID"] = 4;
             dr["token_no"] = "Sanjay";
             dt.Rows.Add(dr);
             dr = dt.NewRow();
             dr["ID"] = 5;
             dr["token_no"] = "Zuber";
             dt.Rows.Add(dr);
             dt.AcceptChanges();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Bind the datagridview with datatable
            dataGridView1.DataSource = dt;
        }

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
    }
}
