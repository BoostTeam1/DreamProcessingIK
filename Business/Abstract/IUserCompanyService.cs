using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserCompanyService
    {
        UserCompanyDto GetByUserId(string userId);
        UserCompanyDto GetByCompanyId(int companyId);
        UserCompanyDto GetByBothId(int companyId, string userId);
        List<UserCompanyDto> GetList();
        void Add(UserCompanyDto userCompanyDto);
        void Update(UserCompanyDto userCompanyDto);
        void Delete(UserCompanyDto userCompanyDto);
    }
}
