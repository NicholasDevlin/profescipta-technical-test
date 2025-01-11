using System;
namespace sales_order.DTOs
{
	public class SalesOrderDto
    {
        public long? Id { get; set; }
        public string? OrderNo { get; set; }
        public string? Address { get; set; }
        public long? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }
        public List<SalesOrderItemDto> Items { get; set; }
    }

    public class SalesOrderFilterDto : BasePagination
    {
        public string? Keyword { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}

