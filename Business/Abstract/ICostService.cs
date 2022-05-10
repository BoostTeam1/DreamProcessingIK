using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
  public interface ICostService
    {
        Cost GetById(int costId);
        List<Cost> GetList();
        void Add(Cost cost);
        void Update(Cost cost);
        void Delete(Cost cost);

    }
}
