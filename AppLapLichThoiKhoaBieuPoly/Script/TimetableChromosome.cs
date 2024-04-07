using System;
using System.Collections.Generic;

namespace AppLapLichThoiKhoaBieuPoly
{
    //Lớp này đại diện cho một cá thể trong quần thể, tức là một thời khóa biểu.
    // TimetableChromosome (thời gian biểu nhiễm sắc thể)
    public class TimetableChromosome
    {
        public TimetableSlot[,] Timetable { get; set; }
        public double Fitness { get; set; }

        public TimetableChromosome(int weekdays, int shifts)
        {
            Timetable = new TimetableSlot[weekdays, shifts];
            // Khởi tạo mỗi slot trong Timetable
            for (int i = 0; i < weekdays; i++)
            {
                for (int j = 0; j < shifts; j++)
                {
                    Timetable[i, j] = new TimetableSlot();
                }
            }
        }
    }
}
