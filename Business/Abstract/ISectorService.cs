using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISectorService
    {
        Sector GetById(int sectorId);
        List<Sector> GetList();
        void Add(Sector sector);
        void Update(Sector sector);
        void Delete(Sector sector);
    }
}
