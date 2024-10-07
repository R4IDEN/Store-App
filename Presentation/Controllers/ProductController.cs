using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _services;

        public ProductController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_services.ProductService.GetAllProducts(false));
        }

    }
}
