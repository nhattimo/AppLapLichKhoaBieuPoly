using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class UC_Professors : UserControl
    {
        DbConnection _KetNoi = new DbConnection();
        string _IDProfessors = "";
        string _IDCoursesInProfessorCourses = "";
        public UC_Professors()
        {
            InitializeComponent();
        }
        private void UC_Professors_Load(object sender, EventArgs e)
        {
            LoadDataCourses();
            LoadDataProfessors();
            guna2PanelProfessorsCourses.Visible = false;
            DataGridViewCourses.Visible = false;
            btnAddProfessors.Visible = true;
            btnNew.Visible = false;
            btnDLCon.Visible = false;
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

        void LoadDataProfessors()
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

        void LoadDataGridViewProfessorsCourses(string id)
        {
            if(id == "")
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
                    string query = "SELECT * FROM Courses c WHERE c.CourseID in (SELECT CourseID FROM ProfessorCourses where ProfessorID = " + _IDProfessors + ")";

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

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            txtNameProfessors.Text = "";
            _IDProfessors = "";
            _IDCoursesInProfessorCourses = "";

            txtNameProfessors.ReadOnly = false;
            guna2PanelProfessorsCourses.Visible = false;
            btnAddProfessors.Visible = true;
            btnNew.Visible = false;
        }

        private void btnAddCourses_Click_1(object sender, EventArgs e)
        {
            DataGridViewCourses.Visible = !DataGridViewCourses.Visible;
        }

        private void btnAddProfessors_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtNameProfessors.Text))
                {
                    MessageBox.Show("Name cannot be empty.");
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                    {
                        connection.Open();

                        // Câu lệnh SQL để chèn dữ liệu vào bảng Professors và trả về ID
                        string insertSql = "INSERT INTO Professors (ProfessorName) OUTPUT INSERTED.ProfessorID VALUES (@ProfessorName)";
                        SqlCommand cmd = new SqlCommand(insertSql, connection);
                        cmd.Parameters.AddWithValue("@ProfessorName", txtNameProfessors.Text);

                        _IDProfessors = cmd.ExecuteScalar().ToString();

                        btnAddProfessors.Visible = false;
                        btnNew.Visible = true;
                        txtNameProfessors.ReadOnly = true;
                        guna2PanelProfessorsCourses.Visible = true;

                        LoadDataProfessors();
                        LoadDataGridViewProfessorsCourses(_IDProfessors);

                        // Đóng kết nối
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNameProfessors.Text))
                {
                    MessageBox.Show("Name cannot be empty.");
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
                    {
                        connection.Open();

                        // Xóa các khóa học của giáo sư từ bảng ProfessorCourses
                        string deleteProfessorCoursesSql = "DELETE FROM ProfessorCourses WHERE ProfessorID = @ProfessorID";
                        SqlCommand deleteProfessorCoursesCmd = new SqlCommand(deleteProfessorCoursesSql, connection);
                        deleteProfessorCoursesCmd.Parameters.AddWithValue("@ProfessorID", _IDProfessors);
                        deleteProfessorCoursesCmd.ExecuteNonQuery();

                        // Xóa giáo sư từ bảng Professors
                        string deleteProfessorsSql = "DELETE FROM Professors WHERE ProfessorID = @ProfessorID";
                        SqlCommand deleteProfessorsCmd = new SqlCommand(deleteProfessorsSql, connection);
                        deleteProfessorsCmd.Parameters.AddWithValue("@ProfessorID", _IDProfessors);
                        deleteProfessorsCmd.ExecuteNonQuery();

                        btnAddProfessors.Visible = false;
                        btnNew.Visible = true;
                        txtNameProfessors.ReadOnly = true;
                        guna2PanelProfessorsCourses.Visible = true;

                        LoadDataProfessors();
                        LoadDataGridViewProfessorsCourses("");

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

        private void DataGridViewCourses_CellClick_1(object sender, DataGridViewCellEventArgs e)
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
                        string insertProfessorCourses = "INSERT INTO ProfessorCourses(CourseID,ProfessorID) Values (" + idCourses + "," + _IDProfessors + ")";
                        _KetNoi.ThucThiCauLenhSQL(insertProfessorCourses);
                        LoadDataGridViewProfessorsCourses(_IDProfessors);
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

        private void DataGridViewProfessor_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewProfessor.CurrentRow.Selected = true;
                DataGridViewProfessor.ReadOnly = true;
                if (DataGridViewProfessor.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    _IDProfessors = DataGridViewProfessor.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNameProfessors.Text = DataGridViewProfessor.Rows[e.RowIndex].Cells[1].Value.ToString();
                    LoadDataGridViewProfessorsCourses(_IDProfessors);
                    btnAddProfessors.Visible = false;
                    btnDelete.Visible = true;
                    btnNew.Visible = true;
                    txtNameProfessors.ReadOnly = true;
                    guna2PanelProfessorsCourses.Visible = true;
                }

            }
            catch (Exception)
            {
                btnDelete.Visible = false;
            }
        }

        private void DataGridViewProfessorsCourses_CellClick_1(object sender, DataGridViewCellEventArgs e)
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
                    _IDCoursesInProfessorCourses = DataGridViewProfessorsCourses.Rows[e.RowIndex].Cells[0].Value.ToString();
                    btnDLCon.Visible = true;
                }

            }
            catch (Exception)
            {
                btnDLCon.Visible = false;
            }

        }

        private void btnDLCon_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("do you want to delete?", "confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Thêm dữ liệu vào database ở đây
                string deleteProfessorCourses = "DELETE FROM ProfessorCourses WHERE ProfessorID = " + _IDProfessors + " and CourseID = " + _IDCoursesInProfessorCourses + ";";
                _KetNoi.ThucThiCauLenhSQL(deleteProfessorCourses);
                LoadDataGridViewProfessorsCourses(_IDProfessors);


                Console.WriteLine("Xóa thành công");
                btnDLCon.Visible = false;
            }
            else
            {
                btnDLCon.Visible = false;
                Console.WriteLine("Không xóa");
            }
        }
        
        private void DataGridViewProfessor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
