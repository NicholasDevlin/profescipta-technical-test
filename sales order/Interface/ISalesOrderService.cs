using System;
using sales_order.DTOs;

namespace sales_order.Interface
{
	public interface ISalesOrderService
	{
        Task<BasePaginationResult<SalesOrderDto>> GetAllSalesOrder(SalesOrderFilterDto dto);
        Task<BaseResponse> SaveSO(SalesOrderDto dto);
        Task<SalesOrderDto> GetSalesOrderById(long id);
        Task<BaseResponse> DeleteSO(long id);
    }
}

