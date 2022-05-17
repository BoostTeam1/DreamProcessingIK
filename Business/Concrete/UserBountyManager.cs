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
    public class UserBountyManager : IUserBountyService
    {
        private IUserBountyDal _userBountyDal;
        public UserBountyManager(IUserBountyDal  userBountyDal)
        {
            _userBountyDal = userBountyDal;
        }
        public void Add(UserBountyDto userBountyDto)
        {
            _userBountyDal.Add(userBountyDto);
        }

        public void Delete(UserBountyDto userBountyDto)
        {
            _userBountyDal.Delete(userBountyDto);
        }

        public UserBountyDto GetByBothId(string userId, int bountyId)
        {
          return _userBountyDal.Get(filter:x=>x.UserId==userId&&x.BountyId==bountyId).Result;
        }

        public UserBountyDto GetByBountyId(int bountyId)
        {
            return _userBountyDal.Get(filter: x => x.BountyId == bountyId).Result;
        }

        public UserBountyDto GetByUserId(string userId)
        {
            return _userBountyDal.Get(filter: x => x.UserId == userId).Result;
        }

        public List<UserBountyDto> GetList()
        {
            return _userBountyDal.GetList().Result.ToList();
        }

        public void Update(UserBountyDto userBountyDto)
        {
            _userBountyDal.Update(userBountyDto);
        }
    }
}
