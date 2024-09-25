using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager _services;

        public UserController(IServiceManager services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            var userList = _services.AuthService.Users;
            return View(userList);
        }

        public IActionResult Create()
        {
            return View(new UserDTOForInsertion()
            {
                Roles = new HashSet<string>(_services.AuthService.Roles.Select(r => r.Name).ToList())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDTOForInsertion userDTO)
        {
            var result = await _services.AuthService.CreateUser(userDTO);

            return result.Succeeded
                ? RedirectToAction("Index")
                : View();
        }
    }
}
