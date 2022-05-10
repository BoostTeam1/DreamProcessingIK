using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserDebitService
    {

        UserDebitDto GetByUserId(string userId);
        UserDebitDto GetByDebitId(int debitId);
        UserDebitDto GetByBothId(int debitId, string userId);
        List<UserDebitDto> GetList();
        void Add(UserDebitDto userDebitDto);
        void Update(UserDebitDto userDebitDto);
        void Delete(UserDebitDto userDebitDto);
    }
}
