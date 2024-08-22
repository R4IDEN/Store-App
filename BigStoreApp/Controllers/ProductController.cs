using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace BigStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            var products = _serviceManager.ProductService.GetAllProducts(false).ToList();
            return View(products);
        }

        public IActionResult Get(int id)
        {
            var product = _serviceManager.ProductService.GetProductWithId(id, false);
            return View(product);
        }
    }
}

