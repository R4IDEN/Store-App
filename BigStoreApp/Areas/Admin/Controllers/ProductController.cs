using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _service;

        public ProductController(IServiceManager service)
        {
            _service = service;
        }

        private SelectList GetCategories(int? selectedCatId)
        {
            if(selectedCatId != 0)
                return new SelectList(_service.CategoryService.GetAllCategories(false), "CategoryId", "Name", selectedCatId);

            return new SelectList(_service.CategoryService.GetAllCategories(false), "CategoryId",
                "Name",
                "1");
        }

        public IActionResult Index()
        {
            var model = _service.ProductService.GetAllProducts(false);
            return View(model);
        }


        //CREATE
        public IActionResult CreateProduct() 
        {
            ViewBag.Categories = GetCategories(0);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([FromForm]ProductDTOInsertion productDTOInsertion, IFormFile file) 
        {
            if (ModelState.IsValid)
            {
                //file operation
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "_photos", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                productDTOInsertion.ImageURL = string.Concat("_photos/", file.FileName);
                _service.ProductService.CreateProduct(productDTOInsertion);
                return RedirectToAction("Index");
            }
            return View();
        }


        //UPDATE
        public IActionResult UpdateProduct([FromRoute(Name ="Id")]int id)
        {
            var model = _service.ProductService.GetProductWithIdForUpdate(id, false);

            if(ModelState.IsValid && model != null) 
            {
                ViewBag.Categories = GetCategories(model.CategoryId);
                //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", model.ImageURL);
                return View(model);
            }
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct([FromForm]ProductDTOUpdate productDTOUpdate, IFormFile file) 
        {
            if (ModelState.IsValid)
            {
                //file operation
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "_photos", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                productDTOUpdate.ImageURL = string.Concat("_photos/", file.FileName);
                _service.ProductService.UpdateProduct(productDTOUpdate);
                return RedirectToAction("Index");
            }
            return View();
        }


        //DELETE
        public IActionResult DeleteProduct([FromRoute(Name = "Id")] int id)
        {
            if(!_service.ProductService.DeleteProduct(id))
            {
                ViewBag.ErrorMessage = "Product not found, try again.";
                return View("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
