namespace OvakentService.EntityLayer.Concrete
{
    public class Car : BaseDto
    {
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string CarModel { get; set; }
        public string ModelYear { get; set; }
        public string ImageUrl { get; set; }
        public string HGSNo { get; set; }
        public DateTime MuayeneTarihi { get; set; }
        public string CarType1 { get; set; }
        public string CarType2 { get; set; }
        public string CarClass { get; set; }
        public string Location { get; set; }
        public string Logo { get; set; }
        public string Weight { get; set; }
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }
        public Arvento Arvento { get; set; }
        public int? ArventoId { get; set; }
    }
}