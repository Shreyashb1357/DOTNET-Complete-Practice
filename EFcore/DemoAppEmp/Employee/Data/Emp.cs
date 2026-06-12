namespace DemoApp.Employee.Data;
using System.ComponentModel.DataAnnotations;

public class Emp
{
    [Key]
    public decimal Empno {get; set;}
    public string Ename {get; set;}
    public DateOnly Hiredate {get; set;}
    public decimal Sal {get; set;}
    public decimal comm {get; set;}
}