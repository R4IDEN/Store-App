using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Components
{
    public class ShowcaseViewComponent : ViewComponent
    {
        private readonly IServiceManager _service;

        public ShowcaseViewComponent(IServiceManager service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            var products = _service.ProductService.GetShowcaseProducts(false);
            return View(products);
        }
    }
}
