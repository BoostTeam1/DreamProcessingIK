using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
public interface IUserShiftBreakService
    {
        UserShiftBreakDto GetByUserId(string userId);
        UserShiftBreakDto GetByShiftId(int shiftId);
        UserShiftBreakDto GetByBreakId(int breakId);
        UserShiftBreakDto GetByBothShiftId(int shiftId, string userId);
        UserShiftBreakDto GetByBothBreakId(int breakId, string userId);
        List<UserShiftBreakDto> GetList();
        void Add(UserShiftBreakDto  userShiftBreakDto);
        void Update(UserShiftBreakDto  userShiftBreakDto);
        void Delete(UserShiftBreakDto userShiftBreakDto);
    }
}
