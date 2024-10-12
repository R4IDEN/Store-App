using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace BigStoreApp.Components
{
    public class CategorySummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _service;

        public CategorySummaryViewComponent(IServiceManager service)
        {
            _service = service;
        }

        public string Invoke()
        {
            return _service.CategoryService.GetAllCategories(false).Count().ToString();
        }
    }
}
