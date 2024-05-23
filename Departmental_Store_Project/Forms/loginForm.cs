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
    public partial class loginForm : Form
    {
      
        public loginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool loginFailed = false;
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd2 = new OracleCommand("Select USERNAME, PASS from ADMINS", conn);

            OracleDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                if(usernameEnter.Text == reader["USERNAME"].ToString() && passwordEnter.Text == reader["PASS"].ToString())
                {

                    Main.isLoggedIn = true;
                    this.Close();
                    loginFailed = false;
                    MessageBox.Show("LOGGED IN SUCCESSFULLY !", "LOGGED IN ");
                    
                    break;
                }
                loginFailed = true;
            }
            conn.Close();
            if (loginFailed)
                MessageBox.Show("WRONG USER OR PASSWORD", "LOGIN FAILED");
        }
    }
}
