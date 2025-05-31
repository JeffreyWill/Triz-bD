-- create_hr_database.sql
-- ������ ��� �������� ���� ������ HR_Database
-- ������������ � �������� ������ �� ���������� "���������� ���������� � ������ ��� ������"

-- �������� ���� ������
CREATE DATABASE HR_Database;
GO

-- ������������ �� ��������� ���� ������
USE HR_Database;
GO

-- �������� ������� ���������
CREATE TABLE Positions (
    Position_ID INT PRIMARY KEY IDENTITY(1,1),
    Position_Name VARCHAR(100) NOT NULL,
    Description VARCHAR(200)
);

-- �������� ������� ������
CREATE TABLE Departments (
    Department_ID INT PRIMARY KEY IDENTITY(1,1),
    Department_Name VARCHAR(100) NOT NULL
);

-- �������� ������� ����������
CREATE TABLE Employees (
    Employee_ID INT PRIMARY KEY IDENTITY(1,1),
    Full_Name VARCHAR(100) NOT NULL,
    Birth_Date DATE,
    Gender CHAR(1) CHECK (Gender IN ('M', 'F')),
    Address VARCHAR(200),
    Phone VARCHAR(20),
    Position_ID INT,
    Department_ID INT,
    FOREIGN KEY (Position_ID) REFERENCES Positions(Position_ID),
    FOREIGN KEY (Department_ID) REFERENCES Departments(Department_ID)
);

-- �������� ������� �������� ��������
CREATE TABLE Employment_Contracts (
    Contract_ID INT PRIMARY KEY IDENTITY(1,1),
    Employee_ID INT,
    Start_Date DATE NOT NULL,
    End_Date DATE,
    Salary DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employees(Employee_ID)
);

-- �������� ������� ��������
CREATE TABLE Salaries (
    Salary_ID INT PRIMARY KEY IDENTITY(1,1),
    Employee_ID INT,
    Amount DECIMAL(10,2) NOT NULL,
    Payment_Date DATE NOT NULL,
    Payment_Type VARCHAR(50),
    FOREIGN KEY (Employee_ID) REFERENCES Employees(Employee_ID)
);

-- �������� ������� �������
CREATE TABLE Vacations (
    Vacation_ID INT PRIMARY KEY IDENTITY(1,1),
    Employee_ID INT,
    Start_Date DATE NOT NULL,
    End_Date DATE NOT NULL,
    Vacation_Type VARCHAR(50),
    FOREIGN KEY (Employee_ID) REFERENCES Employees(Employee_ID)
);
GO

-- ������� �������� ������
INSERT INTO Positions (Position_Name, Description) VALUES 
('��������', '���������� ���������'),
('�����������', '���������� ������������ �����������'),
('���������', '������� ����������� �����');

INSERT INTO Departments (Department_Name) VALUES 
('����� ������'),
('IT-�����'),
('���������� �����');

INSERT INTO Employees (Full_Name, Birth_Date, Gender, Address, Phone, Position_ID, Department_ID) VALUES 
('������ ���� ��������', '1990-05-15', 'M', '��. ������ 10, ������', '123-456-7890', 1, 1),
('������� ���� ���������', '1985-03-22', 'F', '��. ���� 5, ������', '987-654-3210', 2, 2),
('������� ���� ����������', '1992-07-10', 'M', '��. ��������� 3, ������', '555-123-4567', 3, 3);

INSERT INTO Employment_Contracts (Employee_ID, Start_Date, End_Date, Salary) VALUES 
(1, '2025-01-01', NULL, 50000.00),
(2, '2025-02-01', '2026-02-01', 60000.00),
(3, '2025-03-01', NULL, 45000.00);

INSERT INTO Salaries (Employee_ID, Amount, Payment_Date, Payment_Type) VALUES 
(1, 50000.00, '2025-05-01', '��������'),
(2, 60000.00, '2025-05-01', '��������'),
(3, 45000.00, '2025-05-01', '��������');

INSERT INTO Vacations (Employee_ID, Start_Date, End_Date, Vacation_Type) VALUES 
(1, '2025-06-01', '2025-06-15', '���������'),
(2, '2025-07-01', '2025-07-10', '���������'),
(3, '2025-08-01', '2025-08-07', '��������������');
GO

-- �������� �������������
-- ������������� 1: View_EmployeeInfo
CREATE VIEW View_EmployeeInfo AS
SELECT 
    e.Employee_ID,
    e.Full_Name,
    p.Position_Name,
    d.Department_Name
FROM Employees e
JOIN Positions p ON e.Position_ID = p.Position_ID
JOIN Departments d ON e.Department_ID = d.Department_ID;
GO

-- ������������� 2: View_ActiveContracts
CREATE VIEW View_ActiveContracts AS
SELECT 
    ec.Contract_ID,
    e.Full_Name,
    ec.Start_Date,
    ec.End_Date,
    ec.Salary
FROM Employment_Contracts ec
JOIN Employees e ON ec.Employee_ID = e.Employee_ID
WHERE ec.End_Date IS NULL OR ec.End_Date > GETDATE();
GO

-- �������� ���������
-- ������� 1: trg_AfterInsertEmployee
CREATE TRIGGER trg_AfterInsertEmployee
ON Employees
AFTER INSERT
AS
BEGIN
    PRINT '�������� ����� ���������.';
END;
GO

-- ������� 2: trg_CheckSalary
CREATE TRIGGER trg_CheckSalary
ON Employment_Contracts
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE Salary < 20000)
    BEGIN
        RAISERROR ('�������� �� ����� ���� ���� 20000.', 16, 1);
        ROLLBACK;
    END
END;
GO

-- �������� �������� ��������
-- ��������� 1: sp_AddEmployee
CREATE PROCEDURE sp_AddEmployee
    @Full_Name VARCHAR(100),
    @Birth_Date DATE,
    @Gender CHAR(1),
    @Address VARCHAR(200),
    @Phone VARCHAR(20),
    @Position_ID INT,
    @Department_ID INT
AS
BEGIN
    INSERT INTO Employees (Full_Name, Birth_Date, Gender, Address, Phone, Position_ID, Department_ID)
    VALUES (@Full_Name, @Birth_Date, @Gender, @Address, @Phone, @Position_ID, @Department_ID);
END;
GO

-- ��������� 2: sp_GetEmployeeByID
CREATE PROCEDURE sp_GetEmployeeByID
    @Employee_ID INT
AS
BEGIN
    SELECT * FROM Employees WHERE Employee_ID = @Employee_ID;
END;
GO