using Business.Abstract;
using Business.Constants;
using Core.Unilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SectorManager : ISectorService
    {
        private ISectorDal _sectorDal;
        public SectorManager(ISectorDal sectorDal)
        {
            _sectorDal = sectorDal;
           
        }
        public void Add(Sector sector)
        {
            _sectorDal.Add(sector);
           // return new SuccessResult(Messages.CompanyAdded);

        }

        public void Delete(Sector sector)
        {
            _sectorDal.Delete(sector);
            //return new SuccessResult(Messages.CompanyDeleted);
        }

        public Sector GetById(int sectorId)
        {
            //return new SuccessDataResult<Company>(_companyDal.Get(filter: x => x.Id == companyId).Result);
            return _sectorDal.Get(filter: x => x.Id == sectorId).Result;
        }

        public List<Sector> GetList()
        {
            //return new SuccessDataResult<List<Company>>(_companyDal.GetList().Result.ToList());
            return _sectorDal.GetList().Result.ToList();
        }

        public void Update(Sector sector)
        {
            _sectorDal.Update(sector);
            //return new SuccessResult(Messages.CompanyUpdated);
        }
    }
}
