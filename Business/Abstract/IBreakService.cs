using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
   public interface IBreakService
    {
        Break GetById(int breakId);
        List<Break> GetList();
        void Add(Break breaks);
        void Update(Break breaks);
        void Delete(Break  breaks);

    }
}
