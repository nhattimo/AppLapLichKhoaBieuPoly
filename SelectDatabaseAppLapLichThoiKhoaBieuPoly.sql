
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



SELECT clr.RoomName, 
       wd.NameWeekday, 
       st.NameStudy,
       c.ClassName, 
       cr.CourseName, 
       pf.ProfessorName 
FROM ClassSchedules cs 
full JOIN Classes c ON c.ClassID = cs.ClassID 
full JOIN Courses cr ON cr.CourseID = cs.CourseID 
full JOIN Professors pf ON pf.ProfessorID = cs.ProfessorID 
full JOIN Classrooms clr ON clr.RoomID = cs.RoomID 
full JOIN Weekdays wd ON wd.WeekdayID = cs.WeekdayID 
full JOIN Studys st ON st.StudyID = cs.StudyID
where wd.WeekdayID = 1
order by st.StudyID

SELECT clr.RoomName, 
       wd.NameWeekday, 
       st.NameStudy,
       c.ClassName, 
       cr.CourseName, 
       pf.ProfessorName 
FROM ClassSchedules cs 
full JOIN Classes c ON c.ClassID = cs.ClassID 
full JOIN Courses cr ON cr.CourseID = cs.CourseID 
full JOIN Professors pf ON pf.ProfessorID = cs.ProfessorID 
full JOIN Classrooms clr ON clr.RoomID = cs.RoomID 
full JOIN Weekdays wd ON wd.WeekdayID = cs.WeekdayID 
full JOIN Studys st ON st.StudyID = cs.StudyID
order by clr.RoomName




SELECT cs.WeekdayID, st.NameStudy, cr.RoomName, cls.ClassName, co.CourseName, p.ProfessorName
FROM ClassSchedules cs
JOIN Classrooms cr ON cs.RoomID = cr.RoomID
JOIN Classes cls ON cs.ClassID = cls.ClassID
JOIN Courses co ON cs.CourseID = co.CourseID
JOIN Professors p ON cs.ProfessorID = p.ProfessorID
JOIN Studys st on cs.StudyID = st.StudyID
WHERE cs.WeekdayID = 1
ORDER BY cs.StudyID, cr.RoomName;



