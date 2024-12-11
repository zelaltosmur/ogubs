using System.Collections.Generic;
using System.Data.SqlClient;

namespace ogübüs
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Data Source=DESKTOP-N5LCA8G;Initial Catalog=ogübüs;Integrated Security=True;Encrypt=False";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        public List<string> GetCourses()
        {
            List<string> courses = new List<string>();
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT ders, kredi FROM Dersler";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string course = $"{reader["ders"]} - {reader["kredi"]}";
                    courses.Add(course);
                }
            }
            return courses;
        }
        public void DeleteCourse(string courseName)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "DELETE FROM Dersler WHERE ders = @courseName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@courseName", courseName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
