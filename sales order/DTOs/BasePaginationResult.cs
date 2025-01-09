using System;
namespace sales_order.DTOs
{
    public class BasePaginationResult<T>
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public int? TotalItems { get; set; }
        public List<T> Data { get; set; }
    }

}

