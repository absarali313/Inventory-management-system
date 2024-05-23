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
    public partial class stockForm : Form
    {
        public stockForm()
        {
            InitializeComponent();
        }

        private void stockForm_Load(object sender, EventArgs e)
        {


            displayDataGrid();

        }
        private void displayDataGrid()
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select S.STOCK_ID, P.P_ID,P.P_NAME,S.QUANTITY,P.P_PRICE,S.SUPPLIER  from STOCK S LEFT OUTER JOIN PRODUCT P ON S.PRODUCT_ID = P.P_ID", conn);
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
            string sql = "Insert into stock values (" + id.Text + ","  + productID.Text  + ","  + quantity.Text + ",'" + supplier.Text + "'" +  ")";
            this.AUD(sql, 0);
        }
        private void Update()
        {
            string sql;
           
            if (productID.Text.Length != 0)
            {
                sql = "Update stock set PRODUCT_ID =" + productID.Text + " Where STOCK_ID = " + id.Text;
                this.AUD(sql, 1);
            }
            if (quantity.Text.Length != 0)
            {
                sql = "Update stock set quantity =" + quantity.Text + " Where STOCK_ID = " + id.Text;
                this.AUD(sql, 1);
            }
            if (supplier.Text.Length != 0)
            {
                sql = "Update stock set supplier ='" + supplier.Text + "' Where STOCK_ID = " + id.Text;
                this.AUD(sql, 1);
            }
            
            

            
        }
        private void Delete()
        {
            string sql = "Delete from STOCK Where STOCK_ID =  " + id.Text;
            this.AUD(sql, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
            {
                
                
                    if (productID.Text.Length != 0)
                    {
                        if (quantity.Text.Length != 0)
                        {
                            if (supplier.Text.Length != 0)
                            {
                                
                                    Add();
                            
                            }
                            else
                                MessageBox.Show("Enter supplier !");
                        }
                        else
                            MessageBox.Show("Enter quanitity !");

                    }
                    else
                        MessageBox.Show("Enter Product ID !");

                

            }
            else
                MessageBox.Show("Enter stock id !");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
                Update();
            else
                MessageBox.Show("Enter ID !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
                Delete();
            else
                MessageBox.Show("Enter ID !");
        }
    }
}
