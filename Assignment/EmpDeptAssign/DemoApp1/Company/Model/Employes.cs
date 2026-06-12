namespace DemoApp.Company.Model;

public readonly record struct Employes(decimal empno, string ename, string job, decimal mgr, DateOnly hiredate, decimal sal, decimal comm, decimal deptno);