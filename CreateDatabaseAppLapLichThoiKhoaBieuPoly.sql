CREATE DATABASE AppThoiKhoaBieu
GO
USE AppThoiKhoaBieu
GO
-- Tạo Role
CREATE TABLE Role (
	ID INT PRIMARY KEY IDENTITY,
	NameRole NVARCHAR(30),
)
GO

-- Tạo Account
CREATE TABLE Account (
	ID INT PRIMARY KEY IDENTITY,
	NameAccount VARCHAR(30),
	Password VARCHAR(30),
	RoleID INT,
	FOREIGN KEY (RoleID) REFERENCES Role(ID)
)
GO

-- Tạo bảng cho các bộ môn
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY,
    DepartmentName NVARCHAR(100)
);
GO

-- Tạo bảng cho các lớp học
CREATE TABLE Classes (
    ClassID INT PRIMARY KEY IDENTITY,
    ClassName NVARCHAR(100),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);
GO
-- Tạo bảng cho các môn học
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY,
	CourseCode NVARCHAR(10),
    CourseName NVARCHAR(100),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);
GO
-- Tạo bảng liên kết giữa lớp và môn học mà lớp học có thể học
CREATE TABLE ClassesCourses (
    ClassID INT,
    CourseID INT,
    PRIMARY KEY (ClassID, CourseID),
    FOREIGN KEY (ClassID) REFERENCES Classes(ClassID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
GO


-- Tạo bảng cho các giảng viên .
CREATE TABLE Professors (
    ProfessorID INT PRIMARY KEY IDENTITY,
    ProfessorName NVARCHAR(100)
);
GO
-- Tạo bảng liên kết giữa giảng viên và môn học mà họ có thể dạy
CREATE TABLE ProfessorCourses (
    ProfessorID INT,
    CourseID INT,
    PRIMARY KEY (ProfessorID, CourseID),
    FOREIGN KEY (ProfessorID) REFERENCES Professors(ProfessorID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
GO
-- Tạo bảng cho các phòng học
CREATE TABLE Classrooms (
    RoomID INT PRIMARY KEY IDENTITY,
    RoomName NVARCHAR(100)
);
GO
-- Tạo bảng cho các ca học
CREATE TABLE Studys(
	StudyID INT PRIMARY KEY IDENTITY,
	NameStudy NVARCHAR(10),
	StartTime TIME,
    EndTime TIME,
);
GO
-- Tạo bảng cho các thứ trong tuần
CREATE TABLE Weekdays(
	WeekdayID INT PRIMARY KEY IDENTITY,
	NameWeekday NVARCHAR(10)
);
-- Tạo bảng cho các ca học trong tuần
CREATE TABLE ClassSchedules (
    ScheduleID INT PRIMARY KEY IDENTITY,
    ClassID INT,
    CourseID INT,
    ProfessorID INT,
    RoomID INT,
    WeekdayID INT, 
	StudyID Int,
    FOREIGN KEY (ClassID) REFERENCES Classes(ClassID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    FOREIGN KEY (ProfessorID) REFERENCES Professors(ProfessorID),
    FOREIGN KEY (RoomID) REFERENCES Classrooms(RoomID),
    FOREIGN KEY (WeekdayID) REFERENCES Weekdays(WeekdayID),
    FOREIGN KEY (StudyID) REFERENCES Studys(StudyID)
);
GO