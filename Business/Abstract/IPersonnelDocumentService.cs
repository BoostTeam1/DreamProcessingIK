using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPersonnelDocumentService
    {
        PersonnelDocuments GetById(int personnelDocumentId);
        List<PersonnelDocuments> GetList();
        void Add(PersonnelDocuments personnelDocuments);
        void Update(PersonnelDocuments personnelDocuments);
        void Delete(PersonnelDocuments personnelDocuments);
    }
}
