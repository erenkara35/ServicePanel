using OvakentService.DataAccessLayer.Abstract;
using OvakentService.DataAccessLayer.Concrete;

namespace OvakentService.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly DefaultContext _context;

        public GenericRepository(DefaultContext context)
        {
            _context = context;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var value = _context.Set<T>().Find(id);
            return value;
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
