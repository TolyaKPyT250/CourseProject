USE user25;
GO

-- 1. Преподаватели
CREATE TABLE Lecturers (
    Id INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100)
);

-- 2. Группы
CREATE TABLE Groups (
    Id INT PRIMARY KEY IDENTITY,
    GroupName NVARCHAR(20) NOT NULL UNIQUE
);

-- 3. Аудитории
CREATE TABLE Rooms (
    Id INT PRIMARY KEY IDENTITY,
    RoomNumber NVARCHAR(10) NOT NULL,
	Description NVARCHAR(255)
);

-- 4. Расписание (Связующая таблица)
CREATE TABLE Schedules (
    Id INT PRIMARY KEY IDENTITY,
    DayOfWeek NVARCHAR(20) NOT NULL, -- Понедельник, Вторник...
    LessonNumber INT NOT NULL,       -- 1, 2, 3 пара
    GroupId INT FOREIGN KEY REFERENCES Groups(Id),
    LecturerId INT FOREIGN KEY REFERENCES Lecturers(Id),
    RoomId INT FOREIGN KEY REFERENCES Rooms(Id),
    SubjectName NVARCHAR(100) NOT NULL,
    LessonDate DATE NOT NULL
);