using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OvakentService.DtoLayer.Dtos.AppRoleDtos;
using OvakentService.EntityLayer.Concrete;
using OvakentService.Presentation.ViewModels;

namespace OvakentService.Presentation.Controllers
{
    [Authorize(Roles = "Admin,IK")]
    public class AdminRoleController : Controller
    {
        //Completed

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminRoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //Role bazlı eğer Ovakent Admin ise OrderByDesc Department ve OrderByDesc Company, Billas Admin ise OrderByDesc Department,OrderByAsc Company
            var values = _roleManager.Roles.ToList();
            var roleList = values.Select(x => new ResultAppRoleDto()
            {
                Id = x.Id,
                Name = x.Name

            }).ToList();
            return View(roleList);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateAppRoleDto createAppRoleDto)
        {
            if (ModelState.IsValid)
            {
                AppRole appRole = new()
                {
                    Name = createAppRoleDto.Name,
                    NormalizedName = createAppRoleDto.Name.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                var result = await _roleManager.CreateAsync(appRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminRole");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var value = await _roleManager.FindByIdAsync(id.ToString());
            if (value != null)
            {
                UpdateAppRoleDto Role = new()
                {
                    Id = value.Id,
                    ConcurrencyStamp = value.ConcurrencyStamp,
                    Name = value.Name,
                    NormalizedName = value.NormalizedName,
                };
                if (ModelState.IsValid)
                {
                    return View(Role);
                }
                else
                {
                    return View();
                }
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateAppRoleDto updateAppRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(updateAppRoleDto.Id.ToString());

            role.Name = updateAppRoleDto.Name;
            role.NormalizedName = updateAppRoleDto.NormalizedName;
            role.ConcurrencyStamp = updateAppRoleDto.ConcurrencyStamp;
            role.Id = updateAppRoleDto.Id;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "AdminRole");
            }

            return View();

        }

        public async Task<IActionResult> RemoveRole(int id)
        {
            var Role = await _roleManager.FindByIdAsync(id.ToString());
            if (Role != null)
            {
                var result = await _roleManager.DeleteAsync(Role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminRole");
                }
                return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserRole(int id)
        {
            ViewBag.userId = id;
            var currentUser = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var roleViewModelList = new List<AssignRoleToUserViewModel>();

            foreach (var role in roles)
            {
                var assignRoleToUserViewModel = new AssignRoleToUserViewModel() { Id = role.Id, Name = role.Name };
                if (userRoles.Contains(role.Name))
                {
                    assignRoleToUserViewModel.Exist = true;
                }
                roleViewModelList.Add(assignRoleToUserViewModel);
            }
            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> UserRole(string userId, List<AssignRoleToUserViewModel> requestList)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            else
            {
                foreach (var item in requestList)
                {
                    if (item.Exist)
                    {
                        await _userManager.AddToRoleAsync(user, item.Name);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, item.Name);
                    }
                }
            }
            return RedirectToAction("Index", "AdminUser");
        }
    }
}