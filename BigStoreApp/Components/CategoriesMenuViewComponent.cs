using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Components
{
    public class CategoriesMenuViewComponent : ViewComponent
    {
        private readonly IServiceManager _services;

        public CategoriesMenuViewComponent(IServiceManager services)
        {
            _services = services;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _services.CategoryService.GetAllCategories(false);
            return View(categories);
        }
    }
}
