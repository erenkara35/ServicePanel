using Microsoft.AspNetCore.Identity;

namespace OvakentService.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Tittle { get; set; }
        public string Company { get; set; }
        public string? Dahili { get; set; }
        public string? CepTel { get; set; }
        public string? KisaKod { get; set; }
        public List<Car> Cars { get; set; }
        public int? ConfirmCode { get; set; }
    }
}