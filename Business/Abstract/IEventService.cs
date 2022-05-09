using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEventService
    {
        Eventt GetById(int eventId);
        List<Eventt> GetList();
        void Add(Eventt eventt);
        void Update(Eventt eventt);
        void Delete(Eventt eventt);
    }
}
