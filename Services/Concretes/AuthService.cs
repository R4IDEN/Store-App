
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
    }
}
