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
    public class BountyManager : IBountyService
    {
        private IBountyDal _bountyDal;
        public BountyManager(IBountyDal bountyDal)
        {
            _bountyDal = bountyDal;
        }
        public void Add(Bounty bounty)
        {
            _bountyDal.Add(bounty);
        }

        public void Delete(Bounty bounty)
        {
            _bountyDal.Delete(bounty);
        }

        public Bounty GetById(int bountyId)
        {
            return _bountyDal.Get(filter: x => x.Id == bountyId).Result;
        }

        public List<Bounty> GetList()
        {
            return _bountyDal.GetList().Result.ToList();
        }

        public void Update(Bounty bounty)
        {
            _bountyDal.Update(bounty);
        }
    }
}
