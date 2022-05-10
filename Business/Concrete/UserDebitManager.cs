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
    public class UserDebitManager : IUserDebitService
    {
        private IUserDebitDal _userDebitDal;
        public UserDebitManager(IUserDebitDal userDebitDal)
        {
            _userDebitDal = userDebitDal;
        }


        public void Add(UserDebitDto userDebitDto)
        {
            _userDebitDal.Add(userDebitDto);
        }

        public void Delete(UserDebitDto userDebitDto)
        {
            _userDebitDal.Delete(userDebitDto);
        }

        public UserDebitDto GetByBothId(int debitId, string userId)
        {
            return _userDebitDal.Get(filter: x => x.DebitId == debitId && x.UserId == userId).Result;
        }

        public UserDebitDto GetByDebitId(int debitId)
        {
            return _userDebitDal.Get(filter: x => x.DebitId == debitId).Result;
        }

        public UserDebitDto GetByUserId(string userId)
        {
            return _userDebitDal.Get(filter: x => x.UserId == userId).Result;
      
        }

        public List<UserDebitDto> GetList()
        {
            return _userDebitDal.GetList().Result.ToList();
        }

        public void Update(UserDebitDto userDebitDto)
        {
            _userDebitDal.Update(userDebitDto);
        }
    }
}
