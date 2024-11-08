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

namespace ComputersRepairService
{
    public partial class Comments : Form
    {
        int ID;
        public Comments(int id, int role)
        {
            InitializeComponent();
            ID = id;
            load();
            BackColor = Color.FromArgb(255, 215, 227, 191);

            textBox1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button2.BackColor = Color.FromArgb(255, 196, 214, 160);
            if (role != 2)
            {
                textBox1.Enabled = false;
                button2.Enabled = false;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void load() {
            SqlConnectionMethods.OpenConnection();
            DataSet dataGridView1DataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter = new SqlDataAdapter($"SELECT message as Комментарий FROM COMMENTS WHERE REQUESTID = {ID}", SqlConnectionMethods.SqlConnection);
            adapter.Fill(dataGridView1DataSet);
            dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
            SqlConnectionMethods.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("'") || textBox1.Text.Contains("’"))
            {
                MessageBox.Show("Ошибка ввода нового комментария. Измените его содержание.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = string.Empty;
                return;
            }
            SqlConnectionMethods.OpenConnection();
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO COMMENTS VALUES('{textBox1.Text}', {ID})", SqlConnectionMethods.SqlConnection);
                command.ExecuteNonQuery();
                SqlConnectionMethods.CloseConnection();
                load();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода нового комментария. Измените его содержание.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnectionMethods.CloseConnection();
            }
            textBox1.Text = string.Empty;
        }
    }
}
