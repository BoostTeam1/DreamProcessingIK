using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
 public interface IUserBountyService
    {
       UserBountyDto GetByUserId(string userId);
        UserBountyDto GetByBountyId(int bountyId);
        UserBountyDto GetByBothId(string userId, int bountyId);
        List<UserBountyDto> GetList();
        void Add(UserBountyDto userBountyDto);
        void Update(UserBountyDto userBountyDto);
        void Delete(UserBountyDto userBountyDto);
    }
}
