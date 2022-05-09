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
    public class UserVacationManager : IUserVacationService
    {
        private IUserVacationDal _userVacationDal;
        public UserVacationManager(IUserVacationDal userVacationDal)
        {
            _userVacationDal = userVacationDal;
        }

        public void Add(UserVacationDto userVacationDto)
        {
            _userVacationDal.Add(userVacationDto);
        }

        public void Delete(UserVacationDto userVacationDto)
        {
            _userVacationDal.Delete(userVacationDto);
        }

        public UserVacationDto GetByBothId(int vacationId, string userId)
        {
            return _userVacationDal.Get(filter: x => x.HolidayId == vacationId && x.UserId == userId).Result;
        }

        public UserVacationDto GetByUserId(string userId)
        {
            return _userVacationDal.Get(filter: x => x.UserId == userId).Result;
        }

        public UserVacationDto GetByVacationId(int vacationId)
        {
            return _userVacationDal.Get(filter: x => x.HolidayId == vacationId).Result;
        }

        public List<UserVacationDto> GetList()
        {
            return _userVacationDal.GetList().Result.ToList();
        }

        public void Update(UserVacationDto userVacationDto)
        {
            _userVacationDal.Update(userVacationDto);
        }
    }
}
