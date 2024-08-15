using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OvakentService.DtoLayer.Dtos.AppUserDtos;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK")]
    public class AdminUserController : Controller
    {
        //Completed

        private readonly UserManager<AppUser> _userManager;

        public AdminUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //Role bazlı eğer Ovakent Admin ise OrderByDesc Department ve OrderByDesc Company, Billas Admin ise OrderByDesc Department,OrderByAsc Company
            var values = _userManager.Users.ToList().OrderByDescending(y => y.Department).OrderByDescending(z => z.Company);
            var userList = values.Select(x => new ResultAppUserDto()
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
                Tittle = x.Tittle,
                Username = x.UserName
            }).ToList();
            return View(userList);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateAppUserDto createAppUserDto)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new();
                int code = rnd.Next(100000, 999999);
                AppUser appUser = new()
                {
                    UserName = createAppUserDto.Username,
                    Email = createAppUserDto.Email,
                    Name = createAppUserDto.Name,
                    Surname = createAppUserDto.Surname,
                    Company = createAppUserDto.Company,
                    Department = createAppUserDto.Department,
                    CepTel = createAppUserDto.CepTel,
                    KisaKod = createAppUserDto.KisaKod,
                    Dahili = createAppUserDto.Dahili,
                    Tittle = createAppUserDto.Tittle,
                    ConfirmCode = rnd.Next(100000, 999000)
                };
                var result = await _userManager.CreateAsync(appUser, createAppUserDto.Password);
                if (result.Succeeded)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAdressFrom = new MailboxAddress("Servis Portalı", "mail@email.com.tr");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);

                    mimeMessage.From.Add(mailboxAdressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $@"
                        <p>Sayın {appUser.Name + " " + appUser.Surname},</p>
                        <p>Kayıt işleminizi gerçekleştirebilmek için onay kodunuz: {appUser.ConfirmCode}.</p>
                        <p>Onaylamak için lütfen <a href='https://localhost:4050/ConfirmMail/Index/'><b>buraya tıklayınız</b></a>.
                    ";
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    mimeMessage.Subject = "Servis - Onay Kodu";

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("mail@email.com.tr", "Password12!");
                    smtpClient.Send(mimeMessage);
                    smtpClient.Disconnect(true);
                    smtpClient.Dispose();
                    return RedirectToAction("Index", "AdminUser");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var value = await _userManager.FindByIdAsync(id.ToString());
            if (value != null)
            {
                UpdateAppUserDto user = new()
                {
                    CepTel = value.CepTel,
                    Company = value.Company,
                    Dahili = value.Dahili,
                    Department = value.Department,
                    Email = value.Email!,
                    Id = value.Id,
                    KisaKod = value.KisaKod,
                    Name = value.Name,
                    Surname = value.Surname,
                    Tittle = value.Tittle,
                    Username = value.UserName!
                };
                if (ModelState.IsValid)
                {
                    return View(user);
                }
                else
                {
                    return View();
                }
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateAppUserDto updateAppUserDto)
        {
            if (updateAppUserDto.Password == updateAppUserDto.ConfirmPassword)
            {
                var user = await _userManager.FindByEmailAsync(updateAppUserDto.Email);

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
                    return RedirectToAction("Index", "AdminUser");
                }
            }
            return View();

        }

        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminUser");
                }
                return View();
            }
            return View();
        }
    }
}