namespace OvakentService.DtoLayer.Dtos.AppRoleDtos
{
    public class UpdateAppRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}