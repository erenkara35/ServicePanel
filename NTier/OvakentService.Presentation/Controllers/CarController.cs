using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvakentService.BusinessLogic.Abstract;
using OvakentService.DtoLayer.Dtos.CarDtos;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK")]
    public class CarController : Controller
    {
        //Complete

        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Ovakent()
        {
            var values = _carService.TCarListWithUserAndArvento();
            var carList = values.Select(y => new ResultCarDto()
            {
                Brand = y.Brand,
                ArventoId = y.ArventoId,
                CarClass = y.CarClass,
                CarModel = y.CarModel,
                CarType1 = y.CarType1,
                CarType2 = y.CarType2,
                HGSNo = y.HGSNo,
                ImageUrl = y.ImageUrl,
                Location = y.Location,
                Logo = y.Logo,
                ModelYear = y.ModelYear,
                MuayeneTarihi = y.MuayeneTarihi,
                Plate = y.Plate,
                Username = y.AppUser.Name + " " + y.AppUser.Surname,
                Weight = y.Weight,
                Company = y.AppUser.Company,
                Id = y.Id

            }).Where(x => x.Company == "Ovakent").ToList();
            return View(carList);
        }

        [HttpGet]
        public IActionResult Billas()
        {
            var values = _carService.TCarListWithUserAndArvento();
            var carList = values.Select(y => new ResultCarDto()
            {
                Brand = y.Brand,
                ArventoId = y.ArventoId,
                CarClass = y.CarClass,
                CarModel = y.CarModel,
                CarType1 = y.CarType1,
                CarType2 = y.CarType2,
                HGSNo = y.HGSNo,
                ImageUrl = y.ImageUrl,
                Location = y.Location,
                Logo = y.Logo,
                ModelYear = y.ModelYear,
                MuayeneTarihi = y.MuayeneTarihi,
                Plate = y.Plate,
                Username = y.AppUser.Name + " " + y.AppUser.Surname,
                Weight = y.Weight,
                Company = y.AppUser.Company,
                Id = y.Id

            }).Where(x => x.Company == "Billas").ToList();
            return View(carList);
        }
    }
}