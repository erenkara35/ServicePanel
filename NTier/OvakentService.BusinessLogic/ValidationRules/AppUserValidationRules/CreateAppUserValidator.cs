using FluentValidation;
using OvakentService.DtoLayer.Dtos.AppRoleDtos;
using OvakentService.DtoLayer.Dtos.AppUserDtos;

namespace OvakentService.BusinessLogic.ValidationRules.AppUserValidationRules
{
    public class CreateAppUserValidator : AbstractValidator<CreateAppUserDto>
    {
        public CreateAppUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez !");
            RuleFor(x => x.Name).MaximumLength(25).WithMessage("Lütfen en fazla 25 karakter giriniz !");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Lütfen en az 3 karakter giriniz !");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş geçilemez !");
            RuleFor(x => x.Surname).MaximumLength(25).WithMessage("Lütfen en fazla 25 karakter giriniz !");
            RuleFor(x => x.Surname).MinimumLength(3).WithMessage("Lütfen en az 3 karakter giriniz !");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez !");
            RuleFor(x => x.Username).MaximumLength(25).WithMessage("Lütfen en fazla 25 karakter giriniz !");
            RuleFor(x => x.Username).MinimumLength(3).WithMessage("Lütfen en az 3 karakter giriniz !");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Adı alanı boş geçilemez !");
            RuleFor(x => x.Email).MaximumLength(25).WithMessage("Lütfen en fazla 25 karakter giriniz !");
            RuleFor(x => x.Email).MinimumLength(3).WithMessage("Lütfen en az 3 karakter giriniz !");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz !");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez !");
            RuleFor(x => x.Password).MaximumLength(25).WithMessage("Lütfen en fazla 25 karakter giriniz !");
            RuleFor(x => x.Password).MinimumLength(3).WithMessage("Lütfen en az 3 karakter giriniz !");
            //Parola karmaşıklığı için kural yazılacak            

            RuleFor(x => x.ConfirmPassword).Equal(y => y.Password).WithMessage("Şifre eşleşmiyor !");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar alanı boş geçilemez !");
        }
    }
}