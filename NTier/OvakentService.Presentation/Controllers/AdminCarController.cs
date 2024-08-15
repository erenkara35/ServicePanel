using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OvakentService.BusinessLogic.Abstract;
using OvakentService.DtoLayer.Dtos.CarDtos;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK")]
    public class AdminCarController : Controller
    {
        //Completed
        private readonly ICarService _carService;
        private readonly IArventoService _arventoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminCarController(ICarService carService, IArventoService arventoService, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _arventoService = arventoService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var values = _carService.TCarListWithUserAndArvento();

            var cars = values.Select(x => new ResultCarDto()
            {
                Id = x.Id,
                ArventoId = x.ArventoId,
                Brand = x.Brand,
                CarClass = x.CarClass,
                CarModel = x.CarModel,
                CarType1 = x.CarType1,
                CarType2 = x.CarType2,
                Company = x.AppUser.Company,
                HGSNo = x.HGSNo,
                ImageUrl = x.ImageUrl,
                Location = x.Location,
                Logo = x.Logo,
                ModelYear = x.ModelYear,
                MuayeneTarihi = x.MuayeneTarihi,
                Plate = x.Plate,
                Username = x.AppUser.Name + " " + x.AppUser.Surname,
                Weight = x.Weight
            }).ToList();
            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            ViewBag.arventoList = _arventoService.TGetAll().Select(x => new SelectListItem()
            {
                Text = x.ArventoCihazNo,
                Value = x.Id.ToString()
            });
            ViewBag.userList = await _userManager.Users.Select(x => new SelectListItem()
            {
                Text = x.Name + " " + x.Surname,
                Value = x.Id.ToString()
            }).ToListAsync();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCar(CreateCarDto createCarDto, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                Car car = new()
                {
                    AppUserId = createCarDto.AppUserId,
                    Brand = createCarDto.Brand,
                    CarClass = createCarDto.CarClass,
                    CarModel = createCarDto.CarModel,
                    ArventoId = createCarDto.ArventoId,
                    CarType1 = createCarDto.CarType1,
                    CarType2 = createCarDto.CarType2,
                    HGSNo = createCarDto.HGSNo,
                    Location = createCarDto.Location,
                    Logo = createCarDto.Logo,
                    ModelYear = createCarDto.ModelYear,
                    MuayeneTarihi = createCarDto.MuayeneTarihi,
                    Plate = createCarDto.Plate,
                    RegisterDate = DateTime.Now,
                    IsActive = true,
                    Weight = createCarDto.Weight,

                };


                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string uniqueFileName = timestamp + "_" + file.FileName;

                    using (var fileStream = new FileStream(Path.Combine(imagePath, uniqueFileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    car.ImageUrl = $@"/img/" + uniqueFileName;
                }
                else
                {
                    car.ImageUrl = "/Images/car-1.jpg";
                }
                _carService.TInsert(car);
                return RedirectToAction("Index", "AdminCar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult RemoveCar(int id)
        {
            _carService.TDelete(_carService.TGetById(id));
            return RedirectToAction("Index", "AdminCar");
        }

        [HttpGet]
        public IActionResult UpdateCar(int id)
        {
            ViewBag.arventoList = _arventoService.TGetAll().Select(x => new SelectListItem()
            {
                Text = x.ArventoCihazNo,
                Value = x.Id.ToString()
            });
            ViewBag.userList = _userManager.Users.Select(x => new SelectListItem()
            {
                Text = x.Name + " " + x.Surname,
                Value = x.Id.ToString()
            }).ToList();
            var value = _carService.TGetById(id);
            UpdateCarDto updateCarDto = new()
            {
                Weight = value.Weight,
                UpdateDate = value.UpdateDate,
                RegisterDate = value.RegisterDate,
                AppUserId = value.AppUserId,
                ArventoId = value.ArventoId,
                Brand = value.Brand,
                CarClass = value.CarClass,
                CarModel = value.CarModel,
                CarType1 = value.CarType1,
                CarType2 = value.CarType2,
                HGSNo = value.HGSNo,
                Id = value.Id,
                ImageUrl = value.ImageUrl,
                IsActive = value.IsActive,
                Location = value.Location,
                Logo = value.Logo,
                ModelYear = value.ModelYear,
                MuayeneTarihi = value.MuayeneTarihi,
                Plate = value.Plate
            };
            return View(updateCarDto);
        }

        [HttpPost]
        public IActionResult UpdateCar(UpdateCarDto updateCarDto, IFormFile? file)
        {
            if (updateCarDto == null)
            {
                return View();
            }
            else
            {
                Car car = new()
                {
                    AppUserId = updateCarDto.AppUserId,
                    ArventoId = updateCarDto.ArventoId,
                    Brand = updateCarDto.Brand,
                    CarClass = updateCarDto.CarClass,
                    CarModel = updateCarDto.CarModel,
                    CarType1 = updateCarDto.CarType1,
                    CarType2 = updateCarDto.CarType2,
                    HGSNo = updateCarDto.HGSNo,
                    Id = updateCarDto.Id,
                    IsActive = updateCarDto.IsActive,
                    Location = updateCarDto.Location,
                    Logo = updateCarDto.Logo,
                    ModelYear = updateCarDto.ModelYear,
                    MuayeneTarihi = updateCarDto.MuayeneTarihi,
                    Plate = updateCarDto.Plate,
                    RegisterDate = updateCarDto.RegisterDate,
                    UpdateDate = DateTime.Now,
                    Weight = updateCarDto.Weight,
                    ImageUrl = updateCarDto.ImageUrl
                };

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string uniqueFileName = timestamp + "_" + file.FileName;
                    using (var fileStream = new FileStream(Path.Combine(imagePath, uniqueFileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    car.ImageUrl = $@"/img/" + uniqueFileName;
                    _carService.TUpdate(car);
                    return RedirectToAction("Index", "AdminCar");
                }
                else if(car.ImageUrl == "/Images/car-1.jpg" || car.ImageUrl == null)
                {                   
                    car.ImageUrl = "/Images/car-1.jpg";
                    _carService.TUpdate(car);
                    return RedirectToAction("Index", "AdminCar");
                }
                return View();               
            }
        }
    }
}