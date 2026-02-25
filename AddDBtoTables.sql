USE user25;
GO

-- 1. Очистка
DELETE FROM Schedules;
DELETE FROM Groups;
DELETE FROM Lecturers;
DELETE FROM Rooms;

-- 2. Справочники
INSERT INTO Groups (GroupName) VALUES (N'ИТ2024'), (N'ИТ2025'), (N'ПРЕП2024'), (N'ПРЕП2025'), (N'МОБ2024'), (N'МОБ2025'), (N'СВАР2024'), (N'СВАР2025'), (N'МЕХ2024'), (N'МЕХ2025'), (N'ПОВ2024'), (N'ПОВ2025') ;
INSERT INTO Lecturers (FullName, Department) VALUES 
(N'Кокшаров С.В.', N'Информационные системы(ПК:ИТ);BackEnd Разработка(ПК:ИТ)'), (N'Иванов И.И.', N'Высшая математика(ОБЩ:Другое);Проф математика(ОБЩ:ИТ)'),
(N'Петрова А.А.', N'Иностранные языки(ОБЩ:Другое);Иностранные языки по спец(ОБЩ:ИТ)'), (N'Сидоров П.П.', N'Веб Разработка(ПК:ИТ);FrontEnd Разработка(ПК:ИТ)'),
(N'Смирнова Е.М.', N'Физическое воспитание(СПОРТ:Другое);Физкультура(СПОРТ:Другое)'), (N'Кузнецов Д.А.', N'Сетевое администрирование(ПК:ИТ)'),
(N'Васильева О.Н.', N'Гуманитарные науки(ОБЩ:Другое)'), (N'Федоров А.Б.', N'История и право(БИБЛ:Другое)');
INSERT INTO Rooms (RoomNumber) VALUES ('312'), ('315'), ('405'), ('201/2'), ('101'), ('502'), (N'Спортзал'), (N'Библиотека');

--Делаем теги для кабинетов
UPDATE Rooms SET Description = N'ОБЩ' WHERE Description IS NULL;

UPDATE Rooms SET Description = N'СПОРТ' WHERE RoomNumber = N'Спортзал';

UPDATE Rooms SET Description = N'БИБЛ' WHERE RoomNumber = N'Библиотека';

UPDATE Rooms SET Description = N'ПК' WHERE RoomNumber = N'312';
UPDATE Rooms SET Description = N'ПК' WHERE RoomNumber = N'315';

--Делаем тэги для групп
UPDATE Groups SET Description = N'ИТ' WHERE GroupName LIKE N'ИТ%' OR GroupName LIKE N'МОБ%';
UPDATE Groups SET Description = N'Другое' WHERE Description IS NULL;