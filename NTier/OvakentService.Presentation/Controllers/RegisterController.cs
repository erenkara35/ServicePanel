using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OvakentService.DtoLayer.Dtos.AppUserDtos;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.Presentation.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        //Complete

        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateAppUserDto createAppUserDto)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new();
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
                    MailboxAddress mailboxAdressFrom = new MailboxAddress("Servis Portalı", "mail@email.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);

                    mimeMessage.From.Add(mailboxAdressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);
                    var bodyBuilder = new BodyBuilder();
                    //bodyBuilder.TextBody = $"Sayın {appUser.Name + " " + appUser.Surname} Kayıt işleminiz gerçekleştirebilmek için onay kodunuz:{appUser.ConfirmCode}. Onaylamak için lütfen tıklayınız https://servis.ovakent.com.tr/MailConfirm/Index";
                    bodyBuilder.HtmlBody = $@"
                        <p>Sayın {appUser.Name + " " + appUser.Surname},</p>
                        <p>Kayıt işleminizi gerçekleştirebilmek için onay kodunuz: {appUser.ConfirmCode}.</p>
                        <p>Onaylamak için lütfen <a href='https://localhost:4050/ConfirmMail/Index/'><b>buraya tıklayınız</b></a>.
                    ";
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    mimeMessage.Subject = "Ovakent Servis - Onay Kodu";

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("mail.email.com", "Password12!");
                    smtpClient.Send(mimeMessage);
                    smtpClient.Disconnect(true);
                    smtpClient.Dispose();
                    TempData["Mail"] = createAppUserDto.Email;

                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("", item.Description);
                }
            }
            return View();

        }
    }
}