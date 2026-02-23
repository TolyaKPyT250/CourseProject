USE user25;
GO

-- 1. Очистка
DELETE FROM Schedules;
DELETE FROM Groups;
DELETE FROM Lecturers;
DELETE FROM Rooms;

-- 2. Справочники
INSERT INTO Groups (GroupName) VALUES (N'5ИС31п'), (N'4ИС22'), (N'3ВЕБ11'), (N'2ИС21'), (N'1ВЕБ23'), (N'4СИС10');
INSERT INTO Lecturers (FullName, Department) VALUES 
(N'Кокшаров С.В.', N'Информационные системы'), (N'Иванов И.И.', N'Высшая математика'),
(N'Петрова А.А.', N'Иностранные языки'), (N'Сидоров П.П.', N'Информационные системы'),
(N'Смирнова Е.М.', N'Физическое воспитание'), (N'Кузнецов Д.А.', N'Сетевое администрирование'),
(N'Васильева О.Н.', N'Гуманитарные науки'), (N'Федоров А.Б.', N'История и право');
INSERT INTO Rooms (RoomNumber) VALUES ('312'), ('405'), ('201/2'), ('101'), ('502'), (N'Спортзал'), (N'Библиотека');

-- 3. Расписание с датами
-- Понедельник (2026-02-23)
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Понедельник', 1, g.Id, l.Id, r.Id, N'Разработка БД', '2026-02-23' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'5ИС31п' AND l.FullName = N'Кокшаров С.В.' AND r.RoomNumber = '312';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Понедельник', 2, g.Id, l.Id, r.Id, N'Разработка БД', '2026-02-23' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'5ИС31п' AND l.FullName = N'Кокшаров С.В.' AND r.RoomNumber = '312';

-- Вторник (2026-02-24)
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Вторник', 1, g.Id, l.Id, r.Id, N'Английский язык', '2026-02-24' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4ИС22' AND l.FullName = N'Петрова А.А.' AND r.RoomNumber = '502';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Вторник', 2, g.Id, l.Id, r.Id, N'Английский язык', '2026-02-24' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4ИС22' AND l.FullName = N'Петрова А.А.' AND r.RoomNumber = '502';

-- Среда (2026-02-25)
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Среда', 1, g.Id, l.Id, r.Id, N'Архитектура ЭВМ', '2026-02-25' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'3ВЕБ11' AND l.FullName = N'Кузнецов Д.А.' AND r.RoomNumber = '101';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Среда', 2, g.Id, l.Id, r.Id, N'Архитектура ЭВМ', '2026-02-25' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'3ВЕБ11' AND l.FullName = N'Кузнецов Д.А.' AND r.RoomNumber = '101';

-- Четверг (2026-02-26)
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Четверг', 1, g.Id, l.Id, r.Id, N'История', '2026-02-26' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'2ИС21' AND l.FullName = N'Федоров А.Б.' AND r.RoomNumber = N'Библиотека';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Четверг', 2, g.Id, l.Id, r.Id, N'Сети', '2026-02-26' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'2ИС21' AND l.FullName = N'Кузнецов Д.А.' AND r.RoomNumber = '101';

-- Пятница (2026-02-27)
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Пятница', 1, g.Id, l.Id, r.Id, N'Программирование', '2026-02-27' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4СИС10' AND l.FullName = N'Кокшаров С.В.' AND r.RoomNumber = '312';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Пятница', 2, g.Id, l.Id, r.Id, N'Психология', '2026-02-27' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4СИС10' AND l.FullName = N'Васильева О.Н.' AND r.RoomNumber = '502';

-- Еще несколько пар для полноты
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Понедельник', 3, g.Id, l.Id, r.Id, N'Математика', '2026-02-23' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'1ВЕБ23' AND l.FullName = N'Иванов И.И.' AND r.RoomNumber = '405';
INSERT INTO Schedules (DayOfWeek, LessonNumber, GroupId, LecturerId, RoomId, SubjectName, LessonDate)
SELECT N'Вторник', 3, g.Id, l.Id, r.Id, N'Дискретная математика', '2026-02-24' FROM Groups g, Lecturers l, Rooms r WHERE g.GroupName = N'4ИС22' AND l.FullName = N'Сидоров П.П.' AND r.RoomNumber = '201/2';