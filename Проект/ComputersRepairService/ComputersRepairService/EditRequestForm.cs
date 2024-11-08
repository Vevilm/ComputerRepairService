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
    public partial class EditRequestForm : Form
    {
        int id;
        string dateMask;
        public EditRequestForm(int id, int userType)
        {
            InitializeComponent();
            
            dateMask = maskedTextBox2.Text;
            this.id = id;
            SqlConnectionMethods.OpenConnection();
            SqlCommand getRequestStatusID = new SqlCommand("SELECT name FROM requestStatuses", SqlConnectionMethods.SqlConnection);
            var statuses = getRequestStatusID.ExecuteReader();
            while (statuses.Read())
            {
                comboBox1.Items.Add(statuses.GetString(0));
            }
            comboBox1.SelectedIndex = 0;
            statuses.Close();
            SqlCommand getRequest = new SqlCommand("SELECT * FROM requestsView WHERE Код = " + id, SqlConnectionMethods.SqlConnection);
            var request = getRequest.ExecuteReader();
            request.Read();
            string textBox2StartText = request["Клиент"].ToString();
            string textBox3StartText = request["Техник"].ToString();
            label2.Text = "Тип устройства: " + request["Тип устройства"];
            textBox1.Text = request["Модель устройства"].ToString();
            textBox4.Text = request["Описание"].ToString();
            maskedTextBox1.Text = request["Заявка подана"].ToString();
            maskedTextBox2.Text = request["Дата завершения ремонта"].ToString();
            request.Close();
            SqlCommand GetParts = new SqlCommand("SELECT name, count FROM requestsView LEFT JOIN partsToRequests ON requestsView.Код = partsToRequests.requestID WHERE Код = " + id, SqlConnectionMethods.SqlConnection);
            var parts = GetParts.ExecuteReader();
            DataSet dataGridView1DataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            while (parts.Read())
            {
                try
                {
                    dataGridView1DataSet = new DataSet();
                    adapter = new SqlDataAdapter("SELECT Код, name, count FROM requestsView LEFT JOIN partsToRequests ON requestsView.Код = partsToRequests.requestID WHERE Код = " + id, SqlConnectionMethods.SqlConnection);
                    adapter.Fill(dataGridView1DataSet);
                    dataGridView1.DataSource = dataGridView1DataSet.Tables[0];
                }
                catch (Exception)
                {

                }
            }
            parts.Close();
            SqlConnectionMethods.CloseConnection();
            textBox2.Text = textBox2StartText;
            textBox3.Text = textBox3StartText;
            textBox3.Text.Trim();
            textBox6.Enabled = false;
            numericUpDown1.Enabled = false;
            button3.Enabled = false;
            switch (userType)
            {
                case 2:
                    maskedTextBox1.Enabled = false;
                    maskedTextBox2.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox6.Enabled = true;
                    numericUpDown1.Enabled = true;
                    button3.Enabled = true;
                    break;
                case 4:
                    maskedTextBox1.Enabled = false;
                    maskedTextBox2.Enabled = false;
                    textBox1.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox5.Enabled = false;
                    break;
            }
            BackColor = Color.FromArgb(255, 215, 227, 191);
            textBox1.BackColor = Color.FromArgb(255, 196, 214, 160);
            textBox2.BackColor = Color.FromArgb(255, 196, 214, 160);
            textBox3.BackColor = Color.FromArgb(255, 196, 214, 160);
            textBox4.BackColor = Color.FromArgb(255, 196, 214, 160);
            textBox6.BackColor = Color.FromArgb(255, 196, 214, 160);
            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button2.BackColor = Color.FromArgb(255, 196, 214, 160);
            button3.BackColor = Color.FromArgb(255, 196, 214, 160);
            numericUpDown1.BackColor = Color.FromArgb(255, 196, 214, 160);

            comboBox1.BackColor = Color.FromArgb(255, 196, 214, 160);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SqlConnectionMethods.OpenConnection();
            SqlCommand GetClientData = new SqlCommand($"SELECT * FROM EDITVIEW WHERE Тип = 'Заказчик' AND ФИО LIKE '%{textBox2.Text}%'", SqlConnectionMethods.SqlConnection);
            var clientData = GetClientData.ExecuteReader();
            textBox2.AutoCompleteCustomSource.Clear();
            while (clientData.Read())
            {
                textBox2.AutoCompleteCustomSource.Add(clientData["ФИО"].ToString());
            }
            clientData.Close();
            SqlConnectionMethods.CloseConnection();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            SqlConnectionMethods.OpenConnection();
            SqlCommand GetWorkerData = new SqlCommand($"SELECT * FROM EDITVIEW WHERE Тип = 'Техник' AND ФИО LIKE '%{textBox3.Text}%'", SqlConnectionMethods.SqlConnection);
            var workerData = GetWorkerData.ExecuteReader();
            textBox3.AutoCompleteCustomSource.Clear();
            while (workerData.Read())
            {
                textBox3.AutoCompleteCustomSource.Add(workerData["ФИО"].ToString());
            }
            workerData.Close();
            SqlConnectionMethods.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnectionMethods.OpenConnection();
            
            SqlCommand getClientIDByFIO = new SqlCommand($"SELECT Код FROM EDITVIEW WHERE ТИП = 'Заказчик' AND ФИО = '{textBox2.Text.Trim()}'", SqlConnectionMethods.SqlConnection);
            int clientID = (int)(getClientIDByFIO.ExecuteScalar() ?? 0);

            SqlCommand getWorkerIDByFIO = new SqlCommand($"SELECT Код FROM EDITVIEW WHERE ТИП = 'Техник' AND ФИО = '{textBox3.Text.Trim()}'", SqlConnectionMethods.SqlConnection);
            int workerID = (int)(getWorkerIDByFIO.ExecuteScalar() ?? 0);
            
            if (clientID == 0 || (workerID == 0 && textBox3.Text.Trim() != string.Empty))
            {
                MessageBox.Show("Ошибка! Введённое ФИО для техника или заказчика не существует в базе данных.", "Некорректное ФИО", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnectionMethods.CloseConnection();
                return;
            }

            DateTime datetime2;
            try
            {
                DateTime datetime1 = Convert.ToDateTime(maskedTextBox1.Text);
                datetime2 = maskedTextBox2.Text == dateMask ? datetime1.AddYears(10) : Convert.ToDateTime(maskedTextBox2.Text);
                if (datetime2 < datetime1) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Одна или несколько дат были заданы в форме неверно.", "Некорректная дата", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnectionMethods.CloseConnection();
                return;
            }

            try
            {
                SqlCommand getRequestStatusID = new SqlCommand("SELECT requestStatusID FROM requestStatuses", SqlConnectionMethods.SqlConnection);
                int requestStatus = (int)getRequestStatusID.ExecuteScalar();
                
                SqlCommand getTypes = new SqlCommand("SELECT techTypeID FROM computerTechTypes", SqlConnectionMethods.SqlConnection);
                int techType = (int)getTypes.ExecuteScalar();
                
                SqlCommand updateRequest = new SqlCommand($"UPDATE REQUESTS SET startDate = '{maskedTextBox1.Text}', problemDescription = '{textBox4.Text}', requestStatusID = {requestStatus}, masterID = {(workerID == 0 ? (object)"NULL" : (object)workerID)}, clientID = {clientID}, completionDate = {(maskedTextBox2.Text == dateMask ? "NULL" : $"'{maskedTextBox2.Text}'")} WHERE requestID = {id}", SqlConnectionMethods.SqlConnection);
                updateRequest.ExecuteNonQuery();
                
                SqlConnectionMethods.CloseConnection();

            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось внести изменения в запись.", "ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnectionMethods.CloseConnection();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnectionMethods.OpenConnection();
            if (textBox6.Text == string.Empty)
            {
                SqlConnectionMethods.CloseConnection();
                MessageBox.Show("Ошибка оформления заказа. Измените его содержание.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO partsToRequests VALUES({numericUpDown1.Value},'{textBox6.Text}', {id})", SqlConnectionMethods.SqlConnection);
                command.ExecuteNonQuery();
                SqlConnectionMethods.CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка заказа. Измените его содержание.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            numericUpDown1.Value = 0;
            textBox6.Text = string.Empty;
        }
    }
}
