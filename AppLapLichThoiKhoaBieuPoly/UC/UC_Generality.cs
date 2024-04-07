using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class UC_Generality : UserControl
    {
        
        DbConnection _KetNoi = new DbConnection();
        public UC_Generality()
        {
            InitializeComponent();
        }

        Guna2GradientButton[] btnArray;
        UserControl[] controlArray;

        private void UC_Generality_Load(object sender, EventArgs e)
        {
            btnScheduleDetailed.PerformClick();
        }


        #region Các Function

        void UCManagement(UserControl uC)
        {
            controlArray = new UserControl[] { otherDetails1, detailedSchedule1 , createATimetabl1 };
            Management.UCArrayVisible(controlArray, uC);
        }
        void BtnTasbalClickManagement(Guna2GradientButton btn)
        {
            btnArray = new Guna2GradientButton[] { btnOtherDetailed, btnScheduleDetailed, btnCreateATimetable }; //  btn bán chạy, btn doanh thu, btn doanh số
            Management.BtnTasbalClick(btnArray, Color.Transparent, btn, Color.DarkGray);
            btn.BringToFront();
        }




        #endregion

        private void btnScheduleDetailed_Click(object sender, EventArgs e)
        {
            UCManagement(detailedSchedule1);
            BtnTasbalClickManagement(btnScheduleDetailed);
        }

        private void btnOtherDetailed_Click(object sender, EventArgs e)
        {
            UCManagement(otherDetails1);
            BtnTasbalClickManagement(btnOtherDetailed);
        }

        private void btnCreateATimetable_Click(object sender, EventArgs e)
        {
            UCManagement(createATimetabl1);
            BtnTasbalClickManagement(btnCreateATimetable);
        }
    }
}
