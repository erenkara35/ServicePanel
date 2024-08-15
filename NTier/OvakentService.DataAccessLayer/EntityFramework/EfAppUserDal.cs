using OvakentService.DataAccessLayer.Abstract;
using OvakentService.DataAccessLayer.Concrete;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.DataAccessLayer.EntityFramework
{
    public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        public EfAppUserDal(DefaultContext context) : base(context)
        {
        }

    }
}