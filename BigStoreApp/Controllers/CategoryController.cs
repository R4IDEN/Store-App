using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace BigStoreApp.Controllers
{
    public class CategoryController : Controller
    {
        private IRepositoryManager _repositoryManager;

        public CategoryController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public IActionResult Index()
        {
            var model = _repositoryManager.Category.FindAll(false);
            return View(model);
        }
    }
}
