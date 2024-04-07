using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class CreateATimetabl : UserControl
    {
        int _PopulationSize ; // kích thước quần thể khởi tạo
        double _MutationRate ; // tỉ lệ đột biến
        double _ElitismCount ; // tỷ lệ phần trăm của quần thể được chọn lọc
        double _HybridizationRate ; //  tỉ lệ lai ghép
        int _NumberOfGenerations; // số thế hệ muốn lặp
        int _TimeDeley = 1;
        public CreateATimetabl()
        {
            InitializeComponent();
        }

        private void CreateATimetabl_Load(object sender, EventArgs e)
        {
            _PopulationSize = 1000; // kích thước quần thể khởi tạo
            _MutationRate = 0.5; // tỉ lệ đột biến
            _ElitismCount = 0.5; // tỷ lệ phần trăm của quần thể được chọn lọc
            _HybridizationRate = 0.5; //  tỉ lệ lai ghép
            _NumberOfGenerations = 4; // số thế hệ muốn lặp
            _TimeDeley = 1;

            txtPopulationSize.Text = _PopulationSize.ToString();
            txtMutationRate.Text = _MutationRate.ToString();
            txtElitismCount.Text = _ElitismCount.ToString();
            txtHybridizationRate.Text = _HybridizationRate.ToString();
            txtNumberOfGenerations.Text = _NumberOfGenerations.ToString();
            txtTimeDeley.Text = _TimeDeley.ToString();


        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            _PopulationSize = int.Parse(txtPopulationSize.Text); // kích thước quần thể khởi tạo
            _MutationRate = double.Parse(txtMutationRate.Text); // tỉ lệ đột biến
            _ElitismCount = double.Parse(txtElitismCount.Text); // tỷ lệ phần trăm của quần thể được chọn lọc
            _HybridizationRate = double.Parse(txtHybridizationRate.Text); //  tỉ lệ lai ghép
            _NumberOfGenerations = int.Parse(txtNumberOfGenerations.Text); ; // số thế hệ muốn lặp
            _TimeDeley = int.Parse(txtTimeDeley.Text); ;

            DialogResult result = MessageBox.Show("do you want to continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                List<int> checkedValues = new List<int>();

                if (guna2CheckBox1.Checked) checkedValues.Add(1);
                if (guna2CheckBox2.Checked) checkedValues.Add(2);
                if (guna2CheckBox3.Checked) checkedValues.Add(3);
                if (guna2CheckBox4.Checked) checkedValues.Add(4);
                if (guna2CheckBox5.Checked) checkedValues.Add(5);
                if (guna2CheckBox6.Checked) checkedValues.Add(6);
                if (guna2CheckBox7.Checked) checkedValues.Add(7);
                if (guna2CheckBox8.Checked) checkedValues.Add(8);

                // Tạo form mới với các tham số và hiển thị
                FormCreateATimetable formCreateATimetable = new FormCreateATimetable(
                    _PopulationSize,
                    _MutationRate,
                    _ElitismCount,
                    _HybridizationRate,
                    _TimeDeley,
                    _NumberOfGenerations,
                    checkedValues
                );
                formCreateATimetable.Show();


            }
        }
    }
}
