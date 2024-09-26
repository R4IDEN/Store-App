using BigStoreApp.Models;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BigStoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            return View(new LoginModel()
            {
                ReturnURL = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel _model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(_model.UserName);
                if (user is not null)
                {
                    //Oturum acma islemi
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, _model.Password, false, false)).Succeeded)
                    {
                        return Redirect(_model?.ReturnURL);
                    }
                }

                ModelState.AddModelError("Error", "Invalid username or password");
            }
            return View();
        }

        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDTO model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if (roleResult.Succeeded) 
                    return RedirectToAction("Login", new {ReturnUrl = "/"});
            }
            else
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }
            return View();
        }

        public IActionResult AccessDenied([FromQuery(Name ="ReturnUrl")] string returnURL)
        {
            return View();
        }
    }
}
