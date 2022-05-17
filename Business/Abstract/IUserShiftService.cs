using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
   public interface IUserShiftService
    {
       UserShiftDto GetByUserId(string userId);
        UserShiftDto GetByShiftId(int shiftId);
        UserShiftDto GetByBothId(int shiftId, string userId);
        List<UserShiftDto> GetList();
        void Add(UserShiftDto userShiftDto);
        void Update(UserShiftDto userShiftDto);
        void Delete(UserShiftDto userShiftDto);
    }
}
