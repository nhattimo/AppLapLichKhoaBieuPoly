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
    public partial class UC_Courses : UserControl
    {
        DbConnection _KetNoi = new DbConnection();
        string _IDClass;
        public UC_Courses()
        {
            InitializeComponent();
        }
        private void UC_Courses_Load(object sender, EventArgs e)
        {
            LoadDataComboBoxDepartments();
            LoadDataCourses();
            btnNew.PerformClick();
        }

        void LoadDataComboBoxDepartments()
        {
            string connectionString = _KetNoi.ConnectionString;
            string selectQuery = "SELECT * FROM Departments";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                DataTable dataTable = new DataTable();

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);

                // Gán dữ liệu từ DataTable vào ComboBox
                ComboBoxDepartments.DataSource = dataTable;
                ComboBoxDepartments.DisplayMember = "DepartmentName"; // Cột bạn muốn hiển thị trong ComboBox
                ComboBoxDepartments.ValueMember = "DepartmentID"; // Giá trị tương ứng với mỗi mục được chọn trong ComboBox
            }
        }

        void LoadDataCourses()
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtNameClass.Text = "";
            btnAddProfessors.Visible = true;
            btnNew.Visible = false;
            btnDelete.Visible = false;
            txtNameClass.ReadOnly = false;
        }

        private void btnAddProfessors_Click(object sender, EventArgs e)
        {
            string connectionString = _KetNoi.ConnectionString;
            string insertQuery = "INSERT INTO Courses(CourseName, DepartmentID) VALUES (@CourseName, @DepartmentID)";
            // Dữ liệu mẫu để chèn vào bảng
            string CourseName = txtNameClass.Text;
            int departmentID = (int)ComboBoxDepartments.SelectedValue; // Giả sử department có ID là 1
            MessageBox.Show(departmentID.ToString());
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Thêm tham số cho câu lệnh SQL để tránh tấn công SQL Injection và cung cấp giá trị thực cho dữ liệu
                    command.Parameters.AddWithValue("@CourseName", CourseName);
                    command.Parameters.AddWithValue("@DepartmentID", departmentID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        btnNew.Visible = true;
                        LoadDataCourses();

                        btnAddProfessors.Visible = false;
                        btnNew.Visible = true;
                        btnDelete.Visible = true;
                        txtNameClass.ReadOnly = true;

                        Console.WriteLine("Số dòng được thêm vào: " + rowsAffected);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Câu lệnh SQL DELETE với tham số
                    string deleteQuery = "DELETE FROM Classes WHERE ClassID = @ClassID";

                    // Tạo đối tượng Command và gán câu lệnh và kết nối cho nó
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        // Thêm tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@ClassID", _IDClass);

                        // Thực thi câu lệnh
                        int rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra xem có hàng nào bị ảnh hưởng hay không
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Đã xóa lớp học có ClassID = " + _IDClass);
                        }
                        else
                        {
                            Console.WriteLine("Không có lớp học nào được xóa.");
                        }
                    }
                    LoadDataCourses();
                    // Đóng kết nối
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }

        private void DataGridViewClasses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewClasses.CurrentRow.Selected = true;
                DataGridViewClasses.ReadOnly = true;
                if (DataGridViewClasses.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    _IDClass = DataGridViewClasses.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNameClass.Text = DataGridViewClasses.Rows[e.RowIndex].Cells[1].Value.ToString();
                    ComboBoxDepartments.Text = DataGridViewClasses.Rows[e.RowIndex].Cells[2].Value.ToString();
                    btnAddProfessors.Visible = false;
                    btnDelete.Visible = true;
                    btnNew.Visible = true;
                    txtNameClass.ReadOnly = true;
                }

            }
            catch (Exception)
            {
                btnDelete.Visible = false;
            }
        }

        
    }
}
