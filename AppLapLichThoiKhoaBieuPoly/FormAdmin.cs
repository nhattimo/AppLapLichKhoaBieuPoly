using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly
{
    public partial class FormAdmin : Form
    {
        Guna2GradientTileButton[] btnArray;
        UserControl[] controlArray;
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            btnGenerality.PerformClick();
        }

        void UCManagement(UserControl uC)
        {
            controlArray = new UserControl[] {uC_Professors1, uC_Classes1, uC_Courses1 , uC_Classrooms1, uC_Department1, uC_Generality1, uC_Schedule1 };
            Management.UCArrayVisible(controlArray, uC);
        }

        void BtnTasbalClickManagement(Guna2GradientTileButton btn)
        {
            btnArray = new Guna2GradientTileButton[] { btnProfessors, btnClasses, btnCourses, btnClassRooms, btnDepartment, btnGenerality ,btnSchedule };
            Management.BtnTasbalClick(btnArray, Color.Transparent, btn, Color.DarkGray);
            btn.BringToFront();
            //btnLogOut.Visible = false;
        }

        private void btnProfessors_Click(object sender, EventArgs e)
        {
            UCManagement(uC_Professors1);
            BtnTasbalClickManagement(btnProfessors);
        }

        private void btnClasses_Click(object sender, EventArgs e)
        {
            UCManagement(uC_Classes1);
            BtnTasbalClickManagement(btnClasses);
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            UCManagement(uC_Courses1);
            BtnTasbalClickManagement(btnCourses);
        }

        private void btnClassRooms_Click(object sender, EventArgs e)
        {
            UCManagement(uC_Classrooms1);
            BtnTasbalClickManagement(btnClassRooms);
        }


        private void btnGenerality_Click(object sender, EventArgs e)
        {
            UCManagement(uC_Generality1);
            BtnTasbalClickManagement(btnGenerality);
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            UCManagement(uC_Department1);
            BtnTasbalClickManagement(btnDepartment);
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            UCManagement(uC_Schedule1);
            BtnTasbalClickManagement(btnSchedule);
        }
        private void uC_Professors1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
