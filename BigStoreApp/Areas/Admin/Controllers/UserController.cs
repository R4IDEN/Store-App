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
    }
}
