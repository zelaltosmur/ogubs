﻿using System.Data.SqlClient;

namespace ogübüs
{
    public class DeleteStudent
    {
        private readonly DatabaseHelper dbHelper;

        public DeleteStudent()
        {
            dbHelper = new DatabaseHelper();
        }

        public void DeleteAll()
        {
            using (SqlConnection connection = dbHelper.GetConnection())
            {
                string query = "DELETE FROM öğrenciler";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteById(int id)
        {
            using (SqlConnection connection = dbHelper.GetConnection())
            {
                string query = "DELETE FROM öğrenciler WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
