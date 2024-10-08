﻿
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

            if(!result.Succeeded)
                throw new Exception("Something went wrong during Add User operation.");
            
            if(userDTO.Roles.Count > 0) 
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userDTO.Roles);
                if (roleResult.Succeeded)
                    throw new Exception("Something went wrong during add roles to user.");
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user is null)
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

        public Task<IdentityUser> SelectUser(string userName)
        {
            throw new NotImplementedException();
        }

        #region UPDATE

        public async Task<UserDTOForUpdate> SelectUserForUpdate(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new Exception("User not found for update process");

            var userDTO = _mapper.Map<UserDTOForUpdate>(user);

            userDTO.Roles = new HashSet<string>(Roles.Select(r=>r.Name).ToList());
            userDTO.UserRoles = new HashSet<string> (await _userManager.GetRolesAsync(user));
            return userDTO;
        }

        public async Task Update(UserDTOForUpdate userDTO)
        {
            var user = await SelectUser(userDTO.UserName);

            if(user is null)
            {
                throw new Exception("User not found");
            }

            user.PhoneNumber = userDTO.PhoneNumber;
            user.Email = userDTO.Email;
            var updateResult = await _userManager.UpdateAsync(user);

            if(userDTO.Roles.Any())
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, userDTO.Roles);
            }
            return;
        }
        #endregion
    }
}
