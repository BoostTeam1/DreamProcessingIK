using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserCompanyDto : BaseEntity
    {
        public string UserId { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual AppUser User { get; set; }
    }
}
