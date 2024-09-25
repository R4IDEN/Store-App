
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
    }
}
