namespace AppLapLichThoiKhoaBieuPoly
{
    // Lớp này đại diện cho một sự kiện trong thời khóa biểu, bao gồm thông tin về giảng viên, lớp học phần và môn học.
    public class TimetableEvent
    {
        public int ClassID { get; set; }
        public int CourseID { get; set; }
        public int ProfessorID { get; set; }
        public int RoomID { get; set; }
        public int WeekdayID { get; set; }
        public int StudyID { get; set; }
    }
}
