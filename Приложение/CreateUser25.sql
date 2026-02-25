CREATE DATABASE user25;
GO
USE user25;
GO
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Login NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL
);

-- Добавим тестового пользователя (пароль в учебных целях храним открыто)
INSERT INTO Users (Login, Password) VALUES ('admin', '12345');