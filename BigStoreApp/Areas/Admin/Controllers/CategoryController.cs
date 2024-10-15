using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="Admin")]
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
                TempData["success"] = $"{category.Name} has been created at {DateTime.Now.ToShortDateString()}";
                return RedirectToAction("Index");
            }
            return View();
        }

        //DELETE
        public IActionResult DeleteCategory([FromRoute(Name = "Id")] int id)
        {
            if(_service.CategoryService.DeleteCategory(id)) 
            {
                TempData["info"] = $"Category (id : {id}) deleted successfully.";
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Category delete process failed, try again.";
            return View("Index");

        }
    }
}
