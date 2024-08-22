using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var model = _service.CategoryService.GetAllCategories(false);
            return View(model);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _service.CategoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
