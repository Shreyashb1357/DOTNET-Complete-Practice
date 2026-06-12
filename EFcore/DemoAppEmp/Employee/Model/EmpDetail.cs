namespace DemoApp.Employee.Model;

public readonly record struct EmpDetail(decimal Empno, string Name, DateOnly Hiredate, decimal Sal, decimal comm);
