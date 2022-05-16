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
    public class UserShiftManager : IUserShiftService
    {
        private IUserShiftDal _userShiftDal;
        public UserShiftManager(IUserShiftDal userShiftDal)
        {
            _userShiftDal = userShiftDal;
        }
        public void Add(UserShiftDto userShiftDto)
        {
            _userShiftDal.Add(userShiftDto);
        }

        public void Delete(UserShiftDto userShiftDto)
        {
            _userShiftDal.Delete(userShiftDto);
        }

        public UserShiftDto GetByBothId(int shiftId, string userId)
        {
            return _userShiftDal.Get(filter: x => x.ShiftId == shiftId && x.UserId == userId).Result;
        }

        public UserShiftDto GetByShiftId(int shiftId)
        {
            return _userShiftDal.Get(filter: x => x.ShiftId == shiftId).Result;
        }

        public UserShiftDto GetByUserId(string userId)
        {
            return _userShiftDal.Get(filter: x => x.UserId == userId).Result;
        }

        public List<UserShiftDto> GetList()
        {
           return  _userShiftDal.GetList().Result.ToList();
        }

        public void Update(UserShiftDto userShiftDto)
        {
            _userShiftDal.Update(userShiftDto);
        }
    }
}
