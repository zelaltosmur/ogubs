using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ogübüs
{
    public partial class Form1 : Form
    {
        private readonly DatabaseHelper dbHelper;
        private readonly AddStudent addStudent;
        private readonly DeleteStudent deleteStudent;
        private readonly AddCourse addCourse;
        private readonly CourseSelectionHelper courseSelectionHelper;


        public Form1()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            addStudent = new AddStudent();
            deleteStudent = new DeleteStudent();
            addCourse = new AddCourse();
            courseSelectionHelper = new CourseSelectionHelper();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            listBox1.Items.Clear();
            using (SqlConnection connection = dbHelper.GetConnection())
            {
                string query = "SELECT id, ad, soyad FROM öğrenciler";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string studentInfo = $"{reader["id"]} - {reader["ad"]} {reader["soyad"]}";
                    listBox1.Items.Add(studentInfo);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string surname = textBox2.Text;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname))
            {
                addStudent.Add(name, surname);
                MessageBox.Show("Öğrenci eklendi.");
                LoadStudents();
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }



 

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedItem = listBox1.SelectedItem.ToString();
                int id = int.Parse(selectedItem.Split('-')[0].Trim());

                deleteStudent.DeleteById(id);
                MessageBox.Show("Seçilen öğrenci silindi.");
                LoadStudents();
            }
            else
            {
                MessageBox.Show("Lütfen bir öğrenci seçin.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadStudents();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string courseName = textBox3.Text;
            string credit = listBox2.SelectedItem?.ToString();

            if (!string.IsNullOrWhiteSpace(courseName) && !string.IsNullOrWhiteSpace(credit))
            {
                addCourse.Add(courseName, credit);
                MessageBox.Show("Ders eklendi.");
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun ve bir kredi seçin.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                courseSelectionHelper.PopulateCourseSelectionTable();
                MessageBox.Show("Ders ve kredi bilgileri başarıyla ders-secim tablosuna aktarıldı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                listBox3.Items.Clear();
                List<string> courses = dbHelper.GetCourses();
                foreach (var course in courses)
                {
                    listBox3.Items.Add(course);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox3.SelectedItem != null)
                {
                    string selectedItem = listBox3.SelectedItem.ToString();
                    string courseName = selectedItem.Split('-')[0].Trim(); // Ders adını al
                    dbHelper.DeleteCourse(courseName);

                    MessageBox.Show("Seçilen ders silindi.");
                    button6_Click(sender, e); // ListBox'u yeniden yükle
                }
                else
                {
                    MessageBox.Show("Lütfen bir ders seçin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}

