using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Components
{
    public class UserSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _services;

        public UserSummaryViewComponent(IServiceManager services)
        {
            _services = services;
        }

        public string Invoke()
        {
            return _services.AuthService.Users.Count().ToString();
        }
    }
}
