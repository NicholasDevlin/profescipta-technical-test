using System;
using System.ComponentModel.DataAnnotations;

namespace sales_order.Models
{
	public class SO_ITEM
    {
        [Key]
        public long SO_ITEM_ID { get; set; }
        public long SO_ORDER_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public int QUANTITY { get; set; }
        public Double PRICE { get; set; }

        public SO_ORDER SalesOrder { get; set; }
    }
}

