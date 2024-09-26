
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class ResetPasswordDTO
    {
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password for confirm is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
