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
    public partial class CreateRequestForm : Form
    {
        int ID;
        public CreateRequestForm(int ID)
        {
            InitializeComponent();
            this.ID = ID;

            SqlConnectionMethods.OpenConnection();
            SqlCommand getTypes = new SqlCommand("SELECT name FROM computerTechTypes", SqlConnectionMethods.SqlConnection);            
            var types = getTypes.ExecuteReader();
            while (types.Read())
            {
                typeCB.Items.Add(types.GetString(0));
            }
            SqlConnectionMethods.CloseConnection();
            typeCB.SelectedIndex = 0;
            BackColor = Color.FromArgb(255, 215, 227, 191);

            typeCB.BackColor = Color.FromArgb(255, 196, 214, 160);
            descriptiontb.BackColor = Color.FromArgb(255, 196, 214, 160);
            modeltb.BackColor = Color.FromArgb(255, 196, 214, 160);
            button1.BackColor = Color.FromArgb(255, 196, 214, 160);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnectionMethods.OpenConnection();
            
            SqlCommand modelExists = new SqlCommand($"SELECT COUNT(*) FROM computerTechModels WHERE name = '{modeltb.Text}'", SqlConnectionMethods.SqlConnection);
            int modelsCount = (int)(modelExists.ExecuteScalar() ?? 0);

            SqlCommand getTypeID = new SqlCommand($"SELECT TOP 1 (techTypeID) FROM computerTechTypes WHERE name = '{typeCB.Text}'", SqlConnectionMethods.SqlConnection);
            int typeID = (int)getTypeID.ExecuteScalar();

            if (modelsCount == 0)
            {
                SqlCommand insertModel = new SqlCommand($"INSERT INTO computerTechModels VALUES('{modeltb.Text}', {typeID})", SqlConnectionMethods.SqlConnection);
                insertModel.ExecuteNonQuery();
            }

            SqlCommand getTypeFromModel = new SqlCommand($"SELECT TOP 1 (techType) FROM computerTechModels WHERE name = '{modeltb.Text}'", SqlConnectionMethods.SqlConnection);
            int expectedType = (int)(getTypeFromModel.ExecuteScalar() ?? 0);
            
            if (expectedType != typeID)
            {
                MessageBox.Show("Тип устройства не соответствует ожидаемому типу устройства. Измените тип устройства.", "Невозможно внести данные в таблицу!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnectionMethods.CloseConnection();
                return;
            }

            SqlCommand getModelID = new SqlCommand($"SELECT TOP 1 (modelID) FROM computerTechModels WHERE name = '{modeltb.Text}'", SqlConnectionMethods.SqlConnection);
            int modelID = (int)getModelID.ExecuteScalar();
            
            SqlCommand insertRequest = new SqlCommand($"INSERT INTO requests(startDate, problemDescription, requestStatusID, clientID, model) VALUES('{DateTime.Now}','{descriptiontb.Text}', 3, {ID}, {modelID})", SqlConnectionMethods.SqlConnection);
            insertRequest.ExecuteNonQuery();
            
            SqlConnectionMethods.CloseConnection();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
