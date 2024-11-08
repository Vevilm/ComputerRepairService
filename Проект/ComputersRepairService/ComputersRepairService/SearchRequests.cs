using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputersRepairService
{
    public partial class SearchRequests : Form
    {
        int maxCount = 0;
        int type;
        int id;
        string basicRequest = "SELECT * FROM requestsView WHERE 1 = 1";
        public SearchRequests()
        {
            InitializeComponent();
        }
        void GetAll() {
            DataSet dataGridView1DataSet = new DataSet();
            SqlCommand GetType = new SqlCommand("SELECT TYPEID FROM USERS WHERE USERID = " + id, SqlConnectionMethods.SqlConnection);
            SqlConnectionMethods.OpenConnection();
            type = (int)GetType.ExecuteScalar();
            SqlDataAdapter adapter = new SqlDataAdapter();
            switch (type)
            {
                case 2:
                    basicRequest = "SELECT requestsView.* FROM requestsView INNER JOIN requests ON Код = requests.requestID WHERE requests.masterID = " + id;
                    adapter = new SqlDataAdapter(basicRequest, SqlConnectionMethods.SqlConnection);
                    adapter.Fill(dataGridView1DataSet);
                    dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
                    button3.Enabled = false;
                    break;
                case 4:
                    basicRequest = "SELECT requestsView.* FROM requestsView INNER JOIN requests ON Код = requests.requestID WHERE requests.clientID = " + id;
                    adapter = new SqlDataAdapter(basicRequest, SqlConnectionMethods.SqlConnection);
                    adapter.Fill(dataGridView1DataSet);
                    dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
                    break;
                default:
                    button3.Enabled = false;
                    adapter = new SqlDataAdapter(basicRequest, SqlConnectionMethods.SqlConnection);
                    adapter.Fill(dataGridView1DataSet);
                    dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
                    break;
            }
            maxCount = dataGridView1.Rows.Count;
            label1.Text = "Записи: " + maxCount + "/" + maxCount;
            SqlCommand getStatuses = new SqlCommand("SELECT name FROM requestStatuses", SqlConnectionMethods.SqlConnection);
            var statuses = getStatuses.ExecuteReader();
            statusCB.Items.Clear();
            statusCB.Items.Add("Все");

            statusCB.SelectedIndex = 0;
            while (statuses.Read())
            {
                statusCB.Items.Add(statuses.GetString(0));
            }
            statuses.Close();
            SqlCommand getTypes = new SqlCommand("SELECT name FROM computerTechTypes", SqlConnectionMethods.SqlConnection);
            var types = getTypes.ExecuteReader();
            typeCB.Items.Clear();
            typeCB.Items.Add("Все");
            typeCB.SelectedIndex = 0;
            while (types.Read())
            {
                typeCB.Items.Add(types.GetString(0));
            }
            types.Close();

            SqlConnectionMethods.CloseConnection();
        }
        public SearchRequests(int id) : this()
        {
            this.id = id;

            GetAll();

            BackColor = Color.FromArgb(255, 215, 227, 191);
            dataGridView1.BackgroundColor = Color.FromArgb(255, 215, 227, 191);

            typeCB.BackColor = Color.FromArgb(255, 196, 214, 160);
            textBox1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button2.BackColor = Color.FromArgb(255, 196, 214, 160);
            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button3.BackColor = Color.FromArgb(255, 196, 214, 160);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAll();
            dataGridView1.ColumnHeadersVisible = true;
            SqlConnectionMethods.OpenConnection();
            DataSet dataGridView1DataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string filterType = typeCB.Text == "Все" ? "" : $" AND [Тип устройства] = '{typeCB.Text}'";
            string filterStatus = statusCB.Text == "Все" ? "" : $" AND [Статус] = '{statusCB.Text}'";
            try
            {
                dataGridView1DataSet = new DataSet();
                adapter = new SqlDataAdapter(basicRequest + $" {filterStatus} {filterType} AND (Код LIKE '%{textBox1.Text}%' OR [Заявка подана] LIKE '%{textBox1.Text}%' OR [Описание] LIKE '%{textBox1.Text}%' OR [Дата завершения ремонта] LIKE '%{textBox1.Text}%' OR Техник LIKE '%{textBox1.Text}%' OR Клиент LIKE '%{textBox1.Text}%' OR [Модель устройства] LIKE '%{textBox1.Text}%')", SqlConnectionMethods.SqlConnection);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                RequestViewForm requestViewForm = new RequestViewForm((int)dataGridView1[0, e.RowIndex].Value, type);
                this.Hide();
                requestViewForm.ShowDialog();
                this.Show();
                button1_Click(button1, null);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            CreateRequestForm createRequestForm = new CreateRequestForm(id);
            this.Hide();
            createRequestForm.ShowDialog();
            this.Show();
            button1_Click(button1, null);
        }
    }
}
