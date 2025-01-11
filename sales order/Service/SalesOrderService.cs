using System;
using Microsoft.EntityFrameworkCore;
using sales_order.DTOs;
using sales_order.Interface;
using sales_order.Models;

namespace sales_order.Service
{
	public class SalesOrderService : ISalesOrderService
    {
        private readonly ApplicationDbContext _context;
        public SalesOrderService(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<BasePaginationResult<SalesOrderDto>> GetAllSalesOrder(SalesOrderFilterDto dto)
        {
            int currentPage = dto.currentPage ?? 1;
            int pageSize = dto.pageSize ?? 10;

            var query = _context.SO_ORDER.Include(x => x.Customer).AsQueryable();

            // filter
            if (!String.IsNullOrEmpty(dto.Keyword)) query = query.Where(x => x.ADDRESS.Contains(dto.Keyword) || x.ORDER_NO.Contains(dto.Keyword));
            if (dto.OrderDate != null) query = query.Where(x => x.ORDER_DATE == dto.OrderDate);

            int totalItems = query.Count();
            var datas = dto.takeAll == true ? await query.Select(x => new SalesOrderDto
            {
                Id = x.SO_ORDER_ID,
                OrderDate = x.ORDER_DATE,
                OrderNo = x.ORDER_NO,
                CustomerName = x.Customer.CUSTOMER_NAME,
                CustomerId = x.COM_CUSTOMER_ID,
                Address = x.ADDRESS
            }).ToListAsync()
            :
            await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(x => new SalesOrderDto
            {
                Id = x.SO_ORDER_ID,
                OrderDate = x.ORDER_DATE,
                OrderNo = x.ORDER_NO,
                CustomerName = x.Customer.CUSTOMER_NAME,
                CustomerId = x.COM_CUSTOMER_ID,
                Address = x.ADDRESS
            }).ToListAsync();

            var result = new BasePaginationResult<SalesOrderDto>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalItems,
                Data = datas
            };
            return result;
        }

        public async Task<BaseResponse> SaveSO(SalesOrderDto dto)
        {
            var result = new BaseResponse { Success = false, Message = "Server error"};
            
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var isNewData = dto.Id == null || dto.Id == 0;

                    if (dto.CustomerId == null) throw new Exception("customer is not selected");
                    var customer = _context.COM_CUSTOMER.FirstOrDefault(x => x.COM_CUSTOMER_ID == dto.CustomerId);
                    if (customer == null) throw new Exception("Can't find customer");

                    var salesOrder = !isNewData ? _context.SO_ORDER.First(x => x.SO_ORDER_ID == dto.Id) : new SO_ORDER();

                    salesOrder.ORDER_NO = dto.OrderNo;
                    salesOrder.ORDER_DATE = dto.OrderDate ?? DateTime.Now;
                    salesOrder.COM_CUSTOMER_ID = (int)dto.CustomerId;
                    salesOrder.ADDRESS = dto.Address;

                    if(isNewData) _context.SO_ORDER.Add(salesOrder);
                    _context.SaveChanges();

                    if (dto.Items != null && dto.Items.Any())
                    {
                        foreach (var itemDto in dto.Items)
                        {
                            itemDto.SOId = salesOrder.SO_ORDER_ID;
                            if (itemDto.Delete == true)
                            {
                                if (itemDto.Id != 0 && itemDto.Id != null)
                                {
                                    var item = _context.SO_ITEM.FirstOrDefault(x => x.SO_ITEM_ID == itemDto.Id);
                                    if (item != null) _context.SO_ITEM.Remove(item);
                                }
                                continue;
                            }

                            // validation
                            if (String.IsNullOrEmpty(itemDto.ItemName)) throw new Exception("Item name is empty!");
                            if (itemDto.Qty == null || itemDto.Qty == 0) throw new Exception("Invalid SO Item Qty value!");
                            if (itemDto.Price == null || itemDto.Price == 0) throw new Exception("Invalid SO Item Price value!");

                            isNewData = itemDto.Id == null || itemDto.Id == 0;
                            var soItem = !isNewData ? _context.SO_ITEM.First(x => x.SO_ITEM_ID == itemDto.Id) : new SO_ITEM();
                            soItem.SO_ORDER_ID = (long)itemDto.SOId;
                            soItem.ITEM_NAME = itemDto.ItemName;
                            soItem.QUANTITY = (int)itemDto.Qty;
                            soItem.PRICE = (double)itemDto.Price;
                            if (isNewData) _context.SO_ITEM.Add(soItem);
                        }

                        _context.SaveChanges(); 
                    }

                    transaction.Commit();

                    result.Success = true;
                    result.Message = "Sales Order saved successfully";
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public async Task<SalesOrderDto> GetSalesOrderById(long id)
        {
            var data = _context.SO_ORDER.Include(x => x.Customer).Include(x => x.SoItems).FirstOrDefault(x => x.SO_ORDER_ID == id);
            if (data != null)
            {
                return new SalesOrderDto
                {
                    Id = data.SO_ORDER_ID,
                    OrderDate = data.ORDER_DATE,
                    OrderNo = data.ORDER_NO,
                    CustomerName = data.Customer.CUSTOMER_NAME,
                    CustomerId = data.COM_CUSTOMER_ID,
                    Address = data.ADDRESS,
                    Items = data.SoItems.Select(item =>  new SalesOrderItemDto
                    {
                        Id = item.SO_ITEM_ID,
                        SOId = item.SO_ORDER_ID,
                        Qty = item.QUANTITY,
                        Price = item.PRICE,
                        ItemName = item.ITEM_NAME
                    }).ToList()
                }; 
            }
            return new SalesOrderDto();
        }

        public async Task<BaseResponse> DeleteSO(long id)
        {
            var result = new BaseResponse { Success = false, Message = "Failed to delete SO"};
            try
            {
                var data = _context.SO_ORDER.FirstOrDefault(x => x.SO_ORDER_ID == id);
                if (data == null) throw new Exception("SO does not exist");
                _context.SO_ORDER.Remove(data);
                _context.SaveChanges();
                result.Message = "Delete success";
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}

