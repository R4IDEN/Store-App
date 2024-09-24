using System.ComponentModel.DataAnnotations;

namespace BigStoreApp.Models
{
    public class LoginModel
    {
        private string? _returnURL;

        public string ReturnURL
        {
            get
            {
                if (_returnURL is null)
                    return "/";
                else
                    return _returnURL;
            }
            set
            {
                _returnURL = value;
            }
        }

        [Required(ErrorMessage ="Name is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
