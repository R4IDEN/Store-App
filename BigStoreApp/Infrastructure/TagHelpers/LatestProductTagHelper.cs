using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace BigStoreApp.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "products")]
    public class LatestProductTagHelper : TagHelper
    {
        private readonly IServiceManager _service;
        
        [HtmlAttributeName("number")]
        public int productNumber { get; set; }

        public LatestProductTagHelper(IServiceManager service)
        {
            _service = service;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Ana div 
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "my-3 latest-products-container");
            div.Attributes.Add("style", "padding: 20px; background-color: #f0f4f8; border-radius: 10px; box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);");

            // Baslik kismi
            TagBuilder h6 = new TagBuilder("h6");
            h6.Attributes.Add("class", "lead");
            h6.Attributes.Add("style", "margin: 0; font-size: 18px; font-weight: bold; color: #333; display: flex; align-items: center;");

            TagBuilder icon = new TagBuilder("i");
            icon.Attributes.Add("class", "fa fa-box text-secondary");
            icon.Attributes.Add("style", "margin-right: 8px; color: #6c757d;");

            h6.InnerHtml.AppendHtml(icon);
            h6.InnerHtml.AppendHtml(" Latest Products");

            // Urun listesi
            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes.Add("style", "list-style-type: none; padding: 0; margin-top: 10px;");

            var prods = _service.ProductService.GetLatestProducts(productNumber, false);

            foreach (Product prd in prods)
            {
                TagBuilder li = new TagBuilder("li");
                li.Attributes.Add("style", "padding: 8px 0; border-bottom: 1px solid #e0e0e0;"); // Alt çizgi ve boşluklar

                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href", $"/product/get/{prd.ProductId}");
                a.Attributes.Add("style", "text-decoration: none; color: #007bff; font-size: 16px;");
                a.InnerHtml.AppendHtml(prd.Name);

                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }

            div.InnerHtml.AppendHtml(h6);
            div.InnerHtml.AppendHtml(ul);
            output.Content.AppendHtml(div);
        }
    }
}
