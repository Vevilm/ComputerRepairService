using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using System.Windows.Forms;

namespace ComputersRepairService
{
    public partial class RequestViewForm : Form
    {
        int id;
        int role;
        public RequestViewForm(int id, int role)
        {
            InitializeComponent();
            this.id = id;
            this.role = role;
            updateData();

            BackColor = Color.FromArgb(255, 215, 227, 191);

            button2.BackColor = Color.FromArgb(255, 196, 214, 160);
            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button3.BackColor = Color.FromArgb(255, 196, 214, 160);
            button4.BackColor = Color.FromArgb(255, 196, 214, 160);

            generateQR();
        }

        void updateData() {
            //SqlConnectionMethods.OpenConnection();
            
            //SqlCommand getRequest = new SqlCommand("SELECT * FROM requestsView WHERE Код = " + id, SqlConnectionMethods.SqlConnection);
            //var request = getRequest.ExecuteReader();
            //request.Read();
            label2.Text = "Тип устройства: Компьютер";
            label3.Text = "Модель устройства: PC RAZOR X1";
            label4.Text = "Описание проблемы:\nСистемный блок дымится при попытке включения приложения MS Office Word.";
            label5.Text = "Клиент: Иванов Пётр Иванович";
            label6.Text = "Дата подачи обращения: 22.10.23";
            label7.Text = "Дата завершения ремонта: 22.12.23";
            label9.Text = "Техник: Орлов Виктор Антонович";
            //request.Close();
            //SqlCommand GetParts = new SqlCommand("SELECT Код, name, count FROM requestsView LEFT JOIN partsToRequests ON requestsView.Код = partsToRequests.requestID WHERE Код = " + id, SqlConnectionMethods.SqlConnection);
            //var parts = GetParts.ExecuteReader();
            string partsTotal = "";
            //while (parts.Read())
            //{
            //    try
            //    {
            //        partsTotal += parts.GetString(1) + " " + parts.GetInt32(2).ToString() + "шт.; ";
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}
            label8.Text = "Заказанные запчасти:\n" + (partsTotal == "" ? "Нет" : partsTotal);
            if (role == 2 || role == 4) button3.Enabled = false;
            //SqlConnectionMethods.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                EditRequestForm editRequestForm = new EditRequestForm(id, role);
                this.Hide();
                editRequestForm.ShowDialog();
                updateData();
                this.Show();
            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение удаления", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (dialog)
            {
                case DialogResult.OK:
                    SqlCommand delete = new SqlCommand("DELETE requests WHERE requestID = " + id, SqlConnectionMethods.SqlConnection);
                    SqlConnectionMethods.OpenConnection();
                    delete.ExecuteNonQuery();
                    SqlConnectionMethods.CloseConnection();
                    this.Close();
                    break;
            }
        }
        void generateQR()
        {
            BarcodeGenerator QRgenerator = new BarcodeGenerator(EncodeTypes.QR);
            QRgenerator.CodeText = "https://forms.yandex.ru/u/672beb1f84227c03d5f9cca7/";
            QRgenerator.Save("wezsrxdtcfvygbuhjnmkl.png", BarCodeImageFormat.Png);
            panel1.BackgroundImage = Image.FromFile("wezsrxdtcfvygbuhjnmkl.png");
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Comments comments = new Comments(id, role);
            comments.ShowDialog();
        }
    }
}
