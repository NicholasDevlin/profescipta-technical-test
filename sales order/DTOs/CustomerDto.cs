using System;
namespace sales_order.DTOs
{
	public class CustomerDto : BasePagination
	{
        public long? Id { get; set; }
		public string? Name { get; set; }
	}
}

