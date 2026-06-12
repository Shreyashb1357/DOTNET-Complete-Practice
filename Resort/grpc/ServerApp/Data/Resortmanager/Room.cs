using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Resortmanager;

public enum RoomStatus {Available , Occupied , Maintenance};

[Table("rooms")]
public class Room {

    [Column("room_id")]
    public int rId  {get; set;}

    [Column("room_number")]
    public int rNo {get; set;}

    [Column("room_type")]
    public string RoomType {get; set;}

    [Column("price_per_night")]
    public double Price {get; set;}

    // [Column("status")]
    public string  status{get; set;}

    [Column("department_id")]
    public int departmentId { get; set; }

    [ForeignKey(nameof(departmentId))]
    public Department Department { get; set; }

}