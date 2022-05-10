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
    public class ShiftManager : IShiftService
    {
        private IShiftDal _shiftDal;
        public ShiftManager(IShiftDal shiftDal)
        {
            _shiftDal = shiftDal;
        }
        public void Add(Shift shift)
        {
           _shiftDal.Add(shift);
        }

        public void Delete(Shift shift)
        {
          _shiftDal.Delete(shift);
        }

        public Shift GetById(int shiftId)
        {
            return _shiftDal.Get(filter: x => x.Id == shiftId).Result;
        }

        public List<Shift> GetList()
        {
            return _shiftDal.GetList().Result.ToList();
        }

        public void Update(Shift shift)
        {
            _shiftDal.Update(shift);
        }
    }
}
