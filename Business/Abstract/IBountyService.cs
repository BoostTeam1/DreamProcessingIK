using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
 public interface IBountyService
    {
     Bounty GetById(int bountyId);
        List<Bounty> GetList();
        void Add(Bounty bounty);
        void Update(Bounty bounty);
        void Delete(Bounty bounty);
    }
}
