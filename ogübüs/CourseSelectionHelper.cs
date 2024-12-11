using System.Data.SqlClient;

namespace ogübüs
{
    public class CourseSelectionHelper
    {
        private readonly DatabaseHelper dbHelper;

        public CourseSelectionHelper()
        {
            dbHelper = new DatabaseHelper();
        }

        public void PopulateCourseSelectionTable()
        {
            using (SqlConnection connection = dbHelper.GetConnection())
            {
                string query = "INSERT INTO ders_secim (ders, kredi) SELECT ders, kredi FROM Dersler";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
