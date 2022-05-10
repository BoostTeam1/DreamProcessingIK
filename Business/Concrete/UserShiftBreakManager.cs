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
    public class UserShiftBreakManager : IUserShiftBreakService
    {
        private IUserShiftBreakDal _userShiftBreakDal;
            public UserShiftBreakManager(IUserShiftBreakDal userShiftBreakDal)
        {
            _userShiftBreakDal = userShiftBreakDal;
        }
        public void Add(UserShiftBreakDto userShiftBreakDto)
        {
            _userShiftBreakDal.Add(userShiftBreakDto);
        }

        public void Delete(UserShiftBreakDto userShiftBreakDto)
        {
           _userShiftBreakDal.Delete(userShiftBreakDto);
        }

        public UserShiftBreakDto GetByBothBreakId(int breakId, string userId)
        {
            return _userShiftBreakDal.Get(filter: x => x.BreakId == breakId && x.UserId == userId).Result;
        }

        public UserShiftBreakDto GetByBothShiftId(int shiftId, string userId)
        {
            return _userShiftBreakDal.Get(filter: x => x.ShiftId == shiftId && x.UserId == userId).Result;
        }

        public UserShiftBreakDto GetByBreakId(int breakId)
        {
            return _userShiftBreakDal.Get(filter: x => x.BreakId == breakId).Result;
        }

        public UserShiftBreakDto GetByShiftId(int shiftId)
        {
            return _userShiftBreakDal.Get(filter: x => x.ShiftId == shiftId).Result;
        }

        public UserShiftBreakDto GetByUserId(string userId)
        {
            return _userShiftBreakDal.Get(filter: x => x.UserId == userId).Result;
        }

        public List<UserShiftBreakDto> GetList()
        {
            return _userShiftBreakDal.GetList().Result.ToList();
        }

        public void Update(UserShiftBreakDto userShiftBreakDto)
        {
            _userShiftBreakDal.Update(userShiftBreakDto);
        }
    }
}
