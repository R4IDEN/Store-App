using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserDTOForInsertion : UserDTO
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is mandatory")]
        public string? Password { get; init; }
    }
}
