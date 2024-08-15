using Microsoft.AspNetCore.Mvc;

namespace OvakentService.Presentation.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}