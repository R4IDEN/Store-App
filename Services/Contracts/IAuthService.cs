
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }
        IEnumerable<IdentityUser> Users { get; }
        Task<IdentityResult> CreateUser(UserDTOForInsertion userDTO);
        Task<IdentityUser> SelectUser(string userName);
        Task<UserDTOForUpdate> SelectUserForUpdate(string userName);
        Task Update(UserDTOForUpdate userDTO);
        Task<IdentityResult> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<IdentityResult> DeleteUser(string userName);

        //Role Islemleri
        Task<List<RoleDTO>> GetRolesWithUserCountsAsync();
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(string roleId);
    }
}
