using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly
{
    public partial class FormCreateATimetable : Form
    {
        // Cài đặt các tham số của thuật toán di truyền
        int _PopulationSize ; // kích thước quần thể khởi tạo
        double _MutationRate ; // tỉ lệ đột biến
        double _ElitismCount ; // tỷ lệ phần trăm của quần thể được chọn lọc
        double _HybridizationRate ; //  tỉ lệ lai ghép
        int _NumberOfGenerations ; // số thế hệ muốn lặp
        int _TimeDeley ;
        private List<int> _ListRadioButton;
        public FormCreateATimetable(int populationSize, double mutationRate, double elitismCount, double hybridizationRate, int timeDelay, int numberOfGenerations, List<int> check)
        {
            _PopulationSize = populationSize;
            _MutationRate = mutationRate;
            _ElitismCount = elitismCount;
            _TimeDeley = timeDelay;
            _NumberOfGenerations = numberOfGenerations;
            _PopulationSize = populationSize;
            _HybridizationRate = hybridizationRate;
            _ListRadioButton = check;
            InitializeComponent();
        }

        private void CreateATimetable_Load(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task.Run(async () =>
            {
                await GeneticAlgorithm();
                stopwatch.Stop();

                // Invoking the UI thread to update the UI safely
                Invoke(new Action(() =>
                {
                    TimeSpan ts = stopwatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

                    txtLoad.Text = "Has completed the task in " + elapsedTime;

                    DialogResult result = MessageBox.Show("The task has been completed in " + elapsedTime + ". Do you want to restart the application?", "Restart the application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Application.Restart(); // Restart the application
                        Environment.Exit(0);   // Ensure the current application closes
                    }
                    else
                    {
                        MessageBox.Show("The application will not be restarted. You can continue to use it as usual.", "Restart canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }));
            });
        }
        private async Task GeneticAlgorithm()
        {
            // Tạo một instance của thuật toán di truyền
            GeneticAlgorithm ga = new GeneticAlgorithm(_PopulationSize, _MutationRate, _ElitismCount, _HybridizationRate, _TimeDeley, _NumberOfGenerations, _ListRadioButton);

            // Khởi tạo quần thể ban đầu và chạy thuật toán
            ga.Start(); // Giả sử StartAsync là một hàm bất đồng bộ
        }
    }
}
