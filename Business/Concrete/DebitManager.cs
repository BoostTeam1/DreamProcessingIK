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
    public class DebitManager : IDebitService
    {
        private IDebitDal _debitDal;
        public DebitManager(IDebitDal debitDal)
        {
            _debitDal = debitDal;
        }
        public void Add(Debit debit)
        {
            _debitDal.Add(debit);
        }

        public void Delete(Debit debit)
        {
            _debitDal.Delete(debit);
        }

        public Debit GetById(int debitId)
        {
            return _debitDal.Get(filter: x => x.Id == debitId).Result;
        }

        public List<Debit> GetList()
        {
            return _debitDal.GetList().Result.ToList();
        }

        public void Update(Debit debit)
        {
            _debitDal.Update(debit);

        }
    }
}
