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
    public partial class productForm : Form
    {
        public productForm()
        {
            InitializeComponent();
        }

        private void productForm_Load(object sender, EventArgs e)
        {
            displayDataGrid();
        }
        private void displayDataGrid()
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select * from PRODUCT", conn);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void Add()
        {
            string sql = "Insert into product values (" + id.Text + ",'" + name.Text + "'," + price.Text  + ")";
            this.AUD(sql, 0);
        }
        private void Update()
        {
            string sql;
            if(price.Text.Length == 0) 
                sql = "Update product set P_Name =  '" + name.Text + "'  Where  P_ID =  " + id.Text;
            else if(name.Text.Length == 0)
                sql = "Update product set P_Price =  " + price.Text + "  Where  P_ID =  " + id.Text;
            else
                sql = "Update product set P_Name = '" + name.Text + "', P_Price =  " + price.Text + "  Where  P_ID =  " + id.Text;
            this.AUD(sql, 1);
        }
        private void Delete()
        {
            string sql = "Delete from product Where P_ID =  " + id.Text ;
            this.AUD(sql, 2);
        }
        private void AUD(string SQL_statement, int state)
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            string msg = " ";
            switch(state)
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

            }catch(Exception exp) { MessageBox.Show("Error !", "Exception Thrown"); };
           
          
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)

                if (name.Text.Length != 0)

                    if (price.Text.Length != 0)

                        Add();
                    else
                        MessageBox.Show("Enter price !");

                else
                    MessageBox.Show("Enter name !");

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

        private void button3_Click(object sender, EventArgs e)
        {
            if (id.Text.Length != 0)
                Update();
            else
                MessageBox.Show("Enter id !");
        }
    }

   
}
