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
    public partial class DetailedSchedule : UserControl
    {
        DbConnection _KetNoi =  new DbConnection();
        public DetailedSchedule()
        {
            InitializeComponent();
        }

        private void DetailedSchedule_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
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
                    // Bạn có thể gọi hàm Pivot tại đây nếu cần biến đổi dữ liệu
                    // DataTable pivotedData = PivotData(dataTable);
                    // DataGridViewSchedule.DataSource = pivotedData;
                    DataGridViewSchedule.DataSource = dataTable; // Sử dụng dữ liệu nguyên thủy nếu không cần pivot
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }
    }
}
