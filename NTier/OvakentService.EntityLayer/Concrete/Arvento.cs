namespace OvakentService.EntityLayer.Concrete
{
    public class Arvento : BaseDto
    {
        public string ArventoCihazNo { get; set; }
        public string ArventoCihazGrup { get; set; }
        public string Imei { get; set; }
        public string Imsi { get; set; }
        public string YazVersiyon { get; set; }
        public string DonVersiyon { get; set; }
    }
}