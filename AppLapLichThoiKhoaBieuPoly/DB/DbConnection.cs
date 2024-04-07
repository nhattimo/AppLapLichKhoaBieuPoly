using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace AppLapLichThoiKhoaBieuPoly
{
    public class DbConnection
    {
        public string ConnectionString = @"Data Source=NHATTIMO\MSSQLSERVER01;Initial Catalog=AppThoiKhoaBieu;Integrated Security=True";
        public static SqlConnection Connection = new SqlConnection(@"Data Source=NHATTIMO\MSSQLSERVER01;Initial Catalog=AppThoiKhoaBieu;Integrated Security=True");
        public void MoKetNoi()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();

        }
        public void DongKetNoi()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
        public void ThucThiCauLenhSQL(string SQL)
        {
            try
            {
                MoKetNoi();
                SqlCommand thuThi = new SqlCommand(SQL, Connection);
                thuThi.ExecuteNonQuery();
                DongKetNoi();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kháo ngoại");
            }
        }

        public List<int> ThucThiCauLenhSQLGetListID(string SQL, string NameID)
        {
            List<int> courseIDs = new List<int>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SQL, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int courseID = Convert.ToInt32(reader[NameID]);
                                courseIDs.Add(courseID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return courseIDs;
        }

        public DataTable LayDuLieuBang(string SQL)
        {
            try
            {
                MoKetNoi();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlda = new SqlDataAdapter(SQL, Connection);
                sqlda.Fill(dt);
                DongKetNoi();
                return dt;

            }
            catch (Exception)
            {

                return null;
            }

        }
        public string GetValue(string SQL) // SELECT
        {
            string temp = null;
            MoKetNoi();
            SqlCommand sqlcmd = new SqlCommand(SQL, Connection);
            SqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read())
            {
                temp = sqldr[0].ToString();
            }
            DongKetNoi();
            return temp;
        }
    }
}
