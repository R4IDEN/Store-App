using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IServiceManager _services;

        public RoleController(IServiceManager services)
        {
            _services = services;
        }

        public ActionResult Index() 
        {
            return View(_services.AuthService.Roles);
        }
    }
}
