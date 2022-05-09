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
    public class CompanySectorManager : ICompanySectorService
    {
        private ICompanySectorDal _companySectorDal;
        public CompanySectorManager(ICompanySectorDal companySectorDal)
        {
            _companySectorDal = companySectorDal;
        }
        public void Add(CompanySectorDto companySectorDto)
        {
            _companySectorDal.Add(companySectorDto);
            //return new SuccessResult(Messages.CompanySectorAdded);
        }

        public void Delete(CompanySectorDto companySectorDto)
        {
            _companySectorDal.Delete(companySectorDto);
            //return new SuccessResult(Messages.CompanySectorDeleted);
        }

        public CompanySectorDto GetByCompanyId(int companyId)
        {
            //return new SuccessDataResult<CompanySectorDto>(_companySectorDal.Get(filter: x => x.CompanyId == companyId).Result);
            return _companySectorDal.Get(filter: x => x.CompanyId == companyId).Result;
        }

        public CompanySectorDto GetBySectorId(int sectorId)
        {
            //return new SuccessDataResult<CompanySectorDto>(_companySectorDal.Get(filter: x => x.SectorId == sectorId).Result);
            return _companySectorDal.Get(filter: x => x.SectorId == sectorId).Result;
        }

        public CompanySectorDto GetByBothId(int companyId, int sectorId)
        {
            //return new SuccessDataResult<CompanySectorDto>(_companySectorDal.Get(filter: x => x.CompanyId == companyId && x.SectorId == sectorId).Result);
            return _companySectorDal.Get(filter: x => x.CompanyId == companyId && x.SectorId == sectorId).Result;
        }

        public List<CompanySectorDto> GetList()
        {
            //return new SuccessDataResult<List<CompanySectorDto>>(_companySectorDal.GetList().Result.ToList());
            return _companySectorDal.GetList().Result.ToList();
        }


        public void Update(CompanySectorDto companySectorDto)
        {
            _companySectorDal.Update(companySectorDto);
            //return new SuccessResult(Messages.CompanyUpdated);
        }
    }
}
