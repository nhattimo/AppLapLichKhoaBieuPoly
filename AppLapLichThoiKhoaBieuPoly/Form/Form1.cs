using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        DbConnection _KetNoi = new DbConnection();
        //string _SelectSql = "";
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các ô nhập liệu
            string nameAccount = txtNameAccount.Text;
            string password = txtPassword.Text;

            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrEmpty(nameAccount) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Không được để trống tên tài khoản hoặc mật khẩu");
                return;
            }
            // 

            try
            {
                // Tạo và mở kết nối tới cơ sở dữ liệu
                _KetNoi.MoKetNoi();

                // Tạo câu lệnh SQL để kiểm tra tài khoản và mật khẩu
                string _SelectSql = "SELECT Role.NameRole FROM Account INNER JOIN Role ON Account.RoleID = Role.ID WHERE NameAccount = @UserName AND Password = @Pass";
                using (SqlCommand cmd = new SqlCommand(_SelectSql, DbConnection.Connection))
                {
                    // Thêm tham số cho tên tài khoản
                    cmd.Parameters.AddWithValue("@UserName", nameAccount);
                    // Thêm tham số cho mật khẩu
                    cmd.Parameters.AddWithValue("@Pass", password);

                    // Thực thi câu lệnh SQL và kiểm tra kết quả
                    object role = cmd.ExecuteScalar();
                    if (role != null)
                    {
                        switch (role.ToString())
                        {
                            case "admin":
                                this.Hide();
                                // MessageBox.Show($"Đăng nhập thành công với vai trò {role.ToString()}");
                                FormAdmin frm = new FormAdmin();
                                frm.ShowDialog();
                                this.Close();
                                break;
                            default:
                                MessageBox.Show("Không có vai trò");
                                break;
                        }
                    }
                    else
                    {
                        // Nếu không có kết quả trả về, đăng nhập thất bại
                        MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối sau khi hoàn thành
                _KetNoi.DongKetNoi();
            }  
        }

        private void btnNoAccount_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng chưa được cập nhật");
        }

        private void guna2Button1_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void guna2Button1_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '\0';
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

    }
}
