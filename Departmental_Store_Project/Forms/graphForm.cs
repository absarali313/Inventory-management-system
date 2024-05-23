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
    public partial class graphForm : Form
    {
        public graphForm()
        {
            InitializeComponent();
        }

        private void graphForm_Load(object sender, EventArgs e)
        {
            String connect = "USER ID=ABSAR;DATA SOURCE=localhost:1521/ORCL;password=12345";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connect;
            conn.Open();
            OracleCommand cmd2 = new OracleCommand("Select * from stockData",conn);
        
            OracleDataReader reader = cmd2.ExecuteReader();
            while(reader.Read())
            {
                chart1.Series["B1"].Points.AddXY(reader["Stock_ID"], reader["totalsale"]);
            }
            conn.Close();


        }
    }
}
