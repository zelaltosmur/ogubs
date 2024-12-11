using System.Data.SqlClient;

namespace ogübüs
{
    public class AddCourse
    {
        private readonly DatabaseHelper dbHelper;

        public AddCourse()
        {
            dbHelper = new DatabaseHelper();
        }

        public void Add(string courseName, string credit)
        {
            using (SqlConnection connection = dbHelper.GetConnection())
            {
                string query = "INSERT INTO Dersler (ders, kredi) VALUES (@courseName, @credit)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@courseName", courseName);
                command.Parameters.AddWithValue("@credit", credit);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
