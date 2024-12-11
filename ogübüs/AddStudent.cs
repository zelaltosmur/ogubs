using System.Data.SqlClient;

namespace ogübüs
{
    public class AddStudent
    {
        private readonly DatabaseHelper dbHelper;

        public AddStudent()
        {
            dbHelper = new DatabaseHelper();
        }

        public void Add(string name, string surname)
        {
            using (SqlConnection connection = dbHelper.GetConnection())
            {
                string query = "INSERT INTO öğrenciler (ad, soyad) VALUES (@name, @surname)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
