using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OvakentService.EntityLayer.Concrete;
using OvakentService.Presentation.ViewModels;

namespace OvakentService.Presentation.Controllers
{
    public class ConfirmMailController : Controller
    {

        //Complete

        private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["Mail"];
            ViewBag.v = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {

            //var user1 = await _userManager.FindByEmailAsync(ViewBag.v);
            var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);
            if (user.ConfirmCode == confirmMailViewModel.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}