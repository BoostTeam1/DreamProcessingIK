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
    public class PersonnelDocumentManager : IPersonnelDocumentService
    {
        
        private readonly IPersonnelDocumentDal _personnelDocumentDal;
        public PersonnelDocumentManager(IPersonnelDocumentDal personnelDocumentDal)
        {
            _personnelDocumentDal = personnelDocumentDal;
        }
        public void Add(PersonnelDocuments personnelDocuments)
        {
            _personnelDocumentDal.Add(personnelDocuments);
        }

        public void Delete(PersonnelDocuments personnelDocuments)
        {
            _personnelDocumentDal.Delete(personnelDocuments);
        }

        public PersonnelDocuments GetById(int personnelDocumentId)
        {
           return _personnelDocumentDal.Get(filter: x => x.Id == personnelDocumentId).Result;
        }

        public List<PersonnelDocuments> GetList()
        {
            return _personnelDocumentDal.GetList().Result.ToList();
        }

        public void Update(PersonnelDocuments personnelDocuments)
        {
            _personnelDocumentDal.Update(personnelDocuments);
        }
    }
}
