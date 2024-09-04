using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceManager _services;
        private readonly Cart _cart;

        public OrderController(IServiceManager services, Cart cart)
        {
            _services = services;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout() => View(new Order());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout([FromForm] Order order) 
        {
            if (_cart.Lines.Count() == 0) 
                ModelState.AddModelError("","Sorry, cart is empty.");
            if(ModelState.IsValid) 
            {
                order.Lines = _cart.Lines.ToArray();
                _services.OrderService.SaveOrder(order);
                _cart.ClearCart();
                return RedirectToPage("/Complete", new {orderId = order.OrderId});
            }

            return View();
        }
    }
}
