-- ������� ���� �����������
SELECT * FROM Employees;


-- ������� ����������� � �� ���� 10000
SELECT * FROM Employees WHERE Salary > 10000;


-- �������� ����������� ������ 70 ���
DELETE FROM Employees WHERE BirthDate <= DATEADD(YEAR, -70, GETDATE())
-- SELECT * FROM Employees WHERE BirthDate <= DATEADD(YEAR, -70, GETDATE());


-- ���������� �� �� 15000 ���, � ���� ��� ������
UPDATE Employees
SET Salary = 15000
WHERE Salary < 15000;

-- SELECT * FROM Employees WHERE Salary < 15000;