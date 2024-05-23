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
    public partial class adminForm : Form
    {
        public adminForm()
        {
            InitializeComponent();
        }

        private void id_TextChanged(object sender, EventArgs e)
        {

        }
        private void displayDataGrid()
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select USER_ID, Username from admins", conn);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            conn.Close();
        }
        private void adminForm_Load(object sender, EventArgs e)
        {
            displayDataGrid();

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
                    msg = "PRIVILGES granted successfully !";
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
            string sql = "Insert into Admins values (" + id.Text + ",'" + name.Text + "','" + password.Text + "')";
            this.AUD(sql, 0);
           
        }
       
        private void Delete()
        {
            string sql = "DELETE FROM ADMINS WHERE USER_ID =" + id.Text + " OR USERNAME = " + name.Text;   
            this.AUD(sql, 2);
        }
        private void Update()
        {
            string sql = "Update ADMINS SET PASS = " + password.Text +"WHERE USER_ID =" + id.Text + " OR USERNAME = " + name.Text;
            this.AUD(sql, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text.Length >= 5)
            {
                if(password.Text.Length >= 5)
                {
                    
                    Add(); MessageBox.Show("ADDED");
                    displayDataGrid();

                }
                else
                    MessageBox.Show("PASSWORD MUST BE AT LEAST 5 LETTERS LONG !", "INVALID PASSWORD");
            }
            else
                MessageBox.Show("USERNAME MUST BE AT LEAST 5 LETTERS LONG !", "INVALID USER");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (name.Text.Length != 0)
            {
                if (id.Text.Length != 0)
                {
                    Delete();
                    MessageBox.Show("DELETED");
                    displayDataGrid();
                }
                else
                    MessageBox.Show("ENTER ID");
            }
            else
                MessageBox.Show("ENTER USER");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (name.Text.Length != 0 || id.Text.Length != 0)
            {
                
                    Delete();
                    MessageBox.Show("DELETED");
                    displayDataGrid();
                
              
            }
            else
                MessageBox.Show("ENTER EITHER USER OR ID");
        }
    }
}
