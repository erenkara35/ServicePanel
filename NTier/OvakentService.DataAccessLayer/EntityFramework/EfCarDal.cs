using Microsoft.EntityFrameworkCore;
using OvakentService.DataAccessLayer.Abstract;
using OvakentService.DataAccessLayer.Concrete;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.DataAccessLayer.EntityFramework
{
    public class EfCarDal : GenericRepository<Car>, ICarDal
    {
        private readonly DefaultContext _context;

        public EfCarDal(DefaultContext context) : base(context)
        {
            _context = context;
        }

        public List<Car> CarListWithUserAndArvento()
        {
            return _context.Cars.Include(x => x.AppUser).Include(x => x.Arvento).ToList();
        }
    }
}