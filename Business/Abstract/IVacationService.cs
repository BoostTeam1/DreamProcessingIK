using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVacationService
    {
        Vacation GetById(int vacationId);
        List<Vacation> GetList();
        void Add(Vacation vacation);
        void Update(Vacation vacation);
        void Delete(Vacation vacation);
    }
}
