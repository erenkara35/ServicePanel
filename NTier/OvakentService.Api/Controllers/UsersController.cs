using Microsoft.AspNetCore.Mvc;
using OvakentService.BusinessLogic.Abstract;

namespace OvakentService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public UsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _appUserService.TGetById(id);
            return Ok(user);
        }
    }
}