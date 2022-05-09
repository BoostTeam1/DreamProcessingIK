using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfCompanySectorDal : EfEntityRepositoryBase<CompanySectorDto,MyContext>, ICompanySectorDal
    {
        //public List<Sector> Sector(Company company)
        //{
        //    using(var context = new MyContext())
        //    {
        //        var result = from s in context.Sectors
        //                     join csd in context.CompanySectorDtos
        //                     on s.Id equals csd.SectorId
        //                     where csd.CompanyId == company.Id
        //                     select new Sector
        //                     {
        //                         Id = s.Id,
        //                         Name = s.Name
        //                     };
        //        return result.ToList();
        //    }
        //}
    }
}
