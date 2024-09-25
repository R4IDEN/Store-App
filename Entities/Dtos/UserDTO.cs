using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserDTO
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Username is mandatory.")]
        public string? UserName { get; init; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is mandatory.")]
        public string? Email { get; init; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone is mandatory.")]
        public string? PhoneNumber { get; init; }

        public HashSet<String> Roles { get; set; } = new HashSet<string>();
    }
}
