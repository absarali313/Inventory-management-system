using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Departmental_Store_Project
{
    public partial class Main : Form
    {
        public static Boolean isLoggedIn = false;
        private Form activeForm;
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.desktopPanel.Controls.Add(childForm);
            this.desktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        public Main()
        {
            
            InitializeComponent();
        }

        private void closeApplicationButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void desktopPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!isLoggedIn)
            {
                OpenChildForm(new Forms.loginForm());
                tabName.Text = "LOGIN";
            }
            else
            {
                InitializeComponent();
                tabName.Text = "Dashboard";
            }
        }   

        public void openDashboard()
        {
            OpenChildForm(new Forms.productForm());
            tabName.Text = "PRODUCTS";
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            
        }

        private void productButton_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                OpenChildForm(new Forms.productForm());
                tabName.Text = "PRODUCTS";
            }
            else
            {
                
                MessageBox.Show("PLEASE LOGIN !", "UNIDENTIFIED USER");
               
            }
        }

        private void stockButton_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                OpenChildForm(new Forms.stockForm());
                tabName.Text = "STOCK";
            }
            else
            {

                MessageBox.Show("PLEASE LOGIN !", "UNIDENTIFIED USER");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                OpenChildForm(new Forms.adminForm());
                tabName.Text = "ADMINS";
            }
            else
            {

                MessageBox.Show("PLEASE LOGIN !", "UNIDENTIFIED USER");

            }
        }

        private void discountButton_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                OpenChildForm(new Forms.discountForm());
                tabName.Text = "DISCOUNTS";
            }
            else
            {

                MessageBox.Show("PLEASE LOGIN !", "UNIDENTIFIED USER");

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                OpenChildForm(new Forms.paymentForm());
                tabName.Text = "PAYMENTS";
            }
            else
            {

                MessageBox.Show("PLEASE LOGIN !", "UNIDENTIFIED USER");

            }
        }

        private void salesButton_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                OpenChildForm(new Forms.salesForm());
                tabName.Text = "SALES";
            }
            else
            {

                MessageBox.Show("PLEASE LOGIN !", "UNIDENTIFIED USER");

            }
        }

        private void graphButton_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                OpenChildForm(new Forms.graphForm());
                tabName.Text = "GRAPH";
            }
            else
            {

                MessageBox.Show("PLEASE LOGIN !", "UNIDENTIFIED USER");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(isLoggedIn)
            {
                isLoggedIn = false;
                MessageBox.Show("Logged Out");
                OpenChildForm(new Forms.loginForm());
                new Forms.loginForm().Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
            WindowState = FormWindowState.Minimized;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void UpperBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
  
}
