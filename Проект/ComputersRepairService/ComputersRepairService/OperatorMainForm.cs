using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ComputersRepairService
{
    public partial class OperatorMainForm : Form
    {
        int id;
        public OperatorMainForm()
        {
            InitializeComponent();
        }
        public OperatorMainForm(int id) : this()
        {
            this.id = id;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(255, 215, 227, 191);

            button2.BackColor = Color.FromArgb(255, 196, 214, 160);
            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button3.BackColor = Color.FromArgb(255, 196, 214, 160);
            SqlConnectionMethods.OpenConnection();
            SqlCommand getUser = new SqlCommand("SELECT Surname FROM USERS WHERE USERID = " + id, SqlConnectionMethods.SqlConnection);
            SqlConnectionMethods.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchRequests searchRequests = new SearchRequests(id);
            this.Hide();
            searchRequests.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AuthAttempts authAttempts = new AuthAttempts();
            this.Hide();
            authAttempts.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
