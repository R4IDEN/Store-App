using Microsoft.AspNetCore.Mvc;

namespace BigStoreApp.Components
{
    public class ProductFilterMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
