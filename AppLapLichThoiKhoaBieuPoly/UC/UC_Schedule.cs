using System;
using System.Text;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly.UC
{
    public partial class UC_Schedule : UserControl
    {
        // Cài đặt các tham số của thuật toán di truyền
        int _PopulationSize = 6; // kích thước quần thể khởi tạo
        double _MutationRate = 0.2; // tỉ lệ đột biến
        double _ElitismCount = 0.5; // tỷ lệ phần trăm của quần thể được chọn lọc
        int _TimeDeley = 1;

        public UC_Schedule()
        {
            InitializeComponent();
        }

        private void UC_Schedule_Load(object sender, EventArgs e)
        {
           GeneticAlgorithm();
        }



        // Thuật toán di truyền
        void GeneticAlgorithm()
        {
            
            // Khởi tạo thuật toán di truyền với các tham số cụ thể
            GeneticAlgorithm ga = new GeneticAlgorithm(_PopulationSize, _MutationRate, _ElitismCount, _TimeDeley);

            // Khởi tạo quần thể ban đầu
            ga.GenerateInitialPopulation();
            ga.PrintPopulationData();
            // đánh giá 
            ga.EvaluateFitness();
            // Chọn lọc
            ga.SelFitnessSelective();


            /*// Số thế hệ muốn lặp
            int numberOfGenerations = 2; // Bạn có thể điều chỉnh số lượng này

            for (int generation = 0; generation < numberOfGenerations; generation++)
            {
                // Chọn lọc
                ga.SelectiveGeneration();

                // Lai ghép
                ga.CrossoverPopulation();

                // Đột biến
                ga.MutatePopulation();

                // Đánh giá và chọn lọc sau lai ghép và đột biến (nếu cần)
                ga.SelectiveGeneration();

                // Có thể thêm mã để lưu trữ hoặc hiển thị kết quả tốt nhất của mỗi thế hệ
            }*/

            // In ra kết quả của quần thể cuối cùng
            //ga.PrintTimetable_ListPopulation();
            // ga.PrintTimetable_ListOffspring(); // Nếu bạn có logic cụ thể cho việc xử lý thế hệ con

            Console.WriteLine("Genetic Algorithm completed.");
        }



        void LoadDataGridViewSchedule()
        {

        }
    }
}
