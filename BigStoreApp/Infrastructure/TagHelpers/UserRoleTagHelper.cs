using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BigStoreApp.Infrastructure.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class UserRoleTagHelper : TagHelper
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        [HtmlAttributeName("user-name")]
        public string? UserName { get; set; }
        public UserRoleTagHelper(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user is null)
                throw new Exception("User not found. (user/index)");

            TagBuilder ul = new TagBuilder("ul");

            var roles = _roleManager.Roles.ToList().Select(r => r.Name);
            if (!roles.Any())
                throw new Exception("Role not found. (user/index)");

            foreach (var role in roles)
            {
                if (await _userManager.IsInRoleAsync(user, role))
                {
                    TagBuilder li = new TagBuilder("li");
                    ul.InnerHtml.AppendHtml($" <span style='color: green;'>{role}</span> |");
                }
                else
                {
                    TagBuilder li = new TagBuilder("li");
                    ul.InnerHtml.AppendHtml($" <span style='color: red;'>{role}</span> |");
                }
            }

            output.Content.AppendHtml(ul);
        }
    }
}
