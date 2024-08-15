using OvakentService.EntityLayer.Concrete;

namespace OvakentService.BusinessLogic.Abstract
{
    public interface ICarService:IGenericService<Car>
    {
        List<Car> TCarListWithUserAndArvento();
    }
}