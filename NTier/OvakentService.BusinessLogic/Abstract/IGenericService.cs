namespace OvakentService.BusinessLogic.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TInsert(T entity);
        void TDelete(T entity);
        void TUpdate(T entity);
        T TGetById(int id);
        List<T> TGetAll();
    }
}