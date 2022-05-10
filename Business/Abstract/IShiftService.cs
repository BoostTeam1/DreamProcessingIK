using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShiftService
    {
        Shift GetById(int shiftId);
        List<Shift> GetList();
        void Add(Shift shift);
        void Update(Shift shift);
        void Delete(Shift shift);
    }
}
