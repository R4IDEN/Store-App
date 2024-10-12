using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BigStoreApp.Components
{
    public class OrderInProgressViewComponent : ViewComponent
    {
        private readonly IServiceManager _services;

        public OrderInProgressViewComponent(IServiceManager services)
        {
            _services = services;
        }

        public string Invoke()
        {
            return _services.OrderService.NumberOfInProcess.ToString();
        }
    }
}
