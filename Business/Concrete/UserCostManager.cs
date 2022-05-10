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
    public class UserCostManager : IUserCostService
    {
        private IUserCostDal _userCostDal;
        public UserCostManager(IUserCostDal userCostDal)
        {
            _userCostDal=userCostDal;
        }
        public void Add(UserCostDto userCostDto)
        {
            _userCostDal.Add(userCostDto);
        }

        public void Delete(UserCostDto userCostDto)
        {
            _userCostDal.Delete(userCostDto);
        }

        public UserCostDto GetByBothId(int costId, string userId)
        {
            return _userCostDal.Get(filter: x => x.CostId == costId && x.UserId == userId).Result;
        }

        public UserCostDto GetByCostId(int costId)
        {
            return _userCostDal.Get(filter: x => x.CostId == costId).Result;
        }

        public UserCostDto GetByUserId(string userId)
        {
            return _userCostDal.Get(filter: x => x.UserId == userId).Result;
        }

        public List<UserCostDto> GetList()
        {
           return  _userCostDal.GetList().Result.ToList();
        }

        public void Update(UserCostDto userCostDto)
        {
            _userCostDal.Update(userCostDto);
        }
    }
}
