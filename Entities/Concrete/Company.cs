using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Company : BaseEntity
    {
        public Company()
        {
           UserCompanyDtos = new HashSet<UserCompanyDto>();
           CompanySectors  = new HashSet<CompanySectorDto>();
        }
        public string CompanyName { get; set; }

        public bool? IsConfirmed { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CompanyDetail { get; set; }
        public string Logo { get; set; }
        public virtual ICollection<CompanySectorDto> CompanySectors { get; set; }
        public virtual ICollection<UserCompanyDto> UserCompanyDtos { get; set; }
       
    }
}
