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
    public partial class salesForm : Form
    {
        public salesForm()
        {
            InitializeComponent();
        }

        private void salesForm_Load(object sender, EventArgs e)
        {
            displayDataGrid();
        }
        private void displayDataGrid()
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select * from SALES", conn);
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
            string sql = "INSERT INTO SALES VALUES(" +id.Text +" , " + amount.Text +", CAL_SALE(" + amount.Text + "," +stockid.Text +")," +payid.Text+", "+ stockid.Text +")";
            this.AUD(sql, 0);
        }
        private void Delete()
        {
            string sql = "Delete from sales Where sales_id =  " + id.Text;
            this.AUD(sql, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
            {
                if(amount.Text.Length != 0)
                {
                    if(payid.Text.Length != 0)
                    {
                        if (stockid.Text.Length != 0)
                        {
                            Add();
                        }
                        else
                            MessageBox.Show("ENter stock id");
                    }
                    else
                        MessageBox.Show("ENter payment id");
                }
                else
                    MessageBox.Show("ENter amount");
            }
            else
                MessageBox.Show("ENter id");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
            {
                Delete();
            }
            else
                MessageBox.Show("ENter id ");
        }
    }

}
