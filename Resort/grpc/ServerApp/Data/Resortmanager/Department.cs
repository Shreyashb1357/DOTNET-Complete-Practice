using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Resortmanager;

[Table("departments")]
public class Department {

    [Column("department_id")]
    public int departmentId {get; set;}

    [Column("department_name")]
    public string DName {get; set;}

    public string description {get; set;}

}