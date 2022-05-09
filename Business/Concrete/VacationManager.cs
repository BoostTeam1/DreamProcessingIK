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
    public class VacationManager : IVacationService
    {
        private IVacationDal _vacationDal;
        public VacationManager(IVacationDal vacationDal)
        {
            _vacationDal = vacationDal;
        }
        public void Add(Vacation vacation)
        {
            _vacationDal.Add(vacation);
        }

        public void Delete(Vacation vacation)
        {
            _vacationDal.Delete(vacation);
        }

        public Vacation GetById(int vacationId)
        {
            return _vacationDal.Get(filter: x => x.Id == vacationId).Result;

        }

        public List<Vacation> GetList()
        {
            return _vacationDal.GetList().Result.ToList();
        }

        public void Update(Vacation vacation)
        {
            _vacationDal.Update(vacation);
        }
    }
}
