namespace DemoApp.Employee.Model;
using DemoApp.Employee.Data;

public class SiteModel 
{
    public IEnumerable<EmpDetail> GetEmp()
    {
        using var site = new EmpDbContext();
        var selection = from a in site.Emp
            where a.Ename.Length > 1
            select new EmpDetail
            {
                Empno = a.Empno ,
                Name = a.Ename,
                Hiredate = a.Hiredate,
                Sal = a.Sal,
                comm = a.comm
            };
        return selection.ToList();
    }

    public void AddEmp(decimal empNo, string empName , DateOnly hireDate, decimal empSal, decimal empComm)
    {
        using var site = new EmpDbContext();
        var Employee = site.Emp.Find(empNo);
        if (Employee is null)
        {
            Employee = new Emp {
                Empno = empNo,
                Ename = empName,
                Hiredate = hireDate,
                Sal = empSal,
                comm = empComm,

            };
            site.Emp.Add(Employee);
        }
        site.SaveChanges();

    }

    public void RemoveEmp(decimal empNo)
    {
        using var site = new EmpDbContext();
        var Employee = site.Emp.Find(empNo);
        site.Emp.Remove(Employee);
        site.SaveChanges();
    }
      
}