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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace ComputersRepairService
{
    public partial class AuthAttempts : Form
    {
        int maxCount = 0;
        string basicRequest = "SELECT * FROM loginHistory WHERE 1 = 1";
        public AuthAttempts()
        {
            InitializeComponent();
            DataSet dataGridView1DataSet = new DataSet();
            SqlConnectionMethods.OpenConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter = new SqlDataAdapter(basicRequest, SqlConnectionMethods.SqlConnection);
            adapter.Fill(dataGridView1DataSet);
            dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
            maxCount = dataGridView1.Rows.Count;
            label1.Text = "Записи: " + maxCount + "/" + maxCount;
            SqlConnectionMethods.CloseConnection();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(255, 215, 227, 191);
            dataGridView1.BackgroundColor = Color.FromArgb(255, 215, 227, 191);

            textBox1.BackColor = Color.FromArgb(255, 196, 214, 160);

            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button2.BackColor = Color.FromArgb(255, 196, 214, 160);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersVisible = true;
            SqlConnectionMethods.OpenConnection();
            DataSet dataGridView1DataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                dataGridView1DataSet = new DataSet();
                adapter = new SqlDataAdapter(basicRequest + $" AND login like '%{textBox1.Text}%'", SqlConnectionMethods.SqlConnection);
                adapter.Fill(dataGridView1DataSet);
                dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
            }
            catch
            {
                MessageBox.Show("Были введены неверные символы! Повторите попытку поиска.", "Неверные символы", MessageBoxButtons.OK, MessageBoxIcon.Error);
                adapter = new SqlDataAdapter(basicRequest, SqlConnectionMethods.SqlConnection);
                adapter.Fill(dataGridView1DataSet);
                dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
            }
            label1.Text = "Записи: " + dataGridView1.RowCount + "/" + maxCount;
            if (dataGridView1.RowCount == 0)
            {
                dataGridView1.ColumnHeadersVisible = false;
                MessageBox.Show("По введённому запросу не было найдено ни одной записи!", "Запись отсутствует", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            SqlConnectionMethods.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
