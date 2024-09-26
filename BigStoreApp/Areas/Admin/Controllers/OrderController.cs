using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private readonly IServiceManager _service;

        public OrderController(IServiceManager service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var orders = _service.OrderService.Orders;
            return View(orders);
        }

        [HttpPost]
        public IActionResult Complete([FromForm] int orderId)
        {
            _service.OrderService.Complete(orderId);
            return RedirectToAction("Index");
        }
    }
}
