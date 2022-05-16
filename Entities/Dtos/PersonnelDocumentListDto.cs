using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class PersonnelDocumentListDto
    {
        public int DocumentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FileName { get; set; }
        public string FileDetails { get; set; }
        public DateTime FileGeneratedDate { get; set; }
    }
}
