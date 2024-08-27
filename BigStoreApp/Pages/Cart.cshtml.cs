using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace BigStoreApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _services;
        public Cart _Cart { get; set; } //IOC
        
        public CartModel(IServiceManager services, Cart cart)
        {
            _services = services;
            _Cart = cart;
        }

        public string ReturnURL { get; set; } = "/";

        //Biz kullaniciya ne donecegiz
        public void OnGet(string returnURL)
        {
            ReturnURL = returnURL ?? "/";
        }

        public IActionResult OnPost(int productId, String returnURL)
        {
            var product = _services.ProductService.GetProductWithId(productId, false);

            if (product is not null)
                _Cart.AddItem(product, 1);
            return Page();
        }
        public IActionResult OnPostRemove(int id, String returnURL)
        {
            _Cart.RemoveLine(_Cart.Lines.First(cl => cl.Product.ProductId.Equals(id)).Product);
            return Page();
        }
    }
}
