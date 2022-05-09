using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserCompanyManager : IUserCompanyService
    {
        private IUserCompanyDal _userCompanyDal;
        public UserCompanyManager(IUserCompanyDal userCompanyDal)
        {
            _userCompanyDal = userCompanyDal;
        }

        public void Add(UserCompanyDto userCompanyDto)
        {
            _userCompanyDal.Add(userCompanyDto);
        }

        public void Delete(UserCompanyDto userCompanyDto)
        {
            _userCompanyDal.Delete(userCompanyDto);
        }

        public UserCompanyDto GetByBothId(int companyId, string userId)
        {
            return _userCompanyDal.Get(filter: x => x.CompanyId == companyId && x.UserId == userId).Result;
        }

        public UserCompanyDto GetByUserId(string userId)
        {
            return _userCompanyDal.Get(filter: x => x.UserId == userId).Result;
        }

        public UserCompanyDto GetByCompanyId(int companyId)
        {
            return _userCompanyDal.Get(filter: x => x.CompanyId == companyId).Result;
        }

        public List<UserCompanyDto> GetList()
        {
            return _userCompanyDal.GetList().Result.ToList();
        }

        public void Update(UserCompanyDto userCompanyDto)
        {
            _userCompanyDal.Update(userCompanyDto);
        }
    }
}
