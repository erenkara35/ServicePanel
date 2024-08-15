using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvakentService.BusinessLogic.Abstract;
using OvakentService.DtoLayer.Dtos.ArventoDtos;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK")]
    public class ArventoController : Controller
    {
        //Complete

        private readonly IArventoService _arventoService;

        //Complete

        public ArventoController(IArventoService arventoService)
        {
            _arventoService = arventoService;
        }

        public IActionResult Index()
        {
            var list = _arventoService.TGetAll();
            var resultList = list.Select(x => new ResultArventoDto()
            {
                ArventoCihazGrup = x.ArventoCihazGrup,
                ArventoCihazNo = x.ArventoCihazNo,
                DonVersiyon = x.DonVersiyon,
                Id = x.Id,
                Imei = x.Imei,
                Imsi = x.Imsi,
                IsActive = x.IsActive,
                RegisterDate = x.RegisterDate,
                UpdateDate = x.UpdateDate,
                YazVersiyon = x.YazVersiyon
            }).ToList();

            return View(resultList);
        }
    }
}