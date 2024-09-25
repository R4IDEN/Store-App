using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager _services;
        private readonly IMapper _mapper;

        public UserController(IServiceManager services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var userList = _services.AuthService.Users;
            //_mapper.Map<IEnumerable<UserDTOForInsertion>>(userList)
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


        public async Task<IActionResult> Update([FromRoute(Name ="id")] string id)
        {
            return View(await _services.AuthService.SelectUserForUpdate(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDTOForUpdate userDTO)
        {
            if(ModelState.IsValid) 
            {
                await _services.AuthService.Update(userDTO);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
