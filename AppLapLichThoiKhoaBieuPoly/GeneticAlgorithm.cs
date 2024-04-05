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
        #region Properties and Contractor (thuộc tính và hàm tạo)

        DbConnection _KetNoi;
        private string connectionString;
        // Khai báo kích thước quần thể, tỉ lệ đột biến, số lượng cá thể tinh hoa, các thông số lịch trình và danh sách các ID
        private int _PopulationSize; // số lượng cá thể
        private double _MutationRate; // tỉ lệ đột biến
        private double _ElitismCount; // tỉ lệ chọn 
        private int _TimeDelay; // Thời gian trễ giữa các ca học
        private int _Fitness; // Giá trị đánh giá chất lượng của thời khóa biểu
        private List<TimetableChromosome> _ListPopulation; // Danh sách chứa quần thể hiện tại
        private List<TimetableChromosome> _ListOffspring; // Danh sách chứa quần thể con sau lai ghép
        private List<int> _ListShift; // Danh sách ID ca học 
        private List<int> _ListWeekdays; // Danh sách ID thứ trong tuần
        private List<int> _ListClassIDs; // Danh sách ID lớp học
        private List<int> _ListCourseIDs; // Danh sách ID khóa học
        private List<int> _ListProfessorIDs; // Danh sách ID giáo viên
        private List<int> _ListRoomIDs; // Danh sách ID phòng học
        private readonly Random _Random; // Đối tượng Random để tạo số ngẫu nhiên

        // Phương thức khởi tạo lớp GeneticAlgorithm với các tham số cần thiết để thiết lập quá trình tiến hóa
        public GeneticAlgorithm(int populationSize, double mutationRate, double elitismCount, int timeDelay) {
            _KetNoi = new DbConnection();
            connectionString = _KetNoi.ConnectionString;
            _PopulationSize = populationSize;
            _MutationRate = mutationRate;
            _ElitismCount = elitismCount;
            _TimeDelay = timeDelay;
            _Fitness = 0;

            _ListPopulation = new List<TimetableChromosome>(_PopulationSize);
            _Random = new Random(); // Khởi tạo đối tượng Random

            _ListShift = GetListStudyIDs();
            _ListWeekdays = GetListWeekdayIDs();
            _ListClassIDs = GetListClassIDs();
            _ListCourseIDs = GetListCourseIDs();
            _ListProfessorIDs = GetListProfessorIDs();
            _ListRoomIDs = GetListRoomIDs();
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

        public int GetRandomID(List<int> listID)
        {
            Thread.Sleep(this._TimeDelay);
            Random random = new Random();
            int randomIndex = random.Next(0, listID.Count); // Sinh chỉ index số ngẫu nhiên từ 0 đến độ dài của danh sách
            return listID[randomIndex]; // Trả về phần tử tại chỉ số ngẫu nhiên
        }

        #endregion




        // Genetic Algorithm (thuật toán di truyền)
        #region Initialize Population (Khởi tạo dân số)

        // Phương thức để khởi tạo quần thể ban đầu
        public void GenerateInitialPopulation()
        {
            Console.WriteLine("Số lượng phòng học: " + _ListRoomIDs.Count);
            Console.WriteLine("Số lượng ca học: " + _ListShift.Count);
            Console.WriteLine("Số lượng lớp học: " + _ListClassIDs.Count);

            int weekdays = _ListWeekdays.Count;
            int shift = _ListShift.Count - 1;
            int numberOfClassesPerShift = _ListClassIDs.Count / shift;
            int excessClass = _ListClassIDs.Count % shift;

            Console.WriteLine("Số lớp học mỗi ca: " + numberOfClassesPerShift + ". Dư: " + excessClass);



            for (int i = 0; i < _PopulationSize; i++)
            {
                Console.WriteLine($"______________    chromosome # {i}   _______________");
                TimetableChromosome chromosome = new TimetableChromosome(weekdays, shift);
                chromosome.Fitness = _Fitness;

                for (int day = 0; day < 2; day++) // Lặp qua 2 ngày đầu tuần ngày trong tuần
                {
                    Dictionary<string, HashSet<int>> professorAssignments = new Dictionary<string, HashSet<int>>();
                    HashSet<int> selectedClassIDs = new HashSet<int>(); // Đảm bảo tính duy nhất cho mỗi chromosome
                    Console.WriteLine("day (Thứ) : " + day );

                    for (int currentShift = 0; currentShift < shift; currentShift++) // Lặp qua mỗi ca học
                    {
                        Console.WriteLine("currentShift (Ca học) : " + currentShift);
                        string key = $"Day{day}Shift{currentShift}";
                        int classesInCurrentShift = numberOfClassesPerShift + (currentShift < excessClass ? 1 : 0);
                        Console.WriteLine("Số lượng lớp học: " + classesInCurrentShift);
                        for (int classIndex = 0; classIndex < classesInCurrentShift; classIndex++)
                        {
                            Console.WriteLine("classesInCurrentShift " + classIndex);
                            int classID = GetUniqueRandomClassID(selectedClassIDs, _ListClassIDs, _Random);
                            int courseID = GetCourseIDForClass(classID, day); // Lấy CourseID đầu tiên cho ClassID
                            int professorID = GetProfessorForClassAndCourse(classID, courseID); // Sử dụng CourseID này để lấy ProfessorID
                            int roomIndex = _Random.Next(_ListRoomIDs.Count);
                            int roomID = _ListRoomIDs[roomIndex];

                            if (classID == 0)
                            {
                                Console.WriteLine("Lớp rõng nên bỏ qua ");
                                break;
                            }
                            // Kiểm tra xem giáo viên này đã được phân công cho ca học này chưa
                            if (!professorAssignments.ContainsKey(key))
                            {
                                // kiểm tra key nếu chưa có thì thêm mới 
                                professorAssignments[key] = new HashSet<int>();
                            }

                            if (professorAssignments[key].Contains(professorID))
                            {
                                // Giáo viên này đã được phân công cho ca học này, bỏ qua việc thêm
                                Console.WriteLine($"Professor {professorID} đã được phân công cho ca {currentShift} ngày {day}, bỏ qua.");
                                // Tìm một giáo viên thay thế
                                int alternateProfessorID = FindAlternateProfessor(classID, courseID, key, professorAssignments);
                                if (alternateProfessorID == 0)
                                {
                                    Console.WriteLine($"Không tìm thấy giáo viên thay thế cho môn {courseID} lớp {classID} ca {currentShift} ngày {day}.");
                                    //continue; // Nếu không tìm thấy giáo viên thay thế, bỏ qua việc thêm TimetableEvent này
                                }
                                else
                                {
                                    Console.WriteLine($"Tìm thấy giáo viên thay thế cho môn {courseID} lớp {classID} ca {currentShift} ngày {day}.");

                                    professorID = alternateProfessorID; // Sử dụng giáo viên thay thế
                                    // Thêm giáo viên thay thế vào danh sách phân công
                                    professorAssignments[key].Add(professorID);
                                }
                            }
                            else
                            {
                                // Nếu giáo viên này chưa được phân công, thêm vào danh sách phân công
                                professorAssignments[key].Add(professorID);

                            }
                            
                            // Tiếp tục thêm TimetableEvent
                            TimetableEvent timetableEvent = new TimetableEvent
                            {
                                ClassID = classID,
                                RoomID = roomID,
                                ProfessorID = professorID, // Đảm bảo bạn đã thêm trường này vào lớp TimetableEvent
                                CourseID = courseID, // Cũng như trường CourseID
                            };

                            // Tiếp tục logic để thêm timetableEvent vào chromosome
                            Console.WriteLine("chromosome.Timetable[day, currentShift].AddEvent(timetableEvent);");
                            chromosome.Timetable[day, currentShift].AddEvent(timetableEvent);
                            

                        }
                    }
                }
                Console.WriteLine("_ListPopulation.Add(chromosome);");
                _ListPopulation.Add(chromosome);
            }
        }

        // Tìm kiếm giáo viên thay thế
        private int FindAlternateProfessor(int classID, int courseID, string key, Dictionary<string, HashSet<int>> professorAssignments)
        {
            List<int> eligibleProfessors = new List<int>(); // Danh sách các giáo viên đủ điều kiện
            // Giả sử bạn đã có danh sách tất cả giáo viên có thể dạy môn học này
            List<int> allProfessorsForCourse = GetAllProfessorsForCourse(courseID);
            foreach (int professorID in allProfessorsForCourse)
            {
                // Chỉ chọn những giáo viên chưa được phân công trong ca học này
                if (!professorAssignments[key].Contains(professorID))
                {
                    eligibleProfessors.Add(professorID);
                }
            }

            // Nếu không tìm thấy giáo viên thay thế phù hợp, trả về 0
            if (eligibleProfessors.Count == 0) return 0;

            // Trả về ID của giáo viên đầu tiên trong danh sách các giáo viên đủ điều kiện
            return eligibleProfessors[0];
        }
        
        // Lấy tất cả các giáo sư theo id course
        public List<int> GetAllProfessorsForCourse(int courseID)
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
                Console.WriteLine("ID môn học: " + secondCourseID  + "Ngày thứ " + indexCourse);
                return secondCourseID.HasValue ? secondCourseID.Value : 0; // Trả về giá trị nếu có, nếu không trả về 0
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Lấy giáo viên theo lớp học và khóa học
        public int GetProfessorForClassAndCourse(int classID, int courseID)
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
        public void PrintPopulationData()
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

        #endregion


        #region Chọn lọc theo đánh giá
        /*Chọn lọc theo đánh giá(Fitness Selection) : Các cá thể được chọn dựa trên giá trị đánh giá hoặc sự phù hợp
            (fitness) của chúng.Các cá thể có giá trị đánh giá cao hơn có khả năng được chọn lọc nhiều hơn*/

        //Hàm đánh giá
        public void EvaluateFitness()
        {
            Console.WriteLine("**************************___Chọn lọc theo đánh giá___*********************");
            int dem = 0;
            foreach (var chromosome in _ListPopulation)
            {
                chromosome.Fitness = 0; // Khởi tạo giá trị Fitness ban đầu là 0 cho mỗi chromosome

                for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
                {
                    for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                    {
                        // Kiểm tra xem mỗi giáo viên trong ca này có dạy quá một lớp không
                        if (ALecturerInAShiftDoesNotExceedOneClass(chromosome.Timetable[day, shift]))
                        {
                            chromosome.Fitness += 1; // Nếu không vi phạm, cộng điểm thưởng
                        }
                    }
                }
                dem++;
                Console.WriteLine("chromosome " + dem + " Fitness: " + _Fitness);
            }
        }

        // hàm chọn lọc
        public void SelFitnessSelective()
        {
            // Sắp xếp quần thể theo giá trị đánh giá từ cao đến thấp
            _ListPopulation.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));

            // Chọn lọc các cá thể dựa trên giá trị đánh giá hoặc sự phù hợp (fitness)
            // Có thể thay đổi phương pháp chọn lọc tùy thuộc vào yêu cầu cụ thể của bài toán

            // Ví dụ: Chọn lọc 50% các cá thể có giá trị đánh giá cao nhất
            int selectCount = (int)(_ListPopulation.Count * _ElitismCount); // Chọn lọc 50% của quần thể

            // Lưu trữ các cá thể được chọn
            List<TimetableChromosome> selectedPopulation = new List<TimetableChromosome>();

            for (int i = 0; i < selectCount; i++)
            {
                selectedPopulation.Add(_ListPopulation[i]); // Thêm các cá thể có giá trị đánh giá cao nhất vào danh sách được chọn
            }

            // Cập nhật quần thể thành các cá thể được chọn
            _ListPopulation.Clear();
            _ListPopulation.AddRange(selectedPopulation);
        }

        #endregion
        // Phương thức để đánh giá Fitness cho mỗi chromosome trong quần thể


        /*#region Lai ghép thế hệ 
        // Hàm lai ghép với danh sách cha mẹ truyền vào
        public void CrossoverPopulation()
        {
            Console.WriteLine("**************************___Lai ghép thế hệ___*********************");

            List<TimetableChromosome> newGeneration = new List<TimetableChromosome>();

            // Lặp qua từng cặp cha mẹ trong danh sách
            for (int i = 0; i < _ListPopulation.Count - 1; i += 2)
            {
                TimetableChromosome parent1 = _ListPopulation[i];
                TimetableChromosome parent2 = _ListPopulation[i + 1];

                // Kiểm tra tính hợp lệ của cha mẹ
                if (parent1.Timetable.GetLength(0) != parent2.Timetable.GetLength(0) ||
                    parent1.Timetable.GetLength(1) != parent2.Timetable.GetLength(1))
                {
                    // Bỏ qua cặp cha mẹ không hợp lệ và tiếp tục với cặp tiếp theo
                    continue;
                }

                // Lai ghép hai cha mẹ để tạo ra cá thể con mới
                TimetableChromosome offspring = Crossover(parent1, parent2);

                // Thêm cá thể con vào danh sách mới
                newGeneration.Add(offspring);
            }

            // Thay thế thế hệ hiện tại bằng thế hệ mới vừa tạo
            _ListPopulation.Clear();
            _ListPopulation.AddRange(newGeneration);
            PrintTimetable_ListPopulation();
        }


        // Hàm lai ghép với cha và mẹ truyền vào
        private TimetableChromosome Crossover(TimetableChromosome parent1, TimetableChromosome parent2)
        {
            TimetableChromosome offspring = new TimetableChromosome();

            int h = parent1.Timetable.GetLength(0);
            int m = parent1.Timetable.GetLength(1);

            offspring.Timetable = new TimetableEvent[h, m];

            // Lặp qua từng ô trong thời khóa biểu
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // Chọn ngẫu nhiên cha hoặc mẹ cho gen tại vị trí (i, j)
                    if (_Random.NextDouble() < 0.5)
                    {
                        offspring.Timetable[i, j] = parent1.Timetable[i, j];
                    }
                    else
                    {
                        offspring.Timetable[i, j] = parent2.Timetable[i, j];
                    }
                }
            }

            return offspring;
        }
        #endregion*/

        /*#region Đột biến
        public void MutatePopulation()
        {
            Console.WriteLine("**************************___Đột biến___*********************");
            // Bước 1: Tính số cá thể sẽ bị đột biến
            int numberOfMutations = (int)Math.Ceiling(_ListPopulation.Count * _MutationRate);

            // Bước 2: Với mỗi cá thể bị đột biến, thực hiện đột biến
            for (int i = 0; i < numberOfMutations; i++)
            {
                // Xác định vị trí cá thể bị đột biến
                int mutatedIndex = _Random.Next(_ListPopulation.Count);
                TimetableChromosome mutatedChromosome = _ListPopulation[mutatedIndex];

                // Xác định vị trí gen đột biến bằng cách sinh ngẫu nhiên hai cặp số nguyên
                int h = mutatedChromosome.Timetable.GetLength(0);
                int m = mutatedChromosome.Timetable.GetLength(1);
                int vt1 = _Random.Next(1, m);
                int vt2 = _Random.Next(1, m);
                int vt3 = _Random.Next(1, h);
                int vt4 = _Random.Next(1, h);

                // Hoán vị hai cặp gen tại hai vị trí được chọn
                TimetableEvent temp = mutatedChromosome.Timetable[vt1, vt3];
                mutatedChromosome.Timetable[vt1, vt3] = mutatedChromosome.Timetable[vt2, vt4];
                mutatedChromosome.Timetable[vt2, vt4] = temp;
                _ListPopulation.Add(mutatedChromosome);
            }
            PrintTimetable_ListPopulation();
        }

        // Phương thức đổi cá thể giữa các quần thể cho Đa Quần thể
        public void ExchangeIndividualsBetweenPopulations(GeneticAlgorithm otherPopulation, int numberOfIndividuals)
        {
            for (int i = 0; i < numberOfIndividuals; i++)
            {
                int thisIndex = _Random.Next(this._ListPopulation.Count);
                int otherIndex = _Random.Next(otherPopulation._ListPopulation.Count);

                // Trao đổi cá thể
                var temp = this._ListPopulation[thisIndex];
                this._ListPopulation[thisIndex] = otherPopulation._ListPopulation[otherIndex];
                otherPopulation._ListPopulation[otherIndex] = temp;
            }
        }


        public void Mutation(TimetableChromosome chromosome)
        {
            Random random = new Random();


            int rows = chromosome.Timetable.GetLength(0);
            int cols = chromosome.Timetable.GetLength(1);

            // Duyệt qua từng ô của thời khóa biểu
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Áp dụng tỷ lệ đột biến
                    if (random.NextDouble() < _MutationRate)
                    {
                        // Thực hiện đột biến tại ô (i, j)
                        // Trong trường hợp đột biến, thay đổi giáo viên và phòng học của sự kiện tại ô đó

                        // Lấy ngẫu nhiên giáo viên mới
                        int newProfessorID = GetRandomProfessorID();
                        // Lấy ngẫu nhiên phòng học mới
                        int newRoomID = GetRandomRoomID();

                        // Cập nhật giáo viên và phòng học của sự kiện tại ô (i, j)
                        chromosome.Timetable[i, j].ProfessorID = newProfessorID;
                        chromosome.Timetable[i, j].RoomID = newRoomID;
                    }
                }
            }
        }

        #endregion*/

        #region Các ràng buộc cứng

        // điều kiện 1: Một giảng viên trong một ca dạy không quá một lớp;
        /*public bool ALecturerInAShiftDoesNotExceedOneClass()
                {
                    // Từ điển để theo dõi các giáo viên đã được phân công cho mỗi ca học
                    Dictionary<int, HashSet<int>> shiftAssignments = new Dictionary<int, HashSet<int>>();

                    foreach (var chromosome in _ListPopulation)
                    {
                        for (int day = 0; day < chromosome.Timetable.GetLength(0); day++)
                        {
                            for (int shift = 0; shift < chromosome.Timetable.GetLength(1); shift++)
                            {
                                // Khóa duy nhất cho từng ca học trong một ngày cụ thể
                                int shiftKey = day * chromosome.Timetable.GetLength(1) + shift;

                                // Nếu chưa có ca học này trong từ điển, thêm nó vào
                                if (!shiftAssignments.ContainsKey(shiftKey))
                                {
                                    shiftAssignments[shiftKey] = new HashSet<int>();
                                }

                                foreach (var timetableEvent in chromosome.Timetable[day, shift].Events)
                                {
                                    // Nếu giáo viên này đã được phân công trong ca học này, trả về false
                                    if (shiftAssignments[shiftKey].Contains(timetableEvent.ProfessorID))
                                    {
                                        return false;
                                    }

                                    // Nếu chưa, phân công giáo viên này vào ca học
                                    shiftAssignments[shiftKey].Add(timetableEvent.ProfessorID);
                                }
                            }
                        }
                    }

                    // Nếu không có giáo viên nào được phân công quá một lớp trong cùng một ca học, trả về true
                    return true;
                }*/
        private bool ALecturerInAShiftDoesNotExceedOneClass(TimetableSlot slot)
        {
            HashSet<int> seenProfessors = new HashSet<int>(); // Tập hợp lưu trữ giáo viên đã xét trong ca học

            foreach (var e in slot.Events)
            {
                // Kiểm tra xem giáo viên đã được phân công trong ca này chưa
                if (seenProfessors.Contains(e.ProfessorID))
                {
                    return false; // Nếu giáo viên đã được phân công, trả về false (vi phạm)
                }
                else
                {
                    seenProfessors.Add(e.ProfessorID); // Nếu chưa, thêm giáo viên vào tập hợp đã xét
                }
            }

            return true; // Nếu không có vi phạm, trả về true
        }


        // điều kiện 2: Một lớp trong một ca không quá một giảng viên;
        /* public void AClassInAShiftNoMoreThanOneLecturer()
         {
             Console.WriteLine("Một lớp trong một ca không quá một giảng viên");
             foreach (var chromosome in this._ListPopulation) // Duyệt qua mỗi cá thể trong quần thể
             {
                 bool check = true; // Biến kiểm tra xem có lớp học có nhiều hơn một giáo viên trong một ca không

                 // Tạo một Dictionary để lưu trữ danh sách các giáo viên đã dạy trong mỗi ca của mỗi ngày trong tuần
                 Dictionary<int, HashSet<int>> shifts = new Dictionary<int, HashSet<int>>();

                 // Duyệt qua từng ô trong thời khóa biểu của cá thể
                 for (int i = 0; i < chromosome.Timetable.GetLength(0); i++) // Duyệt qua từng ngày trong tuần (hàng)
                 {
                     for (int j = 0; j < chromosome.Timetable.GetLength(1); j++) // Duyệt qua từng ca dạy trong một ngày (cột)
                     {
                         TimetableEvent currentEvent = chromosome.Timetable[i, j]; // Lấy sự kiện thời khóa biểu tại hàng i và cột j (tại ngày và ca học)

                         // Kiểm tra xem có sự kiện thời khóa biểu nào tại ô đó không
                         if (currentEvent != null)
                         {
                             // Kiểm tra xem đã có HashSet cho ca dạy này chưa
                             if (!shifts.ContainsKey(j))
                             {
                                 shifts[j] = new HashSet<int>();
                             }

                             // Kiểm tra xem lớp học đã có nhiều hơn một giáo viên trong ca đó chưa
                             if (shifts[j].Contains(currentEvent.ClassID))
                             {
                                 Console.WriteLine("Lớp học có nhiều hơn một giáo viên trong một ca tại phòng có ID " + currentEvent.RoomID);
                                 Console.WriteLine(
                                     "Weekday: " + (i + 2) + "   "
                                     + "Study: " + (j + 1) + "   "
                                     + "ClassID: " + currentEvent.ClassID + "   "
                                     + "RoomID: " + currentEvent.RoomID + "   "
                                     + "ProfessorID: " + currentEvent.ProfessorID + "   "
                                     + "CourseID: " + currentEvent.CourseID + "   "
                                     + "RoomID: " + currentEvent.RoomID + "   " // Thêm RoomID vào thông điệp
                                 );

                                 check = false; // Đánh dấu là có lớp học có nhiều hơn một giáo viên trong một ca
                                 Console.WriteLine("break");
                                 break;
                             }
                             else
                             {
                                 // Nếu chưa, thêm ID của lớp học vào HashSet để đánh dấu là đã có trong ca dạy j của ngày đó
                                 shifts[j].Add(currentEvent.ClassID);
                             }
                         }
                     }
                     if (check == false)
                     {
                         break;
                     }
                 }
                 if (check == true) // Nếu không có lớp học có nhiều hơn một giáo viên trong một ca nào đó, tăng điểm Fitness của cá thể lên
                 {
                     chromosome.Fitness++;
                     Console.WriteLine("Fitness: " + chromosome.Fitness);
                 }
             }
         }
 */
        // điều kiện 3: Một lớp trong một ca không quá một môn;
        /*public void AClassInAShiftNoMoreThanOnesubject()
        {
            Console.WriteLine("Một lớp trong một ca không quá một môn");
            foreach (var chromosome in this._ListPopulation) // Duyệt qua mỗi cá thể trong quần thể
            {
                bool check = true; // Biến kiểm tra xem có lớp học có nhiều hơn một môn trong một ca không
                
                // Tạo một Dictionary để lưu trữ danh sách các môn học đã được dạy trong mỗi ca của mỗi ngày trong tuần
                Dictionary<int, HashSet<int>> shifts = new Dictionary<int, HashSet<int>>();

                // Duyệt qua từng ô trong thời khóa biểu của cá thể
                for (int i = 0; i < chromosome.Timetable.GetLength(0); i++) // Duyệt qua từng ngày trong tuần (hàng)
                {
                    for (int j = 0; j < chromosome.Timetable.GetLength(1); j++) // Duyệt qua từng ca dạy trong một ngày (cột)
                    {
                        TimetableEvent currentEvent = chromosome.Timetable[i, j]; // Lấy sự kiện thời khóa biểu tại hàng i và cột j (tại ngày và ca học)

                        // Kiểm tra xem có sự kiện thời khóa biểu nào tại ô đó không
                        if (currentEvent != null)
                        {
                            // Kiểm tra xem đã có HashSet cho ca dạy này chưa
                            if (!shifts.ContainsKey(j))
                            {
                                shifts[j] = new HashSet<int>();
                            }

                            // Kiểm tra xem lớp học đã có nhiều hơn một môn trong ca đó chưa
                            if (shifts[j].Contains(currentEvent.CourseID))
                            {
                                Console.WriteLine("Lớp học có nhiều hơn một môn trong một ca tại phòng có ID " + currentEvent.RoomID);
                                Console.WriteLine(
                                    "Weekday: " + (i + 2) + "   "
                                    + "Study: " + (j + 1) + "   "
                                    + "ClassID: " + currentEvent.ClassID + "   "
                                    + "RoomID: " + currentEvent.RoomID + "   "
                                    + "ProfessorID: " + currentEvent.ProfessorID + "   "
                                    + "CourseID: " + currentEvent.CourseID + "   "
                                    + "RoomID: " + currentEvent.RoomID + "   " // Thêm RoomID vào thông điệp
                                );

                                check = false; // Đánh dấu là có lớp học có nhiều hơn một môn trong một ca
                                Console.WriteLine("break");
                                break;
                            }
                            else
                            {
                                // Nếu chưa, thêm ID của môn học vào HashSet để đánh dấu là đã có trong ca dạy j của ngày đó
                                shifts[j].Add(currentEvent.CourseID);
                            }
                        }
                    }
                    if (check == false)
                    {
                        break;
                    }
                }
                if (check == true) // Nếu không có lớp học có nhiều hơn một môn trong một ca nào đó, tăng điểm Fitness của cá thể lên
                {
                    chromosome.Fitness++;
                    Console.WriteLine("Fitness: " + chromosome.Fitness);
                }
            }
        }
*/
        // điều kiện 4: Một môn cho một lớp không quá một ca trong một ngày;
        /* public void OneSubjectForAClassHasNoMoreThanOneShiftInADay()
         {
             Console.WriteLine("Một môn cho một lớp không quá một ca trong một ngày");
             foreach (var chromosome in this._ListPopulation) // Duyệt qua mỗi cá thể trong quần thể
             {
                 bool check = true; // Biến kiểm tra xem có lớp học có nhiều hơn một ca trong một ngày không

                 // Tạo một Dictionary để lưu trữ danh sách các ca đã được dạy trong mỗi ngày của tuần
                 Dictionary<int, HashSet<int>> dailyShifts = new Dictionary<int, HashSet<int>>();

                 // Duyệt qua từng ô trong thời khóa biểu của cá thể
                 for (int i = 0; i < chromosome.Timetable.GetLength(0); i++) // Duyệt qua từng ngày trong tuần (hàng)
                 {
                     for (int j = 0; j < chromosome.Timetable.GetLength(1); j++) // Duyệt qua từng ca dạy trong một ngày (cột)
                     {
                         TimetableEvent currentEvent = chromosome.Timetable[i, j]; // Lấy sự kiện thời khóa biểu tại hàng i và cột j (tại ngày và ca học)

                         // Kiểm tra xem có sự kiện thời khóa biểu nào tại ô đó không
                         if (currentEvent != null)
                         {
                             // Kiểm tra xem đã có HashSet cho ngày này chưa
                             if (!dailyShifts.ContainsKey(i))
                             {
                                 dailyShifts[i] = new HashSet<int>();
                             }

                             // Kiểm tra xem lớp học đã có nhiều hơn một ca trong ngày đó chưa
                             if (dailyShifts[i].Contains(currentEvent.ClassID))
                             {
                                 Console.WriteLine("Lớp học có nhiều hơn một ca trong một ngày");
                                 Console.WriteLine(
                                     "Weekday: " + (i + 2) + "   "
                                     + "Study: " + (j + 1) + "   "
                                     + "ClassID: " + currentEvent.ClassID + "   "
                                     + "RoomID: " + currentEvent.RoomID + "   "
                                     + "ProfessorID: " + currentEvent.ProfessorID + "   "
                                     + "CourseID: " + currentEvent.CourseID + "   "
                                 );

                                 check = false; // Đánh dấu là có lớp học có nhiều hơn một ca trong một ngày
                                 Console.WriteLine("break");
                                 break;
                             }
                             else
                             {
                                 // Nếu chưa, thêm ID của lớp học vào HashSet để đánh dấu là đã có trong ngày đó
                                 dailyShifts[i].Add(currentEvent.ClassID);
                             }
                         }
                     }
                     if (check == false)
                     {
                         break;
                     }
                 }
                 if (check == true) // Nếu không có lớp học có nhiều hơn một ca trong một ngày nào đó, tăng điểm Fitness của cá thể lên
                 {
                     chromosome.Fitness++;
                     Console.WriteLine("Fitness: " + chromosome.Fitness);
                 }
             }
         }*/

        // điều kiện 5: Một phòng học trong một ca không quá một lớp học;
        /*public void OneRoomInAShiftHasNoMoreThanOneClass()
        {
            Console.WriteLine("Một phòng học trong một ca không quá một lớp học");
            foreach (var chromosome in this._ListPopulation) // Duyệt qua mỗi cá thể trong quần thể
            {
                bool check = true; // Biến kiểm tra xem có phòng học nào có nhiều hơn một lớp học trong một ca không

                // Tạo một Dictionary để lưu trữ danh sách các lớp học đã được dạy trong mỗi ca của mỗi ngày trong tuần
                Dictionary<int, HashSet<int>> shifts = new Dictionary<int, HashSet<int>>();

                // Duyệt qua từng ô trong thời khóa biểu của cá thể
                for (int i = 0; i < chromosome.Timetable.GetLength(0); i++) // Duyệt qua từng ngày trong tuần (hàng)
                {
                    for (int j = 0; j < chromosome.Timetable.GetLength(1); j++) // Duyệt qua từng ca dạy trong một ngày (cột)
                    {
                        TimetableEvent currentEvent = chromosome.Timetable[i, j]; // Lấy sự kiện thời khóa biểu tại hàng i và cột j (tại ngày và ca học)

                        // Kiểm tra xem có sự kiện thời khóa biểu nào tại ô đó không
                        if (currentEvent != null)
                        {
                            // Kiểm tra xem đã có HashSet cho ca dạy này chưa
                            if (!shifts.ContainsKey(j))
                            {
                                shifts[j] = new HashSet<int>();
                            }

                            // Kiểm tra xem phòng học đã có nhiều hơn một lớp học trong ca đó chưa
                            if (shifts[j].Contains(currentEvent.RoomID))
                            {
                                Console.WriteLine("Phòng học có nhiều hơn một lớp học trong một ca");
                                Console.WriteLine(
                                    "Weekday: " + (i + 2) + "   "
                                    + "Study: " + (j + 1) + "   "
                                    + "ClassID: " + currentEvent.ClassID + "   "
                                    + "RoomID: " + currentEvent.RoomID + "   "
                                    + "ProfessorID: " + currentEvent.ProfessorID + "   "
                                    + "CourseID: " + currentEvent.CourseID + "   "
                                );

                                check = false; // Đánh dấu là có phòng học có nhiều hơn một lớp học trong một ca
                                Console.WriteLine("break");
                                break;
                            }
                            else
                            {
                                // Nếu chưa, thêm ID của phòng học vào HashSet để đánh dấu là đã có trong ca dạy j của ngày đó
                                shifts[j].Add(currentEvent.RoomID);
                            }
                        }
                    }
                    if (check == false)
                    {
                        break;
                    }
                }
                if (check == true) // Nếu không có phòng học nào có nhiều hơn một lớp học trong một ca nào đó, tăng điểm Fitness của cá thể lên
                {
                    chromosome.Fitness++;
                    Console.WriteLine("Fitness: " + chromosome.Fitness);
                }
            }
        }*/

        // điều kiện 6: Một lớp có nhiều ca của một môn trong tuần thì cố định ca học trong tất cả các ngày học, và các ngày học cách nhau ít nhất 1 ngày chứ không học các ngày liên tiếp nhau
        /*public void OneClassHasNoMoreThanOneShiftInADay()
        {
            Console.WriteLine("Một lớp có nhiều ca của một môn trong tuần thì cố định ca học trong tất cả các ngày học, và các ngày học cách nhau ít nhất 1 ngày");

            foreach (var chromosome in this._ListPopulation) // Duyệt qua mỗi cá thể trong quần thể
            {
                bool check = true; // Biến kiểm tra xem có lớp học nào có nhiều ca học trong một ngày không

                // Tạo một Dictionary để lưu trữ danh sách các ca học của mỗi ngày trong tuần cho mỗi lớp học
                Dictionary<int, List<TimetableEvent>> dailyShifts = new Dictionary<int, List<TimetableEvent>>();

                // Duyệt qua từng ô trong thời khóa biểu của cá thể
                for (int i = 0; i < chromosome.Timetable.GetLength(0); i++) // Duyệt qua từng ngày trong tuần (hàng)
                {
                    List<TimetableEvent> dayEvents = new List<TimetableEvent>(); // Danh sách các sự kiện trong một ngày
                    for (int j = 0; j < chromosome.Timetable.GetLength(1); j++) // Duyệt qua từng ca dạy trong một ngày (cột)
                    {
                        TimetableEvent currentEvent = chromosome.Timetable[i, j]; // Lấy sự kiện thời khóa biểu tại hàng i và cột j (tại ngày và ca học)

                        // Kiểm tra xem có sự kiện thời khóa biểu nào tại ô đó không
                        if (currentEvent != null)
                        {
                            dayEvents.Add(currentEvent); // Thêm sự kiện vào danh sách các sự kiện trong ngày đó
                        }
                    }

                    // Kiểm tra xem có hai sự kiện nào trong cùng một ngày không
                    if (dayEvents.Count > 1)
                    {
                        // Kiểm tra xem các ngày học cách nhau ít nhất 1 ngày
                        for (int k = 1; k < dayEvents.Count; k++)
                        {
                            if (dayEvents[k].WeekdayID - dayEvents[k - 1].WeekdayID == 1)
                            {
                                check = false;
                                Console.WriteLine("Các ngày học không cách nhau 1 ngày");
                                break;
                            }
                        }
                    }

                    if (!check)
                    {
                        break;
                    }
                }

                if (check == true) // Nếu không có lớp học nào có nhiều hơn một ca học trong một ngày, tăng điểm Fitness của cá thể lên
                {
                    chromosome.Fitness++;
                    Console.WriteLine("Fitness: " + chromosome.Fitness);
                }
            }
        }*/
        #endregion

        #region Các ràng buộc mềm

        // điều kiện 7: Số ca dạy của các giảng viên phải xấp xỉ nhau
        /*public void ApproximatelyEqualNumberOfShiftsForProfessors()
        {
            Console.WriteLine("Số ca dạy của các giảng viên phải xấp xỉ nhau");

            // Tạo một Dictionary để lưu trữ số lượng ca dạy của mỗi giảng viên
            Dictionary<int, int> professorShiftCounts = new Dictionary<int, int>();

            // Duyệt qua từng cá thể trong quần thể
            foreach (var chromosome in this._ListPopulation)
            {
                // Đếm số lượng ca dạy của mỗi giảng viên trong cá thể hiện tại
                foreach (var currentEvent in chromosome.Timetable)
                {
                    if (currentEvent != null)
                    {
                        // Tăng số lượng ca dạy của giảng viên lên 1
                        if (professorShiftCounts.ContainsKey(currentEvent.ProfessorID))
                        {
                            professorShiftCounts[currentEvent.ProfessorID]++;
                        }
                        else
                        {
                            professorShiftCounts[currentEvent.ProfessorID] = 1;
                        }
                    }
                }
            }

            // Tính trung bình số lượng ca dạy của các giảng viên
            double averageShiftCount = professorShiftCounts.Values.Average();

            // Kiểm tra xem số lượng ca dạy của mỗi giảng viên có xấp xỉ nhau hay không
            foreach (var count in professorShiftCounts.Values)
            {
                if (Math.Abs(count - averageShiftCount) > 1)
                {
                    // Nếu chênh lệch lớn hơn 1, không thỏa mãn điều kiện
                    Console.WriteLine("Số ca dạy của các giảng viên không xấp xỉ nhau");
                    return;
                }
            }

            // Nếu số lượng ca dạy của các giảng viên xấp xỉ nhau, tăng điểm Fitness của cá thể lên
            foreach (var chromosome in this._ListPopulation)
            {
                chromosome.Fitness++;
                Console.WriteLine("Fitness: " + chromosome.Fitness);
            }
        }*/

        // điều kiện 8 : 
        // Hàm tìm phòng gần nhất
        private int FindClosestRoom(int roomID)
        {
            // Triển khai logic để tìm phòng gần nhất
            // Ví dụ: Tìm phòng có ID gần nhất với roomID
            // Trong thực tế, bạn có thể sử dụng thông tin về vị trí địa lý của các phòng để xác định phòng gần nhất
            return roomID;
        }

        /*public void OptimizeProfessorShifts()
        {
            Console.WriteLine("Ưu tiên xếp các ca dạy liên tiếp của mỗi giảng viên trong cùng một phòng hoặc phòng gần nhau");

            foreach (var chromosome in this._ListPopulation)
            {
                // Lặp qua từng giảng viên
                foreach (var professorID in chromosome.GetDistinctProfessorIDs())
                {
                    List<TimetableEvent> professorEvents = chromosome.GetProfessorEvents(professorID);

                    // Sắp xếp các sự kiện dạy của giảng viên theo thứ tự thời gian
                    professorEvents.Sort((x, y) => x.Time.CompareTo(y.Time));

                    // Tìm chuỗi các sự kiện dạy liên tiếp
                    List<List<TimetableEvent>> consecutiveShifts = new List<List<TimetableEvent>>();
                    List<TimetableEvent> currentShift = new List<TimetableEvent>();

                    foreach (var evt in professorEvents)
                    {
                        if (currentShift.Count == 0 || evt.RoomID == currentShift.Last().RoomID)
                        {
                            currentShift.Add(evt);
                        }
                        else
                        {
                            consecutiveShifts.Add(currentShift);
                            currentShift = new List<TimetableEvent>() { evt };
                        }
                    }

                    // Thêm chuỗi cuối cùng vào danh sách
                    consecutiveShifts.Add(currentShift);

                    // Ưu tiên xếp các ca dạy liên tiếp vào cùng một phòng hoặc phòng gần nhau
                    foreach (var shift in consecutiveShifts)
                    {
                        // Đặt các sự kiện dạy trong cùng một phòng hoặc phòng gần nhau
                        for (int i = 0; i < shift.Count; i++)
                        {
                            shift[i].RoomID = FindClosestRoom(shift[i].RoomID); // Hàm này cần được triển khai để tìm phòng gần nhất
                        }
                    }
                }
            }
        }*/

        #endregion

        #region Các hàm phục vụ cho việc code

        // gán giá trị random 
       /* private TimetableEvent GenerateRandomEvent(int indexWeekdayID, int indexStudyID)
        {
            // Đoạn mã để tạo ra một sự kiện ngẫu nhiên
            // Bạn có thể tùy chỉnh hàm này để phù hợp với cấu trúc của dữ liệu sự kiện của bạn
            // Ví dụ: lấy ngẫu nhiên giảng viên, lớp học phần, và môn học từ danh sách có sẵn
            return new TimetableEvent
            {
                ProfessorID = GetRandomProfessorID(),
                ClassID = GetRandomClassID(),
                CourseID = GetRandomCourseID(),
                RoomID = GetRandomRoomID(),
                StudyID = GetStudyIDByIndex(indexStudyID),
                WeekdayID = GetWeekdayIDByIndex(indexWeekdayID),
            };
        }
*/
        // in ra mà hình console
       /* public void PrintTimetable_ListPopulation()
        {
            Console.WriteLine("print TimetableChromosome  _ListPopulation");
            Print(_ListPopulation);
        }
        public void PrintTimetable_ListOffspring()
        {
            Console.WriteLine("print TimetableChromosome  _ListOffspring");
            Print(_ListOffspring);
        }
        public void Print(List<TimetableChromosome> value)
        {
            string query = "SELECT clr.RoomName, st.NameStudy, wd.NameWeekday, p.ProfessorName, cl.ClassName, cs.CourseCode + ' _ ' + cs.CourseName As Course " +
                             "FROM Classrooms clr, Studys st, Weekdays wd, Professors p, Classes cl, Courses cs " +
                             "WHERE clr.RoomID = @RoomID AND st.StudyID = @StudyID AND wd.WeekdayID = @WeekdayID " +
                             "AND p.ProfessorID = @ProfessorID AND cl.ClassID = @ClassID AND cs.CourseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(_KetNoi.ConnectionString))
            {
                connection.Open();

                foreach (var chromosome in value) 
                {
                    Console.WriteLine("____________________________ Fitness: " + chromosome.Fitness +  "_________________________________\n");

                    for (int i = 0; i < chromosome.Timetable.GetLength(0); i++)
                    {
                        int weekdayID = i + 1;
                        Console.WriteLine($"________________ Weekday {weekdayID + 1}_________________");

                        for (int j = 0; j < chromosome.Timetable.GetLength(0); j++)
                        {
                            int studyID = j + 1;
                            TimetableEvent currentEvent = chromosome.Timetable[i, j];

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@RoomID", currentEvent.RoomID);
                                command.Parameters.AddWithValue("@StudyID", studyID);
                                command.Parameters.AddWithValue("@WeekdayID", weekdayID);
                                command.Parameters.AddWithValue("@ProfessorID", currentEvent.ProfessorID);
                                command.Parameters.AddWithValue("@ClassID", currentEvent.ClassID);
                                command.Parameters.AddWithValue("@CourseID", currentEvent.CourseID);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    StringBuilder sb = new StringBuilder();
                                    while (reader.Read())
                                    {
                                        sb.AppendLine(
                                            $"\tWeekday: {reader["NameWeekday"]} " +
                                            $"\tStudy: {reader["NameStudy"]} " +
                                            $"\tProfessor: {reader["ProfessorName"]} " +
                                            $"\tCourse: {reader["Course"]} " +
                                            $"\tClass: {reader["ClassName"]} " +
                                            $"\tRoom: {reader["RoomName"]} "
                                        );
                                    }
                                    Console.WriteLine(sb.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
*/
        /*public void PrintTimetable()
        {
            foreach (var chromosome in _ListPopulation)
            {
                Console.WriteLine("Chromosome Fitness: " + chromosome.Fitness);
                for (int day = 0; day < _Weekdays; day++)
                {
                    Console.WriteLine($"Day {day + 1}:");
                    for (int shift = 0; shift < _Shift; shift++)
                    {
                        TimetableEvent evt = chromosome.Timetable[day, shift];
                        if (evt != null) // Đảm bảo rằng sự kiện tồn tại trước khi in
                        {
                            // Lấy thông tin về giáo viên, lớp học, môn học, phòng học từ cơ sở dữ liệu hoặc từ một bảng ánh xạ nếu có
                            string professorName = "Giáo viên ID: " + evt.ProfessorID; // Thay thế bằng cách lấy tên giáo viên thực
                            string className = "Lớp học ID: " + evt.ClassID; // Thay thế bằng cách lấy tên lớp học thực
                            string courseName = "Môn học ID: " + evt.CourseID; // Thay thế bằng cách lấy tên môn học thực
                            string roomName = "Phòng học ID: " + evt.RoomID; // Thay thế bằng cách lấy tên phòng học thực

                            Console.WriteLine($"\tShift {shift + 1}: {professorName}, {className}, {courseName}, {roomName}");
                        }
                        else
                        {
                            Console.WriteLine($"\tShift {shift + 1}: No Event");
                        }
                    }
                }
                Console.WriteLine(); // Thêm dòng trống giữa các chromosome
            }
        }*/

        
        // lấy random lớp học
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
    }
}