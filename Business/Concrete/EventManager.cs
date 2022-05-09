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
    public class EventManager : IEventService
    {
        private IEventDal _eventDal;
        public EventManager(IEventDal eventDal)
        {
            _eventDal = eventDal;
        }
        public void Add(Eventt eventt)
        {

            _eventDal.Add(eventt);
            //return new SuccessResult(Messages.CompanyAdded);
        }



        public void Delete(Eventt eventt)
        {
            _eventDal.Delete(eventt);
            //return new SuccessResult(Messages.CompanyDeleted);
        }

        public Eventt GetById(int eventId)
        {
            //return new SuccessDataResult<Company>(_companyDal.Get(filter: x => x.Id == companyId).Result);
            return _eventDal.Get(filter: x => x.Id == eventId).Result;
        }

        public List<Eventt> GetList()
        {
            //return new SuccessDataResult<List<Company>>(_companyDal.GetList().Result.ToList());
            return _eventDal.GetList().Result.ToList();
        }



        public void Update(Eventt eventt)
        {
            _eventDal.Update(eventt);
            //return new SuccessResult(Messages.CompanyUpdated);

        }
    }
}
