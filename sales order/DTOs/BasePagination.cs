using System;
namespace sales_order.DTOs
{
	public class BasePagination
	{
        public int? currentPage { get; set; }
        public int? pageSize { get; set; }
        public bool? takeAll { get; set; } = false;
    }
}

