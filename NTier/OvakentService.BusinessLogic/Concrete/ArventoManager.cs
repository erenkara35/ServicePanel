using OvakentService.BusinessLogic.Abstract;
using OvakentService.DataAccessLayer.Abstract;
using OvakentService.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvakentService.BusinessLogic.Concrete
{
    public class ArventoManager : IArventoService
    {
        private readonly IArventoDal _arventoDal;

        public ArventoManager(IArventoDal arventoDal)
        {
            _arventoDal = arventoDal;
        }

        public void TDelete(Arvento entity)
        {
            _arventoDal.Delete(entity);
        }

        public List<Arvento> TGetAll()
        {
            return _arventoDal.GetAll();
        }

        public Arvento TGetById(int id)
        {
            return _arventoDal.GetById(id);
        }

        public void TInsert(Arvento entity)
        {
            _arventoDal.Insert(entity);
        }

        public void TUpdate(Arvento entity)
        {
            _arventoDal.Update(entity);
        }
    }
}