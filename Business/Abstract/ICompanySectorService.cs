using Core.Unilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanySectorService
    {
        CompanySectorDto GetByCompanyId(int companyId);
        CompanySectorDto GetBySectorId(int sectorId);
        CompanySectorDto GetByBothId(int companyId, int sectorId);
        List<CompanySectorDto> GetList();
        void Add(CompanySectorDto companySectorDto);
        void Update(CompanySectorDto companySectorDto);
        void Delete(CompanySectorDto companySectorDto);
    }
}
