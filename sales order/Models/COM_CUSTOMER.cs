using System.ComponentModel.DataAnnotations;
using sales_order.Models;

public class COM_CUSTOMER
{
    [Key]
    public Int32 COM_CUSTOMER_ID { get; set; }
    public string CUSTOMER_NAME { get; set; }

    public ICollection<SO_ORDER> SalesOrders { get; set; }
}