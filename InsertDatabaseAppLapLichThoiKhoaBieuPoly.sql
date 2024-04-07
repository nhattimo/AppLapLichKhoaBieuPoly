USE AppThoiKhoaBieu

-- insert tài khoản 
INSERT INTO Role (NameRole) VALUES ('admin');
GO
INSERT INTO Account (NameAccount, Password, RoleID) VALUES ('admin', 'admin', 1);
GO
-- insert bộ mô chuyên ngành
INsert INTO Departments (DepartmentName) Values (N'Công nghệ thông tin')
GO
-- insert 23 môn học
INSERT INTO Courses (CourseCode, CourseName, DepartmentID) VALUES
('COM2012', N'Cơ sở dữ liệu', 1),
('COM2034', N'Quản trị cơ sở dữ liệu với SQL Server', 1),
('GAM101', N'Lập trình Game 1', 1),
('GAM104', N'Nhập môn lập trình Game', 1),
('MOB1014', N'Lập trình Java 1', 1),
('NET101', N'Lập trình C#1', 1),
('NET102', N'Lập trình C#2', 1),
('SOA101', N'Hệ quản trị CSDL', 1),
('SOA203', N'Thiết lập và quản trị mạng máy tính', 1),
('SOA204', N'Phần mềm miễn phí và mã nguồn mở', 1),
('SOF3021', N'Lập trình Java 5', 1),
('SOF3031', N'Kiểm thử cơ bản', 1),
('WEB1013', N'Xây dựng trang Web', 1),
('WEB1022', N'Quản trị website', 1),
('WEB105', N'Thiết kế UI/UX', 1),
('WEB2033', N'Xây dựng trang Web 2', 1),
('WEB2041', N'Dự án mẫu (TKTW)', 1),
('WEB2062', N'Lập trình JavaScript nâng cao', 1),
('WEB207', N'Front-End Frameworks', 1),
('WEB208', N'Lập trình Front-End Framework 1', 1),
('WEB3014', N'Lập trình PHP 2', 1),
('WEB3023', N'Thiết kế Web với HTML5&CSS3', 1),
('WEB501', N'Lập trình ECMAScript', 1);
GO
-- INSERT dữ liệu 19 giáo viên
INSERT INTO Professors (ProfessorName) VALUES
('khantn'),
('daotta4'),
('tinhph2'),
('lanptm'),
('duanlv'),
('hanhttb5'),
('dungntt71'),
('tantd12'),
('huyenvtt2'),
('anhnn4'),
('namnv3'),
('hoacq'),
('tienttt15'),
('lienpna'),
('thuvtd'),
('anhdnt'),
('chienpv12'),
('dinhnv'),
('hah7');

GO
-- Insert 44 lớp học
INSERT INTO Classes (ClassName, DepartmentID) VALUES
('WD19307', 1),
('WD19306', 1),
('WD19305', 1),
('WD19304', 1),
('WD19303', 1),
('WD19302', 1),
('WD19301', 1),
('SD19305', 1),
('SD19304', 1),
('SD19303', 1),
('SD19302', 1),
('SD19301', 1),
('SD19201', 1),
('MD18302', 1),
('MD18301', 1),
('GA19302', 1),
('GA19301', 1),
('MD19301', 1),
('SA19303', 1),
('SA19302', 1),
('SA19301', 1),
('SA18303', 1),
('SA18302', 1),
('SA18301', 1),
('SD18201', 1),
('SD18307', 1),
('SD18306', 1),
('SD18305', 1),
('SD18304', 1),
('SD18303', 1),
('SD18302', 1),
('SD18301', 1),
('WD18401', 1),
('WD19201', 1),
('DM19201', 1),
('WD18309', 1),
('WD18308', 1),
('WD18307', 1),
('WD18306', 1),
('WD18305', 1),
('WD18304', 1),
('WD18303', 1),
('WD18302', 1),
('WD18301', 1),
('WD18201', 1);
GO
-- 
-- Inserting professors for course COM2012
INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('khantn', 'daotta4', 'tinhph2', 'lanptm', 'duanlv', 'hanhttb5', 'dungntt71', 'tantd12') 
AND c.CourseCode = 'COM2012';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('huyenvtt2') 
AND c.CourseCode = 'COM2034';


INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('tinhph2') 
AND c.CourseCode = 'GAM101';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('anhnn4') 
AND c.CourseCode = 'GAM104';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('khantn','lanptm','huyenvtt2','duanlv') 
AND c.CourseCode = 'MOB1014';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('tantd12') 
AND c.CourseCode = 'NET101';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('khantn') 
AND c.CourseCode = 'NET102';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('namnv3', 'khantn') 
AND c.CourseCode = 'SOA101';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('hoacq') 
AND c.CourseCode = 'SOA203';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('hoacq','hanhttb5', 'hoacq') 
AND c.CourseCode = 'SOA204';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('anhnn4') 
AND c.CourseCode = 'SOF3021';


INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('anhnn4', 'dinhnv', 'hoacq', 'namnv3') 
AND c.CourseCode = 'SOF3031';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('namnv3', 'tienttt15', 'daotta4', 'thuvtd', 'lienpna') 
AND c.CourseCode = 'WEB1013';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('tienttt15') 
AND c.CourseCode = 'WEB1022';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('tinhph2') 
AND c.CourseCode = 'WEB105';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('khantn') 
AND c.CourseCode = 'WEB2033';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('tienttt15') 
AND c.CourseCode = 'WEB2041';


INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('thuvtd', 'chienpv12', 'dinhnv', 'tienttt15', 'anhdnt') 
AND c.CourseCode = 'WEB2062';


INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('anhdnt') 
AND c.CourseCode = 'WEB208';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('chienpv12','khantn','tienttt15') 
AND c.CourseCode = 'WEB3014';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('thuvtd') 
AND c.CourseCode = 'WEB3023';

INSERT INTO ProfessorCourses (ProfessorID, CourseID) 
SELECT p.ProfessorID, c.CourseID 
FROM Professors p, Courses c 
WHERE p.ProfessorName IN ('chienpv12','thuvtd','hah7') 
AND c.CourseCode = 'WEB501';

GO
-- Inserting courses for the first class
INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SD19301',
'SD19302',
'SD19303',
'SD19304',
'SD19305',
'WD19301',
'WD19302',
'WD19303',
'WD19304',
'WD19305',
'WD19306',
'WD19307'
)
AND c.CourseCode = 'COM2012';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SD19201')
AND c.CourseCode = 'COM2034';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('GA19301',
'GA19302')
AND c.CourseCode = 'GAM101';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('MD18301',
'MD18302')
AND c.CourseCode = 'GAM104';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('MD19301',
'SD19303',
'SD19304',
'SD19305'
)
AND c.CourseCode = 'MOB1014';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('GA19301',
'GA19302',
'SD19301',
'SD19302'
)
AND c.CourseCode = 'NET101';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SD19201'
)
AND c.CourseCode = 'NET102';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SA19301',
'SA19302',
'SA19303'
)
AND c.CourseCode = 'SOA101';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SA18301',
'SA18302',
'SA18303'
)
AND c.CourseCode = 'SOA203';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SA18301',
'SA18302',
'SA18303'
)
AND c.CourseCode = 'SOA204';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SD18201'
)
AND c.CourseCode = 'SOF3021';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SD18301',
'SD18302',
'SD18303',
'SD18304',
'SD18305',
'SD18306',
'SD18307'
)
AND c.CourseCode = 'SOF3031';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('MD19301',
'WD19301',
'WD19302',
'WD19303',
'WD19304',
'WD19305',
'WD19306',
'WD19307'
)
AND c.CourseCode = 'WEB1013';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('WD18401'
)
AND c.CourseCode = 'WEB1022';


INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('WD19201'
)
AND c.CourseCode = 'WEB105';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('DM19201'
)
AND c.CourseCode = 'WEB2033';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('WD18401'
)
AND c.CourseCode = 'WEB2041';


INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('MD18301',
'MD18302',
'WD18301',
'WD18302',
'WD18303',
'WD18304',
'WD18305',
'WD18306',
'WD18307',
'WD18308',
'WD18309'
)
AND c.CourseCode = 'WEB2062';



INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('SD18301',
'SD18302',
'SD18303',
'SD18304',
'SD18305',
'SD18306',
'SD18307'
)
AND c.CourseCode = 'WEB207';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('WD18201'
)
AND c.CourseCode = 'WEB208';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('WD18301',
'WD18302',
'WD18303',
'WD18304'
)
AND c.CourseCode = 'WEB3014';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('WD18305',
'WD18306',
'WD18307',
'WD18308',
'WD18309'
)
AND c.CourseCode = 'WEB501';

INSERT INTO ClassesCourses (ClassID, CourseID) 
SELECT cl.ClassID, c.CourseID 
FROM Classes cl, Courses c 
WHERE cl.ClassName IN ('WD19201'
)
AND c.CourseCode = 'WEB3023';

-- insert các ca học
INSERT INTO Studys (NameStudy, StartTime, EndTime) VALUES (N'Ca1', '07:15:00', '09:15:00'),
(N'Ca2', '09:30:00', '11:30:00'),
(N'Ca3', '12:00:00', '14:00:00'),
(N'Ca4', '14:15:00', '16:15:00'),
(N'Ca5', '16:30:00', '18:30:00'),
(N'Ca6', '19:45:00', '21:45:00');
GO

-- insert phòng học 24 phòng học
INSERT Classrooms (RoomName) values 
(N'R100'),(N'R101'),(N'R102'),(N'R103'),(N'R104'),(N'R105'),
(N'R200'),(N'R201'),(N'R202'),(N'R203'),(N'R204'),(N'R205'),
(N'R300'),(N'R301'),(N'R302'),(N'R303'),(N'R304'),(N'R305'),
(N'R400'),(N'R401'),(N'R402'),(N'R403'),(N'R404'),(N'R405')
GO

