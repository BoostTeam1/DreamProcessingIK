using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
   public interface IUserCostService
    {
          UserCostDto  GetByUserId(string userId);
        UserCostDto GetByCostId(int costId);
        UserCostDto GetByBothId(int costId, string userId);
        List<UserCostDto> GetList();
        void Add(UserCostDto  userCostDto);
        void Update(UserCostDto  userCostDto);
        void Delete(UserCostDto  userCostDto);


    }
}
