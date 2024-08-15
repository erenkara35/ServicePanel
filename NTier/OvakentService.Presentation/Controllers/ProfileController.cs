using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OvakentService.DtoLayer.Dtos.AppUserDtos;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK,User")]
    public class ProfileController : Controller
    {
        //Complete

        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ResultAppUserDto resultAppUserDto = new()
            {
                Surname = values.Surname,
                Name = values.Name,
                CepTel = values.CepTel,
                Company = values.Company,
                Dahili = values.Dahili,
                Department = values.Department,
                Email = values.Email,
                KisaKod = values.KisaKod,
                Tittle = values.Tittle,
                Username = values.UserName
            };
            ViewBag.userInfo = values.Name + " " + values.Surname;
            return View(resultAppUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateAppUserDto updateAppUserDto)
        {
            if (updateAppUserDto.Password == updateAppUserDto.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                user.Surname = updateAppUserDto.Surname;
                user.Name = updateAppUserDto.Name;
                user.CepTel = updateAppUserDto.CepTel;
                user.Company = updateAppUserDto.Company;
                user.Dahili = updateAppUserDto.Dahili;
                user.Department = updateAppUserDto.Department;
                user.Email = updateAppUserDto.Email;
                user.KisaKod = updateAppUserDto.KisaKod;
                user.Tittle = updateAppUserDto.Tittle;
                user.UserName = updateAppUserDto.Username;
                user.EmailConfirmed = true;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updateAppUserDto.Password);
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }
    }
}