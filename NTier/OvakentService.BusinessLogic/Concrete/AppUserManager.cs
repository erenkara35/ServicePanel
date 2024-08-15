using Microsoft.AspNetCore.Identity;
using OvakentService.BusinessLogic.Abstract;
using OvakentService.DataAccessLayer.Abstract;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.BusinessLogic.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _appUserDal;

        private readonly UserManager<AppUser> _userManager;

        public AppUserManager(IAppUserDal appUserDal, UserManager<AppUser> userManager)
        {
            _appUserDal = appUserDal;
            _userManager = userManager;
        }

        public void TDelete(AppUser entity)
        {
            _appUserDal.Delete(entity);
        }

        public List<AppUser> TGetAll()
        {
            return _appUserDal.GetAll();
        }

        public AppUser TGetById(int id)
        {
            return _appUserDal.GetById(id);
        }

        public void TInsert(AppUser entity)
        {
            _appUserDal.Insert(entity);
        }

        public void TUpdate(AppUser entity)
        {
            _appUserDal.Update(entity);
        }
    }
}