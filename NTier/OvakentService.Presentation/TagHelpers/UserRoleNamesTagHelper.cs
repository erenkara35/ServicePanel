using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OvakentService.EntityLayer.Concrete;
using System.Text;

namespace OvakentService.Presentation.TagHelpers
{
    public class UserRoleNamesTagHelper : TagHelper
    {
        public string UserId { get; set; }
        private readonly UserManager<AppUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var stringBuilder = new StringBuilder();
            userRoles.ToList().ForEach(x =>
            {
                stringBuilder.Append(@$"<span class='badge badge-soft-success'>{x.ToUpper()}</span>");


            });

            output.Content.SetHtmlContent(stringBuilder.ToString());
            //return base.ProcessAsync(context, output);
        }
    }
}