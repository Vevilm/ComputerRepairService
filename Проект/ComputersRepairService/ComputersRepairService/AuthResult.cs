using ComputersRepairService.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputersRepairService
{
    public partial class AuthResult : Form
    {
        public AuthResult()
        {
            InitializeComponent();
            panel1.BackgroundImage = Resources.user;
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            
        }
        public AuthResult(int id) : this() 
        {
            SqlCommand getUserData = new SqlCommand("SELECT users.name, users.surname, userTypes.name FROM USERS INNER JOIN USERTYPES ON users.typeID = userTypes.userTypeID WHERE users.userID = " + id, SqlConnectionMethods.SqlConnection);
            SqlConnectionMethods.OpenConnection();
            
            SqlDataReader userData = getUserData.ExecuteReader();

            userData.Read();
            userData.GetString(0);
            nameLabel.Text += " " + userData.GetString(0);
            surnameLabel.Text += " " + userData.GetString(1);
            roleLabel.Text += " " + userData.GetString(2);
            SqlConnectionMethods.CloseConnection();
            BackColor = Color.FromArgb(255, 215, 227, 191);
            EnterBTN.BackColor = Color.FromArgb(255, 196, 214, 160);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
