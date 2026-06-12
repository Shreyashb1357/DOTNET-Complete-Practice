using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Resortmanager;

[Table("room_booking")]
public class RoomBooking {

    //| booking_id | customer_id | room_id | check_in   | check_out  |

    //  FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    //  FOREIGN KEY (room_id) REFERENCES rooms(room_id)
    
    [Column("booking_id")]
    public int BId {get; set;}

    [Column("customer_id")]
    public int CustId {get;set;}

    [ForeignKey(nameof(CustId))]
    public Customer Customer {get; set;}

    [Column("room_id")]
    public int rId {get;set;}

    [ForeignKey(nameof(rId))]
    public Room Room {get; set;}

    [Column("check_in")]
    public DateTime CheckIn { get; set; }

    [Column("check_out")]
    public DateTime CheckOut { get; set; }


    [Column("total_payment")]
    public double? totalPayment {get;set;}


}