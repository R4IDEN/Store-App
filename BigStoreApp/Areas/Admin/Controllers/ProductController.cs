using BigStoreApp.Models;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Concretes;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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

        public IActionResult Index([FromQuery]ProductRequestParameters p)
        {
            ViewData["Title"] = "Products";
            var products = _service.ProductService.GetAllProductsWithDetails(p).ToList();

            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItem = _service.ProductService.GetAllProducts(false).Count()
            };

            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
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
                TempData["success"] = $"{productDTOInsertion.Name} has been created at {DateTime.Now.ToShortDateString()}";
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
                ViewData["Title"] = model.Name;
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
                TempData["info"] = $"Product updated success. ({DateTime.Now.ToShortDateString()})";
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
                TempData["info"] = "Product deleted successfully.";
                return View("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ToggleShowcase([FromForm]int id)
        {
            _service.ProductService.ToggleShowcase(id);
            return RedirectToAction("Index");
        }
    }
}
