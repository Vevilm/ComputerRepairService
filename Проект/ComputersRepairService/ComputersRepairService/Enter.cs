using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputersRepairService
{
    public partial class Enter : Form
    {
        int attempt = 0;
        string expectedCapcha;

        public Enter()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            BackColor = Color.FromArgb(255, 215, 227, 191);

            loginTextBox.BackColor = Color.FromArgb(255, 196, 214, 160);
            passwordTextBox.BackColor = Color.FromArgb(255, 196, 214, 160);
            capchaTextBox.BackColor = Color.FromArgb(255, 196, 214, 160);
            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
            button2.BackColor = Color.FromArgb(255, 196, 214, 160);
            button3.BackColor = Color.FromArgb(255, 196, 214, 160);
            //   MessageBox.Show("Ошибка ввода нового комментария. Измените его содержание.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //   MessageBox.Show("Были введены неверные символы! Повторите попытку поиска.", "Неверные символы", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //   MessageBox.Show("Тип устройства не соответствует ожидаемому типу устройства. Измените тип устройства.", "Невозможно внести данные в таблицу!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //   MessageBox.Show("Ошибка! Введённое ФИО для техника или заказчика не существует в базе данных.", "Некорректное ФИО", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show("Произошла ошибка при подключении к Базе данных. Убедитесь, что подключение возможно, и повторите попытку позже.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = passwordTextBox.UseSystemPasswordChar ?
                "Спрятать" : "Показать";
            passwordTextBox.UseSystemPasswordChar = !passwordTextBox.UseSystemPasswordChar;
        }
        private void ShowCapcha()
        {
            panel1.Visible = true;
            button3.Visible = true;
            capchaTextBox.Visible = true;
            panel1.BackgroundImage = Capcha.CreateCapcha(panel1.Size.Width, panel1.Size.Height, out expectedCapcha);
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (attempt != 0)
            {
                if (capchaTextBox.Text != expectedCapcha)
                {
                    MessageBox.Show("Капча введена неверно. Попробуйте снова.", "Неверный ввод капчи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowCapcha();
                    capchaTextBox.Clear();
                    return;
                }
            }
            attempt++;
            SqlConnectionMethods.OpenConnection();
            try
            {
                SqlCommand insertAttempt = new SqlCommand($"INSERT INTO LOGINHISTORY VALUES('{loginTextBox.Text}', '{DateTime.Now}', 0)", SqlConnectionMethods.SqlConnection);
                SqlCommand findID = new SqlCommand("SELECT TOP 1 userID FROM USERS WHERE LOGIN = '" + loginTextBox.Text + "'", SqlConnectionMethods.SqlConnection);
                int id = (int)(findID.ExecuteScalar() ?? 0);
                if (id != 0)
                {
                    SqlCommand Verify = new SqlCommand("SELECT dbo.VERIFY('" + passwordTextBox.Text + "', PASSWORD) FROM USERS WHERE userID = " + id, SqlConnectionMethods.SqlConnection);
                    bool verified = (bool)Verify.ExecuteScalar();
                    if (verified)
                    {
                        insertAttempt = new SqlCommand($"INSERT INTO LOGINHISTORY VALUES('{loginTextBox.Text}', '{DateTime.Now}', 1)", SqlConnectionMethods.SqlConnection);
                        attempt = 0;
                        insertAttempt.ExecuteNonQuery();
                        SqlConnectionMethods.CloseConnection();
                        goToNextForm(id);
                        return;
                    }
                }
                insertAttempt.ExecuteNonQuery();

                SqlConnectionMethods.CloseConnection();
                capchaTextBox.Clear();

                switch (attempt)
                {
                    case 3:
                        disableAll();
                        timer1.Start();
                        MessageBox.Show("Не удалось осуществить вход в аккаунт! Проверьте правильность введённого логина и пароля. В целях безопасности, форма была заблокироавна на три минуты.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4:
                        disableAll();
                        break;
                    default:
                        MessageBox.Show("Не удалось осуществить вход в аккаунт! Проверьте правильность введённого логина и пароля.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось осуществить вход в аккаунт! Проверьте правильность введённого логина и пароля.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            ShowCapcha();
        }
        private void disableAll()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            passwordTextBox.Enabled = false;
            loginTextBox.Enabled = false;
            capchaTextBox.Enabled = false;
            panel1.Enabled = false;
        }
        private void goToNextForm(int id)
        {
            AuthResult authResult = new AuthResult(id);
            authResult.ShowDialog();
            SqlConnectionMethods.OpenConnection();
            SqlCommand GetRole = new SqlCommand("SELECT typeID FROM USERS WHERE USERID = " + id, SqlConnectionMethods.SqlConnection);
            int role = (int)GetRole.ExecuteScalar();
            SqlConnectionMethods.CloseConnection();
            switch (role)
            {
                case 1:
                    OperatorMainForm managerMain = new OperatorMainForm(id);
                    this.Hide();
                    managerMain.ShowDialog();
                    this.Show();
                    break;
                case 2:
                    SearchRequests searchRequestsMaster = new SearchRequests(id);
                    this.Hide();
                    searchRequestsMaster.ShowDialog();
                    this.Show();
                    break;
                case 3:
                    OperatorMainForm operatorMain = new OperatorMainForm(id);
                    this.Hide();
                    operatorMain.ShowDialog();
                    this.Show();
                    break;
                case 4:
                    SearchRequests searchRequestsClient = new SearchRequests(id);
                    this.Hide();
                    searchRequestsClient.ShowDialog();
                    this.Show();
                    break;
                default:
                    MessageBox.Show("Пользователь имеет роль, не обозначенную в системе!", "Недопустимая роль", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            clearFields();
        }
        private void clearFields()
        {
            passwordTextBox.Clear();
            capchaTextBox.Clear();
            loginTextBox.Clear();

            panel1.Visible = false;
            button3.Visible = false;
            capchaTextBox.Visible = false;
        }
        private void capchaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (loginTextBox.Text == string.Empty || passwordTextBox.Text == string.Empty || (capchaTextBox.Text == string.Empty && attempt > 0))
            {
                button2.Enabled = false;
            }
            else button2.Enabled = true;
        }

        int ticks = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ticks++;
                TimeSpan time = TimeSpan.FromMilliseconds((180 - ticks) * 1000);
                label4.Text = (new DateTime(time.Ticks)).Minute.ToString() + ":" + ((new DateTime(time.Ticks)).Second > 10 ? (new DateTime(time.Ticks)).Second.ToString() : "0"+(new DateTime(time.Ticks)).Second.ToString()).ToString();
            }
            catch (Exception)
            {
                label4.Text = "";
                timer1.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowCapcha();
        }

        private void loginTextBox_TextChanged(object sender, EventArgs e)
        {
            if (loginTextBox.Text == string.Empty || passwordTextBox.Text == string.Empty || (capchaTextBox.Text == string.Empty && attempt > 0))
            {
                button2.Enabled = false;
            }
            else button2.Enabled = true;
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (loginTextBox.Text == string.Empty || passwordTextBox.Text == string.Empty || (capchaTextBox.Text == string.Empty && attempt > 0))
            {
                button2.Enabled = false;
            }
            else button2.Enabled = true;
        }

        private void loginTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'') e.Handled = true;
        }
    }
}
