using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;
using StoreApp.Repositories;

namespace BigStoreApp.Components
{
    public class ProductSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _services;

        public ProductSummaryViewComponent(IServiceManager services)
        {
            _services = services;
        }

        public string Invoke()
        {
            return _services.ProductService.GetAllProducts(false).Count().ToString();
        }
    }
}
