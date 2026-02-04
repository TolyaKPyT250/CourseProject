USE user25;
GO

-- 1. Очистка (чтобы не было конфликтов при повторном запуске)
DELETE FROM Schedules;
DELETE FROM Groups;
DELETE FROM Lecturers;
DELETE FROM Rooms;

-- 2. Наполняем справочники
INSERT INTO Groups (GroupName) VALUES 
(N'5ИС31п'), (N'4ИС22'), (N'3ВЕБ11'), (N'2ИС21'), (N'1ВЕБ23'), (N'4СИС10');

INSERT INTO Lecturers (FullName, Department) VALUES 
(N'Кокшаров С.В.', N'Информационные системы'),
(N'Иванов И.И.', N'Высшая математика'),
(N'Петрова А.А.', N'Иностранные языки'),
(N'Сидоров П.П.', N'Информационные системы'),
(N'Смирнова Е.М.', N'Физическое воспитание'),
(N'Кузнецов Д.А.', N'Сетевое администрирование'),
(N'Васильева О.Н.', N'Гуманитарные науки'),
(N'Федоров А.Б.', N'История и право');

INSERT INTO Rooms (RoomNumber) VALUES ('312'), ('405'), ('201/2'), ('101'), ('502'), (N'Спортзал'), (N'Библиотека');

-- 3. Заполняем расписание (20+ записей)
-- Используем INSERT INTO ... SELECT, чтобы база сама сопоставила имена и ID

-- ПОНЕДЕЛЬНИК
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Понедельник', 1, g.Id, l.Id, r.Id, N'Разработка БД' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'5ИС31п' AND l.FullName = N'Кокшаров С.В.' AND r.RoomNumber = '312';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Понедельник', 2, g.Id, l.Id, r.Id, N'Разработка БД' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'5ИС31п' AND l.FullName = N'Кокшаров С.В.' AND r.RoomNumber = '312';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Понедельник', 1, g.Id, l.Id, r.Id, N'Математика' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'1ВЕБ23' AND l.FullName = N'Иванов И.И.' AND r.RoomNumber = '405';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Понедельник', 2, g.Id, l.Id, r.Id, N'Математика' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'1ВЕБ23' AND l.FullName = N'Иванов И.И.' AND r.RoomNumber = '405';

-- ВТОРНИК
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Вторник', 1, g.Id, l.Id, r.Id, N'Английский язык' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4ИС22' AND l.FullName = N'Петрова А.А.' AND r.RoomNumber = '502';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Вторник', 2, g.Id, l.Id, r.Id, N'Английский язык' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4ИС22' AND l.FullName = N'Петрова А.А.' AND r.RoomNumber = '502';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Вторник', 3, g.Id, l.Id, r.Id, N'Дискретная математика' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4ИС22' AND l.FullName = N'Сидоров П.П.' AND r.RoomNumber = '201/2';

-- СРЕДА
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Среда', 1, g.Id, l.Id, r.Id, N'Архитектура ЭВМ' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'3ВЕБ11' AND l.FullName = N'Кузнецов Д.А.' AND r.RoomNumber = '101';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Среда', 2, g.Id, l.Id, r.Id, N'Архитектура ЭВМ' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'3ВЕБ11' AND l.FullName = N'Кузнецов Д.А.' AND r.RoomNumber = '101';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Среда', 3, g.Id, l.Id, r.Id, N'Физкультура' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'3ВЕБ11' AND l.FullName = N'Смирнова Е.М.' AND r.RoomNumber = N'Спортзал';

-- ЧЕТВЕРГ
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Четверг', 1, g.Id, l.Id, r.Id, N'История' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'2ИС21' AND l.FullName = N'Федоров А.Б.' AND r.RoomNumber = N'Библиотека';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Четверг', 2, g.Id, l.Id, r.Id, N'История' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'2ИС21' AND l.FullName = N'Федоров А.Б.' AND r.RoomNumber = N'Библиотека';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Четверг', 3, g.Id, l.Id, r.Id, N'Сети' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'2ИС21' AND l.FullName = N'Кузнецов Д.А.' AND r.RoomNumber = '101';

-- ПЯТНИЦА
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Пятница', 1, g.Id, l.Id, r.Id, N'Программирование' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4СИС10' AND l.FullName = N'Кокшаров С.В.' AND r.RoomNumber = '312';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Пятница', 2, g.Id, l.Id, r.Id, N'Программирование' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4СИС10' AND l.FullName = N'Кокшаров С.В.' AND r.RoomNumber = '312';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Пятница', 3, g.Id, l.Id, r.Id, N'Психология' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4СИС10' AND l.FullName = N'Васильева О.Н.' AND r.RoomNumber = '502';

-- ДОП ПАРЫ ДЛЯ ПЛОТНОСТИ
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Понедельник', 3, g.Id, l.Id, r.Id, N'ОС' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'5ИС31п' AND l.FullName = N'Кузнецов Д.А.' AND r.RoomNumber = '101';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Вторник', 4, g.Id, l.Id, r.Id, N'Физика' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4ИС22' AND l.FullName = N'Иванов И.И.' AND r.RoomNumber = '405';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Среда', 4, g.Id, l.Id, r.Id, N'Этика' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'3ВЕБ11' AND l.FullName = N'Васильева О.Н.' AND r.RoomNumber = N'Библиотека';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName)
SELECT N'Пятница', 4, g.Id, l.Id, r.Id, N'БЖД' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4СИС10' AND l.FullName = N'Смирнова Е.М.' AND r.RoomNumber = N'Спортзал';

GO