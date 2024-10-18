
using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

        public IEnumerable<IdentityUser> Users => _userManager.Users.ToList();

        public async Task<IdentityResult> CreateUser(UserDTOForInsertion userDTO)
        {
            var user = _mapper.Map<IdentityUser>(userDTO);
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
                return IdentityResult.Failed(result.Errors.ToArray());

            if (userDTO.Roles.Count > 0)
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userDTO.Roles);
                if (!roleResult.Succeeded)
                    return IdentityResult.Failed(roleResult.Errors.ToArray());
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new Exception("User is not found for deleting process.");

            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByNameAsync(resetPasswordDTO.UserName);
            if (user is null)
                throw new Exception("User is not found for Reset Password process.");

            await _userManager.RemovePasswordAsync(user);
            IdentityResult res = await _userManager.AddPasswordAsync(user, resetPasswordDTO.Password);
            return res;
        }

        public async Task<IdentityUser> SelectUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }


        #region UPDATE

        public async Task<UserDTOForUpdate> SelectUserForUpdate(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new Exception("User not found for update process");

            var userDTO = _mapper.Map<UserDTOForUpdate>(user);

            userDTO.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList());
            userDTO.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
            return userDTO;
        }

        public async Task Update(UserDTOForUpdate userDTO)
        {
            var user = await SelectUser(userDTO.UserName);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            user.PhoneNumber = userDTO.PhoneNumber;
            user.Email = userDTO.Email;
            var updateResult = await _userManager.UpdateAsync(user);

            if (userDTO.Roles.Any() && updateResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, userDTO.Roles);
            }
            return;
        }
        #endregion

        // Roller ve kullanıcı sayıları döndüren metod
        public async Task<List<RoleDTO>> GetRolesWithUserCountsAsync()
        {
            var rolesWithCounts = new List<RoleDTO>();

            // Tum rolleri al
            var roles = _roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                // Her rol icin kullanicilari al ve say
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

                // RoleDTO olustur ve listeye ekle
                var roleDto = new RoleDTO
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    NumberOfuser = usersInRole.Count
                };

                rolesWithCounts.Add(roleDto);
            }

            return rolesWithCounts;
        }

        // Rol ekleme islemi
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return IdentityResult.Failed(new IdentityError { Description = "Role name must be provided." });

            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        // Rol silme islemi
        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return IdentityResult.Failed(new IdentityError { Description = $"Role with ID '{roleId}' not found." });

            // Role sahip kullanicilari bul
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in usersInRole)
            {
                // her bir kullanicidan rolu kaldir
                var removeResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!removeResult.Succeeded)
                {
                    return removeResult; // Hata varsa islemi durdur ve hata dondur
                }
            }

            // Rolu sil
            var deleteResult = await _roleManager.DeleteAsync(role);
            return deleteResult;
        }
    }
}
