using OvakentService.BusinessLogic.Abstract;
using OvakentService.DataAccessLayer.Abstract;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.BusinessLogic.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public List<Car> TCarListWithUserAndArvento()
        {
            return _carDal.CarListWithUserAndArvento();
        }

        public void TDelete(Car entity)
        {
            _carDal.Delete(entity);
        }

        public List<Car> TGetAll()
        {
            return _carDal.GetAll();
        }

        public Car TGetById(int id)
        {
            return _carDal.GetById(id);
        }

        public void TInsert(Car entity)
        {
            _carDal.Insert(entity);
        }

        public void TUpdate(Car entity)
        {
            _carDal.Update(entity);
        }
    }
}