using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
 public class UserCostDto : BaseEntity
    {
        public string UserId { get; set; }
        public int? CostId { get; set; }
        public string ManagerApprovedId { get; set; }
        public virtual Cost Cost { get; set; }
        public virtual AppUser User { get; set; }
    }
}
