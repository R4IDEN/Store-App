using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Collections.Generic;

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

        public async Task<IActionResult> Index() 
        {
            var rolesWithCount = await _services.AuthService.GetRolesWithUserCountsAsync();
            return View(rolesWithCount);
        }

        // Rol ekleme islemi
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _services.AuthService.CreateRoleAsync(roleName);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            TempData["SuccessMessage"] = $"{roleName} Role added successfully.";
            return RedirectToAction("Index");
        }

        // Rol silme islemi
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var result = await _services.AuthService.DeleteRoleAsync(roleId);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = string.Join(", ", result.Errors.Select(e => e.Description));
            }
            else
            {
                TempData["SuccessMessage"] = "Role deleted successfully.";
            }

            return RedirectToAction("Index");
        }
    }
}
