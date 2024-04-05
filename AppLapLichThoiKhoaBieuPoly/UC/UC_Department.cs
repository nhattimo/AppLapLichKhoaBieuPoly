using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class UC_Department : UserControl
    {
        DbConnection _KetNoi = new DbConnection();
        string _IDClass;
        public UC_Department()
        {
            InitializeComponent();
        }


        private void UC_Department_Load(object sender, EventArgs e)
        {
            LoadDataComboBoxDepartments();
            LoadDataDepartments();
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
            }
        }

        void LoadDataDepartments()
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


        private void btnNew_Click(object sender, EventArgs e)
        {
            txtDepartmentName.Text = "";
            btnAddDepartments.Visible = true;
            btnNew.Visible = false;
            btnDelete.Visible = false;
            txtDepartmentName.ReadOnly = false;
        }

        private void btnAddProfessors_Click(object sender, EventArgs e)
        {


            string connectionString = _KetNoi.ConnectionString;
            string insertQuery = "INSERT INTO Departments (DepartmentName) VALUES (@DepartmentName)";

            string departmentName = txtDepartmentName.Text;

            if (string.IsNullOrWhiteSpace(departmentName))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@DepartmentName", departmentName);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        btnNew.Visible = true;
                        LoadDataDepartments(); // Hàm này để load lại dữ liệu của Departments sau khi chèn mới

                        btnAddDepartments.Visible = false;
                        btnNew.Visible = true;
                        btnDelete.Visible = true;
                        txtDepartmentName.ReadOnly = true;

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
                    string deleteQuery = "DELETE FROM Departments WHERE DepartmentID = @ClassID";

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
                            Console.WriteLine("Đã xóa Departments có DepartmentID = " + _IDClass);
                        }
                        else
                        {
                            Console.WriteLine("Không Departments nào được xóa.");
                        }
                    }
                    LoadDataDepartments();
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
                DataGridViewDepartments.CurrentRow.Selected = true;
                DataGridViewDepartments.ReadOnly = true;
                if (DataGridViewDepartments.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    _IDClass = DataGridViewDepartments.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtDepartmentName.Text = DataGridViewDepartments.Rows[e.RowIndex].Cells[1].Value.ToString();
                   /* ComboBoxDepartments.Text = DataGridViewClasses.Rows[e.RowIndex].Cells[2].Value.ToString();*/
                    btnAddDepartments.Visible = false;
                    btnDelete.Visible = true;
                    btnNew.Visible = true;
                    txtDepartmentName.ReadOnly = true;
                }

            }
            catch (Exception)
            {
                btnDelete.Visible = false;
            }
        }
    }
}
