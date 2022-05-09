using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserVacationService 
    {
        UserVacationDto GetByUserId(string userId);
        UserVacationDto GetByVacationId(int vacationId);
        UserVacationDto GetByBothId(int vacationId, string userId);
        List<UserVacationDto> GetList();
        void Add(UserVacationDto userVacationDto);
        void Update(UserVacationDto userVacationDto);
        void Delete(UserVacationDto userVacationDto);
    }
}
