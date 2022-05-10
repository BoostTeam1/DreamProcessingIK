using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CostManager : ICostService
    {
        private ICostDal _costDal;
        public CostManager(ICostDal costDal)
        {
            _costDal = costDal;
        }
        public void Add(Cost cost)
        {
            _costDal.Add(cost);
        }

        public void Delete(Cost cost)
        {
            _costDal.Delete(cost);
        }

        public Cost GetById(int costId)
        {
            return _costDal.Get(filter: x => x.Id == costId).Result;
        }

        public List<Cost> GetList()
        {
            return _costDal.GetList().Result.ToList(); 
        }

        public void Update(Cost cost)
        {
            _costDal.Update(cost);
        }
    }
}
