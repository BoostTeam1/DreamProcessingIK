using Core.Unilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        Company GetById(int companyId);
        List<Company> GetList();
        void Add(Company company);
        void Update(Company company);
        void Delete(Company company);
     
    }
}
