-- Criação do Banco de Dados
CREATE DATABASE IF NOT EXISTS SubscriberDB;
USE SubscriberDB;

-- Criação da Tabela de Assinantes
CREATE TABLE IF NOT EXISTS Subscribers (
    Id CHAR(36) NOT NULL,                 -- UUID/Guid
    FullName VARCHAR(150) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    SubscriptionDate DATETIME NOT NULL,
    Plan INT NOT NULL,                    -- Enum: 1-Basic, 2-Standard, 3-Premium
    MonthlyValue DECIMAL(18, 2) NOT NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (Id),
    UNIQUE INDEX IX_Subscribers_Email (Email) -- Regra: E-mail único
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;



INSERT INTO Subscribers (Id, FullName, Email, SubscriptionDate, Plan, MonthlyValue, IsActive)
VALUES 
(UUID(), 'Alice Hamilton', 'alice@gmail.com', '2025-01-10 10:00:00', 3, 99.90, 1),
(UUID(), 'Bob Watson', 'bob@gmail.com', NOW(), 1, 29.90, 1),
(UUID(), 'Charlie Inactive', 'charlie@gmail.com', '2024-05-20 08:00:00', 2, 49.90, 0);