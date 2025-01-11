using System;
using System.ComponentModel.DataAnnotations;

namespace sales_order.Models
{
	public class SO_ORDER
	{
        [Key]
        public long SO_ORDER_ID { get; set; }
        public string ORDER_NO { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public Int32 COM_CUSTOMER_ID { get; set; }
        public string ADDRESS { get; set; }

        public COM_CUSTOMER Customer { get; set; }

        public ICollection<SO_ITEM> SoItems { get; set; }
    }
}

