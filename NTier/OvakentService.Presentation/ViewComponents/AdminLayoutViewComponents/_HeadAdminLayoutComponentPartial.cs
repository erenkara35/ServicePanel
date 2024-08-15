using Microsoft.AspNetCore.Mvc;

namespace OvakentService.Presentation.ViewComponents.AdminLayoutViewComponents
{
    public class _HeadAdminLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}