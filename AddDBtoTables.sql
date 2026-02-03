USE user25;
GO

-- 1. Добавляем группы
INSERT INTO Groups (GroupName) VALUES (N'5ИС31п'), (N'4ИС22'), (N'3ВЕБ11');

-- 2. Добавляем преподавателей
INSERT INTO Lecturers (FullName, Department) VALUES 
(N'Кокшаров С.В.', N'Информационные системы'),
(N'Иванов И.И.', N'Высшая математика'),
(N'Петрова А.А.', N'Иностранные языки');

-- 3. Добавляем аудитории
INSERT INTO Rooms (RoomNumber) VALUES ('312'), ('405'), ('201/2');

-- 5. Добавляем начальное расписание (для проверки вывода в DataGrid)
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
VALUES 
(N'Понедельник', 1, 1, 1, 1, N'Разработка БД'),
(N'Понедельник', 2, 1, 1, 1, N'Разработка БД'),
(N'Вторник', 1, 2, 2, 2, N'Математический анализ');