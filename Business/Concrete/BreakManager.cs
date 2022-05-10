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
    public class BreakManager : IBreakService
    {
        private IBreakDal _breakDal;
        public BreakManager(IBreakDal breakDal)
        {
            _breakDal = breakDal;
        }
        public void Add(Break breaks)
        {
            _breakDal.Add(breaks);
        }

        public void Delete(Break breaks)
        {
            _breakDal.Delete(breaks);
        }

        public Break GetById(int breakId)
        {
            return _breakDal.Get(filter: x => x.Id == breakId).Result;
        }

        public List<Break> GetList()
        {
            return _breakDal.GetList().Result.ToList();
        }

        public void Update(Break breaks)
        {
            _breakDal.Update(breaks);
        }
    }
}
