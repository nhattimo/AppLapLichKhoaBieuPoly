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

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class OtherDetails : UserControl
    {
        DbConnection _KetNoi = new DbConnection();
        public OtherDetails()
        {
            InitializeComponent();
        }

        private void OtherDetails_Load(object sender, EventArgs e)
        {
            LoadDataGridViewClasses();
            LoadDataGridViewProfessor();
            LoadDataGridViewDepartments();
            LoadDataGridViewClassrooms();
            LoadDataGridViewCourses();
        }

        void LoadDataGridViewClasses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Câu lệnh SQL để lấy dữ liệu từ bảng Classes
                    string query = "SELECT a.ClassID, a.ClassName, b.DepartmentName FROM Classes a INNER JOIN Departments b on a.DepartmentID = b.DepartmentID";

                    // Tạo đối tượng Command và gán câu lệnh và kết nối cho nó
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Tạo đối tượng Adapter để đọc dữ liệu
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Tạo DataTable để chứa dữ liệu
                            DataTable dataTable = new DataTable();

                            // Đổ dữ liệu từ Adapter vào DataTable
                            adapter.Fill(dataTable);

                            // Gán DataTable làm nguồn dữ liệu cho DataGridViewClasses
                            DataGridViewClasses.DataSource = dataTable;
                        }
                    }

                    // Đóng kết nối
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void LoadDataGridViewProfessor()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                {
                    connection.Open();

                    // Câu lệnh SQL để lấy dữ liệu từ bảng Courses và Departments
                    //string query = @"SELECT * FROM Professors a INNER JOIN ProfessorCourses b on a.ProfessorID = B.ProfessorID;";
                    string query = @"SELECT * FROM Professors;";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    // Hiển thị dữ liệu lên Guna2DataGridView
                    DataGridViewProfessor.DataSource = dataTable;

                    // Đóng kết nối
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void LoadDataGridViewDepartments()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT DepartmentID, DepartmentName FROM Departments";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            DataGridViewDepartments.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        void LoadDataGridViewClassrooms()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Câu lệnh SQL để lấy dữ liệu từ bảng Classes
                    string query = "SELECT * FROM Classrooms";

                    // Tạo đối tượng Command và gán câu lệnh và kết nối cho nó
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Tạo đối tượng Adapter để đọc dữ liệu
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Tạo DataTable để chứa dữ liệu
                            DataTable dataTable = new DataTable();

                            // Đổ dữ liệu từ Adapter vào DataTable
                            adapter.Fill(dataTable);

                            // Gán DataTable làm nguồn dữ liệu cho DataGridViewClasses
                            DataGridViewClassrooms.DataSource = dataTable;
                        }
                    }

                    // Đóng kết nối
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void LoadDataGridViewCourses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Câu lệnh SQL để lấy dữ liệu từ bảng Classes
                    string query = "SELECT a.CourseID, a.CourseName, b.DepartmentName FROM Courses a INNER JOIN Departments b on a.DepartmentID = b.DepartmentID";

                    // Tạo đối tượng Command và gán câu lệnh và kết nối cho nó
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Tạo đối tượng Adapter để đọc dữ liệu
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Tạo DataTable để chứa dữ liệu
                            DataTable dataTable = new DataTable();

                            // Đổ dữ liệu từ Adapter vào DataTable
                            adapter.Fill(dataTable);

                            // Gán DataTable làm nguồn dữ liệu cho DataGridViewClasses
                            DataGridViewCourses.DataSource = dataTable;
                        }
                    }

                    // Đóng kết nối
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
