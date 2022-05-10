using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
 public interface IDebitService
    {
        Debit GetById(int debitId);
        List<Debit> GetList();
        void Add(Debit debit);
        void Update(Debit debit);
        void Delete(Debit debit);
    }
}
