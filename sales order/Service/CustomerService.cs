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

        public async Task<BasePaginationResult<CustomerDto>> GetAllCustomersAsync(CustomerDto dto)
        {
            int currentPage = dto.currentPage ?? 1;
            int pageSize = dto.pageSize ?? 10;
            
            var query = _context.COM_CUSTOMER.AsQueryable();
            int totalItems = query.Count();

            // filter
            if (!String.IsNullOrEmpty(dto.Name)) query = query.Where(x => x.CUSTOMER_NAME == dto.Name);

            var datas = dto.takeAll == true ? await query.Select(x => new CustomerDto
            {
                Id = x.COM_CUSTOMER_ID,
                Name = x.CUSTOMER_NAME
            }).ToListAsync()
            :
            await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(x => new CustomerDto
            {
                Id = x.COM_CUSTOMER_ID,
                Name = x.CUSTOMER_NAME
            }).ToListAsync();

            var result = new BasePaginationResult<CustomerDto>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalItems,
                Data = datas
            };
            return result;
        }
    }
}

