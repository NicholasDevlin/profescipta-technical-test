using System;
namespace sales_order.DTOs
{
	public class SalesOrderItemDto
	{
        public long? Id { get; set; }
        public long? SOId { get; set; }
        public string? ItemName { get; set; }
        public int? Qty { get; set; }
        public Double? Price { get; set; }
        public bool? Delete { get; set; }
    }
}

