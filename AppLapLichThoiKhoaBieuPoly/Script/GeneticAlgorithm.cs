using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace AppLapLichThoiKhoaBieuPoly
{
    // Lớp này chứa các phương thức để thực hiện thuật toán di truyền.
    public class GeneticAlgorithm
    {
        // Genetic Algorithm (thuật toán di truyền)

        #region Properties and Contractor (thuộc tính và hàm tạo)

        DbConnection _KetNoi;
        private string connectionString;
        // Khai báo kích thước quần thể, tỉ lệ đột biến, số lượng cá thể tinh hoa, các thông số lịch trình và danh sách các ID
        private int _PopulationSize; // số lượng cá thể
        private double _MutationRate; // tỉ lệ đột biến
        private double _ElitismCount; // tỉ lệ chọn 
        private double _HybridizationRate; // tỉ lệ lai ghép
        private int _TimeDelay; // Thời gian trễ giữa các ca học
        private int _Fitness; // Giá trị đánh giá chất lượng của thời khóa biểu
        private int _NumberOfGenerations; // số thế hệ muốn lặp
        private List<TimetableChromosome> _ListPopulation; // Danh sách chứa quần thể hiện tại
        private List<TimetableChromosome> _ListOffspring; // Danh sách chứa quần thể con sau lai ghép
        private bool _SuperiorPopulation;
        private List<int> _ListShift; // Danh sách ID ca học 
        private List<int> _ListWeekdays; // Danh sách ID thứ trong tuần
        private List<int> _ListClassIDs; // Danh sách ID lớp học
        private List<int> _ListCourseIDs; // Danh sách ID khóa học
        private List<int> _ListProfessorIDs; // Danh sách ID giáo viên
        private List<int> _ListRoomIDs; // Danh sách ID phòng học
        private readonly Random _Random; // Đối tượng Random để tạo số ngẫu nhiên
        private List<int> _ListRadioButton;
        // Phương thức khởi tạo lớp GeneticAlgorithm với các tham số cần thiết để thiết lập quá trình tiến hóa
        public GeneticAlgorithm(int populationSize, double mutationRate, double elitismCount,double hybridizationRate, int timeDelay, int numberOfGenerations, List<int> check)
        {
            _KetNoi = new DbConnection();
            connectionString = _KetNoi.ConnectionString;
            _PopulationSize = populationSize;
            _MutationRate = mutationRate;
            _ElitismCount = elitismCount;
            _TimeDelay = timeDelay;
            _NumberOfGenerations = numberOfGenerations;
            _PopulationSize = populationSize;
            _HybridizationRate = hybridizationRate;
            _Fitness = 2000;
            _SuperiorPopulation = false;
            _ListPopulation = new List<TimetableChromosome>(_PopulationSize);
            _ListOffspring = new List<TimetableChromosome>();
            _Random = new Random(); // Khởi tạo đối tượng Random
            _ListRadioButton = check;
            _ListShift = GetListStudyIDs();
            _ListWeekdays = GetListWeekdayIDs();
            _ListClassIDs = GetListClassIDs();
            _ListCourseIDs = GetListCourseIDs();
            _ListProfessorIDs = GetListProfessorIDs();
            _ListRoomIDs = GetListRoomIDs();
            
        }
        #endregion


        #region Initialize Population (Khởi tạo dân số)

        // Phương thức để khởi tạo quần thể ban đầu
        private void GenerateInitialPopulation()
        {
            int weekdays = _ListWeekdays.Count;
            int shift = _ListShift.Count - 1;
            int numberOfClassesPerShift = _ListClassIDs.Count / shift;
            int excessClass = _ListClassIDs.Count % shift;

            // vòng lặp này chạy cho số lượng cá thể 
            for (int i = 0; i < _PopulationSize; i++)
            {
                Console.WriteLine($"______________    chromosome # {i}   _______________");
                TimetableChromosome chromosome = new TimetableChromosome(weekdays, shift); // ma trận [6,5]
                chromosome.Fitness = _Fitness;
                for (int day = 0; day < 2; day++) // Lặp qua 2 ngày đầu tuần ngày trong tuần (thứ 2 và thứ 3)
                {
                    // Đảm bảo tính duy nhất cho mỗi lớp
                    HashSet<int> selectedClassIDs = new HashSet<int>();

                    /*Một giáo viên không được phân công giảng dạy nhiều hơn một lớp trong cùng một ca học của một ngày cụ thể.*/
                    Dictionary<int, Dictionary<int, HashSet<int>>> professorAssignments = new Dictionary<int, Dictionary<int, HashSet<int>>>();

                    /*if (!professorAssignments.ContainsKey(day)) kiểm tra xem trong từ điển professorAssignments,
                     * đã có khóa tương ứng với day (ID của ngày) hay chưa.
                     * Nếu chưa có ContainsKey(day) trả về false (nghĩa là không tìm thấy khóa day trong từ điển), 
                     * thì điều kiện trong câu lệnh if được thỏa mãn*/
                    if (!professorAssignments.ContainsKey(day))  // kiểm tra key day và add thứ vào
                    {
                        /*professorAssignments.Add(day, new Dictionary<int, HashSet<int>>()); 
                         * được thực thi khi điều kiện trong if là true. 
                         * Đoạn code này tạo một từ điển mới new Dictionary<int, HashSet<int>>() và thêm vào từ điển professorAssignments với khóa là day. 
                         * Từ điển mới này dùng để lưu trữ thông tin về việc phân công giáo viên cho các ca học khác nhau trong ngày đó. 
                         * Mỗi khóa trong từ điển này sẽ đại diện cho một ca học, 
                         * và giá trị tương ứng sẽ là một HashSet<int> chứa ID của các giáo viên được phân công giảng dạy trong ca học đó.*/
                        professorAssignments.Add(day, new Dictionary<int, HashSet<int>>()); // (VD: thứ 2,Dictionary[,])
                    }

                    // vòng lặp này chạy số lượng ca học trong 1 ngày 
                    for (int currentShift = 0; currentShift < shift; currentShift++) // Lặp qua mỗi ca học
                    {
                        // số lượng lớp trông 1 hoạt động trong 1 ca
                        int classesInCurrentShift = numberOfClassesPerShift + (currentShift < excessClass ? 1 : 0);


                        // vòng lặp này chạy theo số lượng lớp hoạt động trong 1 ca học
                        for (int classIndex = 0; classIndex < classesInCurrentShift; classIndex++)
                        {
                            /*kiểm tra xem trong Dictionary của ngày hiện tại (day) đã có HashSet cho ca học hiện tại (currentShift) chưa. 
                             * Nếu chưa có (!ContainsKey(currentShift) trả về true), thì điều kiện trong if được thỏa mãn.*/
                            if (!professorAssignments[day].ContainsKey(currentShift)) // kiểm tra key [day] trong professorAssignments.[key ca học hiện tại]
                            {
                                // tại professorAssignments[day].add key mới
                                professorAssignments[day].Add(currentShift, new HashSet<int>());
                            }

                            //Tạo các biến hứng dữ liệu để gán cho các thuộc tính trong TimetableEvent 
                            int classID = GetUniqueRandomClassID(selectedClassIDs, _ListClassIDs, _Random);
                            int courseID = GetCourseIDForClass(classID, day); // Lấy CourseID đầu tiên cho ClassID
                            int professorID = GetProfessorForClassAndCourse(classID, courseID); // Sử dụng CourseID này để lấy ProfessorID
                            int roomIndex = _Random.Next(_ListRoomIDs.Count);
                            int roomID = _ListRoomIDs[roomIndex];
                            int studyID = currentShift + 1; // vì hiện tại currentShift bắt đầu từ 0 nên phải gán = +1 để sau này truy xuất và insert cơ sở dữ liệu
                            int weekdayID = day + 1;// vì hiện tại day bắt đầu từ 0 nên phải gán = +1 để sau này truy xuất và insert cơ sở dữ liệu

                            if(courseID == 0)
                            {
                                Console.WriteLine($"Lớp {classID} tại ngày {day} có courseID = {courseID} nên đã bỏ qua break");
                                break;
                            }
                            
                            do
                            {
                                if (professorID == 0) break;
                                if (professorAssignments[day][currentShift].Contains(professorID)) professorID = FindAlternateProfessor(courseID, day, currentShift, professorAssignments);
                                else break;
                            } while (true);


                            /*if (professorID == 0)
                            {
                                Console.WriteLine($"Không tìm thấy giáo viên thay thế cho môn {courseID} lớp {classID} ca {currentShift} ngày {day}.");
                                //continue; // Nếu không tìm thấy giáo viên thay thế, bỏ qua việc thêm TimetableEvent này
                            }
                            else
                            {
                                Console.WriteLine($"Tìm thấy giáo viên thay thế cho môn {courseID} lớp {classID} ca {currentShift} ngày {day}.");
                                //professorAssignments[day].Add(currentShift, new HashSet<int>());
                            }*/

                            professorAssignments[day][currentShift].Add(professorID);

                            // new TimetableEvent và gán biến đúng với thuộc tính của nó
                            TimetableEvent timetableEvent = new TimetableEvent
                            {
                                ClassID = classID,
                                RoomID = roomID,
                                ProfessorID = professorID, // Đảm bảo bạn đã thêm trường này vào lớp TimetableEvent
                                CourseID = courseID, // Cũng như trường CourseID
                                StudyID = studyID,
                                WeekdayID = weekdayID
                            };

                            // Tiếp tục logic để thêm timetableEvent vào chromosome
                            chromosome.Timetable[day, currentShift].AddEvent(timetableEvent); // VD: tại ngày 1 ca 2 add timetableEvent vào 
                        }
                    }
                }

                // Đánh giá cá nhân
                _ListPopulation.Add(chromosome);
                PersonalFitnessSsessment(chromosome);
                if (_SuperiorPopulation)
                {
                    Console.WriteLine("Lúc khởi tạo đã tìm đc ca thể tốt nhất");
                    PrintPopulationData();
                    break;
                }

            }
        }
        #endregion


        #region Chọn lọc theo đánh giá

        // Hàm đánh giá độ thích nghi của quần thể
        private bool EvaluateFitness()
        {
            Console.WriteLine("************************** Đánh giá Fitness **************************");
            bool hasSuperior = _ListPopulation.Any(chromosome => EvaluateConditions(chromosome));

            if (!hasSuperior)
            {
                SelFitnessSelective();
            }

            Console.WriteLine("************************** END Đánh giá Fitness **************************");
            LogPopulationFitness();

            return hasSuperior; // Trả về true nếu có ít nhất một cá thể đạt điều kiện mong muốn, ngược lại trả về false
        }

        // Đánh giá các điều kiện cho một chromosome
        private bool EvaluateConditions(TimetableChromosome chromosome)
        {
            
            var conditions = new Func<TimetableChromosome, bool>[] {
                ALecturerInAShiftDoesNotExceedOneClass,
                AClassInAShiftNoMoreThanOneLecturer,
                AClassInAShiftNoMoreThanOneSubject,
                OneSubjectForAClassHasNoMoreThanOneShiftInADay,
                OneRoomInAShiftHasNoMoreThanOneClass,
                OneClassHasNoMoreThanOneShiftInADay,
                EnsureFairTeachingLoad,
                PrioritizeConsecutiveShiftsAndCloseRooms

            };

            foreach (var condition in conditions)
            {
                if (!condition(chromosome))
                {
                    return false;
                }
            }

            if (chromosome.Fitness >= _Fitness)
            {
                _ListPopulation = new List<TimetableChromosome> { chromosome };
                _SuperiorPopulation = true;
                return true;
            }

            return false;
        }
        
        // Hàm chọn lọc dựa trên độ thích nghi
        private void SelFitnessSelective()
        {
            // Sắp xếp và chọn lọc
            _ListPopulation = _ListPopulation.OrderByDescending(x => x.Fitness).ToList();
            int selectCount = (int)(_ListPopulation.Count * _ElitismCount);
            _ListPopulation = _ListPopulation.Take(selectCount).ToList();
        }

        // kiểm tra độ thích nghi của 1 ca thể  
        private bool PersonalFitnessSsessment(TimetableChromosome chromosome)
        {
            Console.WriteLine("************************** Đánh giá Personal Fitness Ssessment **************************");
            bool isFit = EvaluateConditions(chromosome);
            Console.WriteLine($"Kết quả đánh giá cho chromosome: không đạt yêu cầu.");
            return isFit;
        }

        // Log fitness của từng chromosome trong quần thể
        private void LogPopulationFitness()
        {
            int i = 0;
            foreach (var item in _ListPopulation)
            {
                Console.WriteLine($"Chromosome # {i} Fitness: {item.Fitness}");
                i++;
            }
        }
        #endregion



        #region Lai ghép thế hệ 
        // Hàm lai ghép ngấu nhiên
        /*private void CrossoverPopulation()
        {
            List<TimetableChromosome> newPopulation = new List<TimetableChromosome>();

            // Lặp qua từng cặp chromosome trong quần thể hiện tại để tạo thế hệ con
            for (int i = 0; i < _ListPopulation.Count; i += 2)
            {
                if (i + 1 < _ListPopulation.Count)
                {
                    TimetableChromosome parent1 = _ListPopulation[i];
                    TimetableChromosome parent2 = _ListPopulation[i + 1];

                    // Tạo hai thể hệ con bằng cách sao chép từ cha mẹ
                    TimetableChromosome child1 = new TimetableChromosome(parent1.Timetable.GetLength(0), parent1.Timetable.GetLength(1));
                    TimetableChromosome child2 = new TimetableChromosome(parent2.Timetable.GetLength(0), parent2.Timetable.GetLength(1));

                    // Lai ghép đều (Uniform Crossover)
                    for (int day = 0; day < parent1.Timetable.GetLength(0); day++)
                    {
                        for (int shift = 0; shift < parent1.Timetable.GetLength(1); shift++)
                        {
                            if (_Random.NextDouble() < _HybridizationRate) // 50% cơ hội chọn từ bố hoặc mẹ
                            {
                                child1.Timetable[day, shift] = parent1.Timetable[day, shift];
                                child2.Timetable[day, shift] = parent2.Timetable[day, shift];
                            }
                            else
                            {
                                child1.Timetable[day, shift] = parent2.Timetable[day, shift];
                                child2.Timetable[day, shift] = parent1.Timetable[day, shift];
                            }
                        }
                    }

                    // Thêm các thể hệ con vào quần thể mới
                    newPopulation.Add(child1);
                    newPopulation.Add(child2);
                }
            }

            // Cập nhật quần thể hiện tại bằng quần thể mới
            _ListPopulation = newPopulation;
        }*/

        /*private void CrossoverPopulation()
        {
            List<TimetableChromosome> newPopulation = new List<TimetableChromosome>();

            // Lặp qua từng cặp chromosome trong quần thể hiện tại để tạo thế hệ con
            for (int i = 0; i < _ListPopulation.Count; i += 2)
            {
                if (i + 1 < _ListPopulation.Count)
                {
                    TimetableChromosome parent1 = _ListPopulation[i];
                    TimetableChromosome parent2 = _ListPopulation[i + 1];
                    parent1.Fitness = _Fitness;
                    parent2.Fitness = _Fitness;
                    // Chọn một điểm cắt ngẫu nhiên
                    int crossoverPoint = _Random.Next(parent1.Timetable.GetLength(0));

                    // Tạo hai thể hệ con bằng cách kết hợp gen từ cha mẹ ở điểm cắt
                    TimetableChromosome child1 = new TimetableChromosome(parent1.Timetable.GetLength(0), parent1.Timetable.GetLength(1));
                    TimetableChromosome child2 = new TimetableChromosome(parent2.Timetable.GetLength(0), parent2.Timetable.GetLength(1));

                    for (int day = 0; day < parent1.Timetable.GetLength(0); day++)
                    {
                        for (int shift = 0; shift < parent1.Timetable.GetLength(1); shift++)
                        {
                            if (shift < crossoverPoint) // Lấy gen từ cha
                            {
                                child1.Timetable[day, shift] = parent1.Timetable[day, shift];
                                child2.Timetable[day, shift] = parent2.Timetable[day, shift];
                            }
                            else // Lấy gen từ mẹ
                            {
                                child1.Timetable[day, shift] = parent2.Timetable[day, shift];
                                child2.Timetable[day, shift] = parent1.Timetable[day, shift];
                            }
                        }
                    }

                    // Thêm các thể hệ con vào quần thể mới
                    newPopulation.Add(child1);
                    newPopulation.Add(child2);
                }
            }

            // Cập nhật quần thể hiện tại bằng quần thể mới
            _ListPopulation = newPopulation;
        }*/


        /* private void CrossoverPopulation()
        {
            List<TimetableChromosome> newPopulation = new List<TimetableChromosome>();

            // Lặp qua từng cặp chromosome trong quần thể hiện tại để tạo thế hệ con
            for (int i = 0; i < _ListPopulation.Count; i += 2)
            {
                if (i + 1 < _ListPopulation.Count)
                {
                    // Chọn một chromosome cha ngẫu nhiên từ danh sách quần thể
                    int fatherIndex = _Random.Next(_ListPopulation.Count);
                    TimetableChromosome father = _ListPopulation[fatherIndex];

                    TimetableChromosome mother1 = _ListPopulation[i];
                    TimetableChromosome mother2 = _ListPopulation[i + 1];
                    mother1.Fitness = _Fitness;
                    mother2.Fitness = _Fitness;

                    // Chọn một điểm cắt ngẫu nhiên
                    int crossoverPoint = _Random.Next(mother1.Timetable.GetLength(0));

                    // Tạo hai thể hệ con bằng cách kết hợp gen từ cha và mẹ ở điểm cắt
                    TimetableChromosome child1 = new TimetableChromosome(mother1.Timetable.GetLength(0), mother1.Timetable.GetLength(1));
                    TimetableChromosome child2 = new TimetableChromosome(mother2.Timetable.GetLength(0), mother2.Timetable.GetLength(1));

                    for (int day = 0; day < mother1.Timetable.GetLength(0); day++)
                    {
                        for (int shift = 0; shift < mother1.Timetable.GetLength(1); shift++)
                        {
                            if (shift < crossoverPoint) // Lấy gen từ cha
                            {
                                child1.Timetable[day, shift] = father.Timetable[day, shift];
                                child2.Timetable[day, shift] = father.Timetable[day, shift];
                            }
                            else // Lấy gen từ mẹ
                            {
                                child1.Timetable[day, shift] = mother2.Timetable[day, shift];
                                child2.Timetable[day, shift] = mother1.Timetable[day, shift];
                            }
                        }
                    }

                    // Thêm các thể hệ con vào quần thể mới
                    newPopulation.Add(child1);
                    newPopulation.Add(child2);
                }
            }

            // Cập nhật quần thể hiện tại bằng quần thể mới
            _ListPopulation = newPopulation;
        }


*/


        private void CrossoverPopulation()
        {
            List<TimetableChromosome> newPopulation = new List<TimetableChromosome>();

            // Lặp qua từng cặp chromosome trong quần thể hiện tại để tạo thế hệ con
            for (int i = 0; i < _ListPopulation.Count; i += 2)
            {
                if (i + 1 < _ListPopulation.Count)
                {
                    // Chọn một chromosome cha ngẫu nhiên từ danh sách quần thể
                    int fatherIndex = _Random.Next(_ListPopulation.Count);
                    TimetableChromosome father = _ListPopulation[fatherIndex];

                    TimetableChromosome mother1 = _ListPopulation[i];
                    TimetableChromosome mother2 = _ListPopulation[i + 1];
                    mother1.Fitness = _Fitness;
                    mother2.Fitness = _Fitness;

                    // Chọn một điểm cắt ngẫu nhiên
                    int crossoverPoint = _Random.Next(mother1.Timetable.GetLength(0));

                    // Tạo hai thể hệ con bằng cách kết hợp gen từ cha và mẹ ở điểm cắt
                    TimetableChromosome child1 = new TimetableChromosome(mother1.Timetable.GetLength(0), mother1.Timetable.GetLength(1));
                    TimetableChromosome child2 = new TimetableChromosome(mother2.Timetable.GetLength(0), mother2.Timetable.GetLength(1));

                    for (int day = 0; day < mother1.Timetable.GetLength(0); day++)
                    {
                        for (int shift = 0; shift < mother1.Timetable.GetLength(1); shift++)
                        {
                            if (_Random.NextDouble() < _HybridizationRate) // Sử dụng tỷ lệ lai ghép
                            {
                                child1.Timetable[day, shift] = father.Timetable[day, shift];
                                child2.Timetable[day, shift] = father.Timetable[day, shift];
                            }
                            else
                            {
                                child1.Timetable[day, shift] = mother2.Timetable[day, shift];
                                child2.Timetable[day, shift] = mother1.Timetable[day, shift];
                            }
                        }
                    }

                    // Thêm các thể hệ con vào quần thể mới
                    newPopulation.Add(child1);
                    newPopulation.Add(child2);
                }
            }

            // Cập nhật quần thể hiện tại bằng quần thể mới
            _ListPopulation = newPopulation;
        }



        #endregion


        #region Đột biến
        private void MutatePopulation()
        {
            Console.WriteLine("Thực hiện đột biến trên quần thể.");
            foreach (TimetableChromosome chromosome in _ListPopulation)
            {
                for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
                {
                    //chromosome.Fitness = _Fitness;
                    for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                    {
                        if (_Random.NextDouble() < _MutationRate) // Xác suất đột biến dựa trên tỉ lệ đột biến
                        {
                            MutateTimetableSlot(chromosome.Timetable[day, shift]);
                        }
                    }
                }
            }
        }

        private void MutateTimetableSlot(TimetableSlot slot)
        {
            // Chọn ngẫu nhiên một sự kiện trong slot để đột biến
            if (slot.Events.Count > 0)
            {
                int eventIndex = _Random.Next(slot.Events.Count);
                TimetableEvent selectedEvent = slot.Events[eventIndex];

                // Đột biến ngẫu nhiên thuộc tính của sự kiện
                selectedEvent.ProfessorID = GetRandomID(_ListProfessorIDs); // Chọn ngẫu nhiên một giáo viên mới
               /* selectedEvent.RoomID = GetRandomID(_ListRoomIDs);*/ // Chọn ngẫu nhiên một phòng học mới

                // Đảm bảo thay đổi được ghi nhận
                slot.Events[eventIndex] = selectedEvent;
                Console.WriteLine($"Đột biến tại ngày {selectedEvent.WeekdayID}, ca {selectedEvent.StudyID}, sự kiện {eventIndex}: Giáo viên mới {selectedEvent.ProfessorID}, Phòng mới {selectedEvent.RoomID}.");
            }
        }


        #endregion


        #region Các ràng buộc cứng

        // điều kiện 1: Một giảng viên trong một ca dạy không quá một lớp;
        private bool ALecturerInAShiftDoesNotExceedOneClass(TimetableChromosome chromosome)
        {
            Console.WriteLine("Điều kiện 1: Một giảng viên trong một ca dạy không quá một lớp.");
            bool noViolations = true; // Giả sử ban đầu không có vi phạm

            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    HashSet<int> seenProfessors = new HashSet<int>(); // Tập hợp lưu trữ giáo viên đã xét trong ca học
                    TimetableSlot slot = chromosome.Timetable[day, shift];

                    foreach (var timetableEvent in slot.Events)
                    {
                        if (!seenProfessors.Add(timetableEvent.ProfessorID))
                        {
                            // Nếu không thể thêm giáo viên vào HashSet, nghĩa là đã có giáo viên đó trong ca học này
                            Console.WriteLine($"Vi phạm: Giáo viên {timetableEvent.ProfessorID} đã dạy lớp khác trong cùng ca {shift + 1} và ngày {day + 1}.");
                            Console.WriteLine($"Class ID: {timetableEvent.ClassID}, \tRoom ID: {timetableEvent.RoomID}, \tProfessor ID: {timetableEvent.ProfessorID}, \tCourse ID: {timetableEvent.CourseID}");
                            chromosome.Fitness--; // Trừ điểm Fitness do vi phạm
                            noViolations = false; // Cập nhật có vi phạm
                        }
                    }
                }
            }

            return noViolations; // Trả về kết quả kiểm tra, true nếu không có vi phạm, false nếu có vi phạm
        }


        // điều kiện 2: Một lớp trong một ca không quá một giảng viên;
        private bool AClassInAShiftNoMoreThanOneLecturer(TimetableChromosome chromosome)
        {
            Console.WriteLine("Điều kiện 2: Một lớp trong một ca không quá một giảng viên.");
            bool noViolations = true; // Giả sử ban đầu không có vi phạm

            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    HashSet<int> seenClasses = new HashSet<int>();  // Tập hợp để theo dõi các lớp đã được kiểm tra trong ca học này
                    TimetableSlot slot = chromosome.Timetable[day, shift];

                    foreach (var timetableEvent in slot.Events)
                    {
                        if (seenClasses.Contains(timetableEvent.ClassID))
                        {
                            // Nếu lớp đã có giáo viên khác, trả về false (có vi phạm)
                            Console.WriteLine($"Vi phạm tại: Class ID: {timetableEvent.ClassID}, Room ID: {timetableEvent.RoomID}, Professor ID: {timetableEvent.ProfessorID}, Day: {day + 1}, Shift: {shift + 1}");
                            chromosome.Fitness--; // Trừ điểm Fitness mỗi khi có vi phạm
                            noViolations = false; // Đánh dấu có vi phạm
                        }
                        else
                        {
                            seenClasses.Add(timetableEvent.ClassID); // Thêm lớp vào tập hợp đã kiểm tra nếu chưa có vi phạm
                        }
                    }
                }
            }

            return noViolations; // Trả về true nếu không có vi phạm, false nếu có vi phạm
        }


        // điều kiện 3: Một lớp trong một ca không quá một môn;
        private bool AClassInAShiftNoMoreThanOneSubject(TimetableChromosome chromosome)
        {
            Console.WriteLine("Điều kiện 3: Một lớp trong một ca không quá một môn.");
            bool noViolations = true;  // Giả sử ban đầu không có vi phạm

            // Duyệt qua từng ngày và ca trong lịch trình
            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    TimetableSlot slot = chromosome.Timetable[day, shift];
                    Dictionary<int, int> classToSubjectMap = new Dictionary<int, int>();  // Từ điển để theo dõi môn học đã được phân cho mỗi lớp trong ca

                    // Duyệt qua từng sự kiện trong ca
                    foreach (var timetableEvent in slot.Events)
                    {
                        if (classToSubjectMap.TryGetValue(timetableEvent.ClassID, out int existingSubjectID))
                        {
                            // Kiểm tra xem môn học hiện tại có khác môn học đã được ghi nhận cho lớp này không
                            if (existingSubjectID != timetableEvent.CourseID)
                            {
                                Console.WriteLine($"Vi phạm: Lớp {timetableEvent.ClassID} được phân cho nhiều môn trong cùng một ca. Môn đã ghi: {existingSubjectID}, Môn mới: {timetableEvent.CourseID}");
                                chromosome.Fitness--; // Trừ điểm Fitness do vi phạm
                                noViolations = false; // Cập nhật có vi phạm
                            }
                        }
                        else
                        {
                            // Ghi nhận môn học cho lớp này
                            classToSubjectMap[timetableEvent.ClassID] = timetableEvent.CourseID;
                        }
                    }
                }
            }

            return noViolations;  // Trả về true nếu không có vi phạm, false nếu có vi phạm
        }


        // Điều kiện 4: Một môn cho một lớp không quá một ca trong một ngày
        private bool OneSubjectForAClassHasNoMoreThanOneShiftInADay(TimetableChromosome chromosome)
        {
            Console.WriteLine("Điều kiện 4: Một môn cho một lớp không quá một ca trong một ngày.");
            // Duyệt qua từng ngày trong lịch
            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                Dictionary<int, HashSet<int>> classToCoursesMap = new Dictionary<int, HashSet<int>>(); // Từ điển lưu trữ các môn đã được gán cho mỗi lớp trong ngày
                                                                                                       // Duyệt qua từng ca trong ngày
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    foreach (var timetableEvent in chromosome.Timetable[day, shift].Events)
                    {
                        // Kiểm tra xem lớp này đã được gán môn này trong ngày chưa
                        if (!classToCoursesMap.ContainsKey(timetableEvent.ClassID))
                        {
                            classToCoursesMap[timetableEvent.ClassID] = new HashSet<int>(); // Khởi tạo HashSet cho lớp nếu chưa có
                        }

                        if (!classToCoursesMap[timetableEvent.ClassID].Add(timetableEvent.CourseID))
                        {
                            // Nếu môn này đã tồn tại trong HashSet, tức là môn đó đã được gán trong ca khác trong cùng một ngày
                            Console.WriteLine($"Vi phạm: Lớp {timetableEvent.ClassID} đã có môn {timetableEvent.CourseID} trong một ca khác của ngày {day + 1}.");
                            chromosome.Fitness--;
                            return false; // Có vi phạm
                        }
                    }
                }
            }
            return true; // Không có vi phạm
        }


        // Điều kiện 5: Một phòng học trong một ca không quá một lớp học
        private bool OneRoomInAShiftHasNoMoreThanOneClass(TimetableChromosome chromosome)
        {
            Console.WriteLine("Điều kiện 5: Một phòng học trong một ca không quá một lớp học.");
            bool noViolations = true;  // Giả sử ban đầu không có vi phạm

            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    HashSet<int> seenRoomIDs = new HashSet<int>(); // Tập hợp để theo dõi các phòng đã được sử dụng trong ca học này
                    TimetableSlot slot = chromosome.Timetable[day, shift];

                    foreach (var timetableEvent in slot.Events)
                    {
                        if (!seenRoomIDs.Add(timetableEvent.RoomID))
                        {
                            // Nếu không thể thêm RoomID vào HashSet, nghĩa là phòng này đã được sử dụng trong ca học này
                            Console.WriteLine($"Vi phạm: Phòng {timetableEvent.RoomID} đã được sử dụng bởi lớp khác trong cùng ca học ngày {day + 1}, ca {shift + 1}.");
                            chromosome.Fitness--; // Trừ điểm Fitness do vi phạm
                            noViolations = false; // Cập nhật có vi phạm
                        }
                    }
                }
            }

            return noViolations; // Trả về true nếu không có vi phạm, false nếu có vi phạm
        }



        // điều kiện 6: Một lớp có nhiều ca của một môn trong tuần thì cố định ca học trong tất cả các ngày học, và các ngày học cách nhau ít nhất 1 ngày chứ không học các ngày liên tiếp nhau
        private bool OneClassHasNoMoreThanOneShiftInADay(TimetableChromosome chromosome)
        {
            Console.WriteLine("điều kiện 6: Kiểm tra một lớp không học quá một ca trong một ngày và các ngày học cách nhau ít nhất một ngày.");
            Dictionary<int, Dictionary<int, List<int>>> classCourseToDaysMap = new Dictionary<int, Dictionary<int, List<int>>>(); // Từ điển lưu trữ mối quan hệ giữa lớp và môn học với danh sách các ngày học

            // Duyệt qua từng ngày và mỗi ca trong ngày đó
            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    TimetableSlot slot = chromosome.Timetable[day, shift];
                    foreach (var timetableEvent in slot.Events)
                    {
                        int classID = timetableEvent.ClassID;
                        int courseID = timetableEvent.CourseID;

                        // Khởi tạo từ điển cho lớp và môn học nếu chưa có
                        if (!classCourseToDaysMap.ContainsKey(classID))
                        {
                            classCourseToDaysMap[classID] = new Dictionary<int, List<int>>();
                        }
                        if (!classCourseToDaysMap[classID].ContainsKey(courseID))
                        {
                            classCourseToDaysMap[classID][courseID] = new List<int>();
                        }

                        // Thêm ngày hiện tại vào danh sách các ngày học của lớp cho môn học này
                        if (!classCourseToDaysMap[classID][courseID].Contains(day))
                        {
                            classCourseToDaysMap[classID][courseID].Add(day);
                        }
                    }
                }
            }

            // Kiểm tra điều kiện các ngày học không liên tiếp nhau
            foreach (var classCourses in classCourseToDaysMap)
            {
                foreach (var courseDays in classCourses.Value)
                {
                    var days = courseDays.Value;
                    days.Sort(); // Sắp xếp danh sách các ngày để dễ dàng kiểm tra

                    for (int i = 1; i < days.Count; i++)
                    {
                        if (days[i] - days[i - 1] == 1) // Kiểm tra nếu có hai ngày liên tiếp nhau
                        {
                            Console.WriteLine($"Vi phạm: Lớp {classCourses.Key} học môn {courseDays.Key} trong các ngày liên tiếp {days[i - 1] + 1} và {days[i] + 1}.");
                            chromosome.Fitness--;
                            return false; // Có vi phạm
                        }
                    }
                }
            }

            return true; // Không có vi phạm
        }


        #endregion


        #region Các ràng buộc mềm

        // điều kiện 7: Số ca dạy của các giảng viên phải xấp xỉ nhau
        private bool EnsureFairTeachingLoad(TimetableChromosome chromosome)
        {
            Console.WriteLine("điều kiện 7: Số ca dạy của các giảng viên phải xấp xỉ nha");
            Dictionary<int, int> professorTeachingLoads = new Dictionary<int, int>(); // Từ điển lưu trữ tải giảng dạy cho mỗi giáo viên

            // Duyệt qua từng ngày và mỗi ca trong ngày đó
            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    TimetableSlot slot = chromosome.Timetable[day, shift];
                    foreach (var timetableEvent in slot.Events)
                    {
                        int professorID = timetableEvent.ProfessorID;
                        if (professorID != 0) // Giả sử ID giảng viên 0 là không hợp lệ
                        {
                            if (!professorTeachingLoads.ContainsKey(professorID))
                            {
                                professorTeachingLoads[professorID] = 0;
                            }
                            professorTeachingLoads[professorID] += 1; // Tăng tải giảng dạy cho giáo viên này
                        }
                    }
                }
            }

            // Đánh giá sự công bằng của tải giảng dạy
            if (professorTeachingLoads.Count > 0)
            {
                int averageLoad = (int)professorTeachingLoads.Values.Average(); // Tính trung bình số ca giảng dạy
                int varianceThreshold = 1; // Ngưỡng cho phép cho biến động số ca giảng dạy

                foreach (int load in professorTeachingLoads.Values)
                {
                    
                    if (Math.Abs(load - averageLoad) > varianceThreshold) // Nếu chênh lệch quá ngưỡng cho phép
                    {
                        Console.WriteLine($"Load imbalance: Professor with load {load} deviates from average load {averageLoad}.");
                        chromosome.Fitness--;
                        return false; // Có sự bất công trong phân bổ tải giảng dạy
                    }
                }
            }

            return true; // Tải giảng dạy được phân bổ công bằng
        }

        // điều kiện 8 : Nếu giảng viên có nhiều ca trong ngày thì ưu tiên xếp các ca liền nhau, dạy ở cùng 1 phòng hoặc phòng gần nhau.
        private bool PrioritizeConsecutiveShiftsAndCloseRooms(TimetableChromosome chromosome)
        {
            Console.WriteLine("điều kiện 8 : Kiểm tra ưu tiên xếp ca liền nhau và dạy ở phòng gần nhau cho giảng viên.");
            bool allProfessorsMeetCriteria = true;

            // Duyệt qua từng ngày
            for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
            {
                Dictionary<int, List<(int shift, int roomID)>> professorShiftsAndRooms = new Dictionary<int, List<(int, int)>>();

                // Thu thập thông tin ca và phòng cho mỗi giảng viên trong ngày
                for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                {
                    TimetableSlot slot = chromosome.Timetable[day, shift];
                    foreach (var timetableEvent in slot.Events)
                    {
                        int professorID = timetableEvent.ProfessorID;
                        int roomID = timetableEvent.RoomID;
                        if (professorID != 0)
                        {
                            if (!professorShiftsAndRooms.ContainsKey(professorID))
                            {
                                professorShiftsAndRooms[professorID] = new List<(int, int)>();
                            }
                            professorShiftsAndRooms[professorID].Add((shift, roomID));
                        }
                    }
                }

                // Kiểm tra liệu các ca dạy có liền nhau và ở phòng gần nhau không
                foreach (var entry in professorShiftsAndRooms)
                {
                    var shiftsAndRooms = entry.Value;
                    shiftsAndRooms.Sort(); // Sắp xếp dựa trên ca học

                    for (int i = 1; i < shiftsAndRooms.Count; i++)
                    {
                        // Kiểm tra xem ca liền nhau và phòng có cách nhau không quá 1 đơn vị
                        if (shiftsAndRooms[i].shift != shiftsAndRooms[i - 1].shift + 1 ||
                            Math.Abs(shiftsAndRooms[i].roomID - shiftsAndRooms[i - 1].roomID) > 1)
                        {
                            Console.WriteLine($"Vi phạm: Giảng viên {entry.Key} không dạy các ca liền nhau hoặc phòng không gần nhau vào ngày {day + 1}.");
                            chromosome.Fitness--;
                            allProfessorsMeetCriteria = false;
                        }
                    }
                }
            }

            return allProfessorsMeetCriteria; // Trả về true nếu tất cả giảng viên đều đáp ứng điều kiện
        }



        #endregion

        
        #region function lấy dữ liệu dạng list 

        private List<int> GetListClassIDs()
        {
            string selectID = "SELECT ClassID FROM Classes", nameID = "ClassID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return listID;
        }

        private List<int> GetListCourseIDs()
        {
            string selectID = "SELECT CourseID FROM Courses", nameID = "CourseID";
            return _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
        }

        private List<int> GetListProfessorIDs()
        {
            string selectID = "SELECT ProfessorID FROM Professors", nameID = "ProfessorID";
            return _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
        }

        private List<int> GetListRoomIDs()
        {
            string selectID = "SELECT RoomID FROM Classrooms", nameID = "RoomID";
            return _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
        }

        private List<int> GetListWeekdayIDs()
        {
            string selectID = "SELECT WeekdayID FROM Weekdays", nameID = "WeekdayID";
            return _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
        }

        private List<int> GetListStudyIDs()
        {
            string selectID = "SELECT StudyID FROM Studys", nameID = "StudyID";
            return _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
        }

        private int GetRandomID(List<int> listID)
        {
            Thread.Sleep(this._TimeDelay);
            Random random = new Random();
            int randomIndex = random.Next(0, listID.Count); // Sinh chỉ index số ngẫu nhiên từ 0 đến độ dài của danh sách
            return listID[randomIndex]; // Trả về phần tử tại chỉ số ngẫu nhiên
        }

        #endregion

        
        #region Các hàm phục vụ cho việc code
        // Tìm kiếm giáo viên thay thế
        private int FindAlternateProfessor(int courseID, int day, int shift_, Dictionary<int, Dictionary<int, HashSet<int>>> professorAssignments)
        {
            // Lấy danh sách tất cả giáo viên có thể dạy khóa học này
            List<int> allProfessorsForCourse = GetAllProfessorsForCourse(courseID);

            // Xác định danh sách giáo viên đã được phân công cho ca và ngày này
            HashSet<int> assignedProfessorsForShiftAndDay = professorAssignments.ContainsKey(day) && professorAssignments[day].ContainsKey(shift_)
                                                            ? professorAssignments[day][shift_]
                                                            : new HashSet<int>();

            // Lọc ra những giáo viên chưa được phân công
            List<int> eligibleProfessors = allProfessorsForCourse.Except(assignedProfessorsForShiftAndDay).ToList();

            if (eligibleProfessors.Count > 0)
            {
                // Lấy ngẫu nhiên một giáo viên thay thế từ danh sách giáo viên đủ điều kiện
                Random random = new Random();
                int randomIndex = random.Next(eligibleProfessors.Count);
                return eligibleProfessors[randomIndex];
            }
            else
            {
                return 0; // Không tìm thấy giáo viên thay thế phù hợp
            }
        }

        // Lấy tất cả các giáo sư theo id course
        private List<int> GetAllProfessorsForCourse(int courseID)
        {
            List<int> professorIDs = new List<int>();
            string connectionString = _KetNoi.ConnectionString; // Giả sử đây là chuỗi kết nối của bạn

            string query = @"
            SELECT ProfessorID
            FROM ProfessorCourses
            WHERE CourseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CourseID", courseID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int professorID = reader.GetInt32(0); // Giả định ProfessorID là cột đầu tiên
                            professorIDs.Add(professorID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return professorIDs;
        }

        // Lấy ID khóa học cho lớp học theo id Class với index khóa học của class đó
        private int GetCourseIDForClass(int classID, int indexCourse)
        {
            try
            {
                indexCourse += 1;
                int? secondCourseID = null; // Sử dụng nullable int để lưu giữ giá trị thứ hai, nếu có
                string connectionString = _KetNoi.ConnectionString; // Thay thế bằng chuỗi kết nối thực tế của bạn
                string query = @"
                SELECT CourseID
                FROM ClassesCourses
                WHERE ClassID = @ClassID
                ORDER BY CourseID"; // Đảm bảo sắp xếp để kết quả có thứ tự nhất định

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ClassID", classID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int count = 0;
                            while (reader.Read()) // Đọc qua mỗi hàng trả về
                            {
                                count++;
                                if(count == indexCourse)
                                {
                                    secondCourseID = reader.GetInt32(0); // Giả định cột đầu tiên là CourseID
                                    break; // Thoát vòng lặp sau khi tìm thấy giá trị tại index cần tìm
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return secondCourseID.HasValue ? secondCourseID.Value : 0; // Trả về giá trị nếu có, nếu không trả về 0
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Lấy giáo viên theo lớp học và khóa học
        private int GetProfessorForClassAndCourse(int classID, int courseID)
        {
            try
            {
                int professorID = 0;
                string connectionString = _KetNoi.ConnectionString; 
                string query = @"
                SELECT pc.ProfessorID
                FROM ProfessorCourses pc
                INNER JOIN ClassesCourses cc ON pc.CourseID = cc.CourseID
                WHERE cc.ClassID = @ClassID AND cc.CourseID = @CourseID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ClassID", classID);
                    command.Parameters.AddWithValue("@CourseID", courseID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        professorID = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return professorID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // lấy ra index của 1 lớp và ko trùng lặp
        private int GetUniqueRandomClassID(HashSet<int> selectedClassIDs, List<int> classIDs, Random random)
        {
            if (selectedClassIDs.Count == classIDs.Count)
            {
                Console.WriteLine("All classes have been selected.");
                return 0;
            }

            List<int> availableClassIDs = classIDs.Except(selectedClassIDs).ToList();
            int randomIndex = random.Next(availableClassIDs.Count);
            int classID = availableClassIDs[randomIndex];
            selectedClassIDs.Add(classID);
            return classID;
        }

        // hàm in ra màn hình 
        private void PrintPopulationData()
        {
            if (_ListPopulation == null || !_ListPopulation.Any())
            {
                Console.WriteLine("Quần thể rỗng hoặc chưa được khởi tạo.");
                return;
            }
            Console.WriteLine("in ra");
            int chromosomeCount = 1;
            foreach (var chromosome in _ListPopulation)
            {
                Console.WriteLine($"Chromosome #{chromosomeCount}, Fitness: {chromosome.Fitness}");
                for (int day = 0; day < _ListWeekdays.Count; day++)
                {
                    for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                    {
                        Console.WriteLine($"Day: {day + 1}, Shift: {shift + 1}");
                        foreach (var timetableEvent in chromosome.Timetable[day, shift].Events)
                        {
                            Console.WriteLine($"Class ID: {timetableEvent.ClassID}, \tRoom ID: {timetableEvent.RoomID}, \tProfessor ID: {timetableEvent.ProfessorID}, \tCourse ID: {timetableEvent.CourseID}");
                            // Bạn có thể thêm thông tin khác của sự kiện ở đây nếu cần
                        }
                    }
                }
                chromosomeCount++;
                Console.WriteLine("\n"); // Thêm một dòng trống cho dễ đọc
            }
        }


        private int GetRandomClassID()
        {
            string selectID = "SELECT ClassID FROM Classes", nameID = "ClassID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return GetRandomID(listID);
        }
        // lấy random môn học
        private int GetRandomCourseID()
        {
            string selectID = "SELECT CourseID FROM Courses", nameID = "CourseID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return GetRandomID(listID);
        }

        // lấy random giảng viên
        private int GetRandomProfessorID()
        {
            string selectID = "SELECT ProfessorID FROM Professors", nameID = "ProfessorID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return GetRandomID(listID);
        }

        // lấy ramdom phòng học
        private int GetRandomRoomID()
        {
            string selectID = "SELECT RoomID FROM Classrooms", nameID = "RoomID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return GetRandomID(listID);
        }

        // lấy random thứ trong tuần
        private int GetRandomWeekdayID()
        {
            string selectID = "SELECT WeekdayID FROM Weekdays", nameID = "WeekdayID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return GetRandomID(listID);
        }
        private int GetWeekdayIDByIndex(int index)
        {
            string selectID = "SELECT WeekdayID FROM Weekdays", nameID = "WeekdayID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return listID[index];
        }
        // lấy random ca học
        private int GetRandomStudyID()
        {
            string selectID = "SELECT StudyID FROM Studys", nameID = "StudyID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return GetRandomID(listID);
        }
        private int GetStudyIDByIndex(int index)
        {
            string selectID = "SELECT StudyID FROM Studys", nameID = "StudyID";
            List<int> listID = _KetNoi.ThucThiCauLenhSQLGetListID(selectID, nameID);
            return listID[index];
        }

        #endregion

        #region Run
        public void Start()
        {
            Console.WriteLine("Starting Genetic Algorithm...");

            // Khởi tạo quần thể ban đầu và đánh giá sơ bộ
            GenerateInitialPopulation();
            EvaluateFitness();

            int generation = 0;
            while (generation < _NumberOfGenerations)
            {
                // Lai ghép quần thể
                CrossoverPopulation();

                // Đánh giá quần thể mới sau lai ghép
                if (!EvaluateFitness())
                {
                    // Thực hiện đột biến nếu quần thể sau lai ghép không đạt yêu cầu
                    MutatePopulation();

                    // Đánh giá lại sau đột biến
                    if (EvaluateFitness())
                    {
                        Console.WriteLine($"Đạt yêu cầu tại thế hệ: {generation}");
                        break;
                    }
                }

                // In thông tin thế hệ hiện tại
                Console.WriteLine($"Thế hệ: {generation}");
                PrintPopulationData();
                generation++;
            }

            // In kết quả của quần thể cuối cùng
            PrintPopulationData();

            // Lưu quần thể vào cơ sở dữ liệu
            Console.WriteLine("Insert dữ liệu vào database");
            InsertPopulationToDatabase();
            Console.WriteLine("Genetic Algorithm completed.");
        }

        #endregion

        #region InsertDatabase 
        private void InsertPopulationToDatabase()
        {
            try
            {
                // Kiểm tra có đủ dữ liệu
                if (_ListPopulation.Count < 2)
                {
                    Console.WriteLine("Population list is not sufficient. Need data for at least Monday and Tuesday.");
                    return;
                }

                // Khởi tạo các biến cho ngày thứ 2 và thứ 3
                TimetableChromosome mondayChromosome = _ListPopulation[0]; // Dữ liệu thứ 2
                TimetableChromosome tuesdayChromosome = _ListPopulation[1]; // Dữ liệu thứ 3

                string connectionString = _KetNoi.ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        SqlCommand deleteCommand = new SqlCommand("DELETE FROM ClassSchedules", connection, transaction);
                        deleteCommand.ExecuteNonQuery();

                        // Lặp qua các ngày trong tuần, từ thứ 2 đến Chủ nhật
                        for (int day = 0; day < _ListWeekdays.Count; day++)
                        {
                            Console.WriteLine("-------------------------------------------------------------------------Day: " + day + "-------------------------------------------------------------------------");

                            TimetableChromosome chromosome = (day % 2 == 0) ? mondayChromosome : tuesdayChromosome;
                            for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                            {
                                foreach (var timetableEvent in chromosome.Timetable[day % 2, shift].Events)
                                {
                                    if (timetableEvent.ProfessorID == 0 || timetableEvent.RoomID == 0 || timetableEvent.ClassID == 0 || timetableEvent.CourseID == 0 ) // Thêm các điều kiện kiểm tra khác tại đây
                                    {
                                        Console.WriteLine($"Invalid data for ProfessorID or RoomID on day {day + 1}");
                                        continue;
                                    }

                                    string insertQuery = @"
                                    INSERT INTO ClassSchedules (ClassID, CourseID, ProfessorID, RoomID, WeekdayID, StudyID)
                                    VALUES (@ClassID, @CourseID, @ProfessorID, @RoomID, @WeekdayID, @StudyID)";
                                    SqlCommand command = new SqlCommand(insertQuery, connection, transaction);
                                    command.Parameters.AddWithValue("@ClassID", timetableEvent.ClassID);
                                    command.Parameters.AddWithValue("@CourseID", timetableEvent.CourseID);
                                    command.Parameters.AddWithValue("@ProfessorID", timetableEvent.ProfessorID);
                                    command.Parameters.AddWithValue("@RoomID", timetableEvent.RoomID);
                                    command.Parameters.AddWithValue("@WeekdayID", day + 1);
                                    command.Parameters.AddWithValue("@StudyID", timetableEvent.StudyID);

                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        transaction.Commit();
                        Console.WriteLine("Data insertion complete.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
        }

        #endregion
    }
}