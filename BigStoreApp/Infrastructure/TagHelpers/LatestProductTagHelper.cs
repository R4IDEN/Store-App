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
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "my-3");

            TagBuilder h6 = new TagBuilder("h6");
            h6.Attributes.Add("class", "lead");

            TagBuilder icon = new TagBuilder("i");
            icon.Attributes.Add("class", "fa fa-box text-secondary");

            h6.InnerHtml.AppendHtml(icon);
            h6.InnerHtml.AppendHtml(" Latest Products");

            TagBuilder ul = new TagBuilder("ul");
            var prods = _service.ProductService.GetLatestProducts(productNumber, false);

            foreach(Product prd in prods)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href", $"/product/get/{prd.ProductId}");
                a.Attributes.Add("style", "text-decoration: none; color: inherit;");
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
