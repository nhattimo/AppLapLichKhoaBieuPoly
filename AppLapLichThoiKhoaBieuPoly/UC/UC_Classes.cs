using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class UC_Classes : UserControl
    {
        DbConnection _KetNoi = new DbConnection();
        string _IDClass;
        string _IDCoursesInClassCourses = "";
        public UC_Classes()
        {
            InitializeComponent();
        }

        private void UC_Classes_Load(object sender, EventArgs e)
        {
            LoadDataComboBoxDepartments();
            LoadDataClasses();
            LoadDataCourses();
            btnNew.PerformClick();
            guna2PanelProfessorsCourses.Visible = false;
            DataGridViewCourses.Visible = false;
            btnAddProfessors.Visible = true;
            btnNew.Visible = false;
            btnDLCon.Visible = false;
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
                    connection.Open();

                    // Câu lệnh SQL để lấy dữ liệu từ bảng Courses và Departments
                    string query = @"SELECT a.CourseID, a.CourseName, b.DepartmentName
                                    FROM Courses a
                                    INNER JOIN Departments b ON a.DepartmentID = b.DepartmentID;";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị dữ liệu lên Guna2DataGridView
                    DataGridViewCourses.DataSource = dataTable;

                    // Đóng kết nối
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void LoadDataClasses()
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
        void LoadDataGridViewClassCourses(string id)
        {
            if (id == "")
            {
                try
                {
                    DataGridViewProfessorsCourses.Rows.Clear();
                    return;
                }
                catch (Exception)
                {

                    return;
                }

            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                {
                    connection.Open();

                    /*string query = "SELECT CourseName FROM Courses c WHERE c.CourseID in (SELECT CourseID FROM ProfessorCourses where ProfessorID = "+_IDProfessors+")";*/
                    string query = "SELECT * FROM Courses c WHERE c.CourseID in (SELECT CourseID FROM ClassesCourses where ClassID = " + _IDClass + ")";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    // Hiển thị dữ liệu lên Guna2DataGridView
                    DataGridViewProfessorsCourses.DataSource = dataTable;

                    btnDLCon.Visible = false;
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
            _IDClass = "";
            _IDCoursesInClassCourses = "";

            txtNameClass.ReadOnly = false;
            guna2PanelProfessorsCourses.Visible = false;
            btnAddProfessors.Visible = true;
            btnNew.Visible = false;
        }

        private void btnAddProfessors_Click(object sender, EventArgs e)
        {
            string connectionString = _KetNoi.ConnectionString;
            string insertQuery = "INSERT INTO Classes (ClassName, DepartmentID) VALUES (@ClassName, @DepartmentID)";

            // Dữ liệu mẫu để chèn vào bảng
            string className = txtNameClass.Text;
            int departmentID = (int)ComboBoxDepartments.SelectedValue; // Giả sử department có ID là 1
            MessageBox.Show(departmentID.ToString());
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Thêm tham số cho câu lệnh SQL để tránh tấn công SQL Injection và cung cấp giá trị thực cho dữ liệu
                    command.Parameters.AddWithValue("@ClassName", className);
                    command.Parameters.AddWithValue("@DepartmentID", departmentID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        btnNew.Visible = true;
                        LoadDataClasses();

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
                    LoadDataClasses();
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
                    guna2PanelProfessorsCourses.Visible = true;
                    LoadDataGridViewClassCourses(_IDClass);
;                }

            }
            catch (Exception)
            {
                btnDelete.Visible = false;
            }
        }

        private void btnAddCourses_Click(object sender, EventArgs e)
        {
            DataGridViewCourses.Visible = !DataGridViewCourses.Visible;
        }

        private void DataGridViewCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCourses.CurrentRow.Selected = true;
                DataGridViewCourses.ReadOnly = true;
                if (DataGridViewCourses.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    string idCourses = DataGridViewCourses.Rows[e.RowIndex].Cells[0].Value.ToString();
                    DialogResult result = MessageBox.Show("You definitely want to add subjects?", "confirm", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Thêm dữ liệu vào database ở đây
                        string insertProfessorCourses = "INSERT INTO ClassesCourses(CourseID,ClassID) Values (" + idCourses + "," + _IDClass + ")";
                        _KetNoi.ThucThiCauLenhSQL(insertProfessorCourses);
                        LoadDataGridViewClassCourses(_IDClass);
                        DataGridViewCourses.Visible = false;


                        Console.WriteLine("Dữ liệu đã được thêm vào.");
                    }
                    else
                    {
                        // Bỏ qua việc thêm dữ liệu
                        DataGridViewCourses.Visible = false;
                        Console.WriteLine("Không thêm dữ liệu.");
                    }

                }
            }
            catch (Exception)
            {
                btnDelete.Visible = false;
            }
        }

        private void DataGridViewProfessorsCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewProfessorsCourses.CurrentRow.Selected = true;
                DataGridViewProfessorsCourses.ReadOnly = true;
                if (DataGridViewProfessorsCourses.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    btnDLCon.Visible = false;
                }
                else
                {
                    _IDCoursesInClassCourses = DataGridViewProfessorsCourses.Rows[e.RowIndex].Cells[0].Value.ToString();
                    btnDLCon.Visible = true;
                }

            }
            catch (Exception)
            {
                btnDLCon.Visible = false;
            }
        }

        private void btnDLCon_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("do you want to delete?", "confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Thêm dữ liệu vào database ở đây
                string deleteProfessorCourses = "DELETE FROM ClassesCourses WHERE ClassID = " + _IDClass + " and CourseID = " + _IDCoursesInClassCourses + ";";
                _KetNoi.ThucThiCauLenhSQL(deleteProfessorCourses);
                LoadDataGridViewClassCourses(_IDClass);


                Console.WriteLine("Xóa thành công");
                btnDLCon.Visible = false;
            }
            else
            {
                btnDLCon.Visible = false;
                Console.WriteLine("Không xóa");
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IDClass == "")
                {
                    MessageBox.Show("class cannot be empty.");
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                    {
                        connection.Open();

                        // Xóa các khóa học của giáo sư từ bảng ProfessorCourses
                        string deleteProfessorCoursesSql = "DELETE FROM ClassesCourses WHERE ClassID = @ClassID";
                        SqlCommand deleteProfessorCoursesCmd = new SqlCommand(deleteProfessorCoursesSql, connection);
                        deleteProfessorCoursesCmd.Parameters.AddWithValue("@ClassID", _IDClass);
                        deleteProfessorCoursesCmd.ExecuteNonQuery();

                        // Xóa giáo sư từ bảng Professors
                        string deleteProfessorsSql = "DELETE FROM Classes WHERE ClassID = @ClassID";
                        SqlCommand deleteProfessorsCmd = new SqlCommand(deleteProfessorsSql, connection);
                        deleteProfessorsCmd.Parameters.AddWithValue("@ClassID", _IDClass);
                        deleteProfessorsCmd.ExecuteNonQuery();

                        btnAddProfessors.Visible = false;
                        btnNew.Visible = true;
                        txtNameClass.ReadOnly = true;
                        guna2PanelProfessorsCourses.Visible = true;

                        LoadDataClasses();
                        LoadDataGridViewClassCourses("");

                        MessageBox.Show("Successful erasing!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            btnNew.PerformClick();
        }
    }
}
