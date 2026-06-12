namespace DemoApp.Company.Model;

public class Employee
{
    public decimal Empno { get; set; }
    public string Ename { get; set; }
    public string Ejob { get; set; }
    public decimal Mgr { get; set; }
    public DateOnly HireDate { get; set; }
    public decimal Sal { get; set; }
    public decimal Comm { get; set; }
    public List<Dept> DeptNo { get; set; }

}

