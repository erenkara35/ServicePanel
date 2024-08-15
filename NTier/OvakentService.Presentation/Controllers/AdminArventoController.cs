using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvakentService.BusinessLogic.Abstract;
using OvakentService.DtoLayer.Dtos.ArventoDtos;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK")]
    public class AdminArventoController : Controller
    {
        private readonly IArventoService _arventoService;

        public AdminArventoController(IArventoService arventoService)
        {
            _arventoService = arventoService;
        }

        public IActionResult Index()
        {
            var values = _arventoService.TGetAll().Select(x => new ResultArventoDto()
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
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateArvento()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateArvento(CreateArventoDto createArventoDto)
        {
            Arvento arvento = new()
            {
                ArventoCihazGrup = createArventoDto.ArventoCihazGrup,
                ArventoCihazNo = createArventoDto.ArventoCihazNo,
                DonVersiyon = createArventoDto.DonVersiyon,
                Imei = createArventoDto.Imei,
                Imsi = createArventoDto.Imsi,
                IsActive = createArventoDto.IsActive,
                RegisterDate = DateTime.Now,
                YazVersiyon = createArventoDto.YazVersiyon
            };
            _arventoService.TInsert(arvento);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "AdminArvento");
            }

            return View();
        }

        public IActionResult RemoveArvento(int id)
        {
            _arventoService.TDelete(_arventoService.TGetById(id));
            return RedirectToAction("Index", "AdminArvento");
        }

        [HttpGet]
        public IActionResult UpdateArvento(int id)
        {
            var value = _arventoService.TGetById(id);
            UpdateArventoDto updateArventoDto = new()
            {
                ArventoCihazGrup = value.ArventoCihazGrup,
                Imei = value.Imei,
                Imsi = value.Imsi,
                IsActive = value.IsActive,
                RegisterDate = value.RegisterDate,
                ArventoCihazNo = value.ArventoCihazNo,
                DonVersiyon = value.DonVersiyon,
                Id = value.Id,
                YazVersiyon = value.YazVersiyon
            };

            return View(updateArventoDto);
        }

        [HttpPost]
        public IActionResult UpdateArvento(UpdateArventoDto updateArventoDto)
        {
            Arvento arvento = new()
            {
                YazVersiyon = updateArventoDto.YazVersiyon,
                Id = updateArventoDto.Id,
                DonVersiyon = updateArventoDto.DonVersiyon,
                ArventoCihazNo = updateArventoDto.ArventoCihazNo,
                RegisterDate = updateArventoDto.RegisterDate,
                ArventoCihazGrup = updateArventoDto.ArventoCihazGrup,
                Imei = updateArventoDto.Imei,
                Imsi = updateArventoDto.Imsi,
                IsActive = updateArventoDto.IsActive,
                UpdateDate = DateTime.Now
            };
            _arventoService.TUpdate(arvento);
            return View();
        }
    }
}