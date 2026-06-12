using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Resortmanager;


[Table("customers")]
public class Customer {
    //| customer_id | customer_name  | phone      | email           | address  |

    [Column("customer_id")]
    public int CustId {get; set;}

    [Column("customer_name")]
    public string CustName {get; set;}

    public string phone {get; set;}

    public string email {get; set;}

    public string address {get; set;}
}
