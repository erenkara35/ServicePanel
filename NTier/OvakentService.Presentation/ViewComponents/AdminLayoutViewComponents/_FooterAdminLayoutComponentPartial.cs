using Microsoft.AspNetCore.Mvc;

namespace OvakentService.Presentation.ViewComponents.AdminLayoutViewComponents
{
    public class _FooterAdminLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}