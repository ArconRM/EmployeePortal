-- Выборка всех сотрудников
SELECT * FROM Employees;


-- Выборка сотрудников с зп выше 10000
SELECT * FROM Employees WHERE Salary > 10000;


-- Удаление сотрудников старше 70 лет
DELETE FROM Employees WHERE BirthDate <= DATEADD(YEAR, -70, GETDATE())
-- SELECT * FROM Employees WHERE BirthDate <= DATEADD(YEAR, -70, GETDATE());


-- Обновление зп до 15000 тем, у кого она меньше
UPDATE Employees
SET Salary = 15000
WHERE Salary < 15000;

-- SELECT * FROM Employees WHERE Salary < 15000;