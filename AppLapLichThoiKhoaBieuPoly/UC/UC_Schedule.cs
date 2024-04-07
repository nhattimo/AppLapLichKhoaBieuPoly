using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using Guna.UI2.WinForms;
using System.Data.Common;
using System.Collections.Generic;

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class UC_Schedule : UserControl
    {
        DbConnection _KetNoi = new DbConnection();
        public UC_Schedule()
        {
            InitializeComponent();
        }

        private void UC_Schedule_Load_1(object sender, EventArgs e)
        {
            LoadDataComboBoxDepartments();
           // LoadDataIntoDataGridView();
            LoadScheduleDataByWeekday(1);
        }

        private void ComboBoxWeekdays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxWeekdays.SelectedValue != null)
            {
                try
                {
                    int weekdayId = Convert.ToInt32(ComboBoxWeekdays.SelectedValue);
                    LoadScheduleDataByWeekday(weekdayId);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error converting weekday ID: " + ex.Message);
                }
            }

        }
        void LoadDataComboBoxDepartments()
        {
            string connectionString = _KetNoi.ConnectionString;
            string selectQuery = "SELECT * FROM Weekdays";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                DataTable dataTable = new DataTable();

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);

                // Gán dữ liệu từ DataTable vào ComboBox
                ComboBoxWeekdays.DataSource = dataTable;
                ComboBoxWeekdays.DisplayMember = "NameWeekday"; // Cột bạn muốn hiển thị trong ComboBox
                ComboBoxWeekdays.ValueMember = "WeekdayID"; // Giá trị tương ứng với mỗi mục được chọn trong ComboBox
            }
        }

        private void LoadDataIntoDataGridView()
        {
            string connectionString = _KetNoi.ConnectionString;  // Cập nhật với chuỗi kết nối thực tế của bạn
            string query = @"
                    SELECT clr.RoomName, 
                           wd.NameWeekday, 
                           st.NameStudy,
                           c.ClassName, 
                           cr.CourseName, 
                           pf.ProfessorName 
                    FROM ClassSchedules cs 
                    INNER JOIN Classes c ON c.ClassID = cs.ClassID 
                    INNER JOIN Courses cr ON cr.CourseID = cs.CourseID 
                    INNER JOIN Professors pf ON pf.ProfessorID = cs.ProfessorID 
                    INNER JOIN Classrooms clr ON clr.RoomID = cs.RoomID 
                    INNER JOIN Weekdays wd ON wd.WeekdayID = cs.WeekdayID 
                    INNER JOIN Studys st ON st.StudyID = cs.StudyID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    DataGridViewSchedule.DataSource = dataTable; // Sử dụng dữ liệu nguyên thủy nếu không cần pivot
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }



        private void LoadScheduleDataByWeekday(int weekdayId)
        {
            LoadScheduleIntoDataGridView(weekdayId);
        }

        private void LoadScheduleIntoDataGridView(int weekdayId)
        {
            string connectionString = _KetNoi.ConnectionString; // Thay thế với chuỗi kết nối của bạn
            List<string> allShifts = GetAllShifts(connectionString);  // Lấy danh sách tất cả các ca học

            string query = @"
            SELECT s.NameStudy, cr.RoomName, cls.ClassName, co.CourseName, p.ProfessorName
            FROM ClassSchedules cs
            JOIN Classrooms cr ON cs.RoomID = cr.RoomID
            JOIN Classes cls ON cs.ClassID = cls.ClassID
            JOIN Courses co ON cs.CourseID = co.CourseID
            JOIN Professors p ON cs.ProfessorID = p.ProfessorID
            JOIN Studys s ON cs.StudyID = s.StudyID
            WHERE cs.WeekdayID = @WeekdayId
            ORDER BY cr.RoomName, s.NameStudy;
            ";

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Phòng / Ca", typeof(string)); // Cột đầu tiên là tên phòng
            foreach (var shift in allShifts)
            {
                dataTable.Columns.Add(shift, typeof(string)); // Thêm một cột cho mỗi ca học
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WeekdayId", weekdayId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string roomName = reader["RoomName"].ToString();
                            string shiftName = reader["NameStudy"].ToString();
                            string classInfo = $"{reader["ClassName"]} - {reader["CourseName"]} - {reader["ProfessorName"]}";

                            // Tìm hoặc tạo dòng cho phòng học
                            DataRow roomRow = dataTable.AsEnumerable().FirstOrDefault(row => row.Field<string>("Phòng / Ca") == roomName);
                            if (roomRow == null)
                            {
                                roomRow = dataTable.NewRow();
                                roomRow["Phòng / Ca"] = roomName;
                                dataTable.Rows.Add(roomRow);
                            }

                            // Thêm thông tin lớp vào ca học tương ứng trong hàng của phòng học
                            roomRow[shiftName] = classInfo;
                        }
                    }
                }
            }

            DataGridViewSchedule.DataSource = dataTable;
        }


        private List<string> GetAllShifts(string connectionString)
        {
            List<string> shifts = new List<string>();
            string query = "SELECT NameStudy FROM Studys ORDER BY StudyID;"; // Truy vấn lấy tất cả ca học

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shifts.Add(reader["NameStudy"].ToString());
                        }
                    }
                }
            }

            return shifts;
        }


    }
}
