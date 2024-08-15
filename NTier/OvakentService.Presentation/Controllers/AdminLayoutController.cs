using Microsoft.AspNetCore.Mvc;

namespace OvakentService.Presentation.Controllers
{
    [NonController]
    public class _AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}