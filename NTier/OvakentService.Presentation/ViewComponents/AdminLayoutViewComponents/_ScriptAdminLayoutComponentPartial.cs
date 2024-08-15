using Microsoft.AspNetCore.Mvc;

namespace OvakentService.Presentation.ViewComponents.AdminLayoutViewComponents
{
    public class _ScriptAdminLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}