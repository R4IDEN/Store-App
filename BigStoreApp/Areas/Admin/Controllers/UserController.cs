using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Data;

namespace BigStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            if (!ModelState.IsValid)
                return View(userDTO); // dogrulama basarisizsa View'e don
            
            var result = await _services.AuthService.CreateUser(userDTO);

            if (!result.Succeeded)
            {
                // Hatalari ModelState'e ekle
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                userDTO.Roles = new HashSet<string>(_services.AuthService.Roles.Select(r => r.Name).ToList());
                return View(userDTO); // Hatali mesajlari gostermek icin View'e don
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update([FromRoute(Name = "id")] string id)
        {
            return View(await _services.AuthService.SelectUserForUpdate(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDTOForUpdate userDTO)
        {
            if (ModelState.IsValid)
            {
                await _services.AuthService.Update(userDTO);
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> ResetPassword([FromRoute(Name = "id")] string id)
        {
            return View(new ResetPasswordDTO()
            {
                UserName = id
            });
        }

        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDTO resetPasswordDTO)
        {
            IdentityResult res = await _services.AuthService.ResetPassword(resetPasswordDTO);
            return res.Succeeded
                ? RedirectToAction("Index")
                : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser([FromForm] UserDTO userDTO)
        {
            var res = await _services
                .AuthService
                .DeleteUser(userDTO.UserName);

            return res.Succeeded
                ? RedirectToAction("Index")
                : View();
        }
    }
}
