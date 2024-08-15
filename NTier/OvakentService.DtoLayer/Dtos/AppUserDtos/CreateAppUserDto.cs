namespace OvakentService.DtoLayer.Dtos.AppUserDtos
{
	public class CreateAppUserDto
	{
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Tittle { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string? Dahili { get; set; }
        public string? CepTel { get; set; }
        public string? KisaKod { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}