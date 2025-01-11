using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sales_order.Models;
using sales_order.DTOs;
using sales_order.Service;
using sales_order.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sales_order.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ISalesOrderService _salesOrderService;

        // Inject ICustomerService via constructor
        public SalesOrderController(ICustomerService customerService, ISalesOrderService salesOrderService)
        {
            _customerService = customerService;
            _salesOrderService = salesOrderService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> getSalesOrder([FromQuery]SalesOrderFilterDto filter)
        {
            try
            {
                var result = await _salesOrderService.GetAllSalesOrder(filter);
                return Json(new { totalCount = result.TotalItems, items = result.Data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveSO([FromBody] SalesOrderDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var result = await _salesOrderService.SaveSO(dto);

            if (result.Success)
                return Ok(new { message = result.Message });

            return BadRequest(result.Message);
        }

        public async Task<IActionResult> Editor(int? id)
        {
            ViewBag.Customers = await _customerService.GetAllCustomersAsync(new CustomerDto());
            if (id.HasValue)
            {
                var salesOrder = await _salesOrderService.GetSalesOrderById(id.Value);
                if (salesOrder == null)
                    return NotFound();

                return View(salesOrder);
            }
            else
            {
                var newSalesOrder = new SalesOrderDto();
                return View(newSalesOrder);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSO(long id)
        {
            if (id == 0)
                return BadRequest("Invalid data.");

            var result = await _salesOrderService.DeleteSO(id);

            if (result.Success)
                return Ok(new { message = result.Message });

            return BadRequest(result.Message);
        }
    }
}

