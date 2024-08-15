using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OvakentService.DtoLayer.Dtos.AppUserDtos;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK,User")]
    public class PersonelController : Controller
    {
        //Completed

        private readonly UserManager<AppUser> _userManager;

        public PersonelController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Ovakent()
        {
            var userList = await _userManager.Users.Where(x => x.Company == "Ovakent").ToListAsync();
            var resultUserList = userList.Select(x => new ResultAppUserDto()
            {
                Id = x.Id,
                Name = x.Name,
                CepTel = x.CepTel,
                Company = x.Company,
                Dahili = x.Dahili,
                Department = x.Department,
                Email = x.Email,
                KisaKod = x.KisaKod,
                Surname = x.Surname,
                Tittle = x.Tittle
            }).ToList();
            return View(resultUserList);
        }

        [HttpGet]
        public async Task<IActionResult> Billas()
        {
            var userList = await _userManager.Users.Where(x => x.Company == "Billas").ToListAsync();
            var resultUserList = userList.Select(x => new ResultAppUserDto()
            {
                Id = x.Id,
                Name = x.Name,
                CepTel = x.CepTel,
                Company = x.Company,
                Dahili = x.Dahili,
                Department = x.Department,
                Email = x.Email,
                KisaKod = x.KisaKod,
                Surname = x.Surname,
                Tittle = x.Tittle
            }).ToList();
            return View(resultUserList);
        }
    }
}