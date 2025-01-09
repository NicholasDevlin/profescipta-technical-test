using System;
using sales_order.DTOs;

namespace sales_order.Service
{
	public interface ICustomerService
	{
        Task<BasePaginationResult<CustomerDto>> GetAllCustomersAsync(CustomerDto dto);
    }
}