-- Insert thứ trong tuần
INSERT INTO Weekdays (NameWeekday) VALUES 
(N'Thứ 2'),(N'Thứ 3'),(N'Thứ 4'),(N'Thứ 5'),(N'Thứ 6'),(N'Thứ 7');
GO
SELECT * FROM Weekdays
GO



INSERT INTO ClassSchedules (ClassID, CourseID, ProfessorID, RoomID, WeekdayID, StudyID)
VALUES
    (1, 1, 1, 1, 1, 1), 
    (2, 2, 2, 2, 2, 2), 
    (1, 1, 1, 1, 3, 1), 
	(2, 2, 2, 2, 4, 2), 
    (1, 1, 1, 1, 5, 1), 
    (2, 2, 2, 2, 6, 2); 
GO

SELECT * FROM Classes
SELECT * FROM Account
SELECT * FROM Role
SELECT * FROM Professors
Select * from Departments
SELECT * FROM Classes

SELECT cs.ScheduleID, c.ClassName, cr.CourseName, pf.ProfessorName, clr.RoomName, wd.NameWeekday, st.NameStudy
FROM ClassSchedules cs 
inner join Classes c on c.ClassID = cs.ClassID
inner join Courses cr on cr.CourseID = cs.CourseID
inner join Professors pf on pf.ProfessorID = cs.ProfessorID
inner join Classrooms clr on clr.RoomID = cs.RoomID
inner join Weekdays wd on wd.WeekdayID = cs.WeekdayID
inner join Studys st on st.StudyID = cs.StudyID
order by wd.WeekdayID

SELECT * FROM ClassSchedules 

SELECT a.CourseID, a.CourseName, b.DepartmentName FROM Courses a
INNER JOIN Departments b ON a.DepartmentID = b.DepartmentID;

SELECT a.ProfessorName FROM Professors a INNER JOIN ProfessorCourses b on a.ProfessorID = B.ProfessorID
DELETE FROM Professors WHERE ProfessorID = 1;
DELETE FROM ProfessorCourses WHERE ProfessorID = 1;
SELECT * FROM ProfessorCourses
DECLARE @ProfessorID int = (SELECT CourseID FROM ProfessorCourses where ProfessorID = 1)
SELECT CourseName FROM Courses c WHERE c.CourseID in (SELECT CourseID FROM ProfessorCourses where ProfessorID = 3)
select * from ProfessorCourses
DELETE FROM ProfessorCourses WHERE ProfessorID = 2 and CourseID = 1 ;
DELETE FROM Professors WHERE ProfessorID = 4;
SELECT * FROM Classrooms
SELECT a.ClassID, a.ClassName, b.DepartmentName FROM Classes a INNER JOIN Departments b on a.DepartmentID = b.DepartmentID
SELECT a.CourseID, a.CourseName, b.DepartmentName FROM Courses a INNER JOIN Departments b on a.DepartmentID = b.DepartmentID
SELECT * FROM Departments

DELETE FROM Departments WHERE DepartmentID

delete ClassSchedules

SELECT CourseID FROM Courses
SELECT ClassID FROM Classes
SELECT ProfessorID FROM Professors
SELECT RoomID FROM Classrooms
SELECT * FROM Weekdays
SELECT WeekdayID FROM
GO
SELECT clr.RoomID FROM Classrooms clr 
inner join Studys st
where clr.RoomID = 1

SELECT clr.RoomName, st.NameStudy, wd.NameWeekday, p.ProfessorName, cl.ClassName, cs.CourseCode +  ' _ ' + cs.CourseName As Course  from Classrooms clr , Studys st, Weekdays wd, Professors p, Classes cl, Courses cs
where clr.RoomID = 1 AND st.StudyID = 1 AND wd.WeekdayID = 1 AND p.ProfessorID = 1 AND cl.ClassID = 1 AND cs.CourseID = 1; 







SELECT pc.ProfessorID
FROM ProfessorCourses pc
INNER JOIN ClassesCourses cc ON pc.CourseID = cc.CourseID
WHERE cc.ClassID = 1 AND cc.CourseID = 1;

SELECT * FROM ClassSchedules

SELECT 
    clr.RoomName, 
    wd.NameWeekday, 
    st.NameStudy,
    c.ClassName, 
    cr.CourseName, 
    pf.ProfessorName 
FROM ClassSchedules cs 
INNER JOIN Classes c ON c.ClassID = cs.ClassID 
INNER JOIN Courses cr ON cr.CourseID = cs.CourseID 
INNER JOIN Professors pf ON pf.ProfessorID = cs.ProfessorID 
INNER JOIN Classrooms clr ON clr.RoomID = cs.RoomID 
INNER JOIN Weekdays wd ON wd.WeekdayID = cs.WeekdayID 
INNER JOIN Studys st ON st.StudyID = cs.StudyID 





