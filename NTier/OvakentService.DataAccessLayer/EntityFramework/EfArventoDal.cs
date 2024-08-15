using OvakentService.DataAccessLayer.Abstract;
using OvakentService.DataAccessLayer.Concrete;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.DataAccessLayer.EntityFramework
{
    public class EfArventoDal : GenericRepository<Arvento>, IArventoDal
    {
        public EfArventoDal(DefaultContext context) : base(context)
        {

        }
    }
}