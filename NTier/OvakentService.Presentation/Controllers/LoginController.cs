using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OvakentService.EntityLayer.Concrete;
using OvakentService.Presentation.ViewModels;

namespace OvakentService.Presentation.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        //Complete

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            var role = await _userManager.GetRolesAsync(user);
            
            if (user == null)
            {
                TempData["errorMessage"] = "Geçersiz giriş denemesi.";
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginViewModel.Password, true, true);
            if (result.Succeeded)
            {
                //En az 1 role sahip olmayan ve Email Confirm olmayan giriş yapamaz.
                if (user.EmailConfirmed && role != null)
                {                    
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    TempData["errorMessage"] = "Lütfen mail adresinizi onaylayın.";
                    await _signInManager.SignOutAsync();
                    return View(loginViewModel);
                }
            }

            if (result.IsLockedOut)
            {
                TempData["errorMessage"] = "Hesabınız kilitlendi. Lütfen daha sonra tekrar deneyin.";
            }
            else
            {
                TempData["errorMessage"] = "Geçersiz giriş denemesi.";
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

    }
}