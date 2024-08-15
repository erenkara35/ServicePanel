namespace OvakentService.EntityLayer.Concrete
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}