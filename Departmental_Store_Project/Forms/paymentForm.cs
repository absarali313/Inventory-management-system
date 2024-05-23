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
    public partial class paymentForm : Form
    {
        public paymentForm()
        {
            InitializeComponent();
        }

        private void paymentForm_Load(object sender, EventArgs e)
        {

        }

        private void displayDataGrid()
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select P.PAYMENT_ID,D.DISCOUNT_ID,P.COLLECT_BY,P.Amount,P.COLLECT_DATE,D.PERCENTAGE from PAYMENT P LEFT OUTER JOIN DISCOUNTS D ON P.DISCOUNT_ID = D.DISCOUNT_ID", conn);
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
            string sql = "Insert into payment values (" + id.Text + ",'" + name.Text + "', CAL_PAY(" + id.Text +")"  +",TO_DATE(CURRENT_DATE)," + DISCOUNT.Text + ")";
            this.AUD(sql, 0);
        }
        private void Update()
        {
            string sql;
         
    
            sql = "Update payment set collect_by = '" + name.Text + "', amount = " + id.Text + ")  Where  payment_id =  " + id.Text;
            this.AUD(sql, 1);
        }
        private void Delete()
        {
            string sql = "Delete from payment Where payment_id =  " + id.Text;
            this.AUD(sql, 2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)

                if (name.Text.Length != 0)

                    

                        Add();
                   

                else
                    MessageBox.Show("Enter collector name !");

            else
                MessageBox.Show("Enter id !");
        }

        private void paymentForm_Load_1(object sender, EventArgs e)
        {
            displayDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
                Update();
            else
                MessageBox.Show("Enter id !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
                Delete();
            else
                MessageBox.Show("Enter id !");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
