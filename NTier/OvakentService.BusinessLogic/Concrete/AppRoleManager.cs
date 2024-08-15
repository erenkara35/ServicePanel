using OvakentService.BusinessLogic.Abstract;
using OvakentService.DataAccessLayer.Abstract;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.BusinessLogic.Concrete
{
    public class AppRoleManager : IAppRoleService
    {
        private readonly IAppRoleDal _appRoleDal;

        public AppRoleManager(IAppRoleDal appRoleDal)
        {
            _appRoleDal = appRoleDal;
        }

        public void TDelete(AppRole entity)
        {
            _appRoleDal.Delete(entity);
        }

        public List<AppRole> TGetAll()
        {
            return _appRoleDal.GetAll();
        }

        public AppRole TGetById(int id)
        {
            return _appRoleDal.GetById(id);
        }

        public void TInsert(AppRole entity)
        {
            _appRoleDal.Insert(entity);
        }

        public void TUpdate(AppRole entity)
        {
            _appRoleDal.Update(entity);
        }
    }
}