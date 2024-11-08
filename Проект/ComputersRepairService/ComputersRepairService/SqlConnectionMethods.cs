using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputersRepairService
{
    internal static class SqlConnectionMethods
    {
        public static SqlConnection SqlConnection = new SqlConnection("Data Source=MSI\\SQLEXPRESS01;Initial Catalog=computersRepairService;Integrated Security=True");

        public static void OpenConnection() {
			try
			{
				SqlConnection.Open();
			}
			catch (Exception)
			{
				MessageBox.Show("Произошла ошибка при подключении к Базе данных. Убедитесь, что подключение возможно, и повторите попытку позже.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

        public static void CloseConnection()
        {
            try
            {
                SqlConnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка при подключении к Базе данных. Убедитесь, что подключение возможно, и повторите попытку позже.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
