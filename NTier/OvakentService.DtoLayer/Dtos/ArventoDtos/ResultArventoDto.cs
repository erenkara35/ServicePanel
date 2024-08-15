﻿namespace OvakentService.DtoLayer.Dtos.ArventoDtos
{
    public class ResultArventoDto
    {
        public int Id { get; set; }
        public string ArventoCihazNo { get; set; }
        public string ArventoCihazGrup { get; set; }
        public string Imei { get; set; }
        public string Imsi { get; set; }
        public string YazVersiyon { get; set; }
        public string DonVersiyon { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}