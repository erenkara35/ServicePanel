using OvakentService.EntityLayer.Concrete;

namespace OvakentService.DataAccessLayer.Abstract
{
    public interface ICarDal : IGenericDal<Car>
    {
        List<Car> CarListWithUserAndArvento();
    }
    
}