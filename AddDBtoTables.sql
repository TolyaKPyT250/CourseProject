USE user25;
GO

-- 1. Добавляем группы
INSERT INTO Groups (GroupName) VALUES ('5ИС31п'), ('4ИС22'), ('3ВЕБ11');

-- 2. Добавляем преподавателей
INSERT INTO Lecturers (FullName, Department) VALUES 
('Кокшаров С.В.', 'Информационные системы'),
('Иванов И.И.', 'Высшая математика'),
('Петрова А.А.', 'Иностранные языки');

-- 3. Добавляем аудитории
INSERT INTO Rooms (RoomNumber) VALUES ('312'), ('405'), ('201/2');

-- 5. Добавляем начальное расписание (для проверки вывода в DataGrid)
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
VALUES 
('Понедельник', 1, 1, 1, 1, 'Разработка БД'),
('Понедельник', 2, 1, 1, 1, 'Разработка БД'),
('Вторник', 1, 2, 2, 2, 'Математический анализ');