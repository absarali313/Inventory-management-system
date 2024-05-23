using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Windows.Forms;

namespace Departmental_Store_Project.Forms
{
    public partial class discountForm : Form
    {
        public discountForm()
        {
            InitializeComponent();
        }

        private void discountForm_Load(object sender, EventArgs e)
        {
            displayDataGrid();
        }
        private void displayDataGrid()
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select * from DISCOUNTS", conn);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void AUD(string SQL_statement, int state)
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            string msg = " ";
            switch (state)
            {
                case 0:
                    msg = "Row inserted successfully !";
                    break;
                case 1:
                    msg = "Row Updated successfully !";
                    break;
                case 2:
                    msg = "Row Deleted successfully !";
                    break;
            }
            OracleCommand cmd2 = new OracleCommand(SQL_statement, conn);
            try
            {
                int n = cmd2.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    displayDataGrid();
                }

            }
            catch (Exception exp) { MessageBox.Show("Error !", "Exception Thrown"); };


            conn.Close();
        }
        private void Add()
        {
            string sql = "Insert into DISCOUNTS values (" + ID.Text + "," + percentage.Text + ")";
            this.AUD(sql, 0);
        }
        private void Update()
        {
            
            string sql = "Update DISCOUNTS set PERCENTAGE = " + percentage.Text + "  Where  DISCOUNT_ID =  " + ID.Text;
            this.AUD(sql, 1);
        }
        private void Delete()
        {

            string sql = "DELETE FROM DISCOUNTS  Where  DISCOUNT_ID =  " + ID.Text;
            this.AUD(sql, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ID.Text.Length != 0)
            {
                if(percentage.Text.Length != 0)
                {
                    Update();
                }
                else
                MessageBox.Show("ENTER Percentage");
            }
            else
                MessageBox.Show("ENTER ID");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ID.Text.Length != 0)
            {
                if (percentage.Text.Length != 0)
                {
                    Add();
                }
                else
                 MessageBox.Show("ENTER Percentage");
            }
            else
                MessageBox.Show("ENTER ID");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ID.Text.Length != 0)
            {
                Delete();
            }
            else
                MessageBox.Show("ENTER ID");
        }
    }
}
