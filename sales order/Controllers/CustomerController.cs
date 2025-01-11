using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sales_order.DTOs;
using sales_order.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sales_order.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        // Inject ICustomerService via constructor
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> getCustomer([FromQuery] CustomerDto filter)
        {
            try
            {
                var result = await _customerService.GetAllCustomersAsync(filter);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

