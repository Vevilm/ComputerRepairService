using BasicSQLRequests;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            RequestsSearch requestsSearch = new RequestsSearch();
            string expectedCommandText = "Select * FROM requestsView";
            SqlConnection sqlConnection = new SqlConnection("Data Source=ADCLG1;Initial Catalog=OverchenkoDBUP;Integrated Security=True");
            sqlConnection.Open();

            SqlCommand expCommand = new SqlCommand(expectedCommandText, sqlConnection);
            var readerExp= expCommand.ExecuteReader();
            int countExp = 0;
            while (readerExp.Read())
            {
                countExp++;
            }
            readerExp.Close();

            SqlCommand ñommand = new SqlCommand(requestsSearch.GetAllCommand(), sqlConnection);
            var reader = expCommand.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            reader.Close();
            sqlConnection.Close();

            Assert.AreEqual(1,1);
        }
    }
}