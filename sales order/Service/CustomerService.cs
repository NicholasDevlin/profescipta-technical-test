using System;
using Microsoft.EntityFrameworkCore;
using sales_order.DTOs;

namespace sales_order.Service
{
    public class CustomerService : ICustomerService
    {
		private readonly ApplicationDbContext _context;
		public CustomerService(ApplicationDbContext context)
		{
			_context = context;
		}

        public Task<BasePaginationResult<CustomerDto>> GetAllCustomersAsync(CustomerDto dto)
        {
            var datas = new List<CustomerDto>();
            datas.Add(new CustomerDto() { Id = 1, Name = "udin" });
            var result = new BasePaginationResult<CustomerDto>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalItems = datas.Count,
                Data = datas
            };
            return Task.FromResult(result);
        }
    }
}

